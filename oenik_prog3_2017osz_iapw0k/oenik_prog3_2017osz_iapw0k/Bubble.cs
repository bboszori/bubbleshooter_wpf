namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    public class Bubble
    {
        private static Random rand = new Random();


        public Bubble(int screenHeight, int screeWidth)
        {
            this.Ball = new EllipseGeometry(new Point(5, 5), 5, 5);
        }

        public Point Location { get; set; }

        protected Geometry Ball { get; set; }

        public Geometry GetTransformedGeometry()
        {
            Geometry copy = this.Ball.Clone();
            copy.Transform = new TranslateTransform(this.Location.X, this.Location.Y);

            return copy.GetFlattenedPathGeometry();
        }
    }
}
