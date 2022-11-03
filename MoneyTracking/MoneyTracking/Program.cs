
using MoneyTracking;

TrackLog transactions = new TrackLog();

Console.WriteLine("Welcome to TrackMoney");

string input;
while (true)
{
    Console.WriteLine($"You have currently {transactions.GetSum()} kr on your account");
    Console.WriteLine("Pick an option:");
    Console.WriteLine("(1) Show items (All/Expense(s)/Income(s)");
    Console.WriteLine("(2) Add new Expense/Income");
    Console.WriteLine("(3) Edit or Remove item");
    Console.WriteLine("(4) Save and Quit");
    input = (Console.ReadLine() ?? "").Trim();

    if(input == "1")
    {
        transactions.Display();
    }
    else if(input == "2")
    {
        transactions.AddTransaction();
    }
    else if(input == "3")
    {
        while (true)
        {
            Console.Write("Please enter E for (E)dit or R for (R)emove: ");
            input = (Console.ReadLine() ?? "").Trim().ToLower();

            if(input == "e")
            {
                transactions.EditTransaction();
                break;
            }
            else if(input == "r")
            {
                transactions.RemoveTransaction();
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
    else if(input == "4")
    {
        transactions.Save();
        return;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Command not understood, please type in one of the numbers 1-4");
        Console.ResetColor();
    }
}
