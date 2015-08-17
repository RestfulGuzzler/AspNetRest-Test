# REST API using ASP.NET and EntityFramework

### Time Spent

- 1 day, which included an EF refresher course.

### TransactionsController:

Repository Pattern used with constructor injection for testing purposes.
The default constructor would ordinarily not exist in this controller and the ITransactionsRepository would
be resolved and injected into the other constructor by an IoC such as Simple Inject, Windsor etc.
For Demonstration purposes I have closely coupled the respository here for playing the solution.

#### Repositories

- TransactionsRepositoryEF uses Entity Framework and will be the default. Seeded with two existing transactions.
- TransactionsRepositoryBasic uses a simple List<Transaction> as storage (not persisted).

#### Test Project

- Added Tests of the Controller and Repositories. 

#### Endpoints for /api/transactions

- GET    - endpoint to get a transaction(s)	
- POST   - endpoint to create a new transaction.
- PUT    - endpoint to update a transaction
- DELETE - endpoint to delete a transaction

#### Fiddler example for POST

{
	"TransactionDate" : "11/10/2016 4:00 PM",
	"Description" : "Fiddler Transaction",
	"TransactionAmount" : "10.99",
	"CurrencyCode" : "GBP",
	"Merchant" : "Merchant Fiddler2"
}

#### Refactoring

- Would leverage an IoC such as Simple Inject to loosely couple and inject the ITransactionsRepository into the TransactionsController.

- Would add exception Handling.

- Would add lots more tests for all scenarios.

- Would add more validation of Transaction for POST/Create and PUT/Update

- Would add some client pages to consume each endpoint action.
