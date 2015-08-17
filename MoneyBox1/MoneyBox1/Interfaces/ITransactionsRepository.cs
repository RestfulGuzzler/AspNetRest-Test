using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyBox1.Models;
using System.Web.Http;

namespace MoneyBox1.Interfaces
{
    public interface ITransactionsRepository
    {
        List<Transaction> Get();
        Transaction GetById(long id);
        Transaction Create(Transaction transaction);
        Transaction Update(Transaction transaction);
        Transaction Delete(long id);
    }
}