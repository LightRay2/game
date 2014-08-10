using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Opengl;
using Utils.Commands;
using Utils.DataClasses;
using Utils.Helpers;
namespace Game2D.Game.Concrete.Battle
{
    class ShootingMain
    {
        const double ELLIPSE_CONSTRICTION_SPEED = 0.0035;
        const double ELLIPSE_DILATATION_SPEED = 0.001;
        int curSpreadIndex = -1;
        ComShootToPoint shootCommand = null;
        public List<Command> Process(List<Command> serverCom, DStateMain state, IGetKeyboardState keyboard, ref Frame frame)
        {
            List<Command> r = new List<Command>();

            if (state.battle == null || state.battle.me == null) return r;
            DTank tank = state.battle.me.tank;
            if(tank.lighthouse == null)
            tank.lighthouse = new DLighthouse();

            #region lighthouse
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
            #endregion

            #region shells
            //--------------------выстрел-------------------------
            if (keyboard.GetActionTime(EKeyboardAction.Fire) == 1)
            {
                if (shootCommand == null)
                {
                    shootCommand = new ComShootToPoint(tank.lighthouse.GetSightPos(new Point2(20, 20), tank.lighthouse.currentSight).point
                        , curSpreadIndex, tank.lighthouse.ellipseSize.now / tank.lighthouse.ellipseSize.max);
                    r.Add(shootCommand);
                }
            }

            if (curSpreadIndex == -1)
            {
                r.Add(new ComSpreadIndex(0));
            }
            foreach (Command c in serverCom)
            {
                if (c is ComSpreadIndex)
                {
                    ComSpreadIndex com = (ComSpreadIndex)c;
                    r.Add(new ComSpreadIndex(com.spreadIndex));
                    curSpreadIndex = com.spreadIndex;
                }

                if (c is ComShootMain)
                {
                    ComShootMain com = (ComShootMain)c;
                    DTank shootedTank = state.battle.players.Where(a => a.id == com.playerID).First().tank;
                    DShooting shooting = shootedTank.shooting;
                    shooting.aim = com.point;
                    shooting.spreadLevel = com.spreadLevel;
                    List<DShell> shells = HShells.SetShellsFlight(shooting, com.spreadIndex, shootedTank.body.pos.point);
                    state.battle.shells.AddRange(shells);
                    shooting.shells.AddRange(shells);
                    shootCommand = null;
                }
            }


            for (int i = 0; i < state.battle.shells.Count; i++)
            {
                DShell shell = state.battle.shells[i];
                HShells.MoveShell(shell);
                if (shell.time == shell.timeFirst + shell.timeSecond)
                {
                    shell.Destroy();
                    state.battle.shells.RemoveAt(i--);
                }
            }
            //-----------------------------------------------------------------
            #endregion

            #region drawing

            frame.Add(new Sprite(ESprite.tank2, new Vector2(22, 20, 0), new Point2(16, 13)));
            frame.Add(new Text(EFont.lilac, new Vector2(10, 10, 1, 2), "WSAD E F SPACE"));
            foreach (DShell shell in state.battle.shells)
            {
                int frameShell = 0;
                if (state.battle.me.tank.shooting == shell.parent)
                {
                    frameShell = 1;
                    if (shell.time > shell.timeFirst + shell.timeSecond - shell.explosionFlightTime)
                        frameShell = 3;
                }
                frame.Add(new Sprite(ESprite.shell0, shell.pos, new Point2(1, 1), frameShell));
            }


            DrawLighthouse(ref frame, tank.lighthouse, new Point2(20,20));
            #endregion
            return r;
        }


        public void DrawLighthouse(ref Frame frame, DLighthouse lighthouse, Point2 start)
        {
            Vector2 cur = new Vector2(start, lighthouse.pos.angleDeg,1);
            Vector2 sight = lighthouse.GetSightPos(start, lighthouse.currentSight);
            double range = (sight.point - start).Length();
            Point2 sightSize = new Point2(9, 15) * lighthouse.ellipseSize.now* lighthouse.GetPartOfMax();
            for (int i = 0; i <= lighthouse.lastSight; i++)
            {
                if(cur.point.DistTo(sight.point) > sightSize.x/2)
                    frame.Add(new Sprite(ESprite.gunMarkDark, cur, new Point2(0.3,0.3)));

                cur = new Vector2(cur.point + new Vector2(new Point2(0, 0), cur.angleDeg, DLighthouse.distBetweenPoints).vector, cur.vector);

            }

            frame.Add(new Sprite(ESprite.AimEllipse, sight, sightSize));

            double indicatorHeight = 5d * lighthouse.ellipseTime.GetPart();

            frame.Add(new Sprite(ESprite.indicator, new Vector2(90, 92.5 - indicatorHeight/2, 0), new Point2(2, indicatorHeight), 2));
            frame.Add(new Sprite(ESprite.indicator, new Vector2(90, 90, 0), new Point2(2, 5), 3)); 
            for (int i = 0; i < lighthouse.sightCount; i++)
            {
                if (i != lighthouse.currentSight)
                {
                    frame.Add(new Sprite(ESprite.gunMarkBright, lighthouse.GetSightPos(start, i), new Point2(1.5, 1.5)));
                }
            }
        }
    }
}
