using JIL.Authorization.Contracts;

namespace JIL.Authorization.Handlers;

sealed class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public RemoveUserRoleHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var userRole = await context.UserRoles
            .Where(_ => _.UserId == request.UserId && _.RoleId == request.RoleId && _.IsDeleted == false)
            .SingleOrDefaultAsync(cancellationToken);

        if (userRole is null)
        {
            return _mapper.Map<RemoveUserRoleCommand, UserRoleNotFoundError>(request);
        }

        var auditInfo = _auditInfoProvider.Current;
        userRole = userRole with 
        {
            IsDeleted = true,
            DeletedById = auditInfo.User.Id,
            DeletedOn = auditInfo.Timestamp
        };
        context.UserRoles.Update(userRole);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new RemoveUserRoleCommand.Response
        {
            Id = userRole.Id,
            RemovedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
