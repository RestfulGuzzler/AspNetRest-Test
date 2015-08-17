﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyBox1.Interfaces;
using MoneyBox1.Models;
using System.Data;

namespace MoneyBox1.Repositories
{
    public class TransactionsRepositoryEF : ITransactionsRepository
    {
        private List<Transaction> transactions = new List<Transaction>
            {
                new Transaction{
                            TransactionId = 1,
                            TransactionDate = new DateTime(2015, 9, 1),
                            Description = "First Transaction",
                            TransactionAmount = 5.50M,
                            CreatedDate = new DateTime(2015, 8, 14),
                            ModifiedDate = new DateTime(2015, 8, 14),
                            CurrencyCode = "GBP",
                            Merchant = "Merchant1"
                            },

                new Transaction{
                            TransactionId = 2,
                            TransactionDate = new DateTime(2015, 9, 2),
                            Description = "Second Transaction",
                            TransactionAmount = 10.99M,
                            CreatedDate = new DateTime(2015, 8, 15),
                            ModifiedDate = new DateTime(2015, 8, 15),
                            CurrencyCode = "GBP",
                            Merchant = "Merchant2"
                            },
            };

        public List<Transaction> Get()
        {
            using (var db = new TransactionContext())
            {
                var query = from t in db.Transactions
                            orderby t.TransactionDate
                            select t;
                return query.ToList();
            }
        }

        public Transaction GetById(long id)
        {
            using (var db = new TransactionContext())
            {
                var query = from t in db.Transactions
                            where t.TransactionId == id
                            select t;
                Transaction transaction = query.FirstOrDefault();
                return transaction;
            }
        }

        /// <summary>
        /// Validates a Transaction.
        /// TransactionDate must be valid and in the furture.
        /// TransactionAmount must be greater than zero.
        /// CurrencyCode must not be empty.
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns>True if valid and False if invalid.</returns>
        private bool IsValid(Transaction transaction)
        {
            return transaction == null
                    || transaction.TransactionDate < DateTime.Now
                    || transaction.TransactionAmount == 0.0M
                    || String.IsNullOrEmpty(transaction.CurrencyCode) ? false : true;
        }

        public Transaction Create(Transaction transaction)
        {
            if (!IsValid(transaction)) return null;

            var dateTimeNow = DateTime.Now;

            var newTransaction = new Transaction()
            {
                TransactionId = transactions.Max(t => t.TransactionId) + 1,
                CreatedDate = dateTimeNow,
                ModifiedDate = dateTimeNow,
                TransactionDate = transaction.TransactionDate,
                TransactionAmount = transaction.TransactionAmount,
                CurrencyCode = transaction.CurrencyCode,
                Description = transaction.Description,
                Merchant = transaction.Merchant
            };

            using (var db = new TransactionContext())
            {
                db.Transactions.Add(newTransaction);
                db.SaveChanges();
            }

            return newTransaction;
        }

        public Transaction Delete(long id)
        {
            var transactionToDelete = GetById(id);
            if (transactionToDelete != null)
            {
                using (var db = new TransactionContext())
                {
                    db.Transactions.Attach(transactionToDelete);
                    db.Transactions.Remove(transactionToDelete);
                    db.SaveChanges();
                }
            }
            return transactionToDelete;
        }

        public Transaction Update(Transaction transaction)
        {
            if (!IsValid(transaction)) return null;

            var dateTimeNow = DateTime.Now;

            var transactionToUpdate = GetById(transaction.TransactionId);
            if (transactionToUpdate != null)
            {
                transactionToUpdate.TransactionDate = transaction.TransactionDate;
                transactionToUpdate.TransactionAmount = transaction.TransactionAmount;
                transactionToUpdate.CurrencyCode = transaction.CurrencyCode;
                transactionToUpdate.Description = transaction.Description;
                transactionToUpdate.Merchant = transaction.Merchant;
                transactionToUpdate.ModifiedDate = dateTimeNow;
            }

            using (var db = new TransactionContext())
            {
                db.Transactions.Attach(transactionToUpdate);
                db.Entry(transactionToUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }
            return transactionToUpdate;
        }

    }
}