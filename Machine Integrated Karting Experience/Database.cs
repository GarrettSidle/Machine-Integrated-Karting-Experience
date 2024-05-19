using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

using System.Threading;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace Machine_Integrated_Karting_Experience
{
    internal class Database
    {

        public enum Tables
        {
            Sets,
            Runs,
            Frames,
            Points
        }

        private static NpgsqlConnection connection;

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

        public static void initializeConnection()
        {


            string connectionString = "Host=localhost; Port = 5432; Database = MIKE; User Id = postgres; Password = postgres; ";

            connection = new NpgsqlConnection(connectionString);
            connection.Open();



        }

        public static List<string> Query(string cmdTxt)
        {
            List<string> output = new List<string>();

            NpgsqlCommand cmd = new NpgsqlCommand(cmdTxt, connection);

            NpgsqlDataReader reader = cmd.ExecuteReader();
            int i = 0;

            while (reader.Read())
            {
                output[i] = reader.GetString(0);
                Console.WriteLine(reader.GetString(0));
                i++;
            }
            reader.Close();

            return output;
        }

        public static void close()
        {
            connection.Close();
        }

    }
}
