using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Output the current directory to verify where the application is looking for the JSON file
        Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

        // Seed the database from JSON data on first run
        SeedDatabase();

        // Run some CRUD operations for testing
        RunCRUDOperations();

        // Export data to Excel
        ExportToExcel();
    }

    static void SeedDatabase()
    {
        using (var db = new AppDbContext())
        {
            if (!db.Accounts.Any())
            {
                // Use the correct path and check if the file exists
                string filePath = "data.json";
                if (File.Exists(filePath))
                {
                    var jsonData = File.ReadAllText(filePath);
                    Console.WriteLine("Reading JSON Data...");
                    Console.WriteLine(jsonData);  // For testing, print the JSON content

                    var data = JsonConvert.DeserializeObject<Root>(jsonData);

                    // Ensure the JSON data is parsed correctly
                    if (data != null)
                    {
                        db.Accounts.AddRange(data.Accounts);
                        db.Transactions.AddRange(data.Transactions);
                        db.SaveChanges();
                        Console.WriteLine("Database seeded successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to deserialize JSON data.");
                    }
                }
                else
                {
                    Console.WriteLine($"File {filePath} not found.");
                }
            }
            else
            {
                Console.WriteLine("Database already contains account data.");
            }
        }
    }

    static void RunCRUDOperations()
    {
        using (var db = new AppDbContext())
        {
            // Example: Add a new transaction
            var transaction = new Transaction
            {
                description = "Online Purchase",
                debit_credit = "debit",
                amount = 100,
                account_id = 1
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();
            Console.WriteLine("New transaction added.");
        }
    }

    static void ExportToExcel()
    {
        using (var db = new AppDbContext())
        {
            //var accounts = db.Accounts.Include(a => a.Transactions).ToList();
            var accounts = db.Accounts.Join(db.Transactions, account => account.id, 
            transaction => transaction.account_id,  (account, transaction) => new { 
                account_name = account.name, 
                transaction_id = transaction.id, 
                amount = transaction.amount,
                debit_credit = transaction.debit_credit,
                current_balance = account.current_balance,
                description = transaction.description
             }).ToList();
            Console.WriteLine(accounts[0]); // Ensure transactions are loaded

            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Accounts");

                // Define headers for the data
                worksheet.Cell(1, 1).Value = "Account Name";
                worksheet.Cell(1, 2).Value = "Balance";
                worksheet.Cell(1, 3).Value = "Transaction Description";
                worksheet.Cell(1, 4).Value = "Debit/Credit";
                worksheet.Cell(1, 5).Value = "Transaction Amount";

                int row = 2;  // Start writing from the second row

                foreach (var account in accounts)
                {
                    // Write account details and related transactions
                    if (account != null)
                    {
                        //foreach (var transaction in account.Transactions)
                        {
                            worksheet.Cell(row, 1).Value = account.account_name;
                            worksheet.Cell(row, 2).Value = account.current_balance;
                            worksheet.Cell(row, 3).Value = account.description;
                            worksheet.Cell(row, 4).Value = account.debit_credit;
                            worksheet.Cell(row, 5).Value = account.amount;
                            row++;
                        }
                    }
                    else
                    {
                        // If no transactions, just write account data
                        worksheet.Cell(row, 1).Value = account.account_name;
                        worksheet.Cell(row, 2).Value = account.current_balance;
                        worksheet.Cell(row, 3).Value = "No Transactions";
                        row++;
                    }
                }

                // Save the Excel file
                workbook.SaveAs("AccountsAndTransactions.xlsx");
                Console.WriteLine("Data exported to AccountsAndTransactions.xlsx.");
            }
        }
    }
}

public class Root
{
    public List<Account> Accounts { get; set; }
    public List<Transaction> Transactions { get; set; }
}
