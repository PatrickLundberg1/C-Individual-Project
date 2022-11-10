using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking
{
    abstract internal class Transaction
    {
        protected Transaction(string title, int amount, int month)
        {
            Title = title;
            Amount = amount;
            Month = month;
        }

        public string Title { get; private set; }
        public int Amount { get; private set; }
        public int Month { get; private set; }

        public void Edit()
        {
            string input;

            while (true)
            {
                Console.Write("Please enter A or M to edit the (A)mount or (M)onth: ");
                input = (Console.ReadLine() ?? "").Trim().ToLower();

                if(input == "a" || input == "m")
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not understood, try again");
                    Console.ResetColor();
                }
            }

            if(input == "a")
            {
                int new_amount;

                while (true)
                {
                    Console.Write("Enter an amount: ");
                    if (!int.TryParse((Console.ReadLine() ?? ""), out new_amount))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not a number, try again");
                        Console.ResetColor();
                    }
                    else if (new_amount < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The amount must be positive");
                        Console.ResetColor();
                    }
                    else
                    {
                        Amount = new_amount;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Amount edited successfully");
                        Console.ResetColor();
                        return;
                    }
                }
            }
            else if(input == "m")
            {
                int new_month;

                while (true)
                {
                    Console.Write("Enter a valid month: ");
                    if (!int.TryParse((Console.ReadLine() ?? ""), out new_month))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not a number, try again");
                        Console.ResetColor();
                    }
                    else if (new_month < 1 || new_month > 12)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The month must be in the range 1-12");
                        Console.ResetColor();
                    }
                    else
                    {
                        Month = new_month;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Month edited successfully");
                        Console.ResetColor();
                        return;
                    }
                }
            }
        }

        public abstract int GetValue();
        public abstract bool IsExpense();
    }
}
