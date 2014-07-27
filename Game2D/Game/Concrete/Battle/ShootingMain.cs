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
        const double ELLIPSE_CONSTRICTION_SPEED = 0.0035;
        const double ELLIPSE_DILATATION_SPEED = 0.001;

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

            if (keyboard.GetActionTime(EKeyboardAction.gunChange) == 1)
            {
                tank.lighthouse.ChangeCurrentSight();
                tank.lighthouse.ellipseSize.now = tank.lighthouse.ellipseSize.max;
            }

            if (keyboard.GetActionTime(EKeyboardAction.gunUp) > 0)
            {
                if(tank.lighthouse.currentSight == tank.lighthouse.sightCount-1)
                    tank.lighthouse.MoveLastSight(true);
                else
                    tank.lighthouse.MoveFirstSight(true);
            }

            if (keyboard.GetActionTime(EKeyboardAction.gunDown) > 0)
            {
                if (tank.lighthouse.currentSight == tank.lighthouse.sightCount - 1)
                    tank.lighthouse.MoveLastSight(false);
                else
                    tank.lighthouse.MoveFirstSight(false);
            }

            if (keyboard.GetActionTime(EKeyboardAction.gunEllipse) > 0)
            {
                if (!tank.lighthouse.ellipseSize.AtMin() && tank.lighthouse.ellipseTime.Decrease())
                {
                    tank.lighthouse.ellipseSize.Decrease();
                }
                else
                {
                    tank.lighthouse.ellipseTime.Increase();
                    tank.lighthouse.ellipseSize.Increase();
                }
            }
            else
            {
                tank.lighthouse.ellipseTime.Increase();
                tank.lighthouse.ellipseSize.Increase();
            }

            DrawLighthouse(ref frame, tank.lighthouse, new Point2(20,20));
        }

        public void DrawLighthouse(ref Frame frame, DLighthouse lighthouse, Point2 start)
        {
            Vector2 cur = new Vector2(start, lighthouse.pos.angleDeg,1);
            Vector2 sight = lighthouse.GetSightPos(start, lighthouse.currentSight);
            double range = (sight.point - start).Length();
            Point2 sightSize = new Point2(6, 10) * lighthouse.ellipseSize.now;
            for (int i = 0; i <= lighthouse.lastSight; i++)
            {
                if(cur.point.DistTo(sight.point) > sightSize.x/2)
                    frame.Add(new Sprite(ESprite.gunMarkDark, cur, new Point2(0.3,0.3)));

                cur = new Vector2(cur.point + new Vector2(new Point2(0, 0), cur.angleDeg, DLighthouse.distBetweenPoints).vector, cur.vector);

            }

            frame.Add(new Sprite(ESprite.AimEllipse, sight, sightSize));

            frame.Add(new Text(EFont.fiol, 90,50,4,2,lighthouse.ellipseTime.now.ToString()));

            for (int i = 0; i < lighthouse.sightCount; i++)
            {
                if (i != lighthouse.currentSight)
                {
                    frame.Add(new Sprite(ESprite.shootMarkBright, lighthouse.GetSightPos(start, i), new Point2(1.5, 1.5)));
                }
            }
        }


    }
}
