using Dnd.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Dnd.Db;

public class QweContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=dndExam;Integrated Security=True");
    
    public DbSet<Monster> Monsters { get; set; }
}