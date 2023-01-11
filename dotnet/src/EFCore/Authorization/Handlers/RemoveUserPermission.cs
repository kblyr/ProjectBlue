using JIL.Authorization.Contracts;

namespace JIL.Authorization.Handlers;

sealed class RemoveUserPermissionHandler : IRequestHandler<RemoveUserPermissionCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public RemoveUserPermissionHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(RemoveUserPermissionCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var userPermission = await context.UserPermissions
            .Where(_ => _.UserId == request.UserId && _.PermissionId == request.PermissionId && _.IsDeleted == false)
            .SingleOrDefaultAsync(cancellationToken);

        if (userPermission is null)
        {
            return _mapper.Map<RemoveUserPermissionCommand, UserPermissionNotFoundError>(request);
        }

        var auditInfo = _auditInfoProvider.Current;
        userPermission = userPermission with
        {
            IsDeleted = true,
            DeletedById = auditInfo.User.Id,
            DeletedOn = auditInfo.Timestamp
        };
        context.UserPermissions.Update(userPermission);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new RemoveUserPermissionCommand.Response
        {
            Id = userPermission.Id,
            RemovedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
