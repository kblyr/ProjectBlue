namespace JIL.Authorization;

public interface IRoleVerifier
{
    ValueTask<bool> Verify(int roleId, CancellationToken cancellationToken = default);
}

public interface IRolesLoader
{
    Task<int[]> Load(CancellationToken cancellationToken = default);
}

sealed class RoleVerifier : IRoleVerifier
{
    readonly IRolesLoader _rolesLoader;

    int[]? _roleIds;

    public RoleVerifier(IRolesLoader rolesLoader)
    {
        _rolesLoader = rolesLoader;
    }

    public async ValueTask<bool> Verify(int roleId, CancellationToken cancellationToken = default)
    {
        if (roleId == 0)
        {
            return false;
        }

        if (_roleIds is null)
        {
            _roleIds = await _rolesLoader.Load(cancellationToken) ?? Array.Empty<int>();
        }

        return _roleIds.Length > 0 && _roleIds.Contains(roleId);
    }
}
