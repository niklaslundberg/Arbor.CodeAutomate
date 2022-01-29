using System.Collections.Immutable;
using Zio;

namespace Arbor.CodeAutomate;

public class RepositoryAnalysis
{
    public RepositoryAnalysis(ImmutableArray<FileEntry> workflowFiles,
        IReadOnlyCollection<ICodeFixSuggestion> codeFixSuggestions)
    {
        CodeFixSuggestions = codeFixSuggestions;
        WorkflowFiles = workflowFiles;

        GitHubActionsEnabled = !WorkflowFiles.IsDefaultOrEmpty;
    }

    public ImmutableArray<FileEntry> WorkflowFiles { get; }

    public bool GitHubActionsEnabled { get; }


    public IReadOnlyCollection<ICodeFixSuggestion> CodeFixSuggestions { get; }
}