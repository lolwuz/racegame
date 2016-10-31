using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaceGame2
{
    /// <summary>
    /// De class Player bevat alle informatie over de speler. Auto bitmap, beweging en input van controller.
    /// </summary>
    public class Player 
    {
        // Voor display
        public int displaySpeed
        {
            get
            {
                return Convert.ToInt16(speed * 10);
            }
        }

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

        public double fuel = 100;

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

        // Items
        public string equiped = "null";

        public Image equipedd = null;

        // Checkpoints
        private int checkPointColor = 255;
        private int checkPointCount = 0;
        public int round = 0;

        // Grafisch
        private Bitmap image;
        private Color colorFront;
        private Color colorBack;
        private Map workingMap;
        private Game workingGame;
        

        // Controller
        public State controllerState;
        private Controller _controller;
        private Vibration fullShake;
        private Vibration gasShake;
        private bool isUsingController;

        
        public Player(Game game, int player, int car)
        {
            workingMap = game.map;
            workingGame = game;
            switch (car)
            {
                case 1:
                    image = new Bitmap(Properties.Resources.MartenMoeder);
                    break;
                case 2:
                    image = new Bitmap(Properties.Resources.MoederJorrit);
                    break;
                case 3:
                    image = new Bitmap(Properties.Resources.MoederKoen);
                    break;
                case 4:
                    image = new Bitmap(Properties.Resources.MoederSimon);
                    break;
                default:
                    break;
            }

            fullShake.LeftMotorSpeed = 65000;
            fullShake.RightMotorSpeed = 65000;
         
            gasShake.LeftMotorSpeed = 0;
            gasShake.RightMotorSpeed = 0;

            
            speed = 0;

            if (player == 1)
            {
                isUsingController = true;
                _controller = new Controller(UserIndex.One);
                if (_controller.IsConnected) return;
                Console.WriteLine("Speler " + player + "Gebruikt geen controller");
            }

            if(player == 2)
            {
                isUsingController = true;
                _controller = new Controller(UserIndex.Two);
                if (_controller.IsConnected) return;
                Console.WriteLine("Speler " + player + "Gebruikt geen controller");
            }
            if (player == 3)
            {
                isUsingController = true;
                _controller = new Controller(UserIndex.Three);
                if (_controller.IsConnected) return;
                Console.WriteLine("Speler " + player + "Gebruikt geen controller");
            }
            if (player == 4)
            {
                isUsingController = true;
                _controller = new Controller(UserIndex.Four);
                if (_controller.IsConnected) return;
                Console.WriteLine("Speler " + player + "Gebruikt geen controller");
            }

            else
            {
                isUsingController = false;
                //_controller = new Controller(UserIndex.Two);
            }       
        }
        public void Update()
        {    
            Move(); // Speler beweging
            CheckPlayerCollision(); // Collision Checks
            CheckSpecialCollision();
            CheckColorCollision();
            Special(); // Special box
        }
        
        private void Move()
        {
            if (isUsingController)
            {
                try
                {
                    controllerState = _controller.GetState();
                }
                catch
                {
                    isUsingController = false;
                }

                string[] words = System.Text.RegularExpressions.Regex.Split(controllerState.Gamepad.Buttons.ToString(), ", ");

                steerAngle = controllerState.Gamepad.LeftThumbX / 81920.0f;

             
                if (speed < maxSpeed)
                {
                    speed = speed + controllerState.Gamepad.RightTrigger / 2550.0f;
                }
                
                gasShake.LeftMotorSpeed = (ushort)(controllerState.Gamepad.RightTrigger * 28);
                gasShake.RightMotorSpeed = (ushort)(controllerState.Gamepad.RightTrigger * 28);

                foreach (string word in words)
                {
                    if (word == "B")
                    {
                        if (speed > 0)
                        {
                            speed -= accel / 2;
                        }
                        else
                        {
                            if (speed > -maxSpeed / 2) { speed -= accel / 2; }
                        }
                    }
                }

          
            }


            if (!isUsingController)
            {
                if (isUp)
                {
                    if (speed < maxSpeed)
                    {
                        speed += accel;
                    }
                }
                else if (isDown)
                {
                    if (speed > 0)
                    {
                        speed -= accel / 2;
                    }
                    else
                    {
                        if (speed > -maxSpeed / 2) { speed -= accel / 2; }
                    }
                }

                          
                // Stuur Angle 
                if (left)
                {
                    if (steerAngle > -maxSteerAngle)
                    {
                        steerAngle = steerAngle - 0.04f;
                    }
                }

                if (isRight)
                {
                    if (steerAngle < maxSteerAngle)
                    {
                        steerAngle = steerAngle + 0.04f;
                    }
                }

                if (!left && !isRight)
                {
                    if (steerAngle > 0)
                    {
                        steerAngle = 0;
                    }
                    if (steerAngle < 0)
                    {
                        steerAngle = 0;
                    }
                }
            }


            if (speed > 0)
            {
                speed -= 0.02f;
            }
            if (speed < 0)
            {
                speed += 0.02f;
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
            Fuel();

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
        private void Fuel()
        {
            if (fuel < 0)
            {
                maxSpeed = 2;
            }
            else
            {
                maxSpeed = 8;
                fuel -= 0.005f * speed;
            }
        }

        private void CheckPlayerCollision()
        {
            foreach (Player p in workingGame.playerList)
            {
                if (p != this)
                {
                    // this.Voorkant/ p.achterkant
                    distance = Math.Sqrt(
                        Math.Pow(((p.posX - Math.Cos(p.angle) * -25) - (this.posX - Math.Cos(angle) * 25)), 2) +
                        Math.Pow(((p.posY - Math.Sin(p.angle) * -25) - (this.posY - Math.Sin(angle) * 25)), 2)
                        );
                    if (distance < 45)
                    {
                        p.speed = -speed / 2;
                        speed = 1;                  
                    }
                }
            }
        }
        private void CheckSpecialCollision()
        {
            foreach (Player p in workingGame.playerList)
            {
                if (p != this)
                {
                    for (int i = 0; i < workingMap.pickUpList.Count; i++)
                    {
                        distance = Math.Sqrt(Math.Pow((workingMap.pickUpList[i].position.X - this.posX), 2) + Math.Pow((workingMap.pickUpList[i].position.Y - this.posY), 2));
                        if (distance < 50)
                        {
                            
                            int newPickPositionX = workingMap.pickUpList[i].position.X;
                            int newPickPositionY = workingMap.pickUpList[i].position.Y;

                            Task.Factory.StartNew(() =>
                            {
                                System.Threading.Thread.Sleep(10000);
                                Console.WriteLine("New Pickup");

                                workingMap.pickUpList.Add(new PickUp(newPickPositionX, newPickPositionY));
                            });

                            
                            if(equiped == "null")
                            {
                                equiped = workingMap.pickUpList[i].ret;              
                            }

                            workingMap.pickUpList.RemoveAt(i);
                            Console.WriteLine("Equiped: " + equiped);                                                                        
                        }
                    }
                }
            }
        }
        private void CheckColorCollision()
        {
            // Collision op basis van bitmap GetPixel() functie
            // Note: achtergrond kleur is per map verschillend.
            // Brug: RGB (192, 192, 192) 
            // Pitstop: RGB (143, 143, 143)
            // Obstakel: RGB(77, 1, 1) 
            try
            {
                angle = Math.PI * rotation / 180.0;
                colorFront = workingMap.map2.GetPixel(Convert.ToInt32(posX + 50 * Math.Cos(angle)), Convert.ToInt32(posY + 50 * Math.Sin(angle)));
                colorBack = workingMap.map2.GetPixel(Convert.ToInt32(posX - 50 * Math.Cos(angle)), Convert.ToInt32(posY - 50 * Math.Sin(angle))); 
            }
            catch(Exception) { } // Komt buiten de map

            // Collision met obstakel. Voor en achter.
            if(colorFront.R == 77 && colorFront.G == 1 && colorFront.B == 1)
            {
                speed = -1;
            }
            if (colorBack.R == 77 && colorBack.G == 1 && colorBack.B == 1)
            {
                speed = 1;
            }

            // Pitstop check.
            if (colorFront.R == 143 && colorFront.G == 143 && colorFront.B == 143)
            {
                if (fuel < 100)
                {
                    fuel += 0.5f;
                }
            }
          
            // Offroad check. Volle stillstand als de speler van de weg geraakt. :-)
            if (colorFront.R == 182 && colorFront.G == 255 && colorFront.B == 254)
            {
                if (isUsingController)
                {
                    fullShake.LeftMotorSpeed = (ushort)(speed  * 8000);
                    fullShake.RightMotorSpeed = (ushort)(speed * 8000) ;
                    _controller.SetVibration(fullShake);
                }

                if (speed > 1)
                {
                    speed -= 0.2f;
                }
            }
            else
            {
                if (isUsingController)
                {
                    _controller.SetVibration(gasShake); 
                }
            }
   
            // Checken voor checkpoints.
            // Checkpoint1: groen: RGB(0, 255, 0) , checkpoint2: RGB(0, 250, 0), enzovoorts. 
            if (colorFront.R == 0 && colorFront.G == checkPointColor && colorFront.B == 0)
            {
                checkPointColor -= 5;
                checkPointCount += 1;
               
                if (checkPointCount == 8)
                {
                    round += 1;
                    checkPointCount = 0;
                    checkPointColor = 255;
                }
            }
        }

        private void Special()
        {
            if(isSpecial)
            {
                if(equiped == "projectile")
                {
                    Console.WriteLine("New project" );
                    workingGame.projectileList.Add(new Projectile(this.posX, this.posY, rotation, workingGame.playerList, this));
                    equiped = "null";
                } 

                if(equiped == "oil")
                {
                    workingGame.oilList.Add(new Oil(this.posX, this.posY, rotation));
                    equiped = "null";
                }

                if (equiped == "speed")
                {
                    Task.Factory.StartNew(() =>
                    {
                        speed = 12;
                    });
                    equiped = "null";
                }

                if (equiped == "fueldrain")
                {
                    foreach(Player p in workingGame.playerList)
                    {
                        if(p != this)
                        {
                            p.fuel -= 20.0f; 
                        }                  
                    }
                    equiped = "null";
                }

            }
        }
  
        public void Draw(Graphics g, List<Projectile> projectileList, double width, double height)
        {
            //Projectielen tekenen
            foreach (Projectile projectile in projectileList)
            {
                projectile.Draw(g, this, width, height);
            }

            foreach(Oil oil in workingGame.oilList)
            {
                oil.Draw(g, this, width, height);
            }

            // Noodzakelijk kwaad om de auto te laten draaien
            g.TranslateTransform(Convert.ToSingle(width), Convert.ToSingle(height));
            g.RotateTransform((float)rotation);
            g.DrawImage(image, -image.Width/2, -image.Height / 2);
            
            g.RotateTransform(-(float)rotation);
            g.ResetTransform();
            g.RotateTransform(0);        
        }
    }
}

 
