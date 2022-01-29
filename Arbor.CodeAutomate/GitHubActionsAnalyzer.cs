using System.Collections.Immutable;
using System.Text;
using Zio;

namespace Arbor.CodeAutomate;

public class GitHubActionsAnalyzer
{
    public async Task<RepositoryAnalysis> GetAnalysis(GitRepositoryInfo gitRepository, GitRepositoryInfo templateRepository)
    {
        var workflowFiles = gitRepository.RepositoryDirectory.EnumerateDirectories(".github")
            .SingleOrDefault()?.EnumerateDirectories("workflows").SingleOrDefault()?.EnumerateFiles();

        var codeFixSuggestions = new List<ICodeFixSuggestion>();

        var sourceFiles = new List<string> {"ci.yml"};

        foreach (string sourceFile in sourceFiles)
        {

            var templateFile = templateRepository.RepositoryDirectory.EnumerateFiles(sourceFile, SearchOption.AllDirectories).SingleOrDefault();
            var targetFile = gitRepository.RepositoryDirectory.EnumerateFiles(sourceFile, SearchOption.AllDirectories).SingleOrDefault();

            if (targetFile is null || FileHasher.CreateHash(targetFile) != FileHasher.CreateHash(templateFile!))
            {
                codeFixSuggestions.Add(new UpdateCiFile(templateFile, gitRepository));
            }
        }


        return new(workflowFiles?.ToImmutableArray() ?? ImmutableArray<FileEntry>.Empty, codeFixSuggestions);
    }
}