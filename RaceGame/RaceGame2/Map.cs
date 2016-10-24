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
    public class Map
    {
        public Bitmap map1;
        public static List<PickUp> pickUpList = new List<PickUp>();

        public Map()
        {
            int caseSwitch = 1;
            switch (caseSwitch)
            {
                case 1:
                    map1 = new Bitmap(Properties.Resources.bigracemap);
                    PickUp pick1 = new PickUp();
                    pick1.position = new Point(20, 100);

                    PickUp pick2 = new PickUp();
                    pick1.position = new Point(200, 100);

                    PickUp pick3 = new PickUp();
                    pick1.position = new Point(400, 500);

                    pickUpList.Add(pick1);
                    pickUpList.Add(pick2);
                    pickUpList.Add(pick3);
                    break;
                default:
                    break;
            }
        }

        public void Draw(Graphics g, Player p, double width, double height)
        {
            g.DrawImage(map1,Convert.ToSingle(-p.posX + width), Convert.ToSingle(-p.posY + height));

            foreach(PickUp pick in pickUpList)
            {
                g.DrawRectangle(new Pen(Color.Blue, 3),
                    Convert.ToInt32(width + (pick.position.X - p.posX)),
                    Convert.ToInt32(height + (pick.position.Y - p.posY)),
                    10,10);       
            }
        }
    }

    public class PickUp
    {
        public Point position { get; set; }

        public enum type {projectile, oil, fuelDrain };
        public static Random rnd = new Random();

        public PickUp()
        {
            string[] types = Enum.GetNames(typeof(type));
            int randomEnum = rnd.Next(types.Length);
            var ret = Enum.Parse(typeof(type), types[randomEnum]);
            Console.WriteLine(ret);
        }
    }
}
