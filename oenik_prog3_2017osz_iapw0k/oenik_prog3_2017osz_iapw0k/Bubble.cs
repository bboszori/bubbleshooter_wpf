namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class Bubble : GameItem
    {
        private static Random rand = new Random();
        private int screenheight;
        private int screenwidth;
        private int posX;
        private int posY;

        public Bubble(int screenHeight, int screeWidth, int row, int column)
        {
            this.screenheight = screenHeight;
            this.screenwidth = screeWidth - 15;
            if (row % 2 == 0)
            {
                this.posX = (column * (this.screenwidth / 10)) + 30;
            }
            else
            {
                this.posX = (column * (this.screenwidth / 10)) + 15;
            }

            this.posY = (row * 25) + 15;
            this.Location = new Point(this.posX, this.posY);
            this.Item = new EllipseGeometry(this.Location, this.screenwidth / 20, this.screenwidth / 20);
            this.ColorNumber = rand.Next(0, 5);
        }

        public Bubble(int screenHeight, int screeWidth)
        {
            this.screenheight = screenHeight - 15;
            this.screenwidth = screeWidth - 15;
            this.posX = this.screenwidth / 2;
            this.posY = this.screenheight;
            this.Location = new Point(this.posX, this.posY);
            this.Item = new EllipseGeometry(this.Location, this.screenwidth / 20, this.screenwidth / 20);
            this.ColorNumber = rand.Next(0, 5);
        }

        public Point Location { get; set; }

        public int ColorNumber { get; private set; }
    }
}
