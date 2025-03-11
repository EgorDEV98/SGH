using Microsoft.EntityFrameworkCore;
using SGH.Data;

namespace SGH.Tests.DbMock;

public class PostgresDbContextMock : PostgresDbContext
{
    public PostgresDbContextMock() : base(new DbContextOptions<PostgresDbContext>()) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}
