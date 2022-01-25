namespace Arbor.CodeAutomate;

public class GitRepositoryStore
{
    private readonly TempDirectoryHelper _tempDirectoryHelper;

    public GitRepositoryStore(TempDirectoryHelper tempDirectoryHelper)
    {
        _tempDirectoryHelper = tempDirectoryHelper;
    }

    public async Task<GitRepositoryInfo> Get(Uri gitUri, CancellationToken cancellationToken = default)
    {
        var asPathFriendly = gitUri.ConvertToPath();

        var repositoryTempDirectory = _tempDirectoryHelper.CreateNewTempDirectory(asPathFriendly);

        return new GitRepositoryInfo();
    }
}