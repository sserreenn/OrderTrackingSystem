using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using OrderTracking.DataAccess.Context;

namespace OrderTracking.DataAccess;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Buradaki bağlantı dizesi sadece migration oluşturmak içindir.
        optionsBuilder.UseSqlServer("Server=LAPTOP-GGGALE7Q;Initial Catalog=OrderTrackingDb; Integrated Security=True;Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}