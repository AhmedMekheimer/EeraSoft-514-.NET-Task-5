using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Task_5_Solution
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; private set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public bool Deposit(double amount)
        {
            if (amount < 0)
            {
                return false;
            }
            else
            {
                Balance += amount;
                return true;
            }
        }

        public bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static double operator +(Account lhs, Account rhs)
        {
            return lhs.Balance + rhs.Balance;
        }
    }
    public class SavingsAccount : Account
    {
        public double IntRate { get; private set; }

        public SavingsAccount(string name = "Unnamed Account", double balance = 0.0, double intRate = 0.0) : base(name, balance)
        {
            IntRate = intRate;
        }

        public new bool Withdraw(double amount)
        {
            return base.Withdraw(amount + (amount * IntRate / 100));
        }
    }
    public class CheckingAccount : Account
    {
        public CheckingAccount(string name = "Unnamed Account", double balance = 0.0) : base(name, balance)
        {

        }

        public new bool Withdraw(double amount)
        {
            return base.Withdraw(amount + 1.5);
        }
    }
    public class TrustAccount : SavingsAccount
    {
        private DateTime WithdrawTime;
        private int WithdrawCounter = 3;
        public TrustAccount(string name = "Unnamed Account", double balance = 0.0, double intrate = 0.0) : base(name, balance, intrate)
        {

        }
        public new bool Deposit(double amount)
        {
            if (5000 <= amount)
            {
                Console.WriteLine("+50 Bonus");
                return base.Deposit(amount + 50);
            }
            else
                return base.Deposit(amount);
        }
        public new bool Withdraw(double amount)
        {
            if (WithdrawCounter == 3)
            {
                WithdrawTime = DateTime.Now;
                WithdrawCounter--;
                Console.WriteLine("You have 2 withdrawals left this year");
                return base.Withdraw(amount);
            }
            else if (WithdrawCounter == 1 || WithdrawCounter == 2)
            {
                WithdrawCounter--;
                Console.WriteLine($"You have {WithdrawCounter} withdrawals left this year");
                return base.Withdraw(amount);
            }
            else if (WithdrawTime.Year == DateTime.Now.Year)
            {
                Console.WriteLine("Sorry, you have reached maximum number of withdrawals this year !");
                return false;
            }
            else
            {
                WithdrawTime = DateTime.Now;
                WithdrawCounter = 2;
                Console.WriteLine("You have 2 withdrawals left this year");
                return base.Withdraw(amount);
            }
        }
    }
    internal class Program
    {
        static void Main()
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);



            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.DepositSavings(savAccounts, 1000);
            AccountUtil.WithdrawSavings(savAccounts, 2000);


            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));


            AccountUtil.DepositChecking(checAccounts, 1000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));


            AccountUtil.DepositTrust(trustAccounts, 1000);
            AccountUtil.DepositTrust(trustAccounts, 6000);
            AccountUtil.WithdrawTrust(trustAccounts, 2000);
            AccountUtil.WithdrawTrust(trustAccounts, 3000);
            AccountUtil.WithdrawTrust(trustAccounts, 500);
            AccountUtil.WithdrawTrust(trustAccounts, 500);

            Console.WriteLine();
        }
    }

    public static class AccountUtil
    {
        // Utility helper functions for Account class
        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
            double total = accounts[0] + accounts[1];
            Console.WriteLine($"{accounts[0].Name} + {accounts[1].Name} Balances = {total} ");
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for SavingsAccount
        public static void DepositSavings(List<SavingsAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
            double total = accounts[0] + accounts[1];
            Console.WriteLine($"{accounts[0].Name} + {accounts[1].Name} Balances = {total} ");
        }

        public static void WithdrawSavings(List<SavingsAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for CheckingAccount
        public static void DepositChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
            double total = accounts[0] + accounts[1];
            Console.WriteLine($"{accounts[0].Name} + {accounts[1].Name} Balances = {total} ");
        }

        public static void WithdrawChecking(List<CheckingAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

        // Helper functions for TrustAccount
        public static void DepositTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
            double total = accounts[0] + accounts[1];
            Console.WriteLine($"{accounts[0].Name} + {accounts[1].Name} Balances = {total} ");
        }

        public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }

    }
}
