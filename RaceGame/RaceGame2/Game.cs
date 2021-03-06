﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RaceGame2
{
    public partial class Game : Form
    {
        public List<Player> playerList = new List<Player>();
        public List<Projectile> projectileList = new List<Projectile>();
        public List<Oil> oilList = new List<Oil>();

        public Player p1;
        public Player p2;
        public Player p3;
        public Player p4;

        private bool isStarted = false;

        private int car1, car2, car3, car4;

        public Map map;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        public Game(int moederSelectP1, int moederSelectP2, int moederSelectP3, int moederSelectP4, int mapSelect)
        {
            //Window full screen
            this.WindowState = FormWindowState.Maximized;
            // Window style   
            

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1024, 768);


            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);

            DoubleBuffered = true;

            // Selecteer auto voor moeder1 
            switch (moederSelectP1)
            {
                case 1:
                    // gebruik MartenMoeder
                    this.car1 = 1;
                    break;
                case 2:
                    // gebruik MoederJorrit
                    this.car1 = 2;
                    break;
                case 3:
                    // gebruik MoederKoen
                    this.car1 = 3;
                    break;
                case 4:
                    // gebruik MoederSimon
                    this.car1 = 4;
                    break;
                default:
                    break;
            }

            // Selecter auto voor moeder2
            switch (moederSelectP2)
            {
                case 1:
                    // gebruik MartenMoeder
                    this.car2 = 1;
                    break;
                case 2:
                    // gebruik MoederJorrit
                    this.car2 = 2;
                    break;
                case 3:
                    // gebruik MoederKoen
                    this.car2 = 3;
                    break;
                case 4:
                    // gebruik MoederSimon
                    this.car2 = 4;
                    break;
                default:
                    break;
            }

            // Selecter auto voor moeder3
            switch (moederSelectP3)
            {
                case 1:
                    // gebruik MartenMoeder
                    this.car3 = 1;
                    break;
                case 2:
                    // gebruik MoederJorrit
                    this.car3 = 2;
                    break;
                case 3:
                    // gebruik MoederKoen
                    this.car3 = 3;
                    break;
                case 4:
                    // gebruik MoederSimon
                    this.car3 = 4;
                    break;
                default:
                    break;
            }

            // Selecter auto voor moeder4
            switch (moederSelectP4)
            {
                case 1:
                    // gebruik MartenMoeder                    
                    this.car4 = 1;
                    break;
                case 2:
                    // gebruik MoederJorrit
                    this.car4 = 2;
                    break;
                case 3:
                    // gebruik MoederKoen
                    this.car4 = 3;
                    break;
                case 4:
                    // gebruik MoederSimon
                    this.car4 = 4;
                    break;
                default:
                    break;
            }

            

            

            // Selecteer map
            switch (mapSelect)
            {
                case 1:            
                    map = new Map(this, 1);
                    break;
                case 2:
                    map = new Map(this, 2);
                    break;
                case 3:
                    map = new Map(this, 2);
                    break;
                default:
                    break;
            }

            p1 = new Player(this, 1, car1);
            p1.posX = 1300;
            p1.posY = 1690;
            p1.accel = 0.1f;
            p1.maxSpeed = 4;

            p2 = new Player(this, 2, car2);
            p2.posX = 1300;
            p2.posY = 1760;
            p2.accel = 0.1f;
            p2.maxSpeed = 4;
            p2.keyLeft = Keys.A;
            p2.keyRight = Keys.D;
            p2.keyDown = Keys.S;
            p2.keyUp = Keys.W;
            p2.keySpecial = Keys.E;

            p3 = new Player(this, 3, car3);
            p3.posX = 1150;
            p3.posY = 1690;
            p3.accel = 0.1f;
            p3.maxSpeed = 4;
            p3.keyLeft = Keys.F;
            p3.keyRight = Keys.H;
            p3.keyDown = Keys.G;
            p3.keyUp = Keys.T;
            p3.keySpecial = Keys.Y;

            p4 = new Player(this, 4, car4);
            p4.posX = 1150;
            p4.posY = 1760;
            p4.accel = 0.1f;
            p4.maxSpeed = 4;
            p4.keyLeft = Keys.J;
            p4.keyRight = Keys.L;
            p4.keyDown = Keys.K;
            p4.keyUp = Keys.I;
            p4.keySpecial = Keys.O;

            switch (mapSelect)
            {
                case 1:
                    p1.posX = 325;
                    p1.posY = 1150;
                    p1.rotation = 90;

                    p2.posX = 255;
                    p2.posY = 1150;
                    p2.rotation = 90;

                    p3.posX = 325;
                    p3.posY = 1025;
                    p3.rotation = 90;

                    p4.posX = 255;
                    p4.posY = 1025;
                    p4.rotation = 90;

                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                default:
                    break;
            }

            // Add players to List
            playerList.Add(p1);
            playerList.Add(p2);
            playerList.Add(p3);
            playerList.Add(p4);


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
                if (e.KeyCode == p.keySpecial) { p.isSpecial = false; }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Width = ClientSize.Width / 2;
            pictureBox1.Height = ClientSize.Height / 2;
            pictureBox1.Location = new Point(0, 0);
            map.Draw(e.Graphics, p1, pictureBox1.Width / 2, pictureBox1.Height / 2);

            p1.Draw(e.Graphics, projectileList, pictureBox2.Width / 2, pictureBox2.Height / 2);
            foreach (Player p in playerList)
            {
                if (p != p1)
                {
                    p.Draw(e.Graphics, projectileList, pictureBox1.Width / 2 + (p.posX - p1.posX), pictureBox1.Height / 2 + (p.posY - p1.posY));
                }
            }
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            pictureBox2.Width = ClientSize.Width / 2;
            pictureBox2.Height = ClientSize.Height / 2;
            pictureBox2.Location = new Point(ClientSize.Width / 2, 0);
            map.Draw(e.Graphics, p2, pictureBox2.Width / 2, pictureBox2.Height / 2);

            p2.Draw(e.Graphics, projectileList, pictureBox2.Width / 2, pictureBox2.Height / 2);

            foreach (Player p in playerList)
            {
                if (p != p2)
                {
                    p.Draw(e.Graphics, projectileList, pictureBox2.Width / 2 + (p.posX - p2.posX), pictureBox2.Height / 2 + (p.posY - p2.posY));
                }
            }
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            pictureBox3.Width = ClientSize.Width / 2;
            pictureBox3.Height = ClientSize.Height / 2;
            pictureBox3.Location = new Point(0, Convert.ToInt16(ClientSize.Height / 2));
            map.Draw(e.Graphics, p3, pictureBox1.Width / 2, pictureBox1.Height / 2);

            p3.Draw(e.Graphics, projectileList, pictureBox3.Width / 2, pictureBox3.Height / 2);

            foreach (Player p in playerList)
            {
                if (p != p3)
                {
                    p.Draw(e.Graphics, projectileList, pictureBox3.Width / 2 + (p.posX - p3.posX), pictureBox3.Height / 2 + (p.posY - p3.posY));
                }
            }
        }
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            pictureBox4.Width = ClientSize.Width / 2;
            pictureBox4.Height = ClientSize.Height / 2;
            pictureBox4.Location = new Point(ClientSize.Width / 2, Convert.ToInt16(ClientSize.Height / 2));
            map.Draw(e.Graphics, p4, pictureBox4.Width / 2, pictureBox4.Height / 2);

            p4.Draw(e.Graphics, projectileList, pictureBox4.Width / 2, pictureBox4.Height / 2);

            foreach (Player p in playerList)
            {
                if (p != p4)
                {
                    p.Draw(e.Graphics, projectileList, pictureBox4.Width / 2 + (p.posX - p4.posX), pictureBox4.Height / 2 + (p.posY - p4.posY));
                }
            }
        }

        public void Start()
        {
            //CheckForIllegalCrossThreadCalls = false;
            // var renderThread = new Thread(Render);
            // InitializeComponent();
            // renderThread.Start();
            //InitializeComponent();

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 1000 / 120; // als de paint event niet zo sloom zou zijn is dit nu 120 FPS. 
            myTimer.Start();
            InitializeComponent();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            // Hier gaat de boel sloom
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
            pictureBox4.Invalidate();
            Update();

            // Geen impact op performance. Label voor fuel enzo: 
            Speler1Ronde.Location = new Point(0, 20);
            Speler2Ronde.Location = new Point(ClientSize.Width - (Speler1Ronde.Width), 20);
            Speler3Ronde.Location = new Point(0, ClientSize.Height / 2 + 20); 
            Speler4Ronde.Location = new Point(ClientSize.Width - (Speler4Ronde.Width), (ClientSize.Height / 2) + 20);

            fuel1.Location = new Point(0, 50);
            fuel2.Location = new Point(ClientSize.Width - (fuel2.Width), 50);
            fuel3.Location = new Point(0, ClientSize.Height / 2 + 50);
            fuel4.Location = new Point(ClientSize.Width - (fuel4.Width), (ClientSize.Height / 2) + 50);

            Speler1Speed.Location = new Point(0, (ClientSize.Height / 2) - 50);
            Speler2Speed.Location = new Point(ClientSize.Width - (Speler2Speed.Width), (ClientSize.Height / 2) - 50);
            Speler3Speed.Location = new Point(0, ClientSize.Height - 50);
            Speler4Speed.Location = new Point(ClientSize.Width - (Speler4Speed.Width), ClientSize.Height - 50);

            equiped1.Location = new Point(0, (ClientSize.Height / 2) - 100);
            equiped2.Location = new Point(ClientSize.Width - (equiped2.Width), (ClientSize.Height / 2) - 100);
            equiped3.Location = new Point(0, ClientSize.Height - 100);
            equiped4.Location = new Point(ClientSize.Width - (equiped4.Width), ClientSize.Height -100);

            stoplicht.Location = new Point(ClientSize.Width / 2 - stoplicht.Width / 2, ClientSize.Height / 2 - stoplicht.Height / 2);
            
            Speler1Ronde.Text = "Ronde: " + p1.round + "/5";
            Speler2Ronde.Text = "Ronde: " + p2.round + "/5";
            Speler3Ronde.Text = "Ronde: " + p3.round + "/5";
            Speler4Ronde.Text = "Ronde: " + p4.round + "/5";

            Speler1Speed.Text = "Snelheid: " + p1.displaySpeed + "km/h";
            Speler2Speed.Text = "Snelheid: " + p2.displaySpeed + "km/h";
            Speler3Speed.Text = "Snelheid: " + p3.displaySpeed + "km/h";
            Speler4Speed.Text = "Snelheid: " + p4.displaySpeed + "km/h";
                        
            equiped1.Image = getEquiped(p1.equiped);
            equiped2.Image = getEquiped(p2.equiped);
            equiped3.Image = getEquiped(p3.equiped);
            equiped4.Image = getEquiped(p4.equiped);

            fuel1.Text = "Fuel: " + Convert.ToInt16(p1.fuel) + " liter";
            fuel2.Text = "Fuel: " + Convert.ToInt16(p2.fuel) + " liter";
            fuel3.Text = "Fuel: " + Convert.ToInt16(p3.fuel) + " liter";
            fuel4.Text = "Fuel: " + Convert.ToInt16(p4.fuel) + " liter";

            // speler 1 wins
            if (p1.round.Equals(5))
            {                
                lblWinner.Text = "De winnaar is moeder 1!";
                lblWinner.Visible = true;
                lblWinner.Location = new Point(ClientSize.Width / 2 - lblWinner.Width / 2, ClientSize.Height / 2 - lblWinner.Height / 2);
                isStarted = false;
            }

            // speler 2 wins
            if (p2.round.Equals(5))
            {
                lblWinner.Text = "De winnaar is moeder 2!";
                lblWinner.Visible = true;
                lblWinner.Location = new Point(ClientSize.Width / 2 - lblWinner.Width / 2, ClientSize.Height / 2 - lblWinner.Height / 2);
                isStarted = false;
            }

            //speler 3 wins
            if (p3.round.Equals(5))
            {
                lblWinner.Text = "De winnaar is moeder 3!";
                lblWinner.Visible = true;
                lblWinner.Location = new Point(ClientSize.Width / 2 - lblWinner.Width / 2, ClientSize.Height / 2 - lblWinner.Height / 2);
                isStarted = false;
            }

            //speler 4 wins
            if (p4.round.Equals(5))
            {
                lblWinner.Text = "De winnaar is moeder 4!";
                lblWinner.Visible = true;
                lblWinner.Location = new Point(ClientSize.Width / 2 - lblWinner.Width / 2, ClientSize.Height / 2 - lblWinner.Height / 2);
                isStarted = false;
            }

            if (isStarted)
            {
                gameUpdate();
            }
            else
            {
                starter.Start();
            }
            
        }

        int counter = 0;
        private void starter_Tick(object sender, EventArgs e)
        {
            if (counter == 4)
            {
                starter.Stop();
            }

            if (counter == 0)
            {
                stoplicht.Image = Properties.Resources.roodlicht;
            }
            else if (counter == 1)
            {
                stoplicht.Image = Properties.Resources.oranje_licht;
            } 
            else if (counter == 2)
            {
                stoplicht.Image = Properties.Resources.groenlicht;
            } 
            else if (counter == 3)
            {
                stoplicht.Visible = false;
                isStarted = true;
            }
            counter++;
        }

        void gameUpdate()
        {
            foreach (Player p in playerList)
            {
                p.Update();
            }
             
            foreach (Projectile projectile in projectileList)
            {
                projectile.Update();       
            }                    
        }
        
        private Image getEquiped (string equiped)
        {
            switch (equiped)
            {
                case "speed":
                    return Properties.Resources.speedboost;
                case "fueldrain":
                    return Properties.Resources.fueldrain;
                case "projectile":
                    return Properties.Resources.projectile;
                case "oil":
                    return Properties.Resources.oilleak;
                default:
                    return null;
            }
        }

        // press escape to go back to menu
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Menu menuForm = new Menu();
                menuForm.Show();
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Game_Load(object sender, EventArgs e)
        {
            // Geen impact op performance. Label voor fuel enzo: 
            Speler1Ronde.Location = new Point(0, 20);
            Speler2Ronde.Location = new Point(ClientSize.Width - (Speler1Ronde.Width), 20);
            Speler3Ronde.Location = new Point(0, ClientSize.Height / 2 + 20);
            Speler4Ronde.Location = new Point(ClientSize.Width - (Speler4Ronde.Width), (ClientSize.Height / 2) + 20);

            fuel1.Location = new Point(0, 50);
            fuel2.Location = new Point(ClientSize.Width - (fuel2.Width), 50);
            fuel3.Location = new Point(0, ClientSize.Height / 2 + 50);
            fuel4.Location = new Point(ClientSize.Width - (fuel4.Width), (ClientSize.Height / 2) + 50);

            Speler1Speed.Location = new Point(0, (ClientSize.Height / 2) - 50);
            Speler2Speed.Location = new Point(ClientSize.Width - (Speler2Speed.Width), (ClientSize.Height / 2) - 50);
            Speler3Speed.Location = new Point(0, ClientSize.Height - 50);
            Speler4Speed.Location = new Point(ClientSize.Width - (Speler4Speed.Width), ClientSize.Height - 50);

            equiped1.Location = new Point(0, (ClientSize.Height / 2) - 100);
            equiped2.Location = new Point(ClientSize.Width - (equiped2.Width), (ClientSize.Height / 2) - 100);
            equiped3.Location = new Point(0, ClientSize.Height - 100);
            equiped4.Location = new Point(ClientSize.Width - (equiped4.Width), ClientSize.Height - 100);

            stoplicht.Location = new Point(ClientSize.Width / 2 - stoplicht.Width / 2, ClientSize.Height / 2 - stoplicht.Height / 2);
        }
    }
}
