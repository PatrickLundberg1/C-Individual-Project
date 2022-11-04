using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking
{
    internal class TrackLog
    {
        public TrackLog()
        {
            // Placeholder empty list, replace with Load when implemented
            trans_list = new List<Transaction>();
        }

        private void Load()
        {
            // To Be Implemented
        }

        public void Save()
        {
            // To Be Implemented
        }

        public void AddTransaction()
        {
            string input;
            while (true)
            {
                Console.Write("Please enter E for (E)xpense or I for (I)ncome: ");
                input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input == "e" || input == "i")
                {
                    string title;
                    int amount;
                    int month;

                    while (true)
                    {
                        Console.Write("Enter a unique title: ");
                        title = (Console.ReadLine() ?? "").Trim().ToLower();

                        if (title.Length == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Title can not be empty");
                            Console.ResetColor();
                        }
                        else if(trans_list.FindIndex(t => t.Title == title) != -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Title has already been used");
                            Console.ResetColor();
                        }
                        else
                        {
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter an amount: ");
                        if (!int.TryParse((Console.ReadLine() ?? ""), out amount))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Not a number, try again");
                            Console.ResetColor();
                        }
                        else if (amount < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The amount must be positive");
                            Console.ResetColor();
                        }
                        else
                        {
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter a valid month: ");
                        if (!int.TryParse((Console.ReadLine() ?? ""), out month))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Not a number, try again");
                            Console.ResetColor();
                        }
                        else if (month < 1 || month > 12)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The month must be in the range 1-12");
                            Console.ResetColor();
                        }
                        else
                        {
                            break;
                        }
                    }

                    // input checks done
                    if(input == "e")
                    {
                        trans_list.Add(new Expense(title, amount, month));
                    }
                    else
                    {
                        trans_list.Add(new Income(title, amount, month));
                    }

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not understood, please try again");
                    Console.ResetColor();
                }
            }
        }

        public void EditTransaction()
        {
            Console.Write("Please enter the title of the item to edit: ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();
            int item_index = trans_list.FindIndex(t => t.Title == input);

            if (item_index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Item not found, try using option (1)");
                Console.ResetColor();
            }
            else
            {
                trans_list[item_index].Edit();
            }
        }

        public void RemoveTransaction()
        {
            Console.Write("Please enter the title of the item to remove: ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();
            int item_index = trans_list.FindIndex(t => t.Title == input);

            if (item_index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Item not found, try using option (1)");
                Console.ResetColor();
            }
            else
            {
                trans_list.RemoveAt(item_index);
            }
        }

        public void Display()
        {
            string input_select;
            string input_by;
            string input_order;

            while (true)
            {
                Console.Write("Please enter A, E or I to show (A)ll/(E)xpenses/(I)ncomes: ");
                input_select = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input_select!="a" && input_select!="e" && input_select!="i")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not understood, try again");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Please enter T, M or A to order by (T)itle/(M)onth/(A)mount: ");
                input_by = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input_by != "t" && input_by != "m" && input_by != "a")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not understood, try again");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }

            List<Transaction> ordered_list = trans_list.OrderBy(t => t.Title).ToList();

            Console.WriteLine("--------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Title".PadRight(20) + "Month".PadRight(20) + "Amount");
            Console.ResetColor();

            foreach (Transaction t in ordered_list)
            {
                Console.WriteLine(t.Title.PadRight(20) + t.Month.ToString().PadRight(20) + t.Amount);
            }
            Console.WriteLine("--------------------------------------------------");
        }

        /**
         * Returns the sum of the value of all transactions stored
         */
        public int GetSum()
        {
            int total_sum = 0;

            foreach(Transaction t in trans_list)
            {
                total_sum += t.GetValue();
            }

            return total_sum;
        }

        private List<Transaction> trans_list;
    }
}
