namespace Volett.Example.Models;

public sealed record Todo(Guid Id, string Title, DateTimeOffset CreatedAt);
