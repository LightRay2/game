using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Opengl;
using Game2D.Game.Concrete;
using Game2D.Game.DataClasses;
using Game2D.Game.Helpers;
using Utils.Commands;

namespace Game2D.Game
{
    class Game:IGame
    {
        //компоненты
        MenuMain _menuMain = new MenuMain();
        TankDriverSimple _tankDriver = new TankDriverSimple();
        NetworkController _networkController = new NetworkController();
        PlayerManager _playerManaged = new PlayerManager();

        //данные
        DStateMain _state = new DStateMain();

        List<byte> symbols = new List<byte>();
        string s = "";
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
            }
            frame.Add(new Sprite(ESprite.tank2, new Vector2(5, 5, 30), new Point2(8, 4)));
            //-------------------------------------------------------------------------------------
            _networkController.SendCommands(createdCommands);
            return frame;
        }
    }
}
