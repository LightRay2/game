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
            if (keyboardState.GetActionTime(EKeyboardAction.Left) != 0)   //checking arrows state
                cameraPosition.x += invert * motionSpeed;
            if (keyboardState.GetActionTime(EKeyboardAction.Right) != 0)
                cameraPosition.x -= invert * motionSpeed;
            if (keyboardState.GetActionTime(EKeyboardAction.Up) != 0)
                cameraPosition.y += invert * motionSpeed;
            if (keyboardState.GetActionTime(EKeyboardAction.Down) != 0)
                cameraPosition.y -= invert * motionSpeed;

            resultFrame.camera = cameraPosition;   //updating camera position
        }
    }

}
