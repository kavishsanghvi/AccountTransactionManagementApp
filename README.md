# Account and Transaction Management Application

## Project Overview

This C# application is designed to manage account and transaction data efficiently. Utilizing a relational database, the application implements the core functionalities required to create, read, update, and delete transactions while ensuring accurate account balance management. The application leverages Entity Framework Core for all database operations and includes business logic validation, unit testing for CRUD operations, and the ability to export data to a human-readable Excel format.

## Requirements

- .NET 6 or higher
- Entity Framework Core
- A local relational database (e.g., SQL Server, SQLite)
- JSON file containing initial account and transaction data

## Data Structure

The application manages the following data structures:

### Accounts
- **Identifier**: Unique account identifier
- **Name**: Account holder's name
- **Number**: Account number
- **Current Balance**: Current balance of the account
- **Overdraft Limit**: Maximum overdraft allowed on the account

### Transactions
- **Identifier**: Unique transaction identifier
- **Description**: Description of the transaction
- **Debit/Credit Flag**: Indicates whether the transaction is a debit or credit
- **Amount**: Amount involved in the transaction
- **Account Identifier**: Links the transaction to its corresponding account

## Functional Requirements

1. **Entity Framework Core**:
   - Utilize Entity Framework Core for all database operations.
   - Set up a local relational database to store account and transaction data.
   - Implement migrations for proper database schema management.

2. **CRUD Operations for Transactions**:
   - **Create**: Add new transactions for an account.
   - **Read**: Display transactions for a specific account.
   - **Update**: Modify existing transactions.
   - **Delete**: Remove transactions.
   - Ensure that each operation updates the current balance of the respective account and maintains database consistency.
   - Validate overdraft limits to prevent transactions that exceed the account's balance minus the overdraft limit.

3. **Unit Testing**:
   - Develop unit tests for all CRUD operations to ensure correct functionality.
   - Validate business logic to confirm that account balances are updated accurately with each transaction.
   - Handle error scenarios, such as attempting to delete non-existent transactions or adding invalid entries.

4. **Data Seeding**:
   - Read initial data from a provided JSON file into the database on the first run.

5. **Export to Excel**:
   - Enable exporting the current state of accounts and transactions to a human-readable Excel file.

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/account-transaction-management.git
   cd account-transaction-management
   ```

2. Restore the project dependencies:
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json` to point to your local database.

4. Apply the migrations to set up the database:
   ```bash
   dotnet ef database update
   ```

5. Seed the database with initial data from the JSON file:
   - Place the JSON file in the appropriate directory and ensure it’s referenced in the code.

6. Run the application:
   ```bash
   dotnet run
   ```

## Usage

Once the application is running, you can use the API endpoints to manage accounts and transactions. Refer to the API documentation for detailed instructions on available operations.

## Unit Testing

To run the unit tests, execute the following command:
```bash
dotnet test
```

Ensure that all tests pass successfully to validate the functionality and business logic of the application.

## Exporting Data to Excel

The application provides functionality to export the current state of accounts and transactions to an Excel file. This feature is accessible via the application interface and allows users to download the data in a human-readable format.

## Contact
<a href="mailto:sanghvi_kavish@yahoo.in">Email</a> | <a href="https://www.linkedin.com/in/kavishsanghvi">LinkedIn</a> | <a href="https://www.medium.com/@kavishsanghvi">Medium</a> | <a href="https://kavishsanghviblog.wordpress.com">Blog</a> | <a href="https://twitter.com/kavishsanghvi25">Twitter</a> | <a href="https://www.facebook.com/kavish.sanghvi.5">Facebook</a> | <a href="https://www.instagram.com/kavishsanghvi96">Instagram</a>