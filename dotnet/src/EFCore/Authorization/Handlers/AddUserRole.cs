using JIL.Authorization.Contracts;
using JIL.Authorization.Entities;

namespace JIL.Authorization.Handlers;

sealed class AddUserRoleHandler : IRequestHandler<AddUserRoleCommand>
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public AddUserRoleHandler(IDbContextFactory<AuthorizationDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        
        if (await context.Roles.AsNoTracking().Where(_ => _.Id == request.RoleId).AnyAsync(cancellationToken) == false)
        {
            return new RoleNotFoundError { Id = request.RoleId };
        }

        if (await context.UserRoles.AsNoTracking().Where(_ => _.UserId == request.UserId && _.RoleId == request.RoleId && _.IsDeleted == false).AnyAsync(cancellationToken))
        {
            return _mapper.Map<AddUserRoleCommand, UserRoleExistsError>(request);
        }

        var auditInfo = _auditInfoProvider.Current;
        var userRole = _mapper.Map<AddUserRoleCommand, UserRole>(request) with
        {
            IsDeleted = false,
            InsertedById = auditInfo.User.Id,
            InsertedOn = auditInfo.Timestamp,
        };
        context.UserRoles.Add(userRole);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new AddUserRoleCommand.Response
        { 
            Id = userRole.Id,
            AddedBy = _mapper.GetLoggedUser(auditInfo)
        };
    }
}
