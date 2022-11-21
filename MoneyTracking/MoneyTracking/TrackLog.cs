using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyTracking
{
    internal class TrackLog
    {
        public TrackLog()
        {
            trans_list = new List<Transaction>();
            Load();
        }

        
         /// <summary>
         /// Loads the contents of the local JSON file to the transaction list.
         /// If the file doesn't exist, creates the file with an empty list instead.
         /// </summary>
        private void Load()
        {
            string file_path = "TrackMoney_Data.json";
            
            if (File.Exists(file_path))
            {
                // read JSON from file as Transaction_JSON
                string json_data = File.ReadAllText(file_path);
                List<Transaction_JSON> load_list = JsonSerializer.Deserialize<List<Transaction_JSON>>(json_data) ?? new List<Transaction_JSON>();

                foreach(Transaction_JSON tj in load_list)
                {
                    if(tj.Type == "EXPENSE")
                    {
                        trans_list.Add(new Expense(tj.Title, tj.Amount, tj.Month));
                    }
                    else if(tj.Type == "INCOME")
                    {
                        trans_list.Add(new Income(tj.Title, tj.Amount, tj.Month));
                    }
                    else
                    {
                        throw new Exception("An error has occured with the JSON input to the Load function");
                    }
                }
            }
            else
            {
                // create the file and write an empty json list to it
                File.WriteAllText(file_path, "[]");
            }
        }

        /// <summary>
        /// Saves all current transactions to the local JSON file.
        /// This overwrites the contents of that file.
        /// Because of this, the Load function must be used before this function.
        /// </summary>
        public void Save()
        {
            string file_path = "TrackMoney_Data.json";

            try
            {
                List<Transaction_JSON> json_list = new List<Transaction_JSON>();
                foreach(Transaction trans in trans_list) { 
                    Transaction_JSON json_obj = new Transaction_JSON();
                    json_obj.Title = trans.Title;
                    json_obj.Amount = trans.Amount;
                    json_obj.Month = trans.Month;
                    json_obj.Type = trans.IsExpense() ? "EXPENSE" : "INCOME";
                    json_list.Add(json_obj);
                }
                string json_data = JsonSerializer.Serialize(json_list);
                File.WriteAllText(file_path, json_data);
            }
            catch (Exception e)
            {
                // create the file and write an empty json list to it
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error when saving to file. Try restarting the application.");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Reads input from user to add a new transaction.
        /// </summary>
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
                        else if(trans_list.Exists(t => t.Title == title))
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Transaction added successfully");
                    Console.ResetColor();

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

        /// <summary>
        /// Reads transaction title as input from user and calls the edit function for that transaction.
        /// <br></br> For the rest, see: <see cref="Transaction.Edit"/>
        /// </summary>
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

        /// <summary>
        /// Reads input from user and uses it to remove the designated transaction.
        /// </summary>
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Transaction \"{input}\" removed successfully.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Reads input from user and uses the input settings to display a transaction table
        /// </summary>
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

            while (true)
            {
                Console.Write("Please enter A or D for (A)scending or (D)escending order: ");
                input_order = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input_order != "a" && input_order != "d")
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

            List<Transaction> ordered_list = trans_list;
            Func<Transaction, int> order_func = (t) => 0;

            if (input_select == "i")
            {
                ordered_list = ordered_list.Where(t => !t.IsExpense()).ToList();
            }
            else if(input_select == "e")
            {
                ordered_list = ordered_list.Where(t => t.IsExpense()).ToList();
            }

            if (input_by == "t")
            {
                if (input_order == "a")
                {
                    ordered_list = ordered_list.OrderBy(t => t.Title).ToList();
                }
                else if (input_order == "d")
                {
                    ordered_list = ordered_list.OrderByDescending(t => t.Title).ToList();
                }
            }
            else 
            {
                if (input_by == "m")
                {
                    order_func = (t) => t.Month;
                }
                else if (input_by == "a")
                {
                    order_func = (t) => t.Amount;
                }

                if (input_order == "a")
                {
                    ordered_list = ordered_list.OrderBy(order_func).ToList();
                }
                else if (input_order == "d")
                {
                    ordered_list = ordered_list.OrderByDescending(order_func).ToList();
                }
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Title".PadRight(20) + "Month".PadRight(20) + "Amount");
            Console.WriteLine("-----".PadRight(20) + "-----".PadRight(20) + "------");

            foreach (Transaction t in ordered_list)
            {
                Console.WriteLine(t.Title.PadRight(20) + t.Month.ToString().PadRight(20) + t.Amount);
            }
            Console.WriteLine("--------------------------------------------------");
        }

        /// <summary>
        /// Returns the sum of the value of all transactions stored
        /// </summary>
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
