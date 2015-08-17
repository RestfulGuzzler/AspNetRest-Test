using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBox1;
using MoneyBox1.Interfaces;
using MoneyBox1.Models;
using MoneyBox1.Controllers;
using Moq;
using System.Net;

namespace MoneyBox1.Tests.Controllers
{
    [TestClass]
    public class TransactionsControllerTest
    {
        private Mock<ITransactionsRepository> mockTransactionsRepository;

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

        [TestInitialize]
        public void Initialize()
        {
            mockTransactionsRepository = new Mock<ITransactionsRepository>();
        }

        [TestCategory("TransactionsController - Get"), TestMethod]
        public void GetReturnsListOfTransactions()
        {
            // Arrange
            mockTransactionsRepository.Setup(t => t.Get()).Returns(transactions);
            TransactionsController controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Transaction>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count());
        }

        [TestCategory("TransactionsController - Get"), TestMethod]
        public void GetEmptyListReturnsNotFound()
        {
            // Arrange
            TransactionsController controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Transaction>>;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestCategory("TransactionsController - GetById"), TestMethod]
        public void GetByIdWithValidIdReturnsTransaction()
        {
            // Arrange
            long validTransactionId = 1;
            mockTransactionsRepository.Setup(x => x.GetById(validTransactionId)).Returns(new Transaction { TransactionId = validTransactionId });

            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Get(validTransactionId);
            var contentResult = actionResult as OkNegotiatedContentResult<Transaction>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(validTransactionId, contentResult.Content.TransactionId);
        }

        [TestCategory("TransactionsController - GetById"), TestMethod]
        public void GetByIdWithInValidIdReturnsNotFound()
        {
            // Arrange
            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Get(42);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestCategory("TransactionsController - Post"), TestMethod]
        public void PostValidTransactionReturnsCreated()
        {
            // Arrange
            var newTransaction = new Transaction()
            {
                Merchant = "PostTest Merchant",
                Description = "PostTest Valid Transaction",
                TransactionDate = new DateTime(2015, 9, 1),
                TransactionAmount = 4.44M,
                CurrencyCode = "GBP"
            };

            mockTransactionsRepository.Setup(x => x.Create(newTransaction)).Returns(newTransaction);

            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Post(newTransaction);
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<Transaction>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult, typeof(CreatedAtRouteNegotiatedContentResult<Transaction>));
        }

        [TestCategory("TransactionsController - Post"), TestMethod]
        public void PostInValidTransactionReturnsBadRequest()
        {
            // Arrange
            var newTransaction = new Transaction()
            {
                Merchant = "PostTest UnKnown Merchant",
                Description = "PostTest InValid Transaction",
                TransactionDate = new DateTime(2010, 9, 1),
                TransactionAmount = 0.00M,
                CurrencyCode = ""
            };

            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Post(newTransaction);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestCategory("TransactionsController - Put"), TestMethod]
        public void PutValidTransactionReturnsUpdated()
        {
            // Arrange
            var transactionToUpdate = new Transaction()
            {
                TransactionId = 2,
                TransactionDate = new DateTime(2015, 9, 3),
                Description = "Second Transaction Updated",
                TransactionAmount = 17.99M,
                CurrencyCode = "USD",
                Merchant = "Merchant Updated"
            };

            mockTransactionsRepository.Setup(x => x.GetById(transactionToUpdate.TransactionId)).Returns(transactionToUpdate);               
            mockTransactionsRepository.Setup(x => x.Update(transactionToUpdate)).Returns(transactionToUpdate);

            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Put(transactionToUpdate);
            var contentResult = actionResult as OkNegotiatedContentResult<Transaction>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult, typeof(OkNegotiatedContentResult<Transaction>));
        }

        [TestCategory("TransactionsController - Put"), TestMethod]
        public void PutInValidTransactionReturnsBadRequest()
        {
            // Arrange
            var transactionToUpdate = new Transaction()
            {
                TransactionId = 42,
            };

            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Put(transactionToUpdate);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestCategory("TransactionsController - Delete"), TestMethod]
        public void DeleteValidIdReturnsOk()
        {
            // Arrange
            long transactionId = 1;
            mockTransactionsRepository.Setup(x => x.GetById(transactionId)).Returns(new Transaction { TransactionId = transactionId });
            mockTransactionsRepository.Setup(x => x.Delete(transactionId)).Returns(new Transaction { TransactionId = transactionId });
                        
            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Delete(transactionId);
            var contentResult = actionResult as OkNegotiatedContentResult<Transaction>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(transactionId, contentResult.Content.TransactionId);
        }

        [TestCategory("TransactionsController - Delete"), TestMethod]
        public void DeleteInValidIdReturnsNotFound()
        {
            // Arrange
            long transactionId = 42;
            var controller = new TransactionsController(mockTransactionsRepository.Object);

            // Act
            var actionResult = controller.Delete(transactionId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
