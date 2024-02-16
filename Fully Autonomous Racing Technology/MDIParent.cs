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
using System.Diagnostics;
using Flee;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;

namespace Fully_Autonomous_Racing_Technology
{
    public partial class MDIParent : Form
    {
        public static IDictionary<string, Form> screens = new Dictionary<string, Form>();
        public static MDIParent mdiParent;


        #region InitializingVariables


        //
        //Congiguration values
        //
        private static int? tickRateValue;
        public static int? tickRate
        {
            get { return tickRateValue; }
            set
            {
                tickRateValue = value;

                if (tickCapacityValue != null & tickCapacityValue != 0)
                {
                    tickEffiecencyValue = (int)tickRateValue / (int)tickCapacityValue;
                }
            }
        }
        private static int? tickCapacityValue = null;
        public static int? tickCapacity
        {
            get { return tickCapacityValue; }
            set
            {
                tickCapacityValue = value;

                if (tickRateValue != null & tickCapacityValue != 0)
                {
                    tickEffiecencyValue = (int) tickRateValue / (int)tickCapacityValue;
                }
            }
        }
        private static int tickEffiecencyValue;
        public static int tickEffiecency
        {
            get { return tickEffiecencyValue; }
        }

        public static int maxJolt;
        public static double maxDisconnectTime;
        public static int maxSpeed;
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

        private static string controllerTypeValue;
        public static string controllerType
        {
            get { return controllerTypeValue; }
            set
            {
                value = value.ToLower();

                if (value == "xbox")
                {
                    controllerTypeValue = value;
                    return;
                }
                if (value == "rf")
                {
                    controllerTypeValue = value;
                    return;
                }
                throw new ArgumentException("controllerType ouside of expected values ( 'Xbox' or 'RF')");
            }
        }

        //TODO
        public static CodeExpression coneOfCaringEuation = new CodeExpression();

        private static string coneOfCaringValue;
        public static string coneOfCaring
        {
            get { return coneOfCaringValue; }
            set
            {
                coneOfCaringValue = value;

            }
        }


        
        //
        //Connectivity Status
        //
        public const int NULL_CONNECTION_STATUS = 0;
        public const int FAILED_CONNECTION_STATUS = 1;
        public const int SUCCESS_CONNECTION_STATUS = 2;
        public const int SIMULATE_CONNECTION_STATUS = 3;

        public static int statusEStopConttroller;
        public static int statusLidar;
        public static int statusRaspPi;
        public static int statusDatabase;
        public static int statusMotorController;
        public static int statusController;
        public static bool isControllerConnected;

        //
        //Info sent to pie
        //
        public static int currentAccelertion;
        public static bool currentBrakeStatus;
        private static int currentSteerAngleValue;
        public static int currentSteerAngle
        {
            get { return currentSteerAngleValue; }
            set
            {
                if (value > 45)
                {
                    value = 45;
                }
                if (value < -45)
                {
                    value = -45;
                }

                currentSteerAngleValue = value;
            }
        }

        //
        //current cart status
        //
        public static int currentSpeed;
        // 1 = safe, 0 = Estop hit
        public static bool currentEstop;

        //
        //program status flags
        //
        public static bool currentSoftEstop;

        private static bool isManualValue = true;
        public static bool isManual
        {
            get { return isManualValue; }
            set { 
                isManualValue = value;
                isAutoValue = !isManualValue;
            }
        }

        private static bool isAutoValue = false;
        public static bool isAuto
        {
            get { return isAutoValue; }
            set { 
                isAutoValue = value;
                isManualValue = !isAutoValue;
            }
        }

        private static bool isRecordingValue;
        public static bool isRecording
        {
            get { return isRecordingValue; }
            set
            {
                if (isAutoValue)
                {
                    MessageBox.Show("The cart must be in manual to record", "Error setting isAuto =" + value.ToString());
                    return;
                }
                isRecordingValue = value;
            }
        }

        private static bool isFlaggingValue;
        public static bool isFlagging
        {
            get { return isFlaggingValue; }
            set
            {
                if (isAutoValue)
                {
                    MessageBox.Show("The cart must be in manual to flag", "Error setting isFlagging = " + value.ToString());
                    return;
                }
                if (isRecordingValue)
                {
                    MessageBox.Show("The cart must be recording to flag", "Error setting isRecording =" + value.ToString());
                    return;
                }
                isFlaggingValue = value;
            }
        }

        //
        //
        //

        public static string[] warnings = [];

        public static Point[] lidarData = [];

        //
        //data logging values
        //

        public static int runNumber;

        public static int runFrame;

        public static int runType;

        public static DateTime dateTime;



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

            //get starting settings
            getSettings();

            if(controllerType == "xbox") 
            {
                XboxController.initializeXboxController();
            }
            if (controllerType == "rf")
            {
                //TODO
                //XboxController.initializeXboxController();
            }

        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)getScreen("Home");

            tmrLoop.Interval = (int)Math.Round(1.0 / (int)tickRate);

            //update indicators based on current values
            frmHome.updateHomeSettingsIndicators();
            updateParentSettingsIndicators();
            frmHome.updateHomeIndicators();
        }


        public void updateParentSettingsIndicators()
        {
            //update connectivity status indicators
            imgMotorControllerStatus.Image = getStatusImageFromCode(statusMotorController);
            imgLidarStatus.Image = getStatusImageFromCode(statusLidar);
            imgRaspPiStatus.Image = getStatusImageFromCode(statusRaspPi);
            imgEstopConttrollerStatus.Image = getStatusImageFromCode(statusEStopConttroller);
            imgDatabaseStatus.Image = getStatusImageFromCode(statusDatabase);
        }

        public static Bitmap getStatusImageFromCode(int statusCode)
        {
            switch (statusCode)
            {
                case NULL_CONNECTION_STATUS:
                    return Properties.Resources.NullConnection;
                case FAILED_CONNECTION_STATUS:
                    return Properties.Resources.Disconnected;
                case SUCCESS_CONNECTION_STATUS:
                    return Properties.Resources.Connected;
                case SIMULATE_CONNECTION_STATUS : 
                    return Properties.Resources.Simulated;
                default:
                    return null;
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

        private void getSettings()
        {
            //get all values from the config file
            tickRate = int.Parse(ConfigurationManager.AppSettings["tickRate"]);
            maxJolt = int.Parse(ConfigurationManager.AppSettings["maxJoltPerSecond"]);
            maxDisconnectTime = double.Parse(ConfigurationManager.AppSettings["maxDisconnectTime"]);
            runType = int.Parse(ConfigurationManager.AppSettings["runType"]);
            controllerType = ConfigurationManager.AppSettings["controllerType"];
            coneOfCaring = ConfigurationManager.AppSettings["coneOfCaring"];
            maxSpeed = int.Parse(ConfigurationManager.AppSettings["maxSpeed"]);
            maxAccelertionPercent = int.Parse(ConfigurationManager.AppSettings["maxAccelertionPercent"]);
            statusEStopConttroller = ConfigurationManager.AppSettings["simulateEStopConttroller"] == "1" ? SIMULATE_CONNECTION_STATUS : NULL_CONNECTION_STATUS;
            statusLidar = ConfigurationManager.AppSettings["simulateLidar"] == "1" ? SIMULATE_CONNECTION_STATUS: NULL_CONNECTION_STATUS;
            statusRaspPi = ConfigurationManager.AppSettings["simulateRaspPi"] == "1" ? SIMULATE_CONNECTION_STATUS: NULL_CONNECTION_STATUS;
            statusDatabase = ConfigurationManager.AppSettings["simulateDatabase"] == "1" ? SIMULATE_CONNECTION_STATUS: NULL_CONNECTION_STATUS;
            statusMotorController = ConfigurationManager.AppSettings["simulateMotorController"] == "1" ? SIMULATE_CONNECTION_STATUS: NULL_CONNECTION_STATUS;
        }

        Stopwatch watch = new Stopwatch();

        private void tmrLoop_Tick(object sender, EventArgs e)
        {
            //start the timer to calculate tick capacity
            watch.Restart();
       


            FrmHome frmHome = (FrmHome)MDIParent.getScreen("Home");

            Lidar.getLidar();
            MotorController.getMCData();

            getMovement();

            checkCurrentsoftEstop();

            RaspPi.writeMovesToPi();



            Database.writeToDatabase();


            updateParentSettingsIndicators();
            watch.Stop();

            if (watch.ElapsedMilliseconds != 0)
            {
                double tickPeriod = watch.Elapsed.TotalMilliseconds;
                //convert the tick capacity from ms to Hz
                tickCapacity = (int?)Math.Round((1000.0 / tickPeriod));
            }
            else
            {
                tickCapacity = 9999;
            }



            //update the numbers that we updated
            frmHome.updateHomeIndicators();
        }

        private void getMovement()
        {
            //if in Estop
            if (currentEstop)
            {
                //do not get movement
                return;
            }
            
            if (isAuto)
            {
                Auto.getAutonomousMovement();
                return;
            }
            if (isManual & controllerType == "xbox")
            {
                 XboxController.updateXboxData();
                 return;
            }
            if (isManual & controllerType == "rf")
            {
                RF.updateRFData();
                return;
            }
        }

        private void checkCurrentsoftEstop()
        {
            bool estopState= false;
            //TODO
            //estopState = ManualEstop || estopState;
            if (controllerType == "xbox")
            {
                estopState = XboxController.XboxSoftEstop || estopState;
            }
            if (controllerType == "rf")
            {
                //estopState = RF.RFSoftEstop || estopState;
            }

            currentSoftEstop = estopState;

            if (currentSoftEstop)
            {
                //todo
                //write brake to pie
            }
        }

    }

}
