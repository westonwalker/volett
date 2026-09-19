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

builder.Services.AddVolett(builder.Configuration);

var app = builder.Build();

app.UseVolett();
app.Run();
```

## Database and Entity Framework Core

Volett configures EF Core from `appsettings.json`. Set the driver to `sqlite`, `postgres`,
`mssql`, `mysql`, or `none`:

```json
{
  "Volett": {
    "Database": {
      "Driver": "sqlite",
      "ConnectionString": "Data Source=volett.sqlite"
    }
  }
}
```

Create a normal EF Core context in the application:

```csharp
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}
```

Then give its type to Volett. Volett reads the configured driver, selects the provider, and
registers the context with the standard scoped lifetime:

```csharp
builder.Services.AddVolett<ApplicationDbContext>(builder.Configuration);
```

With `"Driver": "none"` (the default), use the non-generic registration and no EF Core
context is added:

```csharp
builder.Services.AddVolett(builder.Configuration);
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
