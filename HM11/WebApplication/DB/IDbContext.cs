using Microsoft.EntityFrameworkCore;

namespace WebApplication.DB;

public interface IDbContext<T> where T : class
{
    DbSet<T> Items { get; set; }
    void SaveChanges();
}
