using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBox1.Interfaces;
using MoneyBox1.Models;
using MoneyBox1.Repositories;
using System.Data.Entity;

namespace MoneyBox1.Tests.Repositories
{
    [TestClass]
    public class TransactionsRepositoryEFTests
    {
        private ITransactionsRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new TransactionsRepositoryEF();
        }

        [TestCategory("Repository - Get"), TestMethod]
        public void GetReturnsListOfTransactions()
        {
            // Arrange

            // Act
            var transactions = repository.Get();

            // Assert
            Assert.IsNotNull(transactions);
            Assert.IsTrue(transactions.Count > 0);
        }

        [TestCategory("Repository - GetById"), TestMethod]
        public void GetByIdWithValidIdReturnsTransaction()
        {
            // Arrange
            long transactionId = 2;

            // Act
            var transaction = repository.GetById(transactionId);

            // Assert
            Assert.IsNotNull(transaction);
            Assert.AreEqual(transactionId, transaction.TransactionId);
        }

        [TestCategory("Repository - GetById"), TestMethod]
        public void GetByIdWithInValidIdReturnsNull()
        {
            // Arrange
            long transactionId = 42;

            // Act
            var transaction = repository.GetById(transactionId);

            // Assert
            Assert.IsNull(transaction);
        }

        [TestCategory("Repository - Create"), TestMethod]
        public void CreateValidTransactionReturnsTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                TransactionDate = new DateTime(2025, 9, 3),
                Description = "Third Transaction",
                TransactionAmount = 7.20M,
                CurrencyCode = "GBP",
                Merchant = "Merchant3"
            };

            // Act
            var createdTransaction = repository.Create(transaction);

            // Assert
            Assert.IsNotNull(createdTransaction);            
        }

        [TestCategory("Repository - Create"), TestMethod]
        public void CreateInValidTransactionReturnsNull()
        {
            // Arrange
            var transaction = new Transaction();

            // Act
            var createdTransaction = repository.Create(transaction);

            // Assert
            Assert.IsNull(createdTransaction);
        }

        [TestCategory("Repository - Delete"), TestMethod]
        public void DeleteValidIdDeletesTransaction()
        {
            // Arrange
            long transactionId = 1;

            // Act
            var deletedTransaction = repository.Delete(transactionId);

            // Assert
            var transaction = repository.GetById(transactionId);
            Assert.IsNull(transaction);
        }

        [TestCategory("Repository - Delete"), TestMethod]
        public void DeleteInValidIdReturnsNotFound()
        {
            // Arrange
            long transactionId = 42;

            // Act
            var transaction = repository.Delete(transactionId);

            // Assert
            Assert.IsNull(transaction);
        }

    }
}
