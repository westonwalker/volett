using System.ComponentModel.DataAnnotations;

namespace Volett.Example.Requests;

public sealed class CreateTodoRequest
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; init; } = string.Empty;
}
