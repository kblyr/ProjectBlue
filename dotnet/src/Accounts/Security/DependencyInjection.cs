using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JIL.Accounts.Security;

public sealed record DependencyOptions : DependencyOptionsBase
{
    public FeaturesObj Features { get; } = new();
    public UserPasswordF2BEncryptionObj UserPasswordF2BEncryption { get; } = new();
    public UserPasswordF2BDecryptionObj UserPasswordF2BDecryption { get; } = new();

    public sealed record FeaturesObj
    {
        internal FeaturesObj() { }

        public bool UserPasswordHashAlgorithm { get; set; } = true;
        public bool UserPasswordF2BHashAlgorithm { get; set; } = true;
    }

    public sealed record UserPasswordF2BEncryptionObj : DependencyOptionsBase
    {
        internal UserPasswordF2BEncryptionObj() { }

        public string? PemFilePath { get; set; }
    }

    public sealed record UserPasswordF2BDecryptionObj : DependencyOptionsBase
    {
        internal UserPasswordF2BDecryptionObj() { }

        public string? PemFilePath { get; set; }
    }
}

static class DependencyExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, DependencyOptions options)
    {
        if (options.IsIgnored)
        {
            return services;
        }

        if (options.Features.UserPasswordHashAlgorithm)
        {
            services.TryAddSingleton<IUserPasswordHashAlgorithm, UserPasswordHashAlgorithm>();
        }

        if (options.Features.UserPasswordF2BHashAlgorithm)
        {
            services.TryAddSingleton<IUserPasswordF2BHashAlgorithm, UserPasswordF2BHashAlgorithm>();
        }

        if (options.UserPasswordF2BEncryption.IsIncluded)
        {
            services.TryAddSingleton<IUserPasswordF2BEncryptor, UserPasswordF2BEncryptor>();

            if (options.UserPasswordF2BEncryption.PemFilePath is not null)
            {
                services.TryAddSingleton<IUserPasswordF2BEncryptionKeyLoader>(sp => new UserPasswordF2BEncryptionKeyLoader(options.UserPasswordF2BEncryption.PemFilePath));
            }
        }

        if (options.UserPasswordF2BDecryption.IsIncluded)
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
