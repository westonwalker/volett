# Volett

Volett is an opinionated, batteries-included application framework built on ASP.NET Core and .NET 10.

The project is at an early stage. Its goal is to provide cohesive conventions and strong defaults while preserving access to the underlying ASP.NET Core ecosystem. See [summary.md](summary.md) for the complete north star.

## Build

```bash
dotnet restore
dotnet build
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
