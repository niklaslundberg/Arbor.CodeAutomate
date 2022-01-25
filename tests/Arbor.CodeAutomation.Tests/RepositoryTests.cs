using Arbor.CodeAutomate;
using Xunit;
using Zio.FileSystems;

namespace Arbor.CodeAutomation.Tests;

public class RepositoryTests
{
    [Fact]
    public async Task GetRepository()
    {
        var repository = new GitRepositoryStore(new TempDirectoryHelper(new MemoryFileSystem()));

        var gitRepository = await repository.Get(new Uri("https://github.com/niklaslundberg/Arbor.CodeAutomation.git"),
            CancellationToken.None);
    }
}