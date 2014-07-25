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
        bool tankPosInited;
        Point2 destinationPiont;

        public ATankDriver()
        {
            tankPosInited = false;
        }

        public void Process(List<Command> serverCommands, DStateMain sceneState, ref Frame resultFrame, IGetKeyboardState keyboardState)
        {
            if (sceneState.battle != null && sceneState.battle.me != null)
            {
                DTankBody tank = sceneState.battle.me.tank.body;

                if (!tankPosInited) //initializating tank position
                {
                    tank.pos = new Vector2(0, 0, 0);
                    destinationPiont = new Point2(0, 0);
                    tankPosInited = true;
                }

                if (keyboardState.MouseRightClick)  //new destination point
                {
                    destinationPiont = keyboardState.MousePosMap;
                }

                moveTaktic1(tank);

                //drawing

                resultFrame.Add(new Sprite(tank.sprite, tank.pos, tank.size.x, tank.size.y));
                resultFrame.Add(new Sprite(tank.sprite, new Vector2(destinationPiont), tank.size.x, tank.size.y));
            }
        }

        void moveTaktic1(DTankBody tank)
        {
            if (tank.pos.point.DistTo(destinationPiont) == 0)    //points are similar
                return;

            if (tank.pos.point.DistTo(destinationPiont) < tank.speed)
            {
                tank.pos = new Vector2(destinationPiont, tank.pos.vector);
                return;
            }

            Vector2 newPos = new Vector2(0, 0, 0);

            double dx = destinationPiont.x - tank.pos.x;
            double dy = destinationPiont.y - tank.pos.y;
            double gyp = Math.Pow(Math.Pow(dx, 2) + Math.Pow(dy, 2), 1/2);
            double sin = dy / gyp;
            double cos = dx / gyp;

            newPos.x = tank.pos.x + tank.speed * cos;
            newPos.y = tank.pos.y + tank.speed * sin;

            tank.pos = newPos;
        }
    }
}
