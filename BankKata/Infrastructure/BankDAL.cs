using bank_kata.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace bank_kata.Infrastructure
{
    public class BankDAL
    {
        private readonly BankContext _context;

        public BankDAL(BankContext context)
        {
            _context = context;
        }
        public async Task<Account> Deposit(long AccountID, decimal Amount)
        {
            var account = await _context.Accounts.FindAsync(AccountID);
            if (account == null) throw new KeyNotFoundException();

            account.Deposit(Amount);

            _context.Statements.Add(
                new Statement()
                {
                    AccountId = AccountID,
                    Operation = OperationType.DEPOSIT,
                    Ammount = Amount,
                    Balance = account.Balance,
                    DateTime = DateTime.Now
                });

            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> Withdrawal(long AccountID, decimal Amount)
        {
            var account = await _context.Accounts.FindAsync(AccountID);
            if (account == null) throw new KeyNotFoundException();

            account.Withdrawal(Amount);

            _context.Statements.Add(
                new Statement()
                {
                    AccountId = AccountID,
                    Operation = OperationType.WITHDRAWAL,
                    Ammount = Amount,
                    Balance = account.Balance,
                    DateTime = DateTime.Now
                });

            await _context.SaveChangesAsync();
            return account;
        }
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return _context.Accounts;
        }
        public async Task<IEnumerable<Statement>> GetStatement(long accountId)
        {
            return _context.Statements.Where(w => w.AccountId == accountId);
        }

    }
}
