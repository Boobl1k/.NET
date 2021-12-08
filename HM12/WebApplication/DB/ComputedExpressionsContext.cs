using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication.DB;

public class ComputedExpressionsContext : DbContext, IDbContext<ComputedExpression>
{
    private const string Catalog = "expressionsCache";
    private const string ConnectionString = $"Data Source=localhost;Initial Catalog={Catalog};Integrated Security=True";

    private readonly ILoggerFactory _loggerFactory =
        LoggerFactory.Create(config => config.AddConsole());

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.UseSqlServer(ConnectionString);
    }
    
    public DbSet<ComputedExpression> Items { get; set; }
    public new void SaveChanges() => base.SaveChanges();
}
