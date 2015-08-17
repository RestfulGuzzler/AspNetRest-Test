# Create a simple REST API using ASP.NET

### The Brief:

##### Time 0.5 - 2 hours

Create a working RESTful API using ASP.NET, the API you will create will allow the consumer of the API to save, update and get transactions. Auth is considered out of scope for this test.

A transaction is represented as follows:
```cs
        public long TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime CreatedDate { get; set; }        
        public DateTime ModifiedDate { get; set; }
        public string CurrencyCode { get; set; }
        public string Merchant { get; set; }
```
>  **Merchant and Description are optional**

What you can use is pretty open (use whatever frameworks you are comfortable with) but it has to use ASP.NET and meet the following criteria (order of importance).

1. **Your solution must compile and run first time** using Visual Studio 2013/2015, we shouldn't have to add any additional config etc (feel to add any instructions in the readme).

2. The solution should be as simple as possible. More weight is given to the solution with the **least complexity**. 

3. Transactions must be stored - you can use anything you like to achieve this, but consider the importance 1 & 2 in your solution. 

4. The API must have an endpoint to create a new transaction.

5. The API must have an endpoint to update a transaction.

6.  The API must have an endpoint to delete a transaction.

7. The API must have an endpoint to get a transaction.

8. The API must have an endpoint to get a list of all transactions.

9. Your solution should contain tests, again how you do this is left up to you - unit, integration, acceptance etc. You can add more than one test project to demonstrate different test techniques for example integration / unit.

##### How to Submit your test to us:
 - Fork this repository
	- Create your solution in your repository
	- When you're happy, [create a pull request](https://help.github.com/articles/creating-a-pull-request/)
 - Provide a readme which details:
     - a short description of your API what have used, debugging instructions (*don't forget point 1*) any comments you wish to add
     - The time you spent on the project.
     - If you ran out of time, but would have liked to implemented certain features, tell us why

GOOD LUCK :smile:
