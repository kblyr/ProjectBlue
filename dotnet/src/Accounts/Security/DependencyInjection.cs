using Microsoft.Extensions.DependencyInjection;

namespace JIL.Accounts.Security;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    DependencyInjector(IServiceCollection services) : base(services) { }

    static DependencyInjector? _instance;
    public static DependencyInjector GetInstance(Accounts.DependencyInjector parent) => _instance ??= new(parent.Services);
}

public sealed record DependencyOptions
{
    internal DependencyOptions() { }

    public bool IncludeUserPasswordHashAlgorithm { get; set; }
    public bool IncludeUserPasswordF2BHashAlgorithm { get; set; }
    public UserPasswordF2BEncryptionOptions? UserPasswordF2BEncryption { get; set; }
    public UserPasswordF2BDecryptionOptions? UserPasswordF2BDecryption { get; set; }

    public sealed record UserPasswordF2BEncryptionOptions
    {
        public string? PemFilePath { get; init; }
    }

    public sealed record UserPasswordF2BDecryptionOptions
    {
        public string? PemFilePath { get; init; }
    }
}

public static class DependencyExtensions
{
    public static Accounts.DependencyInjector AddSecurity(this Accounts.DependencyInjector parentInjector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = DependencyInjector.GetInstance(parentInjector);
        injectDependencies(injector);
        return parentInjector;
    }

    public static Accounts.DependencyInjector AddSecurity(this Accounts.DependencyInjector parentInjector, DependencyOptions options)
    {
        return parentInjector.AddSecurity(injector =>
        {
            if (options.IncludeUserPasswordHashAlgorithm)
            {
                injector.AddUserPasswordHashAlgorithm();
            }

            if (options.IncludeUserPasswordF2BHashAlgorithm)
            {
                injector.AddUserPasswordF2BHashAlgorithm();
            }

            if (options.UserPasswordF2BEncryption is not null)
            {
                injector.AddUserPasswordF2BEncryptor();

                if (options.UserPasswordF2BEncryption.PemFilePath is not null)
                {
                    injector.AddUserPasswordF2BPemFileEncryptionKeyLoader(options.UserPasswordF2BEncryption.PemFilePath);
                }
            }

            if (options.UserPasswordF2BDecryption is not null)
            {
                injector.AddUserPasswordF2BDecryptor();

                if (options.UserPasswordF2BDecryption.PemFilePath is not null)
                {
                    injector.AddUserPasswordF2BPemFileDecryptionKeyLoader(options.UserPasswordF2BDecryption.PemFilePath);
                }
            }
        });
    }

    public static DependencyInjector AddUserPasswordHashAlgorithm(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IUserPasswordHashAlgorithm, UserPasswordHashAlgorithm>();
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BHashAlgorithm(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IUserPasswordF2BHashAlgorithm, UserPasswordF2BHashAlgorithm>();
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BEncryptor(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IUserPasswordF2BEncryptor, UserPasswordF2BEncryptor>();
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BPemFileEncryptionKeyLoader(this DependencyInjector injector, string filePath)
    {
        injector.Services.AddSingleton<IUserPasswordF2BEncryptionKeyLoader>(sp => new UserPasswordF2BEncryptionKeyLoader(filePath));
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BDecryptor(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IUserPasswordF2BDecryptor, UserPasswordF2BDecryptor>();
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BPemFileDecryptionKeyLoader(this DependencyInjector injector, string filePath)
    {
        injector.Services.AddSingleton<IUserPasswordF2BDecryptionKeyLoader>(sp => new UserPasswordF2BDecryptionKeyLoader(filePath));
        return injector;
    }
}
