using JIL.Security;

namespace JIL.Accounts.Security;

public interface IUserPasswordF2BEncryptor : ITextEncryptor { }

public interface IUserPasswordF2BDecryptor : ITextDecryptor { }

public interface IUserPasswordF2BEncryptionKeyLoader : ITextEncryptionKeyLoader { }

public interface IUserPasswordF2BDecryptionKeyLoader : ITextDecryptionKeyLoader { }

sealed class UserPasswordF2BEncryptor : TextEncryptorBase, IUserPasswordF2BEncryptor
{
    public UserPasswordF2BEncryptor(IUserPasswordF2BEncryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }
}

sealed class UserPasswordF2BDecryptor : TextDecryptorBase, IUserPasswordF2BDecryptor
{
    public UserPasswordF2BDecryptor(IUserPasswordF2BDecryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }
}

sealed class UserPasswordF2BEncryptionKeyLoader : PemFileTextCryptoKeyLoaderBase, IUserPasswordF2BEncryptionKeyLoader
{
    public UserPasswordF2BEncryptionKeyLoader(string filePath) : base(filePath)
    {
    }
}

sealed class UserPasswordF2BDecryptionKeyLoader : PemFileTextCryptoKeyLoaderBase, IUserPasswordF2BDecryptionKeyLoader
{
    public UserPasswordF2BDecryptionKeyLoader(string filePath) : base(filePath)
    {
    }
}
