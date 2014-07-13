using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameserver.Abstract
{
    public delegate void CommandSentHandler(object sender, CommandSentEventArgs e);

    public class CommandSentEventArgs : EventArgs
    {
        public string Command;

        public CommandSentEventArgs(string command)
        {
            Command = command;
        }
    }

    interface IView
    {
        event CommandSentHandler CommandSent;
    }
}