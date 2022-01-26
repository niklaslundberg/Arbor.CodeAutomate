using Arbor.FS;
using Zio;

namespace Arbor.CodeAutomate;

public class GitPathHelper
{
    private readonly IFileSystem _fileSystem;
    private readonly FileEntry _gitPath;

    public GitPathHelper(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;

        _gitPath = _fileSystem.GetFileEntry(@"C:\Program Files\git\cmd\git.exe".ParseAsPath());
    }

    public FileEntry GitPath => _gitPath.Exists ? _gitPath : throw new InvalidOperationException("Git path " + _gitPath.ConvertPathToInternal() + " does not exist");
}