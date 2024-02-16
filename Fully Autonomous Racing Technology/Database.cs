using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fully_Autonomous_Racing_Technology
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
