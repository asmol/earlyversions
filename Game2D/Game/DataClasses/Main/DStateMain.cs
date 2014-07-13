using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class DStateMain
    {
        public enum EState { local, inBattle}
        public enum EWish { none, joinServer, joinBattle, leaveBattle, leaveServer }
        //По этим состояниям смотрим, какие из компонентов игры задействовать
        public EState state = EState.local;
        public EWish wish = EWish.none;

        //основное содержимое
        public ConnectionInfo connectionInfo = new ConnectionInfo();
        public BattleConfig battleConfig = new BattleConfig();
        public DBattle battle = null;
    }
}
