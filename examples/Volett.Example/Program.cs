var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVolett();

var app = builder.Build();

app.UseVolett();

app.MapGet("/", () => Results.Ok(new
{
    Framework = "Volett",
    Status = "Running"
}));

app.Run();
