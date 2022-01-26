using Zio;

namespace Arbor.CodeAutomate;

public class GitRepositoryInfo
{
    public GitRepositoryInfo(DirectoryEntry repositoryDirectory) => RepositoryDirectory = repositoryDirectory;

    public DirectoryEntry RepositoryDirectory { get; }
}