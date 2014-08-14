using Game2D.Game.DataClasses;
using Game2D.Opengl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Utils.Commands;


namespace Game2D.Game.Concrete
{
    class ACameraMover
    {
        //keys, sides process
        const float motionSpeed = 1F;   //camera motion speed option
        const float invert = -1;    //control invertation

        //hold-and-drag process
        bool mouseClicked;
        Point2 mousePreviousMapPos;
        Point2 mousePreviousScreenPos;

        //global vars
        Point2 cameraPosition;

        public ACameraMover()
        {
            cameraPosition = new Point2(0, 0);

            //hold-and-drag process
            mouseClicked = false;
        }

        public void Process(List<Command> serverCommands, DStateMain sceneState, ref Frame resultFrame, IGetKeyboardState keyboardState)
        {
            HoldAndDragProcess(keyboardState);
            
            if (!mouseClicked)
            {
                ActiveSidesProcess();
                //KeyProcess(keyboardState);
            }

            resultFrame.camera = cameraPosition;   //updating camera position
        }

        void ActiveSidesProcess()
        {
            const int borderWidth = 10;

            bool l, r = false, u = false, d = false;
            l = System.Windows.Forms.Cursor.Position.X < 10;
            r = System.Windows.Forms.Cursor.Position.X > Screen.PrimaryScreen.Bounds.Width - borderWidth;
            u = System.Windows.Forms.Cursor.Position.Y < 10;
            d = System.Windows.Forms.Cursor.Position.Y > Screen.PrimaryScreen.Bounds.Height - borderWidth;

            MotionFromDirections(l, r, u, d);
        }

        void HoldAndDragProcess(IGetKeyboardState keyboardState)
        {
            if (lMousePressed() && !mouseClicked)  //now pressed, before not
            {
                mouseClicked = true;
                mousePreviousMapPos = keyboardState.MousePosMap;
                mousePreviousScreenPos = keyboardState.MousePosScreen;
            }

            if (!lMousePressed() && mouseClicked)  //now released, before pressed
            {
                mouseClicked = false;
            }

            if (mouseClicked && !equal(mousePreviousScreenPos, keyboardState.MousePosScreen))   //processing
            {
                Point2 currentPos = keyboardState.MousePosMap;
                Point2 delta = currentPos - mousePreviousMapPos;
                cameraPosition -= delta;
                mousePreviousMapPos = currentPos - delta;
                mousePreviousScreenPos = keyboardState.MousePosScreen;
            }
        }

        static bool lMousePressed()
        {
            return (Control.MouseButtons & MouseButtons.Left) != 0;
        }

        //void KeyProcess(IGetKeyboardState keyboardState)
        //{
        //    bool l = keyboardState.GetActionTime(EKeyboardAction.CamLeft) != 0; //arraws state
        //    bool r = keyboardState.GetActionTime(EKeyboardAction.CamRight) != 0;
        //    bool u = keyboardState.GetActionTime(EKeyboardAction.CamUp) != 0;
        //    bool d = keyboardState.GetActionTime(EKeyboardAction.CamDown) != 0;

        //    MotionFromDirections(l, r, u, d);
        //}

        void MotionFromDirections(bool l, bool r, bool u, bool d)   //left, right, up, down | calculate new camera position P(speed, directions)
        {
            int directions = 0; //arrows used

            if (l) directions++;
            if (r) directions++;
            if (u) directions++;
            if (d) directions++;

            double koefficient = 1; //saving speed in diagonal motion
            if (directions == 2)
                koefficient = 1 / Math.Sqrt(2);

            if (l) cameraPosition.x += koefficient * invert * motionSpeed;
            if (r) cameraPosition.x -= koefficient * invert * motionSpeed;
            if (u) cameraPosition.y += koefficient * invert * motionSpeed;
            if (d) cameraPosition.y -= koefficient * invert * motionSpeed;
        }

        public static bool equal(Point2 a, Point2 b)
        {
            return a.x == b.x && a.y == b.y;
        }
    }

}
