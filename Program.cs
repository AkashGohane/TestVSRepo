using System;
using System.Collections.Generic;

class BankAccount
{
    public int AccountNumber { get; private set; }
    public string AccountHolder { get; private set; }
    public decimal Balance { get; private set; }

    public BankAccount(int accountNumber, string accountHolder, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive.");
            return;
        }
        Balance += amount;
        Console.WriteLine($"Successfully deposited {amount:C}. New balance is {Balance:C}.");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be positive.");
            return;
        }
        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
            return;
        }
        Balance -= amount;
        Console.WriteLine($"Successfully withdrew {amount:C}. New balance is {Balance:C}.");
    }

    public void DisplayAccountInfo()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Account Holder: {AccountHolder}");
        Console.WriteLine($"Balance: {Balance:C}");
    }
}

class Program
{
    static List<BankAccount> accounts = new List<BankAccount>();
    static int nextAccountNumber = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nBank Account Management System");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Check Balance");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Deposit();
                    break;
                case "3":
                    Withdraw();
                    break;
                case "4":
                    DisplayAccountInfo();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateAccount()
    {
        Console.Write("Enter account holder name: ");
        string accountHolder = Console.ReadLine();
        Console.Write("Enter initial balance: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
        {
            BankAccount account = new BankAccount(nextAccountNumber++, accountHolder, initialBalance);
            accounts.Add(account);
            Console.WriteLine("Account created successfully.");
        }
        else
        {
            Console.WriteLine("Invalid balance. Account creation failed.");
        }
    }

    static void Deposit()
    {
        BankAccount account = GetAccount();
        if (account == null) return;

        Console.Write("Enter amount to deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            account.Deposit(amount);
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    static void Withdraw()
    {
        BankAccount account = GetAccount();
        if (account == null) return;

        Console.Write("Enter amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            account.Withdraw(amount);
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    static void DisplayAccountInfo()
    {
        BankAccount account = GetAccount();
        if (account != null)
        {
            account.DisplayAccountInfo();
        }
    }

    static BankAccount GetAccount()
    {
        Console.Write("Enter account number: ");
        if (int.TryParse(Console.ReadLine(), out int accountNumber))
        {
            foreach (var account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            Console.WriteLine("Account not found.");
        }
        else
        {
            Console.WriteLine("Invalid account number.");
        }
        return null;
    }
}

