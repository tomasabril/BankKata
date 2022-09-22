using bank_kata.Infrastructure;
using bank_kata.Model;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class UnitTest1 : IDisposable
    {
        private BankContext _context;
        private BankDAL _BankDAL;

        public UnitTest1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankContext>();
            optionsBuilder.UseInMemoryDatabase("TestInMemoryDb");
            _context = new BankContext(optionsBuilder.Options);

            _BankDAL = new BankDAL(_context);

            //// initialize sample data
            _context.Accounts.Add(new Account(10));
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        [Fact]
        public void Deposit_Test()
        {
            var account = _context.Accounts.Find((long)1);
            decimal initialBalance = account.Balance;
            decimal value = 10;

            var result = _BankDAL.Deposit(account.Id, value);

            Assert.Equal(initialBalance + value, _context.Accounts.Find((long)1).Balance);
        }

        [Fact]
        public void Withdrawal_Test()
        {
            var account = _context.Accounts.Find((long)1);
            decimal initialBalance = account.Balance;
            decimal value = 5;

            var result = _BankDAL.Withdrawal(account.Id, value);

            Assert.Equal(initialBalance - value, _context.Accounts.Find((long)1).Balance);
        }

        [Fact]
        public void Statement_Test()
        {
            var account = _context.Accounts.Find((long)1);
            decimal value = 5;

            _ = _BankDAL.Deposit(account.Id, value);
            _ = _BankDAL.Withdrawal(account.Id, value);
            var statement = _BankDAL.GetStatement(account.Id).Result.ToList();

            Assert.Equal(2, statement.Count());
        }


    }
}