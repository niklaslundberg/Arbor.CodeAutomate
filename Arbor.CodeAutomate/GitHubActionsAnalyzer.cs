using System.Collections.Immutable;

namespace Arbor.CodeAutomate;

public class GitHubActionsAnalyzer
{
    public RepositoryAnalysis GetAnalysis(GitRepositoryInfo gitRepository)
    {
        var workflowFiles = gitRepository.RepositoryDirectory.EnumerateDirectories(".github")
            .SingleOrDefault()?.EnumerateDirectories("workflows").SingleOrDefault()?.EnumerateFiles();

        return new(workflowFiles.ToImmutableArray());
    }
}