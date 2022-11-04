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
            // To Be Implemented
        }

        public abstract int GetValue();
        public abstract bool IsExpense();
    }
}
