using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking
{
    internal class Income : Transaction
    {
        Income(string title, int amount, int month) :base(title, amount, month)
        { }

        public override int getValue()
        {
            return Amount;
        }

        public override bool isExpense()
        {
            return false;
        }
    }
}
