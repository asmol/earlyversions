﻿namespace gameserver
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TBMain = new System.Windows.Forms.TextBox();
            this.TBCommands = new System.Windows.Forms.TextBox();
            this.SBMain = new System.Windows.Forms.StatusBar();
            this.TimerLoop = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TBMain
            // 
            this.TBMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TBMain.BackColor = System.Drawing.SystemColors.Window;
            this.TBMain.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TBMain.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TBMain.Location = new System.Drawing.Point(0, 0);
            this.TBMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBMain.Multiline = true;
            this.TBMain.Name = "TBMain";
            this.TBMain.ReadOnly = true;
            this.TBMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBMain.Size = new System.Drawing.Size(766, 322);
            this.TBMain.TabIndex = 1;
            // 
            // TBCommands
            // 
            this.TBCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TBCommands.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TBCommands.Location = new System.Drawing.Point(0, 325);
            this.TBCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBCommands.Name = "TBCommands";
            this.TBCommands.Size = new System.Drawing.Size(766, 30);
            this.TBCommands.TabIndex = 0;
            this.TBCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBCommands_KeyDown);
            // 
            // SBMain
            // 
            this.SBMain.Location = new System.Drawing.Point(0, 368);
            this.SBMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SBMain.Name = "SBMain";
            this.SBMain.Size = new System.Drawing.Size(780, 34);
            this.SBMain.TabIndex = 2;
            // 
            // TimerLoop
            // 
            this.TimerLoop.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 402);
            this.Controls.Add(this.SBMain);
            this.Controls.Add(this.TBCommands);
            this.Controls.Add(this.TBMain);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(788, 435);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBMain;
        private System.Windows.Forms.TextBox TBCommands;
        private System.Windows.Forms.StatusBar SBMain;
        private System.Windows.Forms.Timer TimerLoop;
    }
}

