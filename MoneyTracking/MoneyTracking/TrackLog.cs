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
            // To Be Implemented
        }

        public void EditTransaction()
        {
            // To Be Implemented
        }

        public void RemoveTransaction()
        {
            // To Be Implemented
        }

        public void Display()
        {
            // To Be Implemented
        }

        /**
         * Returns the sum of the value of all transactions stored
         */
        public int GetSum()
        {
            // To Be Implemented
            return 0;
        }

        private List<Transaction> trans_list;
    }
}
