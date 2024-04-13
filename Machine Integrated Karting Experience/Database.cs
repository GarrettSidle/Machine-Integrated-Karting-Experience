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
            if (MDIParent.statusDatabase == MDIParent.ConnectionStatus.Simulated)
            {
                return;
            }
            //TODO finalize Database
            //TODO Dynamically add connection string to config
            //TODO Recording SQL 

            //TODO CRUD SQL statement
            //TODO add cancel flag logic

        }
    }
}
