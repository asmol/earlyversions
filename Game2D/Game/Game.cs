using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Opengl;
using Game2D.Game.Concrete;
using Game2D.Game.Abstract;
using Game2D.Game.DataClasses;
using Game2D.Game.Helpers;
using Game2D.Game.DataClasses.Commands;

namespace Game2D.Game
{
    class Game:IGame
    {
        MenuMain _menuMain;
        TankDriverSimple _tankDriver = new TankDriverSimple();
        NetworkController _networkController = new NetworkController();
        PlayerManager _playerManaged = new PlayerManager();

        ConnectionInfo _connectionInfo;
        BattleConfig _battleConfig;
        List<Player> _players;
        Player _me;

        public Game()
        {
            _connectionInfo = new ConnectionInfo();
            _battleConfig = new BattleConfig();
            _players = null;

            _menuMain = new MenuMain();
        }
        
        public Frame Process(IGetKeyboardState keyboard)
        {

            Frame frame = new Frame();

            List<Command> serverCommands = _networkController.GetCommands(_connectionInfo);
            List<Command> createdCommands = new List<Command>();

            if(_connectionInfo.lastResult != ConnectionInfo.EState.connected)
                _menuMain.Process(ref frame,keyboard, _connectionInfo, _battleConfig);
            if (_me != null)
            {
                createdCommands.AddRange(
                    _tankDriver.Process(ref frame, keyboard, serverCommands,
                    _me.tank.move, HDataExcractor.GetDMoveDictionary(_players))
                    );
            }
            _playerManaged.Process(serverCommands, _players,_me, _connectionInfo);

            _networkController.SendCommands(createdCommands, _connectionInfo);
            return frame;
        }
    }
}
