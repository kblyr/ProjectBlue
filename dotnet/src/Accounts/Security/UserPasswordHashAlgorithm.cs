using System.Security.Cryptography;

namespace JIL.Accounts.Security;

static class USERPASSWORDHASHALGORITHM_GLOBAL 
{
    public static readonly int SaltLength = 32;
    public static readonly int Iterations = 1_000;
    public static readonly int HashLength = 128;
    public static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;
}

public interface IUserPasswordHashAlgorithm
{
    UserPasswordHashAlgorithmComputeResult Compute(string password);
    string Compute(string password, string salt);
    bool Verify(string hashedPassword, string password, string salt);
}

public interface IUserPasswordF2BHashAlgorithm
{
    UserPasswordHashAlgorithmComputeResult Compute(string cipherPassword);
    ValueTask<UserPasswordHashAlgorithmComputeResult> ComputeAsync(string cipherPassword, CancellationToken cancellationToken = default);
    string Compute(string cipherPassword, string salt);
    ValueTask<string> ComputeAsync(string cipherPassword, string salt, CancellationToken cancellationToken = default);
    bool Verify(string hashedPassword, string cipherPassword, string salt);
    ValueTask<bool> VerifyAsync(string hashedPassword, string cipherPassword, string salt, CancellationToken cancellationToken = default);
}

public sealed record UserPasswordHashAlgorithmComputeResult
{
    public required string HashedPassword { get; init; }
    public required string Salt { get; init; }
}

sealed class UserPasswordHashAlgorithm : IUserPasswordHashAlgorithm
{
    Rfc2898DeriveBytes InitializeAlgorithm(string password, byte[] salt)
    {
        return new Rfc2898DeriveBytes(password, salt, USERPASSWORDHASHALGORITHM_GLOBAL.Iterations, USERPASSWORDHASHALGORITHM_GLOBAL.AlgorithmName);
    }

    string Compute(string password, byte[] salt)
    {
        using var algorithm = InitializeAlgorithm(password, salt);
        return ConvertDataToText(algorithm.GetBytes(USERPASSWORDHASHALGORITHM_GLOBAL.HashLength));
    }

    public UserPasswordHashAlgorithmComputeResult Compute(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        var salt = GenerateSalt();
        return new()
        {
            HashedPassword = Compute(password, salt),
            Salt = ConvertDataToText(salt)
        };
    }

    public string Compute(string password, string salt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        if (string.IsNullOrWhiteSpace(salt))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(salt));
        }

        return Compute(password, ConvertTextToData(salt));
    }

    public bool Verify(string hashedPassword, string password, string salt)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        if (string.IsNullOrWhiteSpace(salt))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(salt));
        }

        return string.Equals(hashedPassword, Compute(password, ConvertTextToData(salt)));
    }

    static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(USERPASSWORDHASHALGORITHM_GLOBAL.SaltLength);
    }

    static byte[] ConvertTextToData(string text)
    {
        return Convert.FromBase64String(text);
    }

    static string ConvertDataToText(byte[] data)
    {
        return Convert.ToBase64String(data);
    }
}
