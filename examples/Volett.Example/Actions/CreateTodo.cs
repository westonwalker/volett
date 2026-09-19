using Volett.Actions;
using Volett.Example.Models;
using Volett.Example.Requests;

namespace Volett.Example.Actions;

public sealed class CreateTodo : IAction<CreateTodoRequest, Todo>
{
    public Task<Todo> ExecuteAsync(
        CreateTodoRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var todo = new Todo(
            Guid.NewGuid(),
            request.Title.Trim(),
            DateTimeOffset.UtcNow);

        return Task.FromResult(todo);
    }
}
