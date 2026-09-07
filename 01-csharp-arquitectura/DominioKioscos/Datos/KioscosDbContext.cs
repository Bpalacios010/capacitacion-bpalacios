using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DominioKioscos;

public class KioscosDbContext : DbContext
{
    public DbSet<Kiosco> Kioscos => Set<Kiosco>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseSqlite("Data Source=kioscos.db")
            .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        modelo.Entity<Kiosco>(k =>
        {
            k.HasKey(x => x.Id);
            k.Property(x => x.Codigo).IsRequired().HasMaxLength(20);
            k.HasIndex(x => x.Codigo).IsUnique();
            k.Property(x => x.Estado).HasConversion<string>().HasMaxLength(20);
            k.Property(x => x.MotivoFueraDeServicio).HasMaxLength(200);
        });
    }
}
