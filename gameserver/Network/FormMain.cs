using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using gameserver.Abstract;
using gameserver.Concrete;
using gameserver.Concrete.Miscellaneous;
using gameserver.Game;
using gameserver.Concrete.Configuration;
using Utils.Commands;

namespace gameserver
{
    public partial class FormMain : Form, IView
    {
        private static IServer server = Server.Instance;

        public event CommandSentHandler CommandSent;
        GameServer _gameServer;
        public FormMain()
        {
            InitializeComponent();
            server.ServerLogged += serverLogged;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            _gameServer = new GameServer();
            server.Start(this);
            TimerLoop.Interval = Settings.MainLoopInterval;
            TimerLoop.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }

        private void TBCommands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CommandSent(this,new CommandSentEventArgs(TBCommands.Text.Trim()));
                TBCommands.Text = String.Empty;
            }
        }

        private void serverLogged(object sender, ServerLoggedEventArgs e)
        {
            Utilities.Invoke(this,new Action(delegate()
                {
                    TBMain.Text += (TBMain.Text.Length > 0 ? Environment.NewLine : String.Empty) + e.Message;
                    TBMain.SelectionStart = TBMain.TextLength;
                    TBMain.ScrollToCaret();
                    updateStatusBar(e.PlayerCount);
                }));
        }

        private void updateStatusBar(int playerCount)
        {
            SBMain.Text = "Players online: " + playerCount;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Server s = server as Server;
            s.SendCommands(_gameServer.Process(s.GetCommands())); //подцепили mvc к нашему серваку
        }
    }
}