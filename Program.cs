using System;
using System.Collections.Generic;
using System.Text;

class BankAccount
{
    public string AccountHolder { get; set; }
    public double Balance { get; private set; }
    public List<string> TransactionHistory { get; private set; } = new List<string>();

    public BankAccount(string accountHolder, double initialBalance)
    {
        AccountHolder = accountHolder;
        Balance = initialBalance;
    }

    // Deposit money into account
    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive.");
            return;
        }
        Balance += amount;
        TransactionHistory.Add($"Deposited: PKR {amount}, New balance: PKR {Balance}");
        Console.WriteLine($"Deposited PKR {amount}. New balance: PKR {Balance}");
    }

    // Withdraw money from account
    public void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal amount must be positive.");
            return;
        }
        if (Balance < amount)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }
        Balance -= amount;
        TransactionHistory.Add($"Withdrew: PKR {amount}, New balance: PKR {Balance}");
        Console.WriteLine($"Withdrew PKR {amount}. New balance: PKR {Balance}");
    }

    // Display current balance
    public void DisplayBalance()
    {
        Console.WriteLine($"Account balance: PKR {Balance}");
    }

    // Display transaction history (bank statement)
    public void DisplayStatement()
    {
        Console.WriteLine("\nBank Statement for " + AccountHolder + ":");
        Console.WriteLine("========================================");
        foreach (var transaction in TransactionHistory)
        {
            Console.WriteLine(transaction);
        }
        Console.WriteLine("========================================");
    }
}

class Program
{
    // Tax calculation based on Pakistan's rules: 10% tax if income > 25000
    static double CalculateTax(double income)
    {
        const double minimumWage = 25000;
        double tax = 0;
        if (income > minimumWage)
        {
            tax = income * 0.10; // 10% tax on income above 25000
        }
        return tax;
    }

    // Zakat calculation (2.5% if wealth is above Nisab)
    static double CalculateZakat(double wealth)
    {
        const double nisab = 50000;  // Threshold value for Nisab
        if (wealth >= nisab)
        {
            return wealth * 0.025; // 2.5% Zakat
        }
        return 0;
    }

    // Interest calculation (simple interest)
    static double CalculateInterest(double principal, double rate, int time)
    {
        return (principal * rate * time) / 100;
    }

    // Generate a random authentication code (simulating QR code behavior)
    static string GenerateAuthCode()
    {
        Random rand = new Random();
        StringBuilder authCode = new StringBuilder();

        // Generate a random 6-digit code (as an example)
        for (int i = 0; i < 6; i++)
        {
            authCode.Append(rand.Next(0, 10));
        }

        return authCode.ToString();
    }

    // Utility bill payment (mocked for simplicity)
    static void PayUtilityBill()
    {
        Console.WriteLine("\nUtility Bill Payment:");
        Console.WriteLine("1. IESCO - Electricity");
        Console.WriteLine("2. SNGPL - Gas");
        Console.WriteLine("3. PTCL - Internet");
        Console.Write("Choose the utility service (1/2/3): ");
        string billChoice = Console.ReadLine();

        string billType = "";
        if (billChoice == "1")
        {
            billType = "IESCO - Electricity";
        }
        else if (billChoice == "2")
        {
            billType = "SNGPL - Gas";
        }
        else if (billChoice == "3")
        {
            billType = "PTCL - Internet";
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        Console.Write("Enter amount to pay: PKR ");
        double amount = double.Parse(Console.ReadLine());
        Console.WriteLine($"Payment of PKR {amount} for {billType} has been made.");

        // Generate a receipt for the payment
        Console.WriteLine("\n-- Receipt --");
        Console.WriteLine($"Bill Type: {billType}");
        Console.WriteLine($"Amount Paid: PKR {amount}");
        Console.WriteLine($"Payment Date: {DateTime.Now}");
        Console.WriteLine("----------------\n");
    }

    static void Main(string[] args)
    {
        // Create a list to store bank accounts
        List<BankAccount> accounts = new List<BankAccount>();

        // Main menu loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("     Ali Banking System         ");
            Console.WriteLine("=================================");
            Console.WriteLine("        Welcome to Ali Bank!     ");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Create a new account");
            Console.WriteLine("2. Access existing account");
            Console.WriteLine("3. Tax Calculation");
            Console.WriteLine("4. Interest Rate Calculation");
            Console.WriteLine("5. Zakat Calculator");
            Console.WriteLine("6. Customer Support");
            Console.WriteLine("7. Authentication via Code");
            Console.WriteLine("8. Utility Bills Payment");
            Console.WriteLine("9. Bank Statement");
            Console.WriteLine("10. Exit");
            Console.Write("Choose an option (1-10): ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Create a new account
                Console.Write("Enter account holder name: ");
                string name = Console.ReadLine();
                Console.Write("Enter initial deposit amount in PKR: ");
                double deposit = double.Parse(Console.ReadLine());
                BankAccount newAccount = new BankAccount(name, deposit);
                accounts.Add(newAccount);
                Console.WriteLine($"Account created for {name} with PKR {deposit} initial balance.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                // Access an existing account
                Console.Write("Enter account holder name: ");
                string name = Console.ReadLine();
                BankAccount account = accounts.Find(a => a.AccountHolder == name);

                if (account != null)
                {
                    // Account found, display options
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Welcome, {account.AccountHolder}.");
                        Console.WriteLine("1. View balance");
                        Console.WriteLine("2. Deposit");
                        Console.WriteLine("3. Withdraw");
                        Console.WriteLine("4. Go back");
                        Console.Write("Choose an option (1/2/3/4): ");
                        string action = Console.ReadLine();

                        if (action == "1")
                        {
                            account.DisplayBalance();
                        }
                        else if (action == "2")
                        {
                            Console.Write("Enter deposit amount in PKR: ");
                            double depositAmount = double.Parse(Console.ReadLine());
                            account.Deposit(depositAmount);
                        }
                        else if (action == "3")
                        {
                            Console.Write("Enter withdrawal amount in PKR: ");
                            double withdrawAmount = double.Parse(Console.ReadLine());
                            account.Withdraw(withdrawAmount);
                        }
                        else if (action == "4")
                        {
                            break; // Go back to the main menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else if (choice == "3")
            {
                // Tax calculation
                Console.Write("Enter your annual income in PKR: ");
                double income = double.Parse(Console.ReadLine());
                double tax = CalculateTax(income);
                Console.WriteLine($"The calculated tax for an income of PKR {income} is: PKR {tax}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "4")
            {
                // Interest calculation
                Console.Write("Enter principal amount in PKR: ");
                double principal = double.Parse(Console.ReadLine());
                Console.Write("Enter interest rate (as a percentage): ");
                double rate = double.Parse(Console.ReadLine());
                Console.Write("Enter time period (in years): ");
                int time = int.Parse(Console.ReadLine());

                double interest = CalculateInterest(principal, rate, time);
                Console.WriteLine($"The calculated interest for PKR {principal} at {rate}% for {time} years is: PKR {interest}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "5")
            {
                // Zakat calculator
                Console.Write("Enter your wealth in PKR: ");
                double wealth = double.Parse(Console.ReadLine());
                double zakat = CalculateZakat(wealth);
                Console.WriteLine($"The calculated Zakat for PKR {wealth} is: PKR {zakat}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "6")
            {
                // Customer Support
                Console.WriteLine("\nCustomer Support:");
                Console.WriteLine("For assistance, please contact:");
                Console.WriteLine("Phone: 123-456-7890");
                Console.WriteLine("Email: support@alibank.com");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "7")
            {
                // Authentication via Code (simulating QR code)
                Console.WriteLine("\nAuthenticating with your code...");

                // Generate an authentication code (simulating QR code behavior)
                string authCode = GenerateAuthCode();
                Console.WriteLine($"Your authentication code: {authCode}");

                // Simulate user scanning (entering the code)
                Console.Write("\nEnter the authentication code to proceed: ");
                string enteredCode = Console.ReadLine();

                if (enteredCode == authCode)
                {
                    Console.WriteLine("Authentication successful!");
                }
                else
                {
                    Console.WriteLine("Authentication failed. Invalid code.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "8")
            {
                // Utility bills payment
                PayUtilityBill();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "9")
            {
                // Bank statement
                Console.Write("Enter account holder name to generate bank statement: ");
                string name = Console.ReadLine();
                BankAccount account = accounts.Find(a => a.AccountHolder == name);
                if (account != null)
                {
                    account.DisplayStatement();
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "10")
            {
                // Exit the program
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
