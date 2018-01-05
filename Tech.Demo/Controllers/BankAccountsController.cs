using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.Application.MainBoundedContext.BankingModule.Services;
using NLayerApp.Application.MainBoundedContext.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NLayerApp.DistributedServices.MainBoundedContext.Controllers
{
    [Route("api/[controller]")]
    public class BankAccountsController : Controller, IDisposable
    {
        readonly IBankAppService _bankAppService;

        public BankAccountsController(IBankAppService bankAppService)
        {
            _bankAppService = bankAppService;
        }

        // GET: api/bankaccounts
        [HttpGet("[action]")]
        public IEnumerable<BankAccountDTO> Get()
        {
            return _bankAppService.FindBankAccounts();
        }

        // GET api/bankaccounts/getbankaccount/5
        [HttpGet("GetBankAccount/{id}")]
        public BankAccountDTO GetBankAccount(Guid bankAccountId)
        {
            return _bankAppService.FindBankAccount(bankAccountId);
        }

        // GET api/bankaccounts/getactivities/5
        [HttpGet("GetActivities/{id}")]
        public IEnumerable<BankActivityDTO> Get(Guid bankAccountId)
        {
            return _bankAppService.FindBankAccountActivities(bankAccountId);
        }

        // POST api/bankaccounts
        [HttpPost]
        public BankAccountDTO Post([FromBody]BankAccountDTO newBankAccount)
        {
            return _bankAppService.AddBankAccount(newBankAccount);
        }

        // PUT api/bankaccounts
        [HttpPut("[action]")]
        public BankAccountDTO Put([FromBody]BankAccountDTO newBankAccount)
        {
            return _bankAppService.UpdateBankAccount(newBankAccount);
        }

        // PUT api/bankaccounts/lock/5
        [HttpPut("{id}")]
        public bool Lock(Guid bankAccountId)
        {
            return _bankAppService.LockBankAccount(bankAccountId);
        }

        // PUT api/bankaccounts/performtransfer
        [HttpPut]
        public void PerformTransfer([FromBody]BankAccountDTO from, [FromBody]BankAccountDTO to, [FromBody]decimal amount)
        {
            _bankAppService.PerformBankTransfer(from, to, amount);
        }

        #region IDisposable Members
        public void Dispose()
        {
            //dispose all resources
            _bankAppService.Dispose();
        }
        #endregion
    }
}
