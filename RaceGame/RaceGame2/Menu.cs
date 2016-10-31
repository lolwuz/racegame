using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaceGame2
{
    public partial class Menu : Form
    {

        private int car1, car2, car3, car4, track;

        public Menu()
        {
            InitializeComponent();

            // background
            Bitmap BackImg = Properties.Resources.menu_background;
            Bitmap BackBmp = new Bitmap(BackImg.Width, BackImg.Height);
            Graphics memoryGraphics = Graphics.FromImage(BackBmp);
            memoryGraphics.DrawImage(BackImg, 0, 0, BackImg.Width, BackImg.Height);
            BackgroundImage = BackBmp;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            // maximize
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // default auto's en racetrack in picture boxes...

            picBoxCar1.Image = Properties.Resources.MartenMoeder;
            this.car1 = 1;

            picBoxCar2.Image = Properties.Resources.MoederJorrit;
            this.car2 = 2;

            picBoxCar3.Image = Properties.Resources.MoederKoen;
            this.car3 = 3;

            picBoxCar4.Image = Properties.Resources.MoederSimon;
            this.car4 = 4;

            picBoxRaceTrack.Image = Properties.Resources.baan1_thumb;
            this.track = 1;

        }

        // press escape to close menu
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void lblStartGame_MouseEnter(object sender, EventArgs e) { lblStartGame.Font = new Font(lblStartGame.Font, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline); }

        private void lblStartGame_MouseLeave(object sender, EventArgs e) { lblStartGame.Font = new Font(lblStartGame.Font, FontStyle.Bold | FontStyle.Italic); }

        private void lblStartGame_Click(object sender, EventArgs e)
        {
            Game gameForm = new Game(car1, car2, car3, car4, track);
            gameForm.Show();
            this.Close();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        // auto 1 kiezen
        private void lblCar1Back_Click(object sender, EventArgs e)
        {
            switch (this.car1)
            {
                case 1:
                    this.car1 = 4;
                    picBoxCar1.Image = Properties.Resources.MoederSimon;
                    break;
                case 2:
                    this.car1 = 1;
                    picBoxCar1.Image = Properties.Resources.MartenMoeder;
                    break;
                case 3:
                    this.car1 = 2;
                    picBoxCar1.Image = Properties.Resources.MoederJorrit;
                    break;
                case 4:
                    this.car1 = 3;
                    picBoxCar1.Image = Properties.Resources.MoederKoen;
                    break;
                default:
                    break;
            }
        }

        private void lblCar1Next_Click(object sender, EventArgs e)
        {
            switch (this.car1)
            {
                case 1:
                    this.car1 = 2;
                    picBoxCar1.Image = Properties.Resources.MoederJorrit;
                    break;
                case 2:
                    this.car1 = 3;
                    picBoxCar1.Image = Properties.Resources.MoederKoen;
                    break;
                case 3:
                    this.car1 = 4;
                    picBoxCar1.Image = Properties.Resources.MoederSimon;
                    break;
                case 4:
                    this.car1 = 1;
                    picBoxCar1.Image = Properties.Resources.MartenMoeder;
                    break;
                default:
                    break;
            }
        }

        // race baan kiezen
        private void lblTrackBack_Click(object sender, EventArgs e)
        {
            switch (this.track)
            {
                case 1:
                    this.track = 3;
                    //picBoxRaceTrack.Image = Properties.Resources.baan3_thumb;
                    break;
                case 2:
                    this.track = 1;
                    picBoxRaceTrack.Image = Properties.Resources.baan1_thumb;
                    break;
                case 3:
                    this.track = 2;
                    picBoxRaceTrack.Image = Properties.Resources.baan2_thumb;
                    break;
                default:
                    break;
            }
        }

        private void lblTrackNext_Click(object sender, EventArgs e)
        {
            switch (this.track)
            {
                case 1:
                    this.track = 2;
                    picBoxRaceTrack.Image = Properties.Resources.baan2_thumb;
                    break;
                case 2:
                    this.track = 3;
                    //picBoxRaceTrack.Image = Properties.Resources.baan3_thumb;
                    break;
                case 3:
                    this.track = 1;
                    picBoxRaceTrack.Image = Properties.Resources.baan1_thumb;
                    break;
                default:
                    break;
            }
        }

        // auto 2 kiezen
        private void lblCar2Back_Click(object sender, EventArgs e)
        {
            switch (this.car2)
            {
                case 1:
                    this.car2 = 4;
                    picBoxCar2.Image = Properties.Resources.MoederSimon;
                    break;
                case 2:
                    this.car2 = 1;
                    picBoxCar2.Image = Properties.Resources.MartenMoeder;
                    break;
                case 3:
                    this.car2 = 2;
                    picBoxCar2.Image = Properties.Resources.MoederJorrit;
                    break;
                case 4:
                    this.car2 = 3;
                    picBoxCar2.Image = Properties.Resources.MoederKoen;
                    break;
                default:
                    break;
            }
        }

        private void lblCar2Next_Click(object sender, EventArgs e)
        {
            switch (this.car2)
            {
                case 1:
                    this.car2 = 2;
                    picBoxCar2.Image = Properties.Resources.MoederJorrit;
                    break;
                case 2:
                    this.car2 = 3;
                    picBoxCar2.Image = Properties.Resources.MoederKoen;
                    break;
                case 3:
                    this.car2 = 4;
                    picBoxCar2.Image = Properties.Resources.MoederSimon;
                    break;
                case 4:
                    this.car2 = 1;
                    picBoxCar2.Image = Properties.Resources.MartenMoeder;
                    break;
                default:
                    break;
            }
        }
                
        // auto 3 kiezen
        private void lblCar3Back_Click(object sender, EventArgs e)
        {
            switch (this.car3)
            {
                case 1:
                    this.car3 = 4;
                    picBoxCar3.Image = Properties.Resources.MoederSimon;
                    break;
                case 2:
                    this.car3 = 1;
                    picBoxCar3.Image = Properties.Resources.MartenMoeder;
                    break;
                case 3:
                    this.car3 = 2;
                    picBoxCar3.Image = Properties.Resources.MoederJorrit;
                    break;
                case 4:
                    this.car3 = 3;
                    picBoxCar3.Image = Properties.Resources.MoederKoen;
                    break;
                default:
                    break;
            }
        }

        private void lblCar3Next_Click(object sender, EventArgs e)
        {
            switch (this.car3)
            {
                case 1:
                    this.car3 = 2;
                    picBoxCar3.Image = Properties.Resources.MoederJorrit;
                    break;
                case 2:
                    this.car3 = 3;
                    picBoxCar3.Image = Properties.Resources.MoederKoen;
                    break;
                case 3:
                    this.car3 = 4;
                    picBoxCar3.Image = Properties.Resources.MoederSimon;
                    break;
                case 4:
                    this.car3 = 1;
                    picBoxCar3.Image = Properties.Resources.MartenMoeder;
                    break;
                default:
                    break;
            }
        }

        // auto 4 kiezen
        private void lblCar4Back_Click(object sender, EventArgs e)
        {
            switch (this.car4)
            {
                case 1:
                    this.car4 = 4;
                    picBoxCar4.Image = Properties.Resources.MoederSimon;
                    break;
                case 2:
                    this.car4 = 1;
                    picBoxCar4.Image = Properties.Resources.MartenMoeder;
                    break;
                case 3:
                    this.car4 = 2;
                    picBoxCar4.Image = Properties.Resources.MoederJorrit;
                    break;
                case 4:
                    this.car4 = 3;
                    picBoxCar4.Image = Properties.Resources.MoederKoen;
                    break;
                default:
                    break;
            }
        }

        private void lblCar4Next_Click(object sender, EventArgs e)
        {
            switch (this.car4)
            {
                case 1:
                    this.car4 = 2;
                    picBoxCar4.Image = Properties.Resources.MoederJorrit;
                    break;
                case 2:
                    this.car4 = 3;
                    picBoxCar4.Image = Properties.Resources.MoederKoen;
                    break;
                case 3:
                    this.car4 = 4;
                    picBoxCar4.Image = Properties.Resources.MoederSimon;
                    break;
                case 4:
                    this.car4 = 1;
                    picBoxCar4.Image = Properties.Resources.MartenMoeder;
                    break;
                default:
                    break;
            }
        }

    }
}
