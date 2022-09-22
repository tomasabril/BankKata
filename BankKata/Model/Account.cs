using bank_kata.Model;
using Microsoft.AspNetCore.Mvc;

namespace bank_kata.Model
{
    public class Account
    {
        public long Id { get; set; }
        public decimal Balance { get; set; }

        public Account(decimal balance)
        {
            Balance = balance;
        }

        public bool Deposit(decimal Amount)
        {
            Balance += Amount;
            return true;
        }
        public bool Withdrawal(decimal Amount)
        {
            Balance -= Amount;
            return true;
        }
    }

}