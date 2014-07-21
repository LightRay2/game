using Game2D.Game.DataClasses;
using Game2D.Opengl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;

namespace Game2D.Game.Concrete
{
    class ATankDriver
    {
        public ATankDriver()
        {

        }

        public void Process(List<Command> serverCommands, DStateMain sceneState, ref Frame resultFrame, IGetKeyboardState keyboardState)
        {
            if (sceneState.battle != null && sceneState.battle.me != null)
            {
                sceneState.battle.me.tank.body.pos = new Vector2(0, 0, 0);
                sceneState.battle.me.tank.body.Draw(resultFrame);
            }
        }

    }
}
