using Arbor.FS;
using Zio;

namespace Arbor.CodeAutomate;

public class TempDirectoryHelper
{
    private readonly IFileSystem _fileSystem;

    public TempDirectoryHelper(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public DirectoryEntry GetTempDirectory()
    {
        var directoryPath = Path.GetTempPath().ParseAsPath();

        _fileSystem.CreateDirectory(directoryPath);

        var tempDirectory = _fileSystem.GetDirectoryEntry(directoryPath);




        return tempDirectory;
    }

    public DirectoryEntry CreateNewTempDirectory(string? seed = null)
    {
        var tempDirectory = GetTempDirectory();

        seed = string.IsNullOrWhiteSpace(seed) ? Guid.NewGuid().ToString() : seed;

        return tempDirectory.CreateSubdirectory(seed);
    }
}