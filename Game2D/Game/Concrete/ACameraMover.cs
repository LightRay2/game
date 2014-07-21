using Game2D.Game.DataClasses;
using Game2D.Opengl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Utils.Commands;


namespace Game2D.Game.Concrete
{
    ///<summary>
    ///Osu`estvlqet pereme`enie kamery klaviwami CamLeft, CalRight, CamUp, CamDown. Xranit polo]enie kamery.
    ///</summary>
    class ACameraMover
    {
        const float motionSpeed = 1F;   //camera motion speed option
        const float invert = -1;    //control invertation
        Point2 cameraPosition;

        public ACameraMover()
        {
            cameraPosition = new Point2(0, 0);
        }

        public void Process(List<Command> serverCommands, DStateMain sceneState, ref Frame resultFrame, IGetKeyboardState keyboardState)
        {
            bool l = keyboardState.GetActionTime(EKeyboardAction.CamLeft) != 0; //arraws state
            bool r = keyboardState.GetActionTime(EKeyboardAction.CamRight) != 0;
            bool u = keyboardState.GetActionTime(EKeyboardAction.CamUp) != 0;
            bool d = keyboardState.GetActionTime(EKeyboardAction.CamDown) != 0;

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

            resultFrame.camera = cameraPosition;   //updating camera position
        }
    }

}
