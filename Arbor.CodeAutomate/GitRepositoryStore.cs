using Arbor.FS;
using CliWrap;
using Zio;

namespace Arbor.CodeAutomate;

public class GitRepositoryStore
{
    private readonly TempDirectoryHelper _tempDirectoryHelper;
    private readonly GitPathHelper _gitPathHelper;

    public GitRepositoryStore(TempDirectoryHelper tempDirectoryHelper, GitPathHelper gitPathHelper)
    {
        _tempDirectoryHelper = tempDirectoryHelper;
        _gitPathHelper = gitPathHelper;
    }

    public async Task<GitRepositoryInfo> Get(Uri gitUri, CancellationToken cancellationToken = default)
    {
        var asPathFriendly = gitUri.ConvertToPath();

        if (asPathFriendly.EndsWith(".git", StringComparison.OrdinalIgnoreCase))
        {
            asPathFriendly = asPathFriendly.Substring(0, asPathFriendly.Length - 4);
        }

        var repositoryTempDirectory = _tempDirectoryHelper.CreateNewTempDirectory(asPathFriendly);

        await Clone(gitUri, repositoryTempDirectory, cancellationToken);

        return new GitRepositoryInfo(repositoryTempDirectory);
    }

    private async Task Clone(Uri gitUri, DirectoryEntry repositoryDirectory, CancellationToken cancellationToken)
    {
        string internalPath = repositoryDirectory.ConvertPathToInternal();
        var args = new[] {"clone", gitUri.ToString(), internalPath};

        var command = await Cli.Wrap(_gitPathHelper.GitPath.ConvertPathToInternal())
            .WithArguments(args)
            .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .ExecuteAsync(cancellationToken);

       if (command.ExitCode != 0)
       {
           throw new InvalidOperationException($"Failed to clone repository {gitUri} to path {internalPath}");
       }
    }
}