using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading.Tasks;
using SharpDX.XInput;

namespace RaceGame2
{
    public class Player 
    {
        // Beweging variabelen
        public double speed { get; set; }
        public float maxSpeed { get; set; }
        public float accel { get; set; }

        public double posX { get; set; }
        public double posY { get; set; }
    
        public double rotation { get; set; }
        public double angle;
       
        private double backWheelX, backWheelY, frontWheelX, frontWheelY;
        private double wheelBase = 80;
        private double steerAngle = 0;
        public double maxSteerAngle { get; set; } = 0.4;

        private float slipAngle = 0;

        // Collsion vars
        private double distance;

        // Input Vars
        public bool left { get; set; }
        public bool isRight { get; set; }
        public bool isDown { get; set; }
        public bool isUp { get; set; }
        public bool isSpecial { get; set; }

        public Keys keyLeft = Keys.Left;
        public Keys keyRight = Keys.Right;
        public Keys keyDown = Keys.Down;
        public Keys keyUp = Keys.Up;
        public Keys keySpecial = Keys.Enter;

        private bool isShooting = false;

        // Grafisch
        private Bitmap image;
        private Bitmap map;
        private Color color;

        private string _leftAxis;
        private string _rightAxis;
        private string _buttons;
        private Controller _controller;




        public Player(Game form)
        {
            int caseSwitch = 1;
            switch (caseSwitch)
            {
                case 1:
                    image = new Bitmap(Properties.Resources.car2);
                    break;
                default:
                    break;
            }


            map = new Bitmap(Properties.Resources.basicTrack);
            speed = 0;

            _controller = new Controller(UserIndex.One);
            if (_controller.IsConnected) return;
            MessageBox.Show("Game Controller is not connected ... you know ;)");
          
        }

        public void Update(List<Player> playerList, List<Projectile> projectileList)
        {
            Move();
            Collision(playerList);
            Special(playerList, projectileList);      
        }

        private void Move()
        {
            var state = _controller.GetState();

            // Snelheid Omhoog en beneden.


            if(speed < maxSpeed)
            {
                speed = speed + state.Gamepad.LeftTrigger / 5100.0f;
            }
            

            if (isUp)
            {
                if (speed < maxSpeed)
                {
                    //speed += accel;
                }
            }
            else if (isDown)
            {
                if (speed > 0)
                {
                    // speed -= accel / 2; 
                }
                else
                {
                    if (speed > -maxSpeed / 2) { speed -= accel / 2; }
                }    
            }
            else
            {
                if (speed > 0)
                {
                    //speed -= 0.02f;
                }
                if (speed < 0 && !isDown)
                {
                    speed = 0;
                }
            }

            // Stuur Angle 
            if (left)
            {
                if (steerAngle > -maxSteerAngle)
                {
                    //steerAngle = steerAngle - 0.04f;
                }
            }
            
            if (isRight)
            {
                if (steerAngle < maxSteerAngle)
                {
                    //steerAngle = steerAngle + 0.04f;
                }
            }

            steerAngle = state.Gamepad.LeftThumbX / 81920.0f;      

            if (!left && !isRight)
            {

                if (steerAngle > 0)
                {
                    //steerAngle = 0;
                }
                if(steerAngle < 0)
                {
                    //steerAngle = 0;
                } 
            }
            

            // Bij een bepaalde stuurhoek gaan de achterbanden slippen.
            if (steerAngle > 0.2)
            {                
                if (slipAngle < 20)
                {
                    slipAngle += 0.5f;
                }
            }

            else if (steerAngle < -0.2)
            {
               
                if (slipAngle > -20)
                {
                    slipAngle -= 0.5f;
                }
            }

            else
            {
                if (slipAngle > 0)
                {
                    slipAngle -= 1f;
                }
                if (slipAngle < 0)
                {
                    slipAngle += 1f;
                }
            }

            // Verplaatsing berekenen.
            angle = Math.PI * rotation / 180.0;
            frontWheelX = posX + wheelBase / 2 * Math.Cos(angle);
            frontWheelY = posY + wheelBase / 2 * Math.Sin(angle);
            backWheelX = posX - wheelBase / 2 * Math.Cos(angle);
            backWheelY = posY - wheelBase / 2 * Math.Sin(angle);

            backWheelX += speed * Math.Cos(angle - slipAngle / 45);
            backWheelY += speed * Math.Sin(angle - slipAngle / 45);
            frontWheelX += speed * Math.Cos(angle + steerAngle);
            frontWheelY += speed * Math.Sin(angle + steerAngle);

            rotation = ((Math.Atan2(frontWheelY - backWheelY, frontWheelX - backWheelX)) * 180 / Math.PI);
            posX = (frontWheelX + backWheelX) / 2;
            posY = (frontWheelY + backWheelY) / 2;            
        }

        private void Collision(List<Player> playerList)
        {
            // Check color of the map
            try
            {
                color = map.GetPixel(Convert.ToInt16(posX), Convert.ToInt16(posY));
                if (color.R == 39)
                {       
                    if (speed > 1){
                        speed -= 0.2f;
                    }
                }
            }
            catch (Exception){}
            
         
            foreach (Player p in playerList)
            {
                if(p != this)
                {
                    distance = Math.Sqrt(Math.Pow((p.posX - this.posX), 2) + Math.Pow((p.posY - this.posY), 2));

                    if (distance < 25)
                    {
                        p.speed = speed;
                        speed = speed / 2;
                    }                  
                }
            }     
        }

        private void Special(List<Player> playerList, List<Projectile> projectileList)
        {
            if(isSpecial && !isShooting)
            {
                projectileList.Add(new Projectile(this.posX, this.posY, rotation));
            }

            foreach(Player p in playerList)
            {
                if(p != this)
                {
                    foreach (Projectile projectile in projectileList)
                    {
                        projectile.Update();
                        distance = Math.Sqrt(Math.Pow((posX - projectile.posX), 2) + Math.Pow((posY - projectile.posY), 2));

                        if (distance < 25)
                        {
                            speed = 0;
                        }              
                    }
                }
            }           
        }
  
        public void Draw(Graphics g, List<Projectile> projectileList, double width, double height)
        {
            //g.TranslateTransform((float)positionX, (float)positionY);

            foreach (Projectile projectile in projectileList)
            {
                projectile.Draw(g, this, width, height);
            }

            g.TranslateTransform(Convert.ToSingle(width), Convert.ToSingle(height));
            g.RotateTransform((float)rotation);
            g.DrawImage(image, -image.Width/2, -image.Height / 2);
            
            g.RotateTransform(-(float)rotation);
            g.ResetTransform();
            g.RotateTransform(0);

            
        }
    }

    public class Projectile
    {
        public double posX { get; set; }
        public double posY { get; set; }
        public double angle { get; set; }

        public Projectile(double X, double Y, double rotation)
        {
            angle = Math.PI * rotation / 180.0;

            posX = X + 50 * Math.Cos(angle); 
            posY = Y + 50 * Math.Sin(angle);
            
        }
        public void Draw(Graphics g, Player p, double width, double height)
        {
            g.DrawRectangle(new Pen(Color.Blue, 3),
                    Convert.ToInt32(width + (posX - p.posX)),
                    Convert.ToInt32(height + (posY - p.posY)),
                    10, 10);
        }

        public void Update()
        {
            posX += 6 * Math.Cos(angle);
            posY += 6 * Math.Sin(angle);
        }
    }


 
}

 
