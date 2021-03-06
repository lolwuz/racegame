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

        public Bitmap m000;
        public Bitmap m001;
        public Bitmap m002;
        public Bitmap m003;
        public Bitmap m010;
        public Bitmap m011;
        public Bitmap m012;
        public Bitmap m013;
        public Bitmap m020;
        public Bitmap m021;
        public Bitmap m022;
        public Bitmap m023;
        public Bitmap m030;
        public Bitmap m031;
        public Bitmap m032;
        public Bitmap m033;

        public Bitmap map2;
        public Game workingGame;
        public List<PickUp> pickUpList = new List<PickUp>();

        public Map(Game game, int caseSwitch)
        {
            workingGame = game;

           

            // Baan keuze + pickUp's voor de baan + (extra's?)
            switch (caseSwitch)
            {
                case 1:
                    m000 = new Bitmap(Properties.Resources._100);
                    m001 = new Bitmap(Properties.Resources._101);
                    m002 = new Bitmap(Properties.Resources._102);
                    m003 = new Bitmap(Properties.Resources._103);

                    m010 = new Bitmap(Properties.Resources._110);
                    m011 = new Bitmap(Properties.Resources._111);
                    m012 = new Bitmap(Properties.Resources._112);
                    m013 = new Bitmap(Properties.Resources._113);

                    m020 = new Bitmap(Properties.Resources._120);
                    m021 = new Bitmap(Properties.Resources._121);
                    m022 = new Bitmap(Properties.Resources._122);
                    m023 = new Bitmap(Properties.Resources._123);

                    m030 = new Bitmap(Properties.Resources._130);
                    m031 = new Bitmap(Properties.Resources._131);
                    m032 = new Bitmap(Properties.Resources._132);
                    m033 = new Bitmap(Properties.Resources._133);

                    map2 = new Bitmap(Properties.Resources.Baan1ColorMap);

                    pickUpList.Add(new PickUp(1050, 250));
                    pickUpList.Add(new PickUp(1050, 325));

                    pickUpList.Add(new PickUp(1050, 1690));
                    pickUpList.Add(new PickUp(1050, 1760));
                    break;
                case 2:
                    m000 = new Bitmap(Properties.Resources._200);
                    m001 = new Bitmap(Properties.Resources._201);
                    m002 = new Bitmap(Properties.Resources._202);
                    m003 = new Bitmap(Properties.Resources._203);

                    m010 = new Bitmap(Properties.Resources._210);
                    m011 = new Bitmap(Properties.Resources._211);
                    m012 = new Bitmap(Properties.Resources._212);
                    m013 = new Bitmap(Properties.Resources._213);

                    m020 = new Bitmap(Properties.Resources._220);
                    m021 = new Bitmap(Properties.Resources._221);
                    m022 = new Bitmap(Properties.Resources._222);
                    m023 = new Bitmap(Properties.Resources._223);

                    m030 = new Bitmap(Properties.Resources._230);
                    m031 = new Bitmap(Properties.Resources._231);
                    m032 = new Bitmap(Properties.Resources._232);
                    m033 = new Bitmap(Properties.Resources._233);

                    map2 = new Bitmap(Properties.Resources.Baan2colormap);
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
            
            g.DrawImage(m000,Convert.ToSingle(-p.posX + width), Convert.ToSingle(-p.posY + height));
            g.DrawImage(m001, Convert.ToSingle(-p.posX + width + m000.Width * 1 - 1), Convert.ToSingle(-p.posY + height));
            g.DrawImage(m002, Convert.ToSingle(-p.posX + width + m000.Width * 2 - 2), Convert.ToSingle(-p.posY + height));
            g.DrawImage(m003, Convert.ToSingle(-p.posX + width + m000.Width * 3 - 3), Convert.ToSingle(-p.posY + height));

            g.DrawImage(m010, Convert.ToSingle(-p.posX + width), Convert.ToSingle(-p.posY + height + 499 ));
            g.DrawImage(m011, Convert.ToSingle(-p.posX + width + m000.Width * 1 - 1), Convert.ToSingle(-p.posY + height + 499 ));
            g.DrawImage(m012, Convert.ToSingle(-p.posX + width + m000.Width * 2 - 2), Convert.ToSingle(-p.posY + height + 499));
            g.DrawImage(m013, Convert.ToSingle(-p.posX + width + m000.Width * 3 - 3), Convert.ToSingle(-p.posY + height + 499));

            g.DrawImage(m020, Convert.ToSingle(-p.posX + width), Convert.ToSingle(-p.posY + height + 998));
            g.DrawImage(m021, Convert.ToSingle(-p.posX + width + m000.Width * 1 - 1), Convert.ToSingle(-p.posY + height + 998));
            g.DrawImage(m022, Convert.ToSingle(-p.posX + width + m000.Width * 2 - 2), Convert.ToSingle(-p.posY + height + 998));
            g.DrawImage(m023, Convert.ToSingle(-p.posX + width + m000.Width * 3 - 3), Convert.ToSingle(-p.posY + height + 998));

            g.DrawImage(m030, Convert.ToSingle(-p.posX + width), Convert.ToSingle(-p.posY + height + 1497));
            g.DrawImage(m031, Convert.ToSingle(-p.posX + width + m000.Width * 1 - 1), Convert.ToSingle(-p.posY + height + 1497));
            g.DrawImage(m032, Convert.ToSingle(-p.posX + width + m000.Width * 2 - 2), Convert.ToSingle(-p.posY + height + 1497));
            g.DrawImage(m033, Convert.ToSingle(-p.posX + width + m000.Width * 3 - 3), Convert.ToSingle(-p.posY + height + 1497));

            foreach (PickUp pick in pickUpList)
            {
                pick.Draw(g, p, width, height);
            }
        }
    }
}
