﻿using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
                return Convert.ToInt16(speed * 40);
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
        private bool isShooting = false;

        // Checkpoints
        private int checkPointColor = 255;
        private int checkPointCount = 0;
        public int round = 0;

        // Grafisch
        private Bitmap image;
        private Bitmap map;
        private Color color;
        private Map workingMap;
        private Game workingGame;
        

        // Controller
        public State controllerState;
        private Controller _controller;
        private Vibration fullShake;
        private Vibration noShake;
        private Vibration gasShake;
        private bool isUsingController;

        
        public Player(Game game, int player)
        {
            workingMap = game.map;
            workingGame = game;
            switch (player)
            {
                case 1:
                    image = new Bitmap(Properties.Resources.MartenMoeder);
                    break;
                case 2:
                    image = new Bitmap(Properties.Resources.MoederJorrit);
                    break;
                case 3:
                    image = new Bitmap(Properties.Resources.MoederSimon);
                    break;
                case 4:
                    image = new Bitmap(Properties.Resources.MoederKoen);
                    break;
                default:
                    break;
            }

            fullShake.LeftMotorSpeed = 40000;
            fullShake.RightMotorSpeed = 40000;
            noShake.LeftMotorSpeed = 0;
            noShake.RightMotorSpeed = 0;
            gasShake.LeftMotorSpeed = 0;
            gasShake.RightMotorSpeed = 0;

            map = new Bitmap(Properties.Resources.Baan2colormap);
            speed = 0;

            if (player == 1)
            {
                isUsingController = true;
                _controller = new Controller(UserIndex.One);
                if (_controller.IsConnected) return;
                MessageBox.Show("Geen controller gevonden.");
            }

            if(player == 2)
            {
                isUsingController = true;
                _controller = new Controller(UserIndex.Two);
                if (_controller.IsConnected) return;
                MessageBox.Show("Geen controller gevonden.");
            }

            else
            {
                isUsingController = false;
                //_controller = new Controller(UserIndex.Two);
            }       
        }
        public void Update(List<Projectile> projectileList)
        {
            Move();
            Collision();
            Special(projectileList);
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
                fuel -= 0.005f * speed;
            }
        }

        private void Collision()
        {
            CheckPlayerCollision();
            CheckSpecialCollision();
            CheckColorCollision();
        }
        private void CheckPlayerCollision()
        {
            foreach (Player p in workingGame.playerList)
            {
                if (p != this)
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
        private void CheckSpecialCollision()
        {
            foreach (Player p in workingGame.playerList)
            {
                if (p != this)
                {
                    //PickUp pick in workingMap.pickUpList
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

                            if (workingMap.pickUpList[i].ret == "projectile")
                            {
                                // Als projectiel is.
                            }
                            workingMap.pickUpList.RemoveAt(i);
                            Console.WriteLine("Botsing met PickUP");                                                                        
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
            // Pitstop: RGB (143, 143, 142)
            // Obstakel: RGB(77, 1, 1) 

            try
            {
                angle = Math.PI * rotation / 180.0;
                color = map.GetPixel(Convert.ToInt32(posX + 50 * Math.Cos(angle)), Convert.ToInt32(posY + 50 * Math.Sin(angle)));
            }
            catch { } // Komt buiten de map

            if(color.R == 77 && color.G == 1 && color.B == 1)
            {
                speed = -1;
            }

            
            if (color.R == 143 && color.G == 143 && color.B == 143)
            {
                if (fuel < 100)
                {
                    fuel += 0.5;
                }
            }
          

            if (color.R == 182 && color.G == 255 && color.B == 254)
            {
                if (isUsingController)
                {
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
            if (color.R == 0 && color.G == checkPointColor && color.B == 0)
            {
                checkPointColor -= 5;
                checkPointCount += 1;
                Console.WriteLine("Check Point: " + checkPointCount);
                Console.WriteLine("Next checkpointcolor: " + checkPointColor);

                if (checkPointCount == 8)
                {
                    round += 1;
                    checkPointCount = 0;
                    checkPointColor = 255;
                }
            }
        }

        private void Special(List<Projectile> projectileList)
        {
            if(isSpecial && !isShooting)
            {
                projectileList.Add(new Projectile(this.posX, this.posY, rotation));
            }

            foreach(Player p in workingGame.playerList)
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

    /// <summary>
    /// Geschoten projectiel. 
    /// </summary>
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
            posX += 2 * Math.Cos(angle);
            posY += 2 * Math.Sin(angle);
        }
    }

}

 
