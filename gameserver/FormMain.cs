using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameserver
{
    public partial class FormMain : Form, IView
    {
        private static IServer server = Server.Instance;

        public event CommandSentHandler CommandSent;

        public FormMain()
        {
            InitializeComponent();
            server.ServerLogged += serverLogged;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            server.Start(this);
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
            SBMain.Text = "Players online: " + playerCount + "/" + Settings.MaxPlayers;
        }
    }
}