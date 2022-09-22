using Microsoft.EntityFrameworkCore;

namespace bank_kata.Model
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Statement> Statements { get; set; } = null!;
    }
}
