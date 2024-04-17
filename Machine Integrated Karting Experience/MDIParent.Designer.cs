
namespace Machine_Integrated_Karting_Experience
{
    partial class MDIParent
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent));
            pageSelectorToolStrip = new ToolStrip();
            lblHomeSelector = new ToolStripLabel();
            Tool = new ToolStripSeparator();
            lblCRUDSelector = new ToolStripLabel();
            toolStripSeparator8 = new ToolStripSeparator();
            lblSettingsSelector = new ToolStripLabel();
            lblDatabaseStatus = new ToolStripLabel();
            imgDatabaseStatus = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            lblEstopConttrollerStatus = new ToolStripLabel();
            imgEstopConttrollerStatus = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            lblRaspPiStatus = new ToolStripLabel();
            imgRaspPiStatus = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            lblLidarStatus = new ToolStripLabel();
            imgLidarStatus = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            lblMotorControllerStatus = new ToolStripLabel();
            imgMotorControllerStatus = new ToolStripButton();
            connectivityToolStrip = new ToolStrip();
            tmrLoop = new System.Windows.Forms.Timer(components);
            pageSelectorToolStrip.SuspendLayout();
            connectivityToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pageSelectorToolStrip
            // 
            pageSelectorToolStrip.Items.AddRange(new ToolStripItem[] { lblHomeSelector, Tool, lblCRUDSelector, toolStripSeparator8, lblSettingsSelector });
            pageSelectorToolStrip.Location = new Point(0, 0);
            pageSelectorToolStrip.Name = "pageSelectorToolStrip";
            pageSelectorToolStrip.RightToLeft = RightToLeft.No;
            pageSelectorToolStrip.Size = new Size(1169, 25);
            pageSelectorToolStrip.TabIndex = 3;
            pageSelectorToolStrip.Text = "toolStrip1";
            // 
            // lblHomeSelector
            // 
            lblHomeSelector.BackColor = SystemColors.Control;
            lblHomeSelector.ForeColor = Color.Black;
            lblHomeSelector.Name = "lblHomeSelector";
            lblHomeSelector.Size = new Size(40, 22);
            lblHomeSelector.Text = "Home";
            lblHomeSelector.Click += lblHomeSelector_Click;
            // 
            // Tool
            // 
            Tool.Margin = new Padding(10, 0, 10, 0);
            Tool.Name = "Tool";
            Tool.Size = new Size(6, 25);
            // 
            // lblCRUDSelector
            // 
            lblCRUDSelector.Name = "lblCRUDSelector";
            lblCRUDSelector.Size = new Size(89, 22);
            lblCRUDSelector.Text = "Database CRUD";
            lblCRUDSelector.Click += lblCRUDSelector_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 25);
            // 
            // lblSettingsSelector
            // 
            lblSettingsSelector.Font = new Font("Segoe UI", 9F);
            lblSettingsSelector.Name = "lblSettingsSelector";
            lblSettingsSelector.Size = new Size(49, 22);
            lblSettingsSelector.Text = "Settings";
            lblSettingsSelector.Click += lblSettingsSellector_Click;
            // 
            // lblDatabaseStatus
            // 
            lblDatabaseStatus.Name = "lblDatabaseStatus";
            lblDatabaseStatus.Size = new Size(55, 22);
            lblDatabaseStatus.Text = "Database";
            // 
            // imgDatabaseStatus
            // 
            imgDatabaseStatus.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imgDatabaseStatus.Image = Properties.Resources.NullConnection;
            imgDatabaseStatus.ImageTransparentColor = Color.Magenta;
            imgDatabaseStatus.Name = "imgDatabaseStatus";
            imgDatabaseStatus.Size = new Size(23, 22);
            imgDatabaseStatus.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // lblEstopConttrollerStatus
            // 
            lblEstopConttrollerStatus.Name = "lblEstopConttrollerStatus";
            lblEstopConttrollerStatus.Size = new Size(92, 22);
            lblEstopConttrollerStatus.Text = "Estop Controller";
            // 
            // imgEstopConttrollerStatus
            // 
            imgEstopConttrollerStatus.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imgEstopConttrollerStatus.Image = Properties.Resources.NullConnection;
            imgEstopConttrollerStatus.ImageTransparentColor = Color.Magenta;
            imgEstopConttrollerStatus.Name = "imgEstopConttrollerStatus";
            imgEstopConttrollerStatus.Size = new Size(23, 22);
            imgEstopConttrollerStatus.Text = "toolStripButton1";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // lblRaspPiStatus
            // 
            lblRaspPiStatus.Name = "lblRaspPiStatus";
            lblRaspPiStatus.Size = new Size(72, 22);
            lblRaspPiStatus.Text = "Raspberry Pi";
            // 
            // imgRaspPiStatus
            // 
            imgRaspPiStatus.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imgRaspPiStatus.Image = Properties.Resources.NullConnection;
            imgRaspPiStatus.ImageTransparentColor = Color.Magenta;
            imgRaspPiStatus.Name = "imgRaspPiStatus";
            imgRaspPiStatus.Size = new Size(23, 22);
            imgRaspPiStatus.Text = "toolStripButton1";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // lblLidarStatus
            // 
            lblLidarStatus.Name = "lblLidarStatus";
            lblLidarStatus.Size = new Size(33, 22);
            lblLidarStatus.Text = "Lidar";
            // 
            // imgLidarStatus
            // 
            imgLidarStatus.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imgLidarStatus.Image = Properties.Resources.NullConnection;
            imgLidarStatus.ImageTransparentColor = Color.Magenta;
            imgLidarStatus.Name = "imgLidarStatus";
            imgLidarStatus.Size = new Size(23, 22);
            imgLidarStatus.Text = "toolStripButton1";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // lblMotorControllerStatus
            // 
            lblMotorControllerStatus.Name = "lblMotorControllerStatus";
            lblMotorControllerStatus.Size = new Size(96, 22);
            lblMotorControllerStatus.Text = "Motor Controller";
            // 
            // imgMotorControllerStatus
            // 
            imgMotorControllerStatus.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imgMotorControllerStatus.Image = Properties.Resources.NullConnection;
            imgMotorControllerStatus.ImageTransparentColor = Color.Magenta;
            imgMotorControllerStatus.Name = "imgMotorControllerStatus";
            imgMotorControllerStatus.Size = new Size(23, 22);
            imgMotorControllerStatus.Text = "toolStripButton1";
            // 
            // connectivityToolStrip
            // 
            connectivityToolStrip.Dock = DockStyle.Bottom;
            connectivityToolStrip.Items.AddRange(new ToolStripItem[] { lblDatabaseStatus, imgDatabaseStatus, toolStripSeparator1, lblEstopConttrollerStatus, imgEstopConttrollerStatus, toolStripSeparator2, lblRaspPiStatus, imgRaspPiStatus, toolStripSeparator3, lblLidarStatus, imgLidarStatus, toolStripSeparator4, lblMotorControllerStatus, imgMotorControllerStatus });
            connectivityToolStrip.Location = new Point(0, 610);
            connectivityToolStrip.Name = "connectivityToolStrip";
            connectivityToolStrip.RightToLeft = RightToLeft.Yes;
            connectivityToolStrip.Size = new Size(1169, 25);
            connectivityToolStrip.TabIndex = 1;
            connectivityToolStrip.Text = "toolStrip1";
            // 
            // tmrLoop
            // 
            tmrLoop.Enabled = true;
            tmrLoop.Tick += tmrLoop_Tick;
            // 
            // MDIParent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1169, 635);
            Controls.Add(pageSelectorToolStrip);
            Controls.Add(connectivityToolStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            Name = "MDIParent";
            Text = "Machine Integrated Karting Experience (M.I.K.E.)";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            pageSelectorToolStrip.ResumeLayout(false);
            pageSelectorToolStrip.PerformLayout();
            connectivityToolStrip.ResumeLayout(false);
            connectivityToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip pageSelectorToolStrip;
        private ToolStripLabel lblHomeSelector;
        private ToolStripSeparator Tool;
        private ToolStripLabel lblCRUDSelector;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripLabel lblSettingsSelector;
        private ToolStripLabel lblDatabaseStatus;
        private ToolStripButton imgDatabaseStatus;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel lblEstopConttrollerStatus;
        private ToolStripButton imgEstopConttrollerStatus;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel lblRaspPiStatus;
        private ToolStripButton imgRaspPiStatus;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel lblLidarStatus;
        private ToolStripButton imgLidarStatus;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripLabel lblMotorControllerStatus;
        private ToolStripButton imgMotorControllerStatus;
        private ToolStrip connectivityToolStrip;
        private System.Windows.Forms.Timer tmrLoop;
    }
}
