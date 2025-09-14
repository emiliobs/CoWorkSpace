using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoWorkSpace.Infraestructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilder.UseSqlServer("Data Source=.;Initial Catalog=CoWorkSpaceDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        return new AppDbContext(optionBuilder.Options);
    }
}