using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyBox1.Interfaces;
using MoneyBox1.Models;
using MoneyBox1.Repositories;

namespace MoneyBox1.Controllers
{
    /// <summary>
    /// Transactions Service
    /// </summary>
    public class TransactionsController : ApiController
    {
        private readonly ITransactionsRepository repository;

        /// <summary>
        /// This default constructor would ordinarily not exist and the ITransactionsRepository would
        /// be resolved and injected into the other constructor that accepts a repository by an IoC
        /// such as Simple Inject, Windsor etc. For Demonstration purposes I will closely couple the respository here.
        /// </summary> 
        public TransactionsController()
        {
            // Note: Read summary please.
            //this.repository = new TransactionsRepositoryBasic();
            this.repository = new TransactionsRepositoryEF();
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="repository"></param>
        public TransactionsController(ITransactionsRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets a list of all Transactions
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Get()
        {
            var transactions = repository.Get();
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        /// <summary>
        /// Gets a Transaction
        /// </summary>
        /// <param name="id">TransactionId</param>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Get(long id)
        {
            var transaction = repository.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        /// <summary>
        /// Creates a new Transaction
        /// </summary>
        /// <param name="transaction">TransactionId</param>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Post(Transaction transaction)
        {
            var newTransaction = repository.Create(transaction);
            if (newTransaction == null)
            {
                return BadRequest();
            }
            else
            {
                var createdAtRoute = CreatedAtRoute("DefaultApi", new { id = newTransaction.TransactionId }, newTransaction);
                return createdAtRoute;
            }
        }

        /// <summary>
        /// Updates an existing Transaction
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Put(Transaction transaction)
        {
            var updatedTransaction = repository.Update(transaction);
            if (updatedTransaction == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedTransaction);
            }
        }    

        /// <summary>
        /// Deletes a Transaction
        /// </summary>
        /// <param name="id">TransactionId</param>
        /// <returns>IHttpActionResult</returns>
        public IHttpActionResult Delete(long id)
        {
            var transaction = repository.Delete(id);
            if (transaction == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(transaction);
            }
        }

    }
}
