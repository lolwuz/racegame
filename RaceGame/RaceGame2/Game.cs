using System;
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

        public Map map;
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        public Game(String moederSelectP1, String moederSelectP2, String mapSelect)
        {
            // Window full screen
            // this.WindowState = FormWindowState.Maximized;
            // Window style   
            

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1024, 768);


            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);

            DoubleBuffered = true;

            // Selecteer moeder. 
            if (moederSelectP1 == "Aafke")
            {

            }
            if (moederSelectP2 == "Aafke")
            {

            }
            if (mapSelect == "MoederTrack")
            {
                map = new Map();
            }


            p1 = new Player(this, 1);
            p1.posX = 1300;
            p1.posY = 1690;
            p1.accel = 0.1f;
            p1.maxSpeed = 8;

            p2 = new Player(this, 2);
            p2.posX = 1300;
            p2.posY = 1760;
            p2.accel = 0.1f;
            p2.maxSpeed = 8;
            p2.keyLeft = Keys.A;
            p2.keyRight = Keys.D;
            p2.keyDown = Keys.S;
            p2.keyUp = Keys.W;
            p2.keySpecial = Keys.E;

            p3 = new Player(this, 3);
            p3.posX = 1150;
            p3.posY = 1690;
            p3.accel = 0.1f;
            p3.maxSpeed = 8;
            p3.keyLeft = Keys.F;
            p3.keyRight = Keys.H;
            p3.keyDown = Keys.G;
            p3.keyUp = Keys.T;
            p3.keySpecial = Keys.Y;

            p4 = new Player(this, 4);
            p4.posX = 1150;
            p4.posY = 1760;
            p4.accel = 0.1f;
            p4.maxSpeed = 8;
            p4.keyLeft = Keys.J;
            p4.keyRight = Keys.L;
            p4.keyDown = Keys.K;
            p4.keyUp = Keys.I;
            p4.keySpecial = Keys.O;

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

            Speler1Ronde.Text = "Ronde: " + p1.round + "/5";
            Speler2Ronde.Text = "Ronde: " + p2.round + "/5";
            Speler3Ronde.Text = "Ronde: " + p3.round + "/5";
            Speler4Ronde.Text = "Ronde: " + p4.round + "/5";

            Speler1Speed.Text = "Snelheid: " + p1.displaySpeed + "km/h";
            Speler2Speed.Text = "Snelheid: " + p2.displaySpeed + "km/h";
            Speler3Speed.Text = "Snelheid: " + p3.displaySpeed + "km/h";
            Speler4Speed.Text = "Snelheid: " + p4.displaySpeed + "km/h";

            fuel1.Text = "Fuel: " + Convert.ToInt16(p1.fuel) + " liter";
            fuel2.Text = "Fuel: " + Convert.ToInt16(p2.fuel) + " liter";
            fuel3.Text = "Fuel: " + Convert.ToInt16(p3.fuel) + " liter";
            fuel4.Text = "Fuel: " + Convert.ToInt16(p4.fuel) + " liter";
            gameUpdate();
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
    }
}
