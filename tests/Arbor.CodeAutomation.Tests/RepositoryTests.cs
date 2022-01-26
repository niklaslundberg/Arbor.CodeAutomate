using Arbor.CodeAutomate;
using Arbor.FS;
using Xunit;
using Zio.FileSystems;

namespace Arbor.CodeAutomation.Tests;

public class RepositoryTests : IAsyncDisposable, IAsyncLifetime
{
    public RepositoryTests()
    {
        _fileSystem = new WindowsFs(new PhysicalFileSystem());
    }
    private readonly WindowsFs _fileSystem;
    private GitRepositoryInfo? _gitRepository;

    [Fact]
    public async Task GetRepository()
    {
        var repository = new GitRepositoryStore(new TempDirectoryHelper(_fileSystem), new GitPathHelper(_fileSystem));

        _gitRepository = await repository.Get(new Uri("https://github.com/niklaslundberg/Arbor.CodeAutomate.git"),
            CancellationToken.None);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    async Task IAsyncLifetime.DisposeAsync() => await DisposeAsync();

    public ValueTask DisposeAsync()
    {
        if (_gitRepository is { })
        {
            _gitRepository.RepositoryDirectory.DeleteIfExists(true);
        }

        _fileSystem.Dispose();

        return ValueTask.CompletedTask;
    }
}