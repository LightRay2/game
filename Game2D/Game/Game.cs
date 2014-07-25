using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Opengl;
using Game2D.Game.Concrete;
using Game2D.Game.DataClasses;
using Game2D.Game.Helpers;
using Utils.Commands;
using Game2D.Game.Concrete.Battle;

namespace Game2D.Game
{
    class Game:IGame
    {
        //компоненты
        MenuMain _menuMain = new MenuMain();
        TankDriverSimple _tankDriver = new TankDriverSimple();
        NetworkController _networkController = new NetworkController();
        PlayerManager _playerManaged = new PlayerManager();
        ShootingMain _shootingMain = new ShootingMain();

        //данные
        DStateMain _state = new DStateMain();

        public Frame Process(IGetKeyboardState keyboard)
        {

            Frame frame = new Frame();

            List<Command> serverCommands = _networkController.GetCommands();
            List<Command> createdCommands = new List<Command>();

            if(_state.state == DStateMain.EState.local)
                _menuMain.Process(ref frame,keyboard, _state);

            if (_state.wish == DStateMain.EWish.joinServer)
                _networkController.Connect(_state);
            //-----------------тут все игровые компоненты должны быть---------------------------------
            if (_state.state == DStateMain.EState.inBattle)
            {
                _playerManaged.Process(serverCommands, _state, ref frame);
                _shootingMain.Process(serverCommands, _state, keyboard, ref frame);
            }
            //-------------------------------------------------------------------------------------
            _networkController.SendCommands(createdCommands);
            return frame;
        }
    }
}
