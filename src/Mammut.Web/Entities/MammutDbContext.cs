using Microsoft.EntityFrameworkCore;

namespace Mammut.Web.Entities;

public class MammutDbContext: DbContext
{
    public DbSet<User> Users { get; }

    public MammutDbContext(DbContextOptions<MammutDbContext> options) : base(options)
    {
    }
}