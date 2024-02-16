using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Integrated_Karting_Experience
{
    internal class Database
    {
        public static void writeToDatabase()
        {
            if (MDIParent.statusDatabase == MDIParent.SIMULATE_CONNECTION_STATUS)
            {
                return;
            }
        }
    }
}
