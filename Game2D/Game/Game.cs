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

        
        public Frame Process(IGetKeyboardState keyboard)
        {

            Frame frame = new Frame();

            List<Command> serverCommands = _networkController.GetCommands();
            List<Command> createdCommands = new List<Command>();

            if(_state.state == DStateMain.EState.local)
                _menuMain.Process(ref frame,keyboard, _state);

            if (_state.wish == DStateMain.EWish.joinServer)
                _networkController.Connect(_state);
            /*if (_me != null)
            {
                createdCommands.AddRange(
                    _tankDriver.Process(ref frame, keyboard, serverCommands,
                    _me.tank.move, HDataExcractor.GetDMoveDictionary(_players))
                    );
            }*/
            if(_state.state == DStateMain.EState.inBattle) 
                _playerManaged.Process(serverCommands, _state);

            _networkController.SendCommands(createdCommands);
            return frame;
        }
    }
}
