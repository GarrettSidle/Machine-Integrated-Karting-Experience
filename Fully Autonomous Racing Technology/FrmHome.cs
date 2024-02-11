using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fully_Autonomous_Racing_Technology
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        public void updateHomeSettingsIndicators()
        {
            lblTickFreqValue.Text = MDIParent.tickRate.ToString();
            lblMaxJoltValue.Text = MDIParent.maxJolt.ToString();
            lblRunTypeValue.Text = MDIParent.runType.ToString();
            lblControllerTypeValue.Text = MDIParent.controllerType;
            lblConeOfCaringValue.Text = MDIParent.coneOfCaring;
            //.Text = MDIParent.maxSpeed.ToString();
            //.Text = MDIParent.maxAccelertionPercent.ToString();
        }

        public void updateHomeIndicators()
        {
            lblAccelerationValue.Text = MDIParent.currentAccelertion.ToString();
            lblSpeedValue.Text = MDIParent.currentSpeed.ToString();
            labelBrakeValue.Text = MDIParent.currentBrakeStatus.ToString();
            lblSteerAngleValue.Text = MDIParent.currentSteerAngle.ToString();
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
    }
}
