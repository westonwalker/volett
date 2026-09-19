using Microsoft.AspNetCore.Mvc;
using Volett.Example.Actions;
using Volett.Example.Models;
using Volett.Example.Requests;

namespace Volett.Example.Controllers;

[ApiController]
[Route("todos")]
public sealed class TodosController(CreateTodo createTodo) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Todo>> Store(
        [FromBody] CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await createTodo.ExecuteAsync(request, cancellationToken);

        return Created($"/todos/{todo.Id}", todo);
    }
}
