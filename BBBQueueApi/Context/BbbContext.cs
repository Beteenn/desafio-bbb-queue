using Microsoft.EntityFrameworkCore;

public class BbbContext : DbContext
{
    public BbbContext(DbContextOptions<BbbContext> options) : base(options) { }

    public DbSet<Voto> Votos { get; set; }
}