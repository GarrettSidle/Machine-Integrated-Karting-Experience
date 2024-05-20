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
using System.Configuration;
using ScottPlot.Statistics;

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

        //set in the config file
        public static int setID;

        //set dynamically based on the last runID
        public static int runID;

        //incremented throughout on every new frame
        public static int frameID;

        //incremented throughout on every new frame
        public static int pointID;

        private static NpgsqlConnection connection;

        public static void initializeRecording()
        {
            //create a new run
            runID = getNewRunID();

            string cmdText = $"INSERT INTO runs (setid, runid, datetime, ignore) VALUES ({setID}, {runID}, CURRENT_TIMESTAMP, False);";

            Query(cmdText);

            frameID = 0;
        }

        public static void stopRecording()
        {
            //reset all the accumulated ID;s
            runID = 0;
            frameID = 0;
            pointID = 0;
        }

        public static void writeToDatabase()
        {
            if (MDIParent.statusDatabase == MDIParent.ConnectionStatus.Simulated)
            {
                return;
            }

            if (!MDIParent.isRecording)
            {
                return;
            }

            //TODO check if the set settings are the same as the current

            frameID++;

            //log the current frame
            string columns = "setid, runid, frameID, datetime, percentaccel, brake, stearing, velocity, ignore, flag";
            string values  = $"{setID}, {runID}, {frameID},CURRENT_TIMESTAMP, {MDIParent.currentAccelertion}, {MDIParent.currentBrakeStatus}, {MDIParent.currentSteerAngle}, {MDIParent.currentSpeed},  False, {MDIParent.isFlagging}";

            string cmdText = $"INSERT INTO frames ({columns}) VALUES ({values})";

            Query(cmdText);


            //TODO convert to use clean lidar data
            //log the lidar points
            for (int i = 0; i < MDIParent.lidarData.Length; i++) 
            {
                pointID = i + 1;

                cmdText = $"INSERT INTO points (setid, runid, frameid, pointid, X, Y) VALUES ({setID}, {runID},{frameID}, {pointID}, {MDIParent.lidarData[i].X}, {MDIParent.lidarData[i].Y})";

                Query(cmdText);

            }


            //TODO finalize Database

            //TODO CRUD SQL statement
            //TODO add cancel flag logic

        }

        public static int getNewRunID()
        {
            return ((int) Query($"SELECT MAX(runid) FROM runs where setid = {setID}")) + 1;
        }

        public static void initializeConnection()
        {
            //if we are simulated 
            if (MDIParent.statusDatabase == MDIParent.ConnectionStatus.Simulated)
            {
                //ignore the connection
                return;
            }


            string? host     = ConfigurationManager.AppSettings["host"];
            string? port     = ConfigurationManager.AppSettings["port"];
            string? database = ConfigurationManager.AppSettings["database"];
            string? userId   = ConfigurationManager.AppSettings["userId"];
            string? password = ConfigurationManager.AppSettings["password"];

            string connectionString = $"Host={host}; Port = {port}; Database = {database}; User Id = {userId}; Password = {password}; ";


            connection = new NpgsqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                Utils.LogError("Failed to open Database");
            }



        }
        public static void checkConnection()
        {
            if (connection == null) 
            {
                initializeConnection();

                Utils.LogWarning("Connection was opened outside of immediate launch");
            }
        }

        public static object Query(string cmdTxt)
        {
            //if we are simulated 
            if (MDIParent.statusDatabase == MDIParent.ConnectionStatus.Simulated)
            {
                //ignore the query
                return 0;
            }

            checkConnection();

            using (NpgsqlCommand cmd = new NpgsqlCommand(cmdTxt, connection))
            {
                return cmd.ExecuteScalar();
            }
        }

        public static (List<string[]>, string[]) QueryTable(string cmdTxt)
        {
            //if we are simulated 
            if (MDIParent.statusDatabase == MDIParent.ConnectionStatus.Simulated)
            {
                //ignore the query
                return (new List<string[]>(), new string[1]);
            }

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
