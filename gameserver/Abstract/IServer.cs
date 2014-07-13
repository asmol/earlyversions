using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameserver.Abstract
{
    public delegate void ServerLoggedHandler(object sender, ServerLoggedEventArgs e);

    public class ServerLoggedEventArgs : EventArgs
    {
        public string Message;
        public int PlayerCount;

        public ServerLoggedEventArgs(string message, int playerCount)
        {
            Message = message;
            PlayerCount = playerCount;
        }
    }

    public enum EServerMessage
    {
        ConnectionResult,
        PlayerConnected,
        PlayerMoved,
        PlayerDied,
        MovePointChanged,
        PlayerWrote
    }

    public enum EConnectionResult
    {
        Success,
        ServerIsFull
    }

    interface IServer
    {
        event ServerLoggedHandler ServerLogged;

        void Start(IView view);
        void Stop();
    }
}