using JIL.Authorization.Contracts;
using JIL.Authorization.Entities;

namespace JIL.Authorization.Handlers;

sealed class AddUserPermissionsHandler : IRequestHandler<AddUserPermissionsCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public AddUserPermissionsHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(AddUserPermissionsCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        var auditInfo = _auditInfoProvider.Current;
        var userPermissions = new List<UserPermission>();

        foreach (var permissionId in request.PermissionIds)
        {
            if (await context.Permissions.AsNoTracking().Where(_ => _.Id == permissionId).AnyAsync(cancellationToken) == false)
            {
                return new PermissionNotFoundError { Id = permissionId };
            }

            if (await context.UserPermissions.AsNoTracking().Where(_ => _.UserId == request.UserId && _.PermissionId == permissionId && _.IsDeleted == false).AnyAsync(cancellationToken))
            {
                return new UserPermissionExistsError { UserId = request.UserId, PermissionId = permissionId };
            }

            var userPermission = new UserPermission
            {
                UserId = request.UserId,
                PermissionId = permissionId,
                IsDeleted = false,
                InsertedById = auditInfo.User.Id,
                InsertedOn = auditInfo.Timestamp
            };
            context.UserPermissions.Add(userPermission);
            userPermissions.Add(userPermission);
        }

        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new AddUserPermissionsCommand.Response
        {
            Ids = userPermissions.Select(_ => _.Id),
            AddedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
