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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Machine_Integrated_Karting_Experience.MDIParent;

namespace Machine_Integrated_Karting_Experience
{
    public partial class MDIParent : Form
    {
        public static IDictionary<Screens, Form> screens = new Dictionary<Screens, Form>();
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

                if (tickCapacityValue != null && tickCapacityValue != 0)
                {
                    tickEffiecencyValue = 100 * (int)tickRateValue / (int)tickCapacityValue;
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

                if (tickRateValue != null && tickCapacityValue != 0)
                {
                    tickEffiecencyValue = 100 * (int)tickRateValue / (int)tickCapacityValue;
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
                if (value >= 0 && value <= 100)
                {
                    maxAccelertionPercentValue = value;
                    return;
                }
                throw new ArgumentException("maxAccelertionPercent ouside of range");
            }
        }

        public static Controllers controllerType;

        //TODO, Dynamically get cone of caring from config
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


        public static ConnectionStatus statusEStopConttroller;
        public static ConnectionStatus statusLidar;
        public static ConnectionStatus statusRaspPi;
        public static ConnectionStatus statusDatabase;
        public static ConnectionStatus statusMotorController;
        public static ConnectionStatus statusController;
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
        // 0 = Estop hit, 1 = Estop hit
        private static bool currentEstopValue;
        public static bool currentEstop
        {
            get { return currentEstopValue; }
            set
            {
                currentEstopValue = value;
                if (currentEstopValue)
                {
                    isRecording = false;
                    isFlagging = false;
                }

            }
        }

        //
        //program status flags
        //
        private static bool currentSoftEstopValue;
        public static bool currentSoftEstop
        {
            get { return currentSoftEstopValue; }
            set
            {
                currentSoftEstopValue = value;
                if (currentSoftEstopValue)
                {
                    isRecording = false;
                    isFlagging = false;
                }

            }
        }

        private static bool isManualValue = true;
        public static bool isManual
        {
            get { return isManualValue; }
            set
            {
                isManualValue = value;
                isAutoValue = !isManualValue;
                isRecording = false;
                isFlagging = false;
            }
        }

        private static bool isAutoValue = false;
        public static bool isAuto
        {
            get { return isAutoValue; }
            set
            {
                isAutoValue = value;
                isManualValue = !isAutoValue;
                isRecording = false;
                isFlagging = false;
            }
        }

        private static bool isRecordingValue;
        public static bool isRecording
        {
            get { return isRecordingValue; }
            set
            {
                if (isAutoValue && value)
                {
                    MessageBox.Show("The cart must be in manual to record", "Error setting isRecording =" + value.ToString());
                    return;
                }
                if ((currentSoftEstop || currentEstop) && value)
                {
                    MessageBox.Show("The cart cannot be in estop when altering this value", "Error setting isRecording =" + value.ToString());
                    return;
                }
                //TODO, Warning : Cannot record with simulated MC / Lidar / Pi, if the database is live
                //TODO, Warning : Cannot record with disconected MC / Lidar / Pi

                isRecordingValue = value;
            }
        }

        private static bool isFlaggingValue;
        public static bool isFlagging
        {
            get { return isFlaggingValue; }
            set
            {
                if (isAutoValue && value)
                {
                    MessageBox.Show("The cart must be in manual to flag", "Error setting isFlagging = " + value.ToString());
                    return;
                }
                if (!isRecordingValue && value)
                {
                    MessageBox.Show("The cart must be recording to flag", "Error setting isFlagging =" + value.ToString());
                    return;
                }
                if ((currentSoftEstop || currentEstop) && value)
                {
                    MessageBox.Show("The cart cannot be in estop when altering this value", "Error setting isFlagging =" + value.ToString());
                    return;
                }
                isFlaggingValue = value;
            }
        }


        //
        //Connectivity Status
        //
        public enum ConnectionStatus
        {
            Null,
            Failed,
            Success,
            Simulated
        }

        public enum Screens
        {
            Null,
            Home,
            CRUD,
            Settings
        }

        public enum Controllers
        {
            Xbox,
            RF
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
            FrmHome frmHome = new FrmHome();
            screens.Add(Screens.Home, frmHome);

            FrmEventCRUD eventCRUD = new FrmEventCRUD();
            screens.Add(Screens.CRUD, eventCRUD);

            FrmSettings settings = new FrmSettings();
            screens.Add(Screens.Settings, settings);

            mdiParent = this;

            swapScreen(Screens.Home);

            //get starting settings
            getSettings();

            if (controllerType == Controllers.Xbox)
            {
                XboxController.initializeXboxController();
            }
            if (controllerType == Controllers.RF)
            {
                //TODO
                //RF.initializeXboxController();
            }

            //update indicators based on current values
            frmHome.updateHomeSettingsIndicators();
            updateParentSettingsIndicators();
            frmHome.updateHomeIndicators();

            //update the loop frequency
            tmrLoop.Interval = (int)Math.Round(1000.0 / (int)tickRate);

            //TODO: set up the key press event

        }

        public void updateParentSettingsIndicators()
        {
            //update connectivity status indicators
            imgMotorControllerStatus.Image  = getStatusImageFromCode(statusMotorController);
            imgLidarStatus.Image            = getStatusImageFromCode(statusLidar);
            imgRaspPiStatus.Image           = getStatusImageFromCode(statusRaspPi);
            imgEstopConttrollerStatus.Image = getStatusImageFromCode(statusEStopConttroller);
            imgDatabaseStatus.Image         = getStatusImageFromCode(statusDatabase);

        }

        public static Bitmap getStatusImageFromCode(ConnectionStatus statusCode)
        {
            switch (statusCode)
            {
                case ConnectionStatus.Null:
                    return Properties.Resources.NullConnection;
                case ConnectionStatus.Failed:
                    return Properties.Resources.Disconnected;
                case ConnectionStatus.Success:
                    return Properties.Resources.Connected;
                case ConnectionStatus.Simulated:
                    return Properties.Resources.Simulated;
                default:
                    return Properties.Resources.NullConnection;
            }
        }



        public static void swapScreen(Screens screenCode)
        {
            //get the new the screen
            screens.TryGetValue(screenCode, out var screen);

            if(screen == null)
            {
                return;
            }

            //set it as the new active screen
            screen.MdiParent = mdiParent;
            screen.Dock = DockStyle.Fill;
            screen.Show();
            screen.BringToFront();

            //TODO Highlight the active screen on the toolbar
        }


        public static Form getScreen(Screens screenName)
        {
            //get the form object based on the screen name
            screens.TryGetValue(screenName, out var screen);
            return screen;
        }

        private void getSettings()
        {
            //get all values from the config file
            tickRate              = int.Parse(   ConfigurationManager.AppSettings["tickRate"]);
            maxJolt               = int.Parse(   ConfigurationManager.AppSettings["maxJoltPerSecond"]);
            maxDisconnectTime     = double.Parse(ConfigurationManager.AppSettings["maxDisconnectTime"]);
            runType               = int.Parse(   ConfigurationManager.AppSettings["runType"]);
            coneOfCaring          =              ConfigurationManager.AppSettings["coneOfCaring"];
            maxSpeed              = int.Parse(   ConfigurationManager.AppSettings["maxSpeed"]);
            maxAccelertionPercent = int.Parse(   ConfigurationManager.AppSettings["maxAccelertionPercent"]);

            if (ConfigurationManager.AppSettings["controllerType"] == "xbox")
            {
                controllerType = Controllers.Xbox;
            }
            else if (ConfigurationManager.AppSettings["controllerType"] == "RF")
            {
                controllerType = Controllers.RF;
            }

            statusEStopConttroller = ConfigurationManager.AppSettings["simulateEStopConttroller"] == "1" ? ConnectionStatus.Simulated : ConnectionStatus.Null;
            statusLidar            = ConfigurationManager.AppSettings["simulateLidar"]            == "1" ? ConnectionStatus.Simulated : ConnectionStatus.Null;
            statusRaspPi           = ConfigurationManager.AppSettings["simulateRaspPi"]           == "1" ? ConnectionStatus.Simulated : ConnectionStatus.Null;
            statusDatabase         = ConfigurationManager.AppSettings["simulateDatabase"]         == "1" ? ConnectionStatus.Simulated : ConnectionStatus.Null;
            statusMotorController  = ConfigurationManager.AppSettings["simulateMotorController"]  == "1" ? ConnectionStatus.Simulated : ConnectionStatus.Null;
        }

        Stopwatch watch = new Stopwatch();

        private void tmrLoop_Tick(object sender, EventArgs e)
        {
            //start the timer to calculate tick capacity
            watch.Restart();

            FrmHome frmHome = (FrmHome)getScreen(Screens.Home);

            Lidar.getLidar();
            MotorController.getMCData();

            getMovement();

            checkCurrentsoftEstop();

            RaspPi.writeMovesToPi();

            //TODO, stop recordings if disconected MC / Lidar / Pi 
            //TODO, stop recordings if estop is hit

            Database.writeToDatabase();


            updateParentSettingsIndicators();
            watch.Stop();

            getTickCapacity(watch);

            //update the numbers that we updated
            frmHome.updateHomeIndicators();
        }

        private void getTickCapacity(Stopwatch watch)
        {
            if (watch.ElapsedMilliseconds != 0)
            {
                double tickPeriod = watch.Elapsed.TotalMilliseconds;
                //convert the tick capacity from ms to Hz
                tickCapacity = (int?)Math.Round(1000.0 / tickPeriod);
            }
            else
            {
                tickCapacity = 9999;
            }
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
            if (isManual && controllerType == Controllers.Xbox)
            {
                XboxController.updateXboxData();
                return;
            }
            if (isManual && controllerType == Controllers.RF)
            {
                RF.updateRFData();
                return;
            }
        }

        private void checkCurrentsoftEstop()
        {
            bool estopState = false;
            estopState = FrmHome.manualSoftEstop || estopState;
            if (controllerType == Controllers.Xbox)
            {
                estopState = XboxController.XboxSoftEstop || estopState;
            }
            else if (controllerType == Controllers.RF)
            {
                estopState = RF.RFSoftEstop || estopState;
            }

            currentSoftEstop = estopState;

            //if we are in Estop
            if (currentSoftEstop)
            {
                //stop the vehicle
                currentAccelertion = 0;
                currentBrakeStatus = true;
            }
        }

        private void lblHomeSelector_Click(object sender, EventArgs e)
        {
            swapScreen(Screens.Home);
        }

        private void lblCRUDSelector_Click(object sender, EventArgs e)
        {
            swapScreen(Screens.CRUD);
        }

        private void lblSettingsSellector_Click(object sender, EventArgs e)
        {
            swapScreen(Screens.Settings);
        }
    }

}
