using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking
{
    internal class Expense : Transaction
    {
        public Expense(string title, int amount, int month) : base(title, amount, month)
        { }
        public override int GetValue()
        {
            return Amount * -1;
        }

        public override bool IsExpense()
        {
            return true;
        }
    }
}
