using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking
{
    internal class Income : Transaction
    {
        public Income(string title, int amount, int month) :base(title, amount, month)
        { }

        public override int GetValue()
        {
            return Amount;
        }

        public override bool IsExpense()
        {
            return false;
        }
    }
}
