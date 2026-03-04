namespace SistemaBancario.Data;

using Microsoft.EntityFrameworkCore;
using SistemaBancario.Models;

public class AppDbContext : DbContext
{   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        /* 
            - Un'account può essere sia ricevente che mandante 
            - Gli account possono essere coinvolti in piu transizioni
        */

        // Relazione transazione(n)-account(1) (mandante)
        modelBuilder.Entity<Transaction>()
                    .HasOne(t => t.SenderAccount)
                    .WithMany(a => a.SentTransactions)
                    .HasForeignKey(t => t.SenderAccountId)
                    .OnDelete(DeleteBehavior.Restrict);

        // Relazione transazione(n)-account(1) (ricevente)
        modelBuilder.Entity<Transaction>()
                    .HasOne(t => t.ReceiverAccount)
                    .WithMany(a => a.ReceivedTransactions)
                    .HasForeignKey(t => t.ReceiverAccountId)
                    .OnDelete(DeleteBehavior.Restrict); // Blocca la cancellazione di un'account per evitare la cancellazione di account
        
        // Il balance dell'account può essere solo positivo
        modelBuilder.Entity<Account>()
                    .ToTable(tb => tb.HasCheckConstraint("CK_Account_Balance_Positive", "[Balance] >= 0"));

        base.OnModelCreating(modelBuilder);
    }
}