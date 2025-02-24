using System;
using System.Collections.Generic;

public class BankAccount
{
    private decimal balance;
    private string accountNumber;
    private List<Transaction> transactions;

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        this.accountNumber = accountNumber;
        balance = initialBalance;
        transactions = new List<Transaction>(); 
    }

    public void ProcessTransaction(Transaction? transaction)
    {

        if (transaction == null)
        {
            Console.WriteLine("Error: Attempted to process a null transaction.");
            return;
        }

        transactions.Add(transaction);

        if (transaction.Type == TransactionType.Deposit)
        {
            balance += transaction.Amount;
        }
        else if (transaction.Type == TransactionType.Withdrawal)
        {
            if (transaction.Amount > balance)
            {
                Console.WriteLine("Error: Insufficient funds for withdrawal.");
                return;
            }

            balance -= transaction.Amount;
        }
    }

    public decimal GetBalance()
    {
        decimal calculatedBalance = 0;

        foreach (var transaction in transactions)
        {
            if (transaction.Type == TransactionType.Deposit)
                calculatedBalance += transaction.Amount;
            else if (transaction.Type == TransactionType.Withdrawal)
                calculatedBalance -= transaction.Amount; 
        }

        return calculatedBalance;
    }

    public void PrintTransactions()
    {
        Console.WriteLine("\nTransaction History:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"{transaction.Date}: {transaction.Type} ${transaction.Amount}");
        }
    }
}

public class Transaction
{
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}

public enum TransactionType
{
    Deposit,
    Withdrawal
}

class Program
{
    static void Main()
    {
        var account = new BankAccount("1234", 1000);

        Console.WriteLine($"Initial Balance: ${account.GetBalance()}");

        account.ProcessTransaction(new Transaction
        {
            Amount = 500,
            Type = TransactionType.Deposit
        });

        Console.WriteLine($"Balance after deposit: ${account.GetBalance()}");

        account.ProcessTransaction(new Transaction
        {
            Amount = 200,
            Type = TransactionType.Withdrawal
        });

        Console.WriteLine($"Balance after withdrawal: ${account.GetBalance()}");

        account.ProcessTransaction(null);

        account.ProcessTransaction(new Transaction
        {
            Amount = 2000,
            Type = TransactionType.Withdrawal
        });

        account.PrintTransactions();

        Console.WriteLine($"\nFinal Balance: ${account.GetBalance()}");
    }
}
