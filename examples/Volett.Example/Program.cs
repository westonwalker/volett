using Volett.Example.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVolett<ApplicationDbContext>(builder.Configuration);

var app = builder.Build();

app.UseVolett();

app.MapGet("/", () => Results.Ok(new
{
    Framework = "Volett",
    Status = "Running"
}));

app.Run();

// Exposes the compiler-generated entry point to WebApplicationFactory in the integration tests.
// This is a testing convenience for the example application, not a Volett requirement.
public partial class Program;
