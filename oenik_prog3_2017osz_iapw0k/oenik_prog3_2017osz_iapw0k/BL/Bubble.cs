// <copyright file="Bubble.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    class Bubble : GameItem
    {
        private static Random rand = new Random();
        private int screenheight;
        private int screenwidth;
        private double posX;
        private double posY;

        public Bubble(Point location, double bubbleSize, int nrofColors)
        {
            this.Location = location;
            this.Item = new EllipseGeometry(this.Location, bubbleSize / 2, bubbleSize / 2);
            this.ColorNumber = rand.Next(0, nrofColors);
            this.Processed = false;
        }

        public Bubble(int screenHeight, int screeWidth, double bubbleSize, int nrofColors)
        {
            this.screenheight = screenHeight - 15;
            this.screenwidth = screeWidth;
            this.posX = this.screenwidth / 2;
            this.posY = this.screenheight;
            this.Location = new Point(this.posX, this.posY);
            this.Item = new EllipseGeometry(this.Location, bubbleSize / 2, bubbleSize / 2);
            this.ColorNumber = rand.Next(0, nrofColors);
            this.Processed = false;
        }

        public Point Location { get; set; }

        public int Velocity { get; set; }

        public int ColorNumber { get; set; }

        public bool Processed { get; set; }
    }
}
