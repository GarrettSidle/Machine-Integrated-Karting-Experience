using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ScottPlot;
using ScottPlot.Plottables;

namespace Machine_Integrated_Karting_Experience
{
    public partial class FrmHome : Form
    {
        const double KART_WIDTH_HEIGHT_RATIO = 0.89;
        const double PERC_TIRE_TO_WIDTH = .108;
        const double PERC_TIRE_TO_HEIGHT = .188;
        const double PERC_Y_MARGIN = .14;
        const double PERC_X_MARGIN = .116;

        public FrmHome()
        {
            InitializeComponent();
        }

        public void initializeTires()
        {
            int imgWidth = imgKart.Width;
            int totalHeight = imgKart.Height;
            int imgHeight = (int)Math.Round(imgWidth / KART_WIDTH_HEIGHT_RATIO);
            int marginHeight = totalHeight - imgHeight;

            int xMargin = (int)Math.Round(imgWidth * PERC_X_MARGIN);
            int yMargin = (int)Math.Round((imgHeight * PERC_Y_MARGIN) + (.5 * marginHeight));

            int tireWidth = (int)Math.Round(imgWidth * PERC_TIRE_TO_WIDTH);
            int tireHeight = (int)Math.Round(imgHeight * PERC_TIRE_TO_HEIGHT);


            pnlLeftTire.Width = tireWidth;
            pnlRightTire.Width = tireWidth;

            pnlLeftTire.Height = tireHeight;
            pnlRightTire.Height = tireHeight;

            pnlLeftTire.Location = new Point(xMargin, yMargin);
            pnlRightTire.Location = new Point(imgWidth - xMargin - tireWidth, yMargin);


        }

        //used for unchanging value, (does not update)
        public void updateHomeSettingsIndicators()
        {
            lblTickFreqValue.Text = MDIParent.tickRate.ToString();
            lblMaxJoltValue.Text = MDIParent.maxJolt.ToString();
            lblRunTypeValue.Text = MDIParent.runType.ToString();
            lblControllerTypeValue.Text = MDIParent.controllerType;
            lblConeOfCaringValue.Text = MDIParent.coneOfCaring;

            addConeofCaring();

            //.Text = MDIParent.maxSpeed.ToString();
            //.Text = MDIParent.maxAccelertionPercent.ToString();

        }

        public void addConeofCaring()
        {
            //TODO, dynaically add
            pltCleanLidar.Plot.Add.Line(new Coordinates(-10, 10), new Coordinates(0, 0));
            pltCleanLidar.Plot.Add.Line(new Coordinates(10, 10), new Coordinates(0, 0));
        }

        public void updateHomeIndicators()
        {
            //update kart metrics
            lblAccelerationValue.Text = MDIParent.currentAccelertion.ToString();
            lblSpeedValue.Text = MDIParent.currentSpeed.ToString();
            labelBrakeValue.Text = MDIParent.currentBrakeStatus.ToString();
            lblSteerAngleValue.Text = MDIParent.currentSteerAngle.ToString();

            //update tick settings
            lblTickCapacityValue.Text = MDIParent.tickCapacity.ToString();
            lblTickEfficiencyValue.Text = MDIParent.tickEffiecency.ToString();

            //update controller connection
            imgControllerConnection.Image = MDIParent.getStatusImageFromCode(MDIParent.statusController);

            //update bit status indicators
            imgEStopStatusValue.Image = MDIParent.currentEstop ? Properties.Resources.TrueIndicator : Properties.Resources.FalseIndicator;
            imgSoftEstopValue.Image = MDIParent.currentSoftEstop ? Properties.Resources.TrueIndicator : Properties.Resources.FalseIndicator;
            imgAutomaticStatusValue.Image = MDIParent.isAuto ? Properties.Resources.TrueIndicator : Properties.Resources.FalseIndicator;
            imgManualStatusValue.Image = MDIParent.isManual ? Properties.Resources.TrueIndicator : Properties.Resources.FalseIndicator;
            imgRecordingStatusValue.Image = MDIParent.isRecording ? Properties.Resources.TrueIndicator : Properties.Resources.FalseIndicator;
            imgFlagStatusValue.Image = MDIParent.isFlagging ? Properties.Resources.TrueIndicator : Properties.Resources.FalseIndicator;

            pltCleanLidar.Plot.Clear();
            addConeofCaring();

            lblDateTimeValue.Text = DateTime.Now.ToString();

            int nodeCount = MDIParent.lidarData.Count();

            //Coordinates coords; ;
            //for (int i = 0; i < nodeCount; i++)
            //{
            //    coords = new Coordinates(MDIParent.lidarData[i].X, MDIParent.lidarData[i].Y);
            //    pltDirtyLidar.Plot.Add.Circle(coords, .1);
            //    //TODO, add dynamically
            //    if (MDIParent.lidarData[i].Y >= (.5 * Math.Abs(MDIParent.lidarData[i].X)))
            //    {
            //        pltCleanLidar.Plot.Add.Circle(coords, .1);
            //    }
            //}
            //pltCleanLidar.Update();


        }



        private void pltDirtyLidar_Load(object sender, EventArgs e)
        {

        }

        private void pltCleanLidar_Load(object sender, EventArgs e)
        {

        }

        private void lblSteeringAngleTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblSteeringAngle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTickFreqValue_Click(object sender, EventArgs e)
        {

        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            initializeTires();
        }
    }
}
