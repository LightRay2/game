using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Opengl;
using Utils.Commands;
namespace Game2D.Game.Concrete.Battle
{
    class ShootingMain
    {
        
        public void Process(List<Command> serverCom, DStateMain state, IGetKeyboardState keyboard, ref Frame frame)
        {
            if (state.battle == null || state.battle.me == null) return;
            DTank tank = state.battle.me.tank;
            if(tank.lighthouse == null)
            tank.lighthouse = new DLighthouse();

            if (keyboard.GetActionTime(EKeyboardAction.gunLeft) > 0)
            {
                tank.lighthouse.pos.Rotate(-0.3);
            }
            if (keyboard.GetActionTime(EKeyboardAction.gunRight) > 0)
            {
                tank.lighthouse.pos.Rotate(0.3);
            }
            DrawLighthouse(ref frame, tank.lighthouse, new Point2(20,20));
        }

        public void DrawLighthouse(ref Frame frame, DLighthouse lighthouse, Point2 start)
        {
            Vector2 cur = new Vector2(start, lighthouse.pos.angleDeg,1);
            for (int i = 0; i <= lighthouse.lastSight; i++)
            {
                frame.Add(new Sprite(ESprite.gunMarkDark, cur, new Point2(0.3,0.3)));
                cur = new Vector2(cur.point + new Vector2(new Point2(0, 0), cur.angleDeg, DLighthouse.distBetweenPoints).vector, cur.vector);

            }
        }


    }
}
