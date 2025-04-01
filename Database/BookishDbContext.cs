namespace Bookish.Database;
using Microsoft.EntityFrameworkCore;
using Bookish.Models;
using Bookish.Utils;

public class BookishDbContext : DbContext
{
    public BookishDbContext(): base() {}
    public DbSet<Book> Book { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<User> User { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCollation("case_insensitive", locale: "en-u-ks-primary", provider: "icu", deterministic: false);

        modelBuilder.Entity<Book>().Property(c => c.Title)
        .UseCollation("case_insensitive");
        modelBuilder.Entity<Book>().Property(c => c.Author)
        .UseCollation("case_insensitive");

    }
            

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        if (!optionsBuilder.IsConfigured)

        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=bookish;User Id=bookish;Password=bookish;Include Error Detail=true");
        }
    }
}