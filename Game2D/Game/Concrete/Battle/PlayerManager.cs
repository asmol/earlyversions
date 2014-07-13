using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Opengl;
using Utils.Commands;

namespace Game2D.Game.Concrete
{
    class PlayerManager
    {
        public void Process(List<Command> serverCom, DStateMain state)
        {
            foreach (Command c in serverCom)
            {
                if (c is ComAddPlayer)
                {
                    ComAddPlayer com = ((ComAddPlayer)c);
                    bool isNew = true;
                    foreach (var p in state.battle.players) if (p.id == com.playerID) isNew = false;
                    if (!isNew) continue;

                    state.battle.players.Add(CreateNewPlayer(com.playerID, com.nickname));
                }
                else if (c is ComRemovePlayer)
                {
                    throw new NotImplementedException(); //todo удалять игрока, в т.ч. если это мы
                }
                else if (c is ComConnectionResult)
                {
                    if (state.battle == null || state.battle.me == null)
                    {
                        state.battle = new DBattle();
                        ComConnectionResult com = ((ComConnectionResult)c);
                        DPlayer me = state.battle.me;
                        me = CreateNewPlayer(com.playerID, com.nickname);
                        me.controlled = true;
                        me.tank.controlled = true;
                        state.battle.players.Add(me);
                    }

                }
            }
        }

        DPlayer CreateNewPlayer(int id, string nickname)
        {
            //todo конечно, засунуть танк подальше до первого обновления координат не очень хорошая идея
            DPlayer player = new DPlayer(id, nickname, false, new DTank());
            player.tank.ID = id;
            player.tank.controlled = player.controlled;      
            player.tank.body = new DTankBody(new Vector2(-100, -100, 0), ESprite.tank0);
            return player;
        }
    }
}
