using Arbor.FS;
using CliWrap;
using Zio;

namespace Arbor.CodeAutomate;

public class GitRepositoryStore
{
    private readonly GitPathHelper _gitPathHelper;
    private readonly TempDirectoryHelper _tempDirectoryHelper;

    public GitRepositoryStore(TempDirectoryHelper tempDirectoryHelper, GitPathHelper gitPathHelper)
    {
        _tempDirectoryHelper = tempDirectoryHelper;
        _gitPathHelper = gitPathHelper;
    }

    public async Task<GitRepositoryInfo> Get(Uri gitUri, CancellationToken cancellationToken = default)
    {
        int seed = Random.Shared.Next();
        string asPathFriendly = gitUri.ConvertToPath();

        if (asPathFriendly.EndsWith(".git", StringComparison.OrdinalIgnoreCase))
        {
            asPathFriendly = asPathFriendly.Substring(0, asPathFriendly.Length - 4);
        }

        var repositoryTempDirectory = _tempDirectoryHelper.CreateNewTempDirectory(asPathFriendly + "_" + seed);

        await Clone(gitUri, repositoryTempDirectory, cancellationToken);

        return new GitRepositoryInfo(repositoryTempDirectory);
    }

    private async Task Clone(Uri gitUri, DirectoryEntry repositoryDirectory, CancellationToken cancellationToken)
    {
        string internalPath = repositoryDirectory.ConvertPathToInternal();
        string[] args = new[] {"clone", gitUri.ToString(), internalPath};

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