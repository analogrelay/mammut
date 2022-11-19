using Microsoft.EntityFrameworkCore;

namespace Mammut.Web.Entities;

public class MammutDbContext: DbContext
{
    public MammutDbContext(DbContextOptions<MammutDbContext> options) : base(options)
    {
    }
}