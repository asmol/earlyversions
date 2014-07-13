using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Game.Abstract;
using Game2D.Opengl;
using Game2D.Game.DataClasses.Commands;

namespace Game2D.Game.Concrete
{
    class PlayerManager
    {
        public void Process(List<Command> serverCom, List<Player> players, ref Player me, ConnectionInfo connectionInfo)
            
        {
            foreach (Command c in serverCom)
            {
                if (c is ComAddPlayer)
                {
                    if (me == null) continue; //todo andrey сюда раньше приходим почему то, чем приконнектились
                    ComAddPlayer com = ((ComAddPlayer)c);
                    bool isNew = true;
                    foreach (var p in players) if (p.id == com.playerID) isNew = false;
                    if (!isNew) continue;

                   players.Add(CreateNewPlayer(com.playerID, com.nickname));

                    
                    

                }
                else if (c is ComRemovePlayer)
                {
                    ComRemovePlayer com = ((ComRemovePlayer)c);
                    for (int i = 0; i < players.Count; i++)
                        if (players[i].id == com.playerID)
                            players.RemoveAt(i--);
                            
                }
                else if (c is ComConnectionResult)
                {
                    ComConnectionResult com = ((ComConnectionResult)c);
                    if (me == null)
                    {
                        me = CreateNewPlayer(com.playerID, com.nickname);
                        me.controlled = true;
                        me.tank.controlled = true;
                        players.Add(me);
                    }

                }
            }
        }

        Player CreateNewPlayer(int id, string nickname)
        {
            //todo конечно, засунуть танк подальше до первого обновления координат не очень хорошая идея
            Player player = new Player(id, nickname, false, new Tank());
            player.tank.ID = id;
            player.tank.controlled = player.controlled;      //todo сравнивать по конн инфо ну очень криво
            player.tank.move = new DMove(new Vector2(-100, -100, 0), ESprite.tank0);
            return player;
        }
    }
}
