using Microsoft.EntityFrameworkCore;
using Volett.Example.Models;

namespace Volett.Example.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}
