using Carta_Aleatoria_CP6.Entity;
using Microsoft.EntityFrameworkCore;

namespace Carta_Aleatoria_CP6.Database;

public class CartaDbContext : DbContext
{
    public DbSet<Carta> Cartas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(optionsBuilder);    
    }
    
}