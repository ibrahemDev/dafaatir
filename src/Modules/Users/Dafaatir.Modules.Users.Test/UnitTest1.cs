using Dafaatir.Shared.Database.Sqlite;
using Moq;
namespace Dafaatir.Modules.Users.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {   
        
        
        
        var mockDbContext = new Mock<SqliteDbContext>();
        mockDbContext.Setup(db => db.Users.Find(1))
            .Returns(new Product { Id = 1, Name = "Product 1" });
    }
}