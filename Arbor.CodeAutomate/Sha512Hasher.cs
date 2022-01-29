using System.Security.Cryptography;

namespace Arbor.CodeAutomate;

public static class Sha512Hasher
{
    public static byte[] Hash(Stream stream)
    {
        using var hasher = SHA512.Create();
        byte[] hashBytes = hasher.ComputeHash(stream);

        return hashBytes;
    }
}