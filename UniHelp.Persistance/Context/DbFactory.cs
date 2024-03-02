namespace UniHelp.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DbFactory : IDesignTimeDbContextFactory<UniDataContext>
{
    public UniDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UniDataContext>();
        optionsBuilder.UseSqlServer(
            "Data Source=.; Initial Catalog=UniHelpDB; Integrated Security=True; MultiSubnetFailover=False; Trusted_Connection=True; TrustServerCertificate=True");

        return new UniDataContext(optionsBuilder.Options);
    }
}