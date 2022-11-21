using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MoneyTracking
{
    /// <summary>
    /// Class used to represent the Transaction JSON object for the local JSON file.
    /// Simple, easier for the JSON library.
    /// Type should be "INCOME" or "EXPENSE", to establish the type of transaction.
    /// </summary>
    public class Transaction_JSON
    {
        public string Title { get; set; }
        public int Amount { get; set; }
        public int Month { get; set; }
        public string Type { get; set; }
    }
}
