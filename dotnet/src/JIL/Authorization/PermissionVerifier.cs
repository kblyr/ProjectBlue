namespace JIL.Authorization;

public interface IPermissionVerifier
{
    ValueTask<bool> Verify(int permissionId, CancellationToken cancellationToken = default);
}

public interface IPermissionsLoader
{
    Task<int[]> Load(CancellationToken cancellationToken = default);
}

sealed class PermissionVerifier : IPermissionVerifier
{
    readonly IPermissionsLoader _permissionsLoader;
    
    int[]? _permissionIds;

    public PermissionVerifier(IPermissionsLoader permissionsLoader)
    {
        _permissionsLoader = permissionsLoader;
    }

    public async ValueTask<bool> Verify(int permissionId, CancellationToken cancellationToken = default)
    {
        if (permissionId == 0)
        {
            return false;
        }

        if (_permissionIds is null)
        {
            _permissionIds = await _permissionsLoader.Load(cancellationToken) ?? Array.Empty<int>();
        }

        return _permissionIds.Length > 0 && _permissionIds.Contains(permissionId);
    }
}
