using Zio;

namespace Arbor.CodeAutomate;

public static class FileHasher
{
    public static string CreateHash(FileEntry file)
    {
        using var fs = file.Open(FileMode.Open, FileAccess.Read);

        byte[] hashBytes = Sha512Hasher.Hash(fs);

        return BitConverter.ToString(hashBytes).Replace("-", "");
    }
}