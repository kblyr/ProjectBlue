using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Accounts.Security;

public sealed record DependencyOptions
{
    public FeaturesObj Features { get; } = new();
    public UserPasswordF2BEncryptionObj? UserPasswordF2BEncryption { get; set; }
    public UserPasswordF2BDecryptionObj? UserPasswordF2BDecryption { get; set; }

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool UserPasswordHashAlgorithm { get; set; } = true;
        public bool UserPasswordF2BHashAlgorithm { get; set; } = true;
    }

    public sealed record UserPasswordF2BEncryptionObj
    {
        public string? PemFilePath { get; init; }
    }

    public sealed record UserPasswordF2BDecryptionObj
    {
        public string? PemFilePath { get; init; }
    }
}

static class DependencyExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, DependencyOptions options)
    {
        if (options.Features.UserPasswordHashAlgorithm)
        {
            services.TryAddSingleton<IUserPasswordHashAlgorithm, UserPasswordHashAlgorithm>();
        }

        if (options.Features.UserPasswordF2BHashAlgorithm)
        {
            services.TryAddSingleton<IUserPasswordF2BHashAlgorithm, UserPasswordF2BHashAlgorithm>();
        }

        if (options.UserPasswordF2BEncryption is not null)
        {
            services.TryAddSingleton<IUserPasswordF2BEncryptor, UserPasswordF2BEncryptor>();

            if (options.UserPasswordF2BEncryption.PemFilePath is not null)
            {
                services.TryAddSingleton<IUserPasswordF2BEncryptionKeyLoader>(sp => new UserPasswordF2BEncryptionKeyLoader(options.UserPasswordF2BEncryption.PemFilePath));
            }
        }

        if (options.UserPasswordF2BDecryption is not null)
        {
            services.TryAddSingleton<IUserPasswordF2BDecryptor, UserPasswordF2BDecryptor>();

            if (options.UserPasswordF2BDecryption.PemFilePath is not null)
            {
                services.TryAddSingleton<IUserPasswordF2BDecryptionKeyLoader>(sp => new UserPasswordF2BDecryptionKeyLoader(options.UserPasswordF2BDecryption.PemFilePath));
            }
        }

        return services;
    }
}
