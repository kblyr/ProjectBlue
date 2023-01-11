using JIL.Authorization.Contracts;
using JIL.Authorization.Entities;

namespace JIL.Authorization.Handlers;

sealed class AddUserPermissionHandler : IRequestHandler<AddUserPermissionCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public AddUserPermissionHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(AddUserPermissionCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        if (await context.Permissions.AsNoTracking().Where(_ => _.Id == request.PermissionId).AnyAsync(cancellationToken) == false)
        {
            return new PermissionNotFoundError { Id = request.PermissionId };
        }

        if (await context.UserPermissions.AsNoTracking().Where(_ => _.UserId == request.UserId && _.PermissionId == request.PermissionId && _.IsDeleted == false).AnyAsync(cancellationToken))
        {
            return _mapper.Map<AddUserPermissionCommand, UserPermissionExistsError>(request);
        }

        var auditInfo = _auditInfoProvider.Current;
        var userPermission = _mapper.Map<AddUserPermissionCommand, UserPermission>(request) with
        {
            IsDeleted = false,
            InsertedById = auditInfo.User.Id,
            InsertedOn = auditInfo.Timestamp
        };
        context.UserPermissions.Add(userPermission);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new AddUserPermissionCommand.Response
        {
            Id = userPermission.Id,
            AddedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
