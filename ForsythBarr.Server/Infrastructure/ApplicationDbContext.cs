using Microsoft.EntityFrameworkCore;
using Task = ForsythBarr.Server.Domain.Models.Task;

namespace ForsythBarr.Server.Infrastructure;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Task> Tasks { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}