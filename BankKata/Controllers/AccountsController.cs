using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bank_kata.Model;
using bank_kata.Infrastructure;

namespace bank_kata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private BankDAL _BankDAL;
        public AccountsController(BankContext context)
        {
            _BankDAL = new BankDAL(context);
        }

        [HttpGet()]
        //[Route("accounts")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var account = await _BankDAL.GetAllAccounts();
            return account.ToList();
        }

        // GET: api/AccountStatements/5
        [HttpGet()]
        [Route("statement")]
        public async Task<ActionResult<IEnumerable<Statement>>> GetAccountStatement(long accountId)
        {
            var statement = await _BankDAL.GetStatement(accountId);
            return statement.ToList();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("deposit")]
        public async Task<ActionResult<Account>> Deposit(long accountId, decimal ammount)
        {
            try
            {
                return await _BankDAL.Deposit(accountId, ammount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Account with id {accountId} not found.");
            }

        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("withdrawal")]
        public async Task<ActionResult<Account>> Withdrawal(long accountId, decimal ammount)
        {
            try
            {
                return await _BankDAL.Withdrawal(accountId, ammount);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Account with id {accountId} not found.");
            }

        }

    }
}
