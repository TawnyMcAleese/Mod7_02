Exercises 2: Banking Transaction Debugger
Debug a banking application that processes transactions and find/fix the bugs using Visual Studio's debugging tools.

Requirements:
Use breakpoints to:

Track balance changes
Verify transaction processing
Check for null transactions
Use the Watch window to:

Monitor the balance variable
Track transaction list contents
Verify calculations
Find and fix:

Null reference potential
Balance calculation error
Missing validation
Logic errors in transaction processing
Starter Code:
public class BankAccount
{
    private decimal balance;
    private string accountNumber;
    private List transactions;

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        this.accountNumber = accountNumber;
        balance = initialBalance;
        transactions = new List();
    }

    public void ProcessTransaction(Transaction transaction)
    {
        // Bug 1: Missing null check
        transactions.Add(transaction);

        if (transaction.Type == TransactionType.Deposit)
        {
            balance += transaction.Amount;
        }
        else if (transaction.Type == TransactionType.Withdrawal)
        {
            // Bug 2: Missing insufficient funds check
            balance -= transaction.Amount;
        }
    }

    public decimal GetBalance()
    {
        // Bug 3: Calculation error in balance verification
        decimal calculatedBalance = 0;
        foreach (var transaction in transactions)
        {
            if (transaction.Type == TransactionType.Deposit)
                calculatedBalance += transaction.Amount;
            else if (transaction.Type == TransactionType.Withdrawal)
                calculatedBalance += transaction.Amount; // Bug: Should be subtraction
        }
        return calculatedBalance;
    }
}

public class Transaction
{
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}

public enum TransactionType
{
    Deposit,
    Withdrawal
}  
Example Testing Scenarios:
// Test code
var account = new BankAccount("1234", 1000);

// Test various scenarios
account.ProcessTransaction(new Transaction { Amount = 500, Type = TransactionType.Deposit });
account.ProcessTransaction(new Transaction { Amount = 200, Type = TransactionType.Withdrawal });
account.ProcessTransaction(null); // Should handle this
account.ProcessTransaction(new Transaction { Amount = 2000, Type = TransactionType.Withdrawal }); // Should check funds  
