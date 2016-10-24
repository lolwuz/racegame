using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RaceGame2
{
    public class Player 
    {
        // Beweging variabelen
        public float speed { get; set; }
        public float maxSpeed { get; set; }
        public float accel { get; set; }

        public double posX { get; set; }
        public double posY { get; set; }
    
        public double rotation { get; set; }
        public double angle;
       
        private double backWheelX, backWheelY, frontWheelX, frontWheelY;
        private double wheelBase = 80;
        private double steerAngle = 0;
        public double maxSteerAngle { get; set; }

        // Collsion vars
        private double distance;

        // Input Vars
        public bool left { get; set; }
        public bool right { get; set; }
        public bool down { get; set; }
        public bool up { get; set; }

        public Keys keyLeft = Keys.Left;
        public Keys keyRight = Keys.Right;
        public Keys keyDown = Keys.Down;
        public Keys keyUp = Keys.Up;

        // Grafisch
        private Bitmap image;
        private Bitmap map;
        
       
        public Player(Game form)
        {
            image = new Bitmap(Properties.Resources.car2);
            map = new Bitmap(Properties.Resources.formula);
            maxSteerAngle = 1;
            speed = 0;
        }

        public void Update(List<Player> playerList)
        {   
            Move();
            Collision(playerList);    
        }

        private void Move()
        {
            if (up)
            {
                if (speed < maxSpeed)
                {
                    speed += accel;
                }
            }
            else if (down)
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
            else
            {
                if (speed > 0)
                {
                    speed -= 0.02f;
                }
                if (speed < 0 && !down)
                {
                    speed = 0;
                }
            }

            if (left)
            {
                if (steerAngle > -maxSteerAngle) { steerAngle = steerAngle - 0.05f; }
            }
            
            if (right)
            {
                if (steerAngle < maxSteerAngle) { steerAngle = steerAngle + 0.05f; }
            }

            if(!left && !right)
            {

                if (steerAngle > 0)
                {
                    steerAngle = steerAngle - 0.1f;
                }
                if(steerAngle < 0)
                {
                    steerAngle = steerAngle + 0.1f;
                }

                
            }

            angle = Math.PI * rotation / 180.0;

            frontWheelX = posX + wheelBase / 2 * Math.Cos(angle);
            frontWheelY = posY + wheelBase / 2 * Math.Sin(angle);
            backWheelX = posX - wheelBase / 2 * Math.Cos(angle);
            backWheelY = posY - wheelBase / 2 * Math.Sin(angle);

            backWheelX += speed * Math.Cos(angle);
            backWheelY += speed * Math.Sin(angle);
            frontWheelX += speed * Math.Cos(angle + steerAngle);
            frontWheelY += speed * Math.Sin(angle + steerAngle);

            posX = (frontWheelX + backWheelX) / 2;
            posY = (frontWheelY + backWheelY) / 2;

            rotation = (Math.Atan2(frontWheelY - backWheelY, frontWheelX - backWheelX)) * 180 / Math.PI;
        }

        private void Collision(List<Player> playerList)
        {
            // Check color of the map
            //try
            //{
                //color = map.GetPixel(Convert.ToInt16(posX), Convert.ToInt16(posY));
                //if (color.R == 232)
                //{       
                //    if (speed > 1){  }
                //}
            //}
            //catch (Exception){}
            
            // Collision between Cars 
            foreach (Player p in playerList)
            {
                if(p != this)
                {
                    distance = Math.Sqrt(Math.Pow((p.posX - this.posX), 2) + Math.Pow((p.posY - this.posY), 2));

                    if (distance < 100)
                    {
                        p.speed = speed;
                        speed = speed / 2;
                    }                  
                }
            }     
        }
  
        public void Draw(Graphics g, double width, double height)
        {
            //g.TranslateTransform((float)positionX, (float)positionY);

            g.TranslateTransform(Convert.ToSingle(width), Convert.ToSingle(height));
            g.RotateTransform((float)rotation);
            g.DrawImage(image, -image.Width/2, -image.Height / 2);
            
            g.RotateTransform(-(float)rotation);
            g.ResetTransform();
            g.RotateTransform(0);

            // Maak vierkant uit afbeelden van de auto.? Voor Collision ?
            // GraphicsUnit units = GraphicsUnit.Point;
            // RectangleF bmpRectangleF = image.GetBounds(ref units); 
            //Rectangle bmpRectangle = Rectangle.Round(bmpRectangleF);
            //g.DrawRectangle(Pens.Blue, bmpRectangle);
        }
    }
}

 
