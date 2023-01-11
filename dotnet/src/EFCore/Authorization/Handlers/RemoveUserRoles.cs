using JIL.Authorization.Contracts;

namespace JIL.Authorization.Handlers;

sealed class RemoveUserRolesHandler : IRequestHandler<RemoveUserRolesCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IMapper _mapper;

    public RemoveUserRolesHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
        _mapper = mapper;
    }

    public async Task<IResponse> Handle(RemoveUserRolesCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        var auditInfo = _auditInfoProvider.Current;
        var userRoleIds = new List<long>();

        foreach (var roleId in request.RoleIds)
        {
            var userRole = await context.UserRoles
                .Where(_ => _.UserId == request.UserId && _.RoleId == roleId && _.IsDeleted == false)
                .SingleOrDefaultAsync(cancellationToken);

            if (userRole is null)
            {
                return new UserRoleNotFoundError 
                {
                    UserId = request.UserId,
                    RoleId = roleId
                };
            }

            userRole = userRole with
            {
                IsDeleted = true,
                DeletedById = auditInfo.User.Id,
                DeletedOn = auditInfo.Timestamp
            };
            context.UserRoles.Update(userRole);
            userRoleIds.Add(userRole.Id);
        }

        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new RemoveUserRolesCommand.Response
        {
            Ids = userRoleIds,
            RemovedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
