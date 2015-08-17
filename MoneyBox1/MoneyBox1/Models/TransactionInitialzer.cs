using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyBox1.Models
{
    public class TransactionInitialzer : System.Data.Entity.DropCreateDatabaseAlways<TransactionContext>
    {
        protected override void Seed(TransactionContext context)
        {
            var transactions = new List<Transaction>
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

            transactions.ForEach(s => context.Transactions.Add(s));
            context.SaveChanges();

        }

    }
}