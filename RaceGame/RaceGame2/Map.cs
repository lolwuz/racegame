﻿using System;
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
        public List<PickUp> pickUpList = new List<PickUp>();

        public Map()
        {
            int caseSwitch = 2;

            // Baan keuze + pickUp's voor de baan + (extra's?)
            switch (caseSwitch)
            {
                case 1:
                    map1 = new Bitmap(Properties.Resources.basicTrack);
                    pickUpList.Add(new PickUp(10, 20));
                    break;
                case 2:
                    map1 = new Bitmap(Properties.Resources.Baan2);
                    pickUpList.Add(new PickUp(684, 1000));
                    pickUpList.Add(new PickUp(755, 1000));

                    pickUpList.Add(new PickUp(1112, 860));
                    pickUpList.Add(new PickUp(1190, 860));

                    pickUpList.Add(new PickUp(1905, 440));
                    pickUpList.Add(new PickUp(1839, 440));

                    pickUpList.Add(new PickUp(1330, 1476));
                    pickUpList.Add(new PickUp(1330, 1404));
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
                pick.Draw(g, p, width, height);
            }
        }
    }
}
