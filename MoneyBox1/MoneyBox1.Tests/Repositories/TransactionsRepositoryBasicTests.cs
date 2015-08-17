using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBox1.Interfaces;
using MoneyBox1.Models;
using MoneyBox1.Repositories;

namespace MoneyBox1.Tests.Repositories
{
    [TestClass]
    public class TransactionsRepositoryBasicTests
    {
        private ITransactionsRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new TransactionsRepositoryBasic();
        }

        [TestCategory("Repository - Get"), TestMethod]
        public void GetReturnsListOfTransactions()
        {
            // Arrange

            // Act
            var transactions = repository.Get();

            // Assert
            Assert.IsNotNull(transactions);
            Assert.AreEqual(2, transactions.Count);
        }

        [TestCategory("Repository - GetById"), TestMethod]
        public void GetByIdWithValidIdReturnsTransaction()
        {
            // Arrange
            long transactionId = 1;

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
            Assert.IsNotNull(deletedTransaction);
            Assert.AreEqual(transactionId, deletedTransaction.TransactionId);

            var transaction = repository.GetById(deletedTransaction.TransactionId);
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

        [TestCategory("Repository - Update"), TestMethod]
        public void UpdateValidTransactionReturnsTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                TransactionId = 2,
                TransactionDate = new DateTime(2015, 9, 3),
                Description = "Second Transaction Updated",
                TransactionAmount = 17.99M,
                CurrencyCode = "USD",
                Merchant = "Merchant Updated"
            };

            // Act
            var updatedTransaction = repository.Update(transaction);

            // Assert
            Assert.IsNotNull(updatedTransaction);
        }

        [TestCategory("Repository - Update"), TestMethod]
        public void UpdateInValidTransactionReturnsNull()
        {
            // Arrange
            var transaction = new Transaction
            {
                TransactionId = 42,
                Description = "Invalid Transaction",
            };

            // Act
            var updatedTransaction = repository.Update(transaction);

            // Assert
            Assert.IsNull(updatedTransaction);
        }
    }
}
