namespace JIL.Authorization;

sealed class PermissionsLoader : IPermissionsLoader
{
    readonly IDbContextFactory<AuthorizationDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public PermissionsLoader(IDbContextFactory<AuthorizationDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider)
    {
        this._contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<int[]> Load(CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var auditInfo = _auditInfoProvider.Current;
        var permissionIds = await context.UserPermissions.AsNoTracking()
            .Where(_ => _.UserId == auditInfo.User.Id && _.IsDeleted == false)
            .Select(_ => _.PermissionId)
            .Distinct()
            .ToArrayAsync(cancellationToken);
        return permissionIds;
    }
}
