using Arbor.CodeAutomate;
using Arbor.FS;
using Xunit;
using Zio.FileSystems;

namespace Arbor.CodeAutomation.Tests;

public class RepositoryTests : IAsyncDisposable, IAsyncLifetime
{
    private readonly WindowsFs _fileSystem;
    private GitRepositoryInfo? _gitRepository;
    private GitRepositoryInfo? _templateRepository;

    public RepositoryTests() => _fileSystem = new WindowsFs(new PhysicalFileSystem());

    public ValueTask DisposeAsync()
    {
        if (_gitRepository is { })
        {
            _gitRepository.RepositoryDirectory.DeleteIfExists();
        }

        if (_templateRepository is { })
        {
            _templateRepository.RepositoryDirectory.DeleteIfExists();
        }

        _fileSystem.Dispose();

        return ValueTask.CompletedTask;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    async Task IAsyncLifetime.DisposeAsync() => await DisposeAsync();

    [Fact]
    public async Task GetRepository()
    {
        var repository = new GitRepositoryStore(new TempDirectoryHelper(_fileSystem), new GitPathHelper(_fileSystem));

        _gitRepository = await repository.Get(new Uri("https://github.com/niklaslundberg/Arbor.CodeAutomate.git"),
            CancellationToken.None);

        _templateRepository = await repository.Get(new Uri("https://github.com/niklaslundberg/Arbor.CodeAutomate.git"),
            CancellationToken.None);

        var gitHubActionsAnalyzer = new GitHubActionsAnalyzer();
        var repositoryAnalysis = await gitHubActionsAnalyzer.GetAnalysis(_gitRepository, _templateRepository);

        if (repositoryAnalysis.GitHubActionsEnabled && repositoryAnalysis.CodeFixSuggestions.Any())
        {
        }
    }
}