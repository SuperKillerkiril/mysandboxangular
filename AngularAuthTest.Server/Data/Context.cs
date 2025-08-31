using AngularAuthTest.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthTest.Server.Data;

public class Context : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=UrbanMuseModels.db"); 
    }
}