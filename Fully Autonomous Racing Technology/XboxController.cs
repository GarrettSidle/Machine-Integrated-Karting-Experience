using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.XInput;

namespace Fully_Autonomous_Racing_Technology
{
    internal static class XboxController
    {
        static Controller controller;
        static Gamepad gamepad;
        public static bool connected = false;
        public static int deadband = 2500;
        public static Point leftThumb, rightThumb = new Point(0, 0);
        public static float leftTrigger, rightTrigger;

        public static void initializeXboxController()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
        }

        // Call this method to update all class values
        public static void updateXboxData()
        {
            if (!connected)
                return;

            gamepad = controller.GetState().Gamepad;


            leftThumb.X = (int)Math.Round((Math.Abs((float)gamepad.LeftThumbX) < deadband) ? 0 : (float)gamepad.LeftThumbX / short.MinValue * -100);
            leftThumb.Y = (int)Math.Round((Math.Abs((float)gamepad.LeftThumbY) < deadband) ? 0 : (float)gamepad.LeftThumbY / short.MaxValue * 100);
            rightThumb.Y = (int)Math.Round((Math.Abs((float)gamepad.RightThumbX) < deadband) ? 0 : (float)gamepad.RightThumbX / short.MaxValue * 100);
            rightThumb.X = (int)Math.Round((Math.Abs((float)gamepad.RightThumbY) < deadband) ? 0 : (float)gamepad.RightThumbY / short.MaxValue * 100);

            leftTrigger = gamepad.LeftTrigger;
            rightTrigger = gamepad.RightTrigger;
        }

    }
}
