using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.XInput;

namespace RaceGame2
{
    public partial class Menu : Form
    {
        // cars and track
        private int car1, car2, car3, car4, track;

        // music player
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        // Controller
        public State controller1State, controller2State, controller3State, controller4State;
        private Controller _controller1, _controller2, _controller3, _controller4;
        private bool isUsingController1, isUsingController2, isUsingController3, isUsingController4;

        public Menu()
        {
            InitializeComponent();

            // background
            Bitmap BackImg = Properties.Resources.menu_background;
            Bitmap BackBmp = new Bitmap(BackImg.Width, BackImg.Height);
            Graphics memoryGraphics = Graphics.FromImage(BackBmp);
            memoryGraphics.DrawImage(BackImg, 0, 0, BackImg.Width, BackImg.Height);
            BackgroundImage = BackBmp;

            // style
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            // center panel in form
            panel1.Location = new Point(
            this.ClientSize.Width / 2 - panel1.Size.Width / 2,
            this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;

            // maximize
            this.WindowState = FormWindowState.Maximized;

            // controller 1
            isUsingController1 = true;
            _controller1 = new Controller(UserIndex.One);

            // controller 2 
            isUsingController2 = true;
            _controller2 = new Controller(UserIndex.Two);

            // controller 3
            isUsingController3 = true;
            _controller3 = new Controller(UserIndex.Three);

            // controller 4
            isUsingController4 = true;
            _controller4 = new Controller(UserIndex.Four);

            // background music
            
            player.Stream = Properties.Resources.MenuMusic;
            player.Play();
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

            // start controller selection timer...
            timer2.Start();

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
            startGame();
        }

        private void startGame()
        {
            player.Stop();
            Game gameForm = new Game(car1, car2, car3, car4, track);
            gameForm.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lblTitle.ForeColor == Color.Black)
            {
                lblTitle.ForeColor = Color.Turquoise;
            }
            else if (lblTitle.ForeColor == Color.Turquoise)
            {
                lblTitle.ForeColor = Color.Fuchsia;
            }
            else
            {
                lblTitle.ForeColor = Color.Black;
            }
        }

        // timer for controller selection
        private void timer2_Tick(object sender, EventArgs e)
        {
            SelectCar1andTrack();
            SelectCar2();
            SelectCar3();
            SelectCar4();

            // if all players selected a car and the map is selected (by player 1) start the game...
            if (player1Selected && mapSelected && player2Selected && player3Selected && player4Selected)
            {
                startGame();
                timer2.Stop();
            }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        // auto 1 kiezen
        private void lblCar1Back_Click(object sender, EventArgs e)
        {
            car1Back();
        }

        private void lblCar1Next_Click(object sender, EventArgs e)
        {
            car1Next();
        }

        // race baan kiezen
        private void lblTrackBack_Click(object sender, EventArgs e)
        {
            raceTrackBack();
        }

        private void lblTrackNext_Click(object sender, EventArgs e)
        {
            raceTrackNext();
        }

        // auto 2 kiezen
        private void lblCar2Back_Click(object sender, EventArgs e)
        {
            car2Back();
        }

        private void lblCar2Next_Click(object sender, EventArgs e)
        {
            car2Next();
        }

        // auto 3 kiezen
        private void lblCar3Back_Click(object sender, EventArgs e)
        {
            car3Back();
        }

        private void lblCar3Next_Click(object sender, EventArgs e)
        {
            car3Next();
        }

        // auto 4 kiezen
        private void lblCar4Back_Click(object sender, EventArgs e)
        {
            car4Back();
        }

        private void lblCar4Next_Click(object sender, EventArgs e)
        {
            car4Next();
        }

        // Controller selection for car 1 and race track
        bool player1Selected = false;
        bool mapSelected = false;
        Image tempCar1;
        Image tempMap;
        private void SelectCar1andTrack()
        {
            if (isUsingController1)
            {
                try
                {
                    controller1State = _controller1.GetState();
                }
                catch
                {
                    isUsingController1 = false;
                }

                string[] controllerbuttons = System.Text.RegularExpressions.Regex.Split(controller1State.Gamepad.Buttons.ToString(), ", ");

                if (!player1Selected)
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("DPadRight"))
                        {
                            car1Next();
                        }
                        else if (button.Equals("DPadLeft"))
                        {
                            car1Back();
                        }
                        else if (button.Equals("A"))
                        {
                            tempCar1 = picBoxCar1.Image;
                            picBoxCar1.Image = Properties.Resources.selected;
                            player1Selected = true;
                        }
                    }
                } 
                else if (!mapSelected)
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("DPadRight"))
                        {
                            raceTrackNext();
                        }
                        else if (button.Equals("DPadLeft"))
                        {
                            raceTrackBack();
                        }
                        else if (button.Equals("B"))
                        {
                            picBoxCar1.Image = tempCar1;
                            player1Selected = false;
                        }
                        else if (button.Equals("A"))
                        {
                            tempMap = picBoxRaceTrack.Image;
                            picBoxRaceTrack.Image = Properties.Resources.selected;
                            mapSelected = true;
                        }
                    }
                }
                else
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("B"))
                        {
                            picBoxRaceTrack.Image = tempMap;
                            mapSelected = false;
                        }
                    }
                }
                
            }
        }

        // Controller selection for other cars
        bool player2Selected = false;
        Image tempCar2;
        private void SelectCar2()
        {
            if (isUsingController2)
            {
                try
                {
                    controller2State = _controller2.GetState();
                }
                catch
                {
                    isUsingController2 = false;
                }

                string[] controllerbuttons = System.Text.RegularExpressions.Regex.Split(controller2State.Gamepad.Buttons.ToString(), ", ");

                if (!player2Selected)
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("DPadRight"))
                        {
                            car2Next();
                        }
                        else if (button.Equals("DPadLeft"))
                        {
                            car2Back();
                        }
                        else if (button.Equals("A"))
                        {
                            tempCar2 = picBoxCar2.Image;
                            picBoxCar2.Image = Properties.Resources.selected;
                            player2Selected = true;
                        }
                    }
                }
                else
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("B"))
                        {
                            picBoxCar2.Image = tempCar2;
                            player2Selected = false;
                        }
                    }
                }
                

            }
        }
        bool player3Selected = false;
        Image tempCar3;
        private void SelectCar3()
        {
            if (isUsingController3)
            {
                try
                {
                    controller3State = _controller3.GetState();
                }
                catch
                {
                    isUsingController3 = false;
                }

                string[] controllerbuttons = System.Text.RegularExpressions.Regex.Split(controller3State.Gamepad.Buttons.ToString(), ", ");

                if(!player3Selected)
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("DPadRight"))                        {

                            car3Next();
                        }
                        else if (button.Equals("DPadLeft"))
                        {
                            car3Back();
                        }
                        else if (button.Equals("A"))
                        {
                            tempCar3 = picBoxCar3.Image;
                            picBoxCar3.Image = Properties.Resources.selected;
                            player3Selected = true;
                        }
                    }
                }
                else
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("B"))
                        {
                            picBoxCar3.Image = tempCar3;
                            player3Selected = false;
                        }
                    }
                }
                

            }
        }
        bool player4Selected = false;
        Image tempCar4;
        private void SelectCar4()
        {
            if (isUsingController4)
            {
                try
                {
                    controller4State = _controller4.GetState();
                }
                catch
                {
                    isUsingController4 = false;
                }

                string[] controllerbuttons = System.Text.RegularExpressions.Regex.Split(controller4State.Gamepad.Buttons.ToString(), ", ");

                if (!player4Selected)
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("DPadRight"))
                        {
                            car4Next();
                        }
                        else if (button.Equals("DPadLeft"))
                        {
                            car4Back();
                        }
                        else if (button.Equals("A"))
                        {
                            tempCar4 = picBoxCar4.Image;
                            picBoxCar4.Image = Properties.Resources.selected;
                            player4Selected = true;
                        }
                    }
                } 
                else
                {
                    foreach (string button in controllerbuttons)
                    {
                        if (button.Equals("B"))
                        {
                            picBoxCar4.Image = tempCar4;
                            player4Selected = false;
                        }
                    }
                }
            }
        }

        // auto 1 kies methodes
        private void car1Back()
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
        private void car1Next()
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

        // race track kies methodes
        private void raceTrackBack()
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
        private void raceTrackNext()
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

        // auto 2 kies methodes
        private void car2Back()
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
        private void car2Next()
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

        // auto 3 kies methodes
        private void car3Back()
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
        private void car3Next()
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

        // auto 4 kies methodes
        private void car4Back()
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
        private void car4Next()
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
