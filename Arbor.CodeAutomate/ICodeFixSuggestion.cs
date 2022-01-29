namespace Arbor.CodeAutomate;

public interface ICodeFixSuggestion
{
    Task Apply(CancellationToken cancellationToken);
}