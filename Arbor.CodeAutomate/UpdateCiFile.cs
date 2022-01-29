using Zio;

namespace Arbor.CodeAutomate;

public class UpdateCiFile:ICodeFixSuggestion
{
    public FileEntry SourceFile { get; }
    public GitRepositoryInfo GitRepository { get; }

    public UpdateCiFile(FileEntry sourceFile, GitRepositoryInfo gitRepository)
    {
        SourceFile = sourceFile;
        GitRepository = gitRepository;
    }

    public Task Apply(CancellationToken cancellationToken) => Task.CompletedTask;
}