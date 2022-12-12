using JIL.Authorization.Contracts;

namespace JIL.Authorization.Handlers;

sealed class RemoveUserPermissionsHandler : IRequestHandler<RemoveUserPermissionsCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IMapper _mapper;

    public RemoveUserPermissionsHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(RemoveUserPermissionsCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        var auditInfo = _auditInfoProvider.Current;
        var userPermissionIds = new List<long>();

        foreach (var permissionId in request.PermissionIds)
        {
            var userPermission = await context.UserPermissions
                .Where(_ => _.UserId == request.UserId && _.PermissionId == permissionId && _.IsDeleted == false)
                .SingleOrDefaultAsync(cancellationToken);

            if (userPermission is null)
            {
                return new UserPermissionNotFoundError
                {
                    UserId = request.UserId,
                    PermissionId = permissionId
                };
            }

            userPermission = userPermission with
            {
                IsDeleted = true,
                DeletedById = auditInfo.User.Id,
                DeletedOn = auditInfo.Timestamp
            };
        }

        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new RemoveUserPermissionsCommand.Response
        {
            Ids = userPermissionIds,
            RemovedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
