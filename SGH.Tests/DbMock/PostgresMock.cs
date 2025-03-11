namespace SGH.Tests.DbMock;

public class PostgresMock
{
    public static PostgresDbContextMock Create()
    {
        var chargePointDbContextMock = new PostgresDbContextMock();
        chargePointDbContextMock.Database.EnsureDeleted();
        chargePointDbContextMock.Database.EnsureCreated();
        chargePointDbContextMock.SaveChanges();
        return chargePointDbContextMock;
    }
}