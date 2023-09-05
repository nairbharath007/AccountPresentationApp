using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAppClassLibrary.Service
{
    public class AccountManager
    {
        public const string filePath = @"C:\Users\acer\Desktop\Aurionpro\Training\Classes\Session17\AccountAppClassLibrary\data.txt";
        Account masterAccount;
        public AccountManager() { }
        public string CreateAccount(int accountNo, string name, string bankName, double balance)
        {
            masterAccount = new Account(accountNo, name, bankName, balance);
            DataSerialization.BinarySerializer(filePath, masterAccount);
            return "Accodunt created succesfully";
        }
        public double GetInitialAccountBalance()
        {
            masterAccount = DataSerialization.BinaryDeserializer(filePath);
            if (masterAccount == null)
                return masterAccount.Balance;
            return 0;
        }

        public string Deposit(double amount)
        {
            masterAccount.Balance += amount;
            DataSerialization.BinarySerializer(filePath, masterAccount);
            return "Amount deposited succesfully";

        }

        public string Withdraw(double amount)
        {
            if (masterAccount.Balance - amount >= Account.MIN_BALANCE)
            {
                masterAccount.Balance -= amount;
                DataSerialization.BinarySerializer(filePath, masterAccount);
                return "Amount withdrawn succesfully";

            }
            throw new InsufficientBalanceException($"Minimum Balance of Rs.{Account.MIN_BALANCE} " +
                "should be maintained. Withdraw denied!");
        }
    }
}
