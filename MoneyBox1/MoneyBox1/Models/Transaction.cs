using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web; 

namespace MoneyBox1.Models
{
    public class Transaction
    {
        /// <summary>
        /// Unique TransactionId
        /// </summary>
        [Key] 
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long TransactionId { get; set; }

        /// <summary>
        /// Transaction Date
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Transaction Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Transaction Amount
        /// </summary>
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Transaction Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Transaction Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Transaction Currency Code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Transaction Merchant
        /// </summary>
        public string Merchant { get; set; }
    }
}