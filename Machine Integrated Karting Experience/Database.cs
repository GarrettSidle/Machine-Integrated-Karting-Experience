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
        public static void checkConnection()
        {
            if (connection == null) 
            {
                initializeConnection();

                Utils.LogWarning("Connection was opened outside of immediate launch");
            }
        }

        public static (List<string[]>, string[]) Query(string cmdTxt)
        {
            checkConnection();


            NpgsqlCommand cmd = new NpgsqlCommand(cmdTxt, connection);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            List<string[]> data = new List<string[]>();
            string[] headers = new string[reader.FieldCount];

            while (reader.Read())
            {
                int i = 0;

                string[] row = new string[reader.FieldCount];
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    row[j] = reader[j]?.ToString() ?? "NULL";

                    if ( i == 0 )
                    {
                        headers[j] = reader.GetName(j);
                    }
                }
                data.Add(row);
            }
            reader.Close();

            return (data, headers);
        }

        public static void close()
        {
            connection.Close();
        }

    }
}
