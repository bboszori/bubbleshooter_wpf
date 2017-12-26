namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows;

    public class Player : GameItem
    {
        private double lbound = 15;
        private double ubound = 165;

        public Player(int posX, int posY)
        {
            this.PosX = posX -7;
            this.PosY = posY;
            this.Item = new LineGeometry(new Point(this.PosX, this.PosY), new Point(this.PosX, this.PosY -40));
        }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public double Angle { get; private set; }

        public void CalcAngle(double pX, double pY)
        {
            if (pX == this.PosX -7)
            {
                this.Angle = 90;
            }
            else
            {
                double a = (this.PosX - pX);
                double b = this.PosY - pY;

                this.Angle = System.Math.Atan2(a, b) * (180/System.Math.PI);
                if (this.Angle < 0)
                {
                    this.Angle = 180 + this.Angle;
                }

                if (this.Angle > 90 && this.Angle < 270)
                {
                    if (this.Angle > this.ubound)
                    {
                        this.Angle = this.ubound;
                    }
                }
                else
                {
                    if (this.Angle < this.lbound || this.lbound >= 270)
                    {
                        this.Angle = this.lbound;
                    }
                }

            }
        }

        public void Rotate()
        {
            double diffX = 40 * Math.Sin(this.Angle * (Math.PI / 180));
            if (this.Angle < 90)
            {
                diffX = this.PosX - diffX;
            }
            else
            {
                diffX = this.PosX + diffX;
            }

            double diffY = 40 * Math.Cos(this.Angle * (Math.PI / 180));
            diffY = this.PosY - Math.Abs(diffY);

            this.Item = new LineGeometry(new Point(this.PosX, this.PosY), new Point(diffX, diffY));

            if (this.Angle == 90)
            {
                new LineGeometry(new Point(this.PosX, this.PosY), new Point(this.PosX, this.PosY - 40));
            }

        }


    }
}
