namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class Bubble
    {
        private static Random rand = new Random();
        private int screenheight;
        private int screenwidth;
        private int posX;
        private int posY;

        public Bubble(int screenHeight, int screeWidth, int row, int column)
        {
            screenheight = screenHeight;
            screenwidth = screeWidth -15;
            if (row % 2 == 0)
            {
                posX = (column * (screenwidth / 10)) + 30;
            }
            else
            {
                posX = (column * (screenwidth / 10)) + 15;
            }
            posY = (row * 24) +15;
            Ball = new EllipseGeometry(new Point(posX, posY), screenwidth / 20, screenwidth / 20);
            ColorNumber = rand.Next(0, 5);
        }

        public Point Location { get; set; }

        public int ColorNumber { get; private set; }

        public Geometry Ball { get; set; }

        public Geometry GetTransformedGeometry()
        {
            Geometry copy = Ball.Clone();
            copy.Transform = new TranslateTransform(Location.X, Location.Y);

            return copy.GetFlattenedPathGeometry();
        }
    }
}
