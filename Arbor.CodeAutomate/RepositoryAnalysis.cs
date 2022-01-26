using System.Collections.Immutable;
using Zio;

namespace Arbor.CodeAutomate;

public class RepositoryAnalysis
{
    private readonly ImmutableArray<FileEntry> _workflowFiles;

    public RepositoryAnalysis(ImmutableArray<FileEntry> workflowFiles)
    {
        _workflowFiles = workflowFiles;

        GitHubActionsEnabled = !_workflowFiles.IsDefaultOrEmpty;
    }

    public bool GitHubActionsEnabled { get; }
}