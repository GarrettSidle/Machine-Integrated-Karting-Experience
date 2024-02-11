using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

using static Fully_Autonomous_Racing_Technology.XboxController;

namespace Fully_Autonomous_Racing_Technology
{
    public partial class MDIParent : Form
    {
        public static IDictionary<string, Form> screens = new Dictionary<string, Form>();
        public static MDIParent mdiParent;


        #region InitializingVariables

        private static int tickRateValue;
        public static int tickRate
        {
            get { return tickRateValue; }
            set
            {
                tickRateValue = value;
            }
        }

        private static int maxJoltValue;
        public static int maxJolt
        {
            get { return maxJoltValue; }
            set { maxJoltValue = value; }
        }

        private static int runTypeValue;
        public static int runType
        {
            get { return runTypeValue; }
            set { runTypeValue = value; }
        }

        private static string controllerTypeValue;
        public static string controllerType
        {
            get { return controllerTypeValue; }
            set
            {
                if (value.ToLower() == "xbox")
                {
                    controllerTypeValue = value;
                    return;
                }
                if (value.ToLower() == "rf")
                {
                    controllerTypeValue = value;
                    return;
                }
                throw new ArgumentException("controllerType ouside of expected values ( 'Xbox' or 'RF')");
            }
        }

        private static string coneOfCaringValue;
        public static string coneOfCaring
        {
            get { return coneOfCaringValue; }
            set { coneOfCaringValue = value; }
        }

        private static int maxSpeedValue;
        public static int maxSpeed
        {
            get { return maxSpeedValue; }
            set { maxSpeedValue = value; }
        }

        private static int maxAccelertionPercentValue;
        public static int maxAccelertionPercent
        {
            get { return maxAccelertionPercentValue; }
            set
            {
                if (value >= 0 & value <= 100)
                {
                    maxAccelertionPercentValue = value;
                    return;
                }
                throw new ArgumentException("maxAccelertionPercent ouside of range");
            }
        }

        private static bool simulateEStopConttrollerValue;
        public static bool simulateEStopConttroller
        {
            get { return simulateEStopConttrollerValue; }
            set { simulateEStopConttrollerValue = value; }
        }

        private static bool simulateLidarValue;
        public static bool simulateLidar
        {
            get { return simulateLidarValue; }
            set { simulateLidarValue = value; }
        }

        private static bool simulateRaspPiValue;
        public static bool simulateRaspPi
        {
            get { return simulateRaspPiValue; }
            set { simulateRaspPiValue = value; }
        }

        private static bool simulateDatabaseValue;
        public static bool simulateDatabase
        {
            get { return simulateDatabaseValue; }
            set { simulateDatabaseValue = value; }
        }

        private static bool simulateMotorControllerValue;
        public static bool simulateMotorController
        {
            get { return simulateMotorControllerValue; }
            set { simulateMotorControllerValue = value; }
        }

        private static int currentAccelertionValue;
        public static int currentAccelertion
        {
            get { return currentAccelertionValue; }
            set { currentAccelertionValue = value; }
        }

        private static int currentSpeedValue;
        public static int currentSpeed
        {
            get { return currentSpeedValue; }
            set { currentSpeedValue = value; }
        }

        private static bool currentBrakeStatusValue;
        public static bool currentBrakeStatus
        {
            get { return currentBrakeStatusValue; }
            set { currentBrakeStatusValue = value; }
        }

        private static int currentStearAngleValue;
        public static int currentStearAngle
        {
            get { return currentStearAngleValue; }
            set
            {
                if (value >= -45 & value <= 45)
                {
                    currentStearAngleValue = value;
                    return;
                }
                throw new ArgumentException("currentStearAngle ouside of range (-45 - 45)");
            }
        }

        #endregion



        public MDIParent()
        {
            InitializeComponent();

            //initialize each screen and add it to the screens array
            FrmHome home = new FrmHome();
            screens.Add("Home", home);

            FrmEventCRUD eventCRUD = new FrmEventCRUD();
            screens.Add("EventCRUD", eventCRUD);

            mdiParent = this;

            home.MdiParent = this;
            home.Dock = DockStyle.Fill;
            home.Show();

            getSettings();

        }

        public void updateParentSettingsIndicators()
        {
            if (simulateMotorController)
            {
                imgMotorControllerStatus.Image = Properties.Resources.Simulated;
            }
            else
            {

            }

            if (simulateLidar)
            {
                imgLidarStatus.Image = Properties.Resources.Simulated;
            }
            if (simulateRaspPi)
            {
                imgRaspPiStatus.Image = Properties.Resources.Simulated;
            }
            if (simulateEStopConttroller)
            {
                imgEstopConttrollerStatus.Image = Properties.Resources.Simulated;
            }
            if (simulateDatabase)
            {
                imgDatabaseStatus.Image = Properties.Resources.Simulated;
            }
        }


        public static void swapScreen(string screenName)
        {
            //get the new the screen
            screens.TryGetValue(screenName, out var screen);
            //set it as the new active screen
            screen.MdiParent = mdiParent;
            screen.Dock = DockStyle.Fill;
            screen.Show();
        }


        public static Form getScreen(string screenName)
        {
            //get the form object based on the screen name
            screens.TryGetValue(screenName, out var screen);
            return screen;
        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)getScreen("Home");

            frmHome.updateHomeSettingsIndicators();
            updateParentSettingsIndicators();
            frmHome.updateHomeIndicators();
        }

        private void getSettings()
        {
            tickRate = int.Parse(ConfigurationManager.AppSettings["tickRate"]);
            maxJolt = int.Parse(ConfigurationManager.AppSettings["maxJolt"]);
            runType = int.Parse(ConfigurationManager.AppSettings["runType"]);
            controllerType = ConfigurationManager.AppSettings["controllerType"];
            coneOfCaring = ConfigurationManager.AppSettings["coneOfCaring"];
            maxSpeed = int.Parse(ConfigurationManager.AppSettings["maxSpeed"]);
            maxAccelertionPercent = int.Parse(ConfigurationManager.AppSettings["maxAccelertionPercent"]);
            simulateEStopConttroller = bool.Parse(ConfigurationManager.AppSettings["simulateEStopConttroller"]);
            simulateLidar = bool.Parse(ConfigurationManager.AppSettings["simulateLidar"]);
            simulateRaspPi = bool.Parse(ConfigurationManager.AppSettings["simulateRaspPi"]);
            simulateDatabase = bool.Parse(ConfigurationManager.AppSettings["simulateDatabase"]);
            simulateMotorController = bool.Parse(ConfigurationManager.AppSettings["simulateMotorController"]);
        }

        private void tmrLoop_Tick(object sender, EventArgs e)
        {
            //XboxController.updaupdateXboxData();
        }
    }

}
