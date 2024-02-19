using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.XInput;
using System.Data;

namespace Machine_Integrated_Karting_Experience
{
    internal static class XboxController
    {
        static Controller controller;
        static Gamepad gamepad;
        private static bool connected = false;
        private static int deadband = 2500;
        private static Point leftThumb, rightThumb = new Point(0, 0);
        private static float leftTrigger, rightTrigger;
        private static int connectionAttempts = 0;
        private static int framesUntilDisconect;
        private static int maxjoltPerFrame;
        public static bool XboxSoftEstop = true;

        public static void initializeXboxController()
        {
            controller = new Controller(UserIndex.One);
            framesUntilDisconect = (int) Math.Round(MDIParent.maxDisconnectTime / (int) MDIParent.tickRate);
            maxjoltPerFrame = (int)Math.Round(MDIParent.maxJolt / ((double) MDIParent.tickRate));
        }

        // Call this method to update all class values
        public static void updateXboxData()
        {
            connected = controller.IsConnected;

            if (!connected)
            {
                MDIParent.statusController = MDIParent.FAILED_CONNECTION_STATUS;
                controller = new Controller(UserIndex.One);
                
                //if it has been longer than the max time
                connectionAttempts++;
                if(connectionAttempts > framesUntilDisconect)
                {
                    //update the estop
                    XboxSoftEstop = true;
                }

                return;
            }
            //reset the connection and cancel the current estop
            connectionAttempts = 0;
            XboxSoftEstop = false;
            MDIParent.statusController = MDIParent.SUCCESS_CONNECTION_STATUS;

            //if we are in Sstop
            if(MDIParent.currentEstop || MDIParent.currentSoftEstop)
            {
                //stop the kart
                MDIParent.currentBrakeStatus = true;
                MDIParent.currentAccelertion = 0;
                MDIParent.currentSteerAngle = 0;
                return;
            }

            //get the current controller state
            gamepad = controller.GetState().Gamepad;

            //get the values that we car about
            leftThumb.X = (int)Math.Round((Math.Abs((float)gamepad.LeftThumbX) < deadband) ? 0 : (float)gamepad.LeftThumbX / short.MinValue * -100);
            rightThumb.X = (int)Math.Round((Math.Abs((float)gamepad.RightThumbY) < deadband) ? 0 : (float)gamepad.RightThumbY / short.MaxValue * 100);

            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;

            //convert the new angle to our range (-45 - 45)
            int newAngle = (int)Math.Round(((XboxController.leftThumb.X) / 100.0) * 45);

            //if the angle is changeing to fast
            if (Math.Abs(newAngle - MDIParent.currentSteerAngle) > maxjoltPerFrame)
            {
                //limit the change amount to the max jolt
                MDIParent.currentSteerAngle += (5 * (newAngle < MDIParent.currentSteerAngle ? -1 : 1));
            }
            //if it is not changing to fast
            else
            {
                //allow the new value
                MDIParent.currentSteerAngle = newAngle;
            }

            //convert acceleration to a value between (1-100)
            MDIParent.currentAccelertion = (int)Math.Round((XboxController.rightTrigger / 255.0) * 100);
            //convert the brake value to a discrete value
            MDIParent.currentBrakeStatus = Math.Round(XboxController.leftTrigger / 255) == 1;
        }
            

    }
}
