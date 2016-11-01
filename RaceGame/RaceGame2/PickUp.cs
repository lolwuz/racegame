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
    public class PickUp
    {
        public Point position;
        public enum type {projectile, oil, speed, fueldrain };
        public static Random rnd = new Random();
        private Bitmap pickUpBlock = new Bitmap(Properties.Resources.powerup);
        public string ret;

        public PickUp(int x, int y)
        {
            position.X = x;
            position.Y = y;

            string[] types = Enum.GetNames(typeof(type));
            int randomEnum = rnd.Next(types.Length);
            ret = Convert.ToString(Enum.Parse(typeof(type), types[randomEnum]));
            Console.WriteLine(ret);
        }
        public void Draw(Graphics g, Player p, double width, double height)
        {
            g.DrawImage(pickUpBlock, new Point(Convert.ToInt32(width + position.X - p.posX - (pickUpBlock.Width / 2)), Convert.ToInt32(height + position.Y - p.posY - (pickUpBlock.Height / 2))));
        }
    }

    public class Projectile
    {
        public double posX { get; set; }
        public double posY { get; set; }
        public double angle { get; set; }

        private double _distanceMax = 0;
        private double _distance;
        private double _slope;


        private Player followPlayer;
        
        public Projectile(double X, double Y, double rotation, List<Player> playerList, Player activePlayer)
        {
            angle = Math.PI * rotation / 180.0;
            posX = X;
            posY = Y;

            foreach (Player p in playerList)
            {
                if(p != activePlayer)
                {
                    _distance = Math.Sqrt(Math.Pow((p.posX - posX), 2) + Math.Pow((p.posY - posY), 2));
                    if(_distance > _distanceMax)
                    {
                        _distanceMax = _distance;
                        followPlayer = p;
                    }
                }
                else
                {

                }      
            }
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
            _slope = (posY - followPlayer.posY) / (posX - followPlayer.posX);
            angle = Math.Tanh(_slope);

            if(_slope > 0)
            {
                posX += 8 * Math.Cos(angle);
                posY += 8 * Math.Sin(angle);
            }
            
            else{
                posX -= 8 * Math.Cos(angle);
                posY -= 8 * Math.Sin(angle);
            }
        }
    }

    public class Oil
    {
        public double posX { get; set; }
        public double posY { get; set; }
        public double angle { get; set; }
        public Oil(double X, double Y, double rotation)
        {
            angle = Math.PI * rotation / 180.0;
            posX = X - 50 * Math.Cos(angle); // 50 pixels achter de auto
            posY = Y - 50 * Math.Sin(angle);
        }
        public void Draw(Graphics g, Player p, double width, double height)
        {
            g.DrawImage(Properties.Resources.oileleak_ingame, new Point(Convert.ToInt32(width + (posX - p.posX) - Properties.Resources.oileleak_ingame.Width), Convert.ToInt32(height + (posY - p.posY))));
        }
    }
}
