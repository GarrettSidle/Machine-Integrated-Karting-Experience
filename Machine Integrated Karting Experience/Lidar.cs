using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Machine_Integrated_Karting_Experience
{
    internal class Lidar
    {
        private const int MOCK_POINT_COUNT = 30;
        private const int MOCK_POINT_MIN = -10;
        private const int MOCK_POINT_MAX = 10;

        public static void getLidar()
        {
            if (MDIParent.statusLidar == MDIParent.ConnectionStatus.Simulated)
            {
                generateMockData();
                return;
            }
            //TODO add lidar connectivity logic


            //TODO get lidar points logic

        }

        private static void generateMockData()
        {
            //TODO, fix lidar simulation (might be functional ??)
            Point[] mockLidar = new Point[MOCK_POINT_COUNT];

            Random rand = new Random();
            for (int i = 0; i < MOCK_POINT_COUNT; i++)
            {
                int x = rand.Next(MOCK_POINT_MIN, MOCK_POINT_MAX );
                int y = rand.Next(MOCK_POINT_MIN, MOCK_POINT_MAX );
                mockLidar[i] = new Point(x, y);
            }

            MDIParent.lidarData = mockLidar;

        }
    }
}
