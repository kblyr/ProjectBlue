using JIL.Auditing;

namespace JIL.Authorization;

sealed class RolesLoader : IRolesLoader
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public RolesLoader(IDbContextFactory<AuthorizationDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<int[]> Load(CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var auditInfo = _auditInfoProvider.Current;
        var roleIds = await context.UserRoles.AsNoTracking()
            .Where(_ => _.UserId == auditInfo.User.Id && _.IsDeleted == false)
            .Select(_ => _.RoleId)
            .Distinct()
            .ToArrayAsync(cancellationToken);
        return roleIds;
    }
}
