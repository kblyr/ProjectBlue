using JIL.Authorization.Contracts;
using JIL.Authorization.Entities;

namespace JIL.Authorization.Handlers;

sealed class AddUserRolesHandler : IRequestHandler<AddUserRolesCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public AddUserRolesHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(AddUserRolesCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        var auditInfo = _auditInfoProvider.Current;
        var userRoles = new List<UserRole>();

        foreach (var roleId in request.RoleIds)
        {
            if (await context.Roles.AsNoTracking().Where(_ => _.Id == roleId).AnyAsync(cancellationToken) == false)
            {
                return new RoleNotFoundError { Id = roleId };
            }

            if (await context.UserRoles.AsNoTracking().Where(_ => _.UserId == request.UserId && _.RoleId == roleId && _.IsDeleted == false).AnyAsync(cancellationToken))
            {
                return new UserRoleExistsError { UserId = request.UserId, RoleId = roleId };
            }

            var userRole = new UserRole
            {
                UserId = request.UserId,
                RoleId = roleId,
                IsDeleted = false,
                InsertedById = auditInfo.User.Id,
                InsertedOn = auditInfo.Timestamp
            };
            context.UserRoles.Add(userRole);
            userRoles.Add(userRole);
        }

        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new AddUserRolesCommand.Response
        {
            Ids = userRoles.Select(_ => _.Id),
            AddedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
