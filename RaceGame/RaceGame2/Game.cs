﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Numerics;

namespace RaceGame2
{
    public partial class Game : Form
    {
        Bitmap Backbuffer;
        public static List<Player> playerList = new List<Player>();
        public static List<Projectile> projectileList = new List<Projectile>();

        public Player p1;
        public Player p2;

        public Map map;

        public Game(String moederSelectP1, String moederSelectP2, String mapSelect)
        {
            // Window full screen
            this.WindowState = FormWindowState.Maximized;

            // Window style   
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1024, 768);

            // Selecteer moeder. 
            if(moederSelectP1 == "Aafke")
            {
                
            }
            if(moederSelectP2 == "Aafke")
            {

            }
            if(mapSelect == "MoederTrack")
            {
                map = new Map();
            }


            p1 = new Player(this);
            p1.posX = 100;
            p1.posY = 100;
            p1.accel = 0.1f;
            p1.maxSpeed = 6;

            p2 = new Player(this);
            p2.posX = 300;
            p2.posY = 200;
            p2.accel = 0.05f;
            p2.maxSpeed = 6;
            p2.keyLeft = Keys.A;
            p2.keyRight = Keys.D;
            p2.keyDown = Keys.S;
            p2.keyUp = Keys.W;
            p2.keySpecial = Keys.E;

            // Add players to List
            playerList.Add(p1);
            playerList.Add(p2);

            Start();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Player p in playerList)
            {
                if (e.KeyCode == p.keyRight) { p.isRight = true; }
                if (e.KeyCode == p.keyLeft) { p.left = true; }
                if (e.KeyCode == p.keyDown) { p.isDown = true; }
                if (e.KeyCode == p.keyUp) { p.isUp = true; }
                if (e.KeyCode == p.keySpecial) { p.isSpecial = true; }
            }
        }
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Player p in playerList)
            {
                if (e.KeyCode == p.keyRight) { p.isRight = false; }
                if (e.KeyCode == p.keyLeft) { p.left = false; }
                if (e.KeyCode == p.keyDown) { p.isDown = false; }
                if (e.KeyCode == p.keyUp) { p.isUp = false; }
                if (e.KeyCode == p.keySpecial) { p.isSpecial = false;}
            }
        }

        void Form1_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null) { Backbuffer.Dispose(); }
            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        void GameTimer_Tick(object sender, EventArgs e)
        {
            foreach (Player p in playerList)
            {
                p.Update(playerList, projectileList);
            }
            Invalidate();
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
                  
            //Tijd.Text = System.DateTime.Now.Second.ToString();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {        
            pictureBox1.Width = ClientSize.Width/2;
            pictureBox1.Height = ClientSize.Height;
            pictureBox1.Location = new Point(0,0);

            map.Draw(e.Graphics, p1, pictureBox1.Width / 2, pictureBox1.Height / 2);

            p1.Draw(e.Graphics, projectileList, pictureBox2.Width / 2, pictureBox2.Height / 2);
            p2.Draw(e.Graphics, projectileList,
                pictureBox2.Width / 2 + (p2.posX - p1.posX), 
                pictureBox2.Height / 2 + (p2.posY - p1.posY));     
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            pictureBox2.Width = ClientSize.Width / 2;
            pictureBox2.Height = ClientSize.Height;
            pictureBox2.Location = new Point(ClientSize.Width / 2, 0);

            map.Draw(e.Graphics,p2 ,pictureBox2.Width / 2, pictureBox2.Height / 2);

            p2.Draw(e.Graphics, projectileList, pictureBox2.Width/2, pictureBox2.Height/2);
            p1.Draw(e.Graphics, projectileList,
                pictureBox2.Width / 2 + (p1.posX - p2.posX), 
                pictureBox2.Height / 2 + (p1.posY - p2.posY));
        }

        // press escape to go back to menu
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                //Menu menuForm = new Menu();
                //menuForm.Show();
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Start()
        {

            // Game timer
            Timer GameTimer = new Timer();
            GameTimer.Interval = 1;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();

            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
