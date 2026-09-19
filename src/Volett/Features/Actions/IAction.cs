namespace Volett.Actions;

/// <summary>
/// Represents a business operation with a request and result.
/// </summary>
/// <typeparam name="TRequest">The input required by the action.</typeparam>
/// <typeparam name="TResult">The value produced by the action.</typeparam>
public interface IAction<in TRequest, TResult>
{
    /// <summary>
    /// Executes the business operation.
    /// </summary>
    Task<TResult> ExecuteAsync(
        TRequest request,
        CancellationToken cancellationToken = default);
}
