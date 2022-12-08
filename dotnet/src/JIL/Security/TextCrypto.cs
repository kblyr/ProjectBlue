using System.Security.Cryptography;
using System.Text;

namespace JIL.Security;

static class TEXTCRYPTO_GLOBAL
{
    public static readonly Encoding Encoding = Encoding.UTF8;
    public static readonly RSAEncryptionPadding EncryptionPadding = RSAEncryptionPadding.OaepSHA512;
}

public interface ITextEncryptor : IDisposable
{
    string Encrypt(string text);
    ValueTask<string> EncryptAsync(string text, CancellationToken cancellationToken = default);
}

public interface ITextDecryptor : IDisposable
{
    string Decrypt(string cipherText);
    ValueTask<string> DecryptAsync(string cipherText, CancellationToken cancellationToken = default);
}

public interface ITextCryptoKeyLoader
{
    void Load(RSA algorithm);
    Task LoadAsync(RSA algorithm, CancellationToken cancellationToken = default);
}

public interface ITextEncryptionKeyLoader : ITextCryptoKeyLoader { }

public interface ITextDecryptionKeyLoader : ITextCryptoKeyLoader { }

public abstract class TextCryptorBase<TKeyLoader> where TKeyLoader : ITextCryptoKeyLoader
{
    readonly TKeyLoader _keyLoader;
    RSA? _algorithm;

    protected TextCryptorBase(TKeyLoader keyLoader)
    {
        _keyLoader = keyLoader;
    }

    RSA InitializeAlgorithm() 
    {
        var algorithm = RSA.Create();
        _keyLoader.Load(algorithm);
        return algorithm;
    }

    async ValueTask<RSA> InitializeAlgorithmAsync(CancellationToken cancellationToken)
    {
        var algorithm = RSA.Create();
        await _keyLoader.LoadAsync(algorithm, cancellationToken);
        return algorithm;
    }

    public virtual void Dispose()
    {
        _algorithm?.Dispose();
    }

    protected RSA GetAlgorithm()
    {
        return _algorithm ??= InitializeAlgorithm();
    }

    protected async ValueTask<RSA> GetAlgorithmAsync(CancellationToken cancellationToken = default)
    {
        return _algorithm ??= await InitializeAlgorithmAsync(cancellationToken);
    }
}

public abstract class TextEncryptorBase : TextCryptorBase<ITextEncryptionKeyLoader>
{
    protected TextEncryptorBase(ITextEncryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }

    public string Encrypt(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return "";
        }

        var data = TEXTCRYPTO_GLOBAL.Encoding.GetBytes(text);
        var cipherData = GetAlgorithm().Encrypt(data, TEXTCRYPTO_GLOBAL.EncryptionPadding);
        var cipherText = Convert.ToBase64String(cipherData);
        return cipherText;
    }

    public async ValueTask<string> EncryptAsync(string text, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return "";
        }

        var data = TEXTCRYPTO_GLOBAL.Encoding.GetBytes(text);
        var cipherData = (await GetAlgorithmAsync(cancellationToken)).Encrypt(data, TEXTCRYPTO_GLOBAL.EncryptionPadding);
        var cipherText = Convert.ToBase64String(cipherData);
        return cipherText;
    }
}

public abstract class TextDecryptorBase : TextCryptorBase<ITextDecryptionKeyLoader>
{
    protected TextDecryptorBase(ITextDecryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrWhiteSpace(cipherText))
        {
            return "";
        }

        var cipherData = Convert.FromBase64String(cipherText);
        var data = GetAlgorithm().Decrypt(cipherData, TEXTCRYPTO_GLOBAL.EncryptionPadding);
        var text = TEXTCRYPTO_GLOBAL.Encoding.GetString(data);
        return text;
    }

    public async ValueTask<string> DecryptAsync(string cipherText, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(cipherText))
        {
            return "";
        }

        var cipherData = Convert.FromBase64String(cipherText);
        var data = (await GetAlgorithmAsync(cancellationToken)).Decrypt(cipherData, TEXTCRYPTO_GLOBAL.EncryptionPadding);
        var text = TEXTCRYPTO_GLOBAL.Encoding.GetString(data);
        return text;
    }
}

public abstract class PemFileTextCryptoKeyLoaderBase
{
    readonly string _filePath;

    protected PemFileTextCryptoKeyLoaderBase(string filePath)
    {
        _filePath = filePath;
    }

    public void Load(RSA algorithm)
    {
        ThrowIfMissingFile();
        algorithm.ImportFromPem(File.ReadAllText(_filePath));
    }

    public async Task LoadAsync(RSA algorithm, CancellationToken cancellationToken = default)
    {
        ThrowIfMissingFile();
        algorithm.ImportFromPem(await File.ReadAllTextAsync(_filePath, cancellationToken));
    }

    void ThrowIfMissingFile()
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
        {
            throw new CryptoKeyFileNotFoundException("Crypto key file does not exists", _filePath);
        }
    }
}