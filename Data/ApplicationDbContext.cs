using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using parcial.Models;

namespace parcial.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TransactionUSD> TransactionsUSD { get; set; }
    public DbSet<TransactionBTC> TransactionsBTC { get; set; }
    public DbSet<ConversionHistory> ConversionHistories { get; set; }
}
