# Volett

Volett is an opinionated, batteries-included application framework built on ASP.NET Core and .NET 10.

The project is at an early stage. Its goal is to provide cohesive conventions and strong defaults while preserving access to the underlying ASP.NET Core ecosystem. See [summary.md](summary.md) for the complete north star.

## Build

```bash
dotnet restore
dotnet build
dotnet test
```

## Application integration

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVolett();

var app = builder.Build();

app.UseVolett();
app.Run();
```

The runnable application in [`examples/Volett.Example`](examples/Volett.Example) demonstrates this integration against the local Volett project:

```bash
dotnet run --project examples/Volett.Example
```

It also contains a complete request-to-action example:

```text
POST /todos
  → CreateTodoRequest
  → TodosController
  → CreateTodo
  → Todo
```

Actions implement `IAction<TRequest, TResult>` and are registered automatically with scoped lifetime. Controllers inject the concrete action and call `ExecuteAsync` directly.
