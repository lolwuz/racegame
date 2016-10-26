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
            Game gameForm = new Game("Aafke", "Aafke", "MoederTrack");
            gameForm.Show();
            this.Close();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
