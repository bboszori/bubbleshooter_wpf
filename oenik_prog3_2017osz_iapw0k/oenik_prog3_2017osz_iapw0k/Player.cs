// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        private double lbound = 10;
        private double ubound = 170;
        

        public Player(int posX, int posY, double size)
        {
            this.PosX = posX / 2;
            this.PosY = posY - 15;
            this.Item = new LineGeometry(new Point(this.PosX, this.PosY), new Point(this.PosX, this.PosY - 40));
            this.Bullet = new Bubble(posY, posX, size);
            this.NextBullets = new List<Bubble>();
            this.NextBullets.Add(new Bubble(posX, posY, size, 16 , 0));
            this.NextBullets.Add(new Bubble(posX, posY, size, 16, 1));
            this.ShootingAngle = 0;
        }

        public double PosX { get; set; }

        public double PosY { get; set; }

        public Bubble Bullet { get; set; }

        public List<Bubble> NextBullets { get; set; }

        public double Angle { get; private set; }

        public double ShootingAngle { get; set; }

        public void CalcAngle(double pX, double pY)
        {
            if (pX == this.PosX)
            {
                this.Angle = 90;
            }
            else
            {
                double a = (this.PosX - pX);
                double b = this.PosY - pY;

                this.Angle = System.Math.Atan2(b, a) * (180/System.Math.PI);
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
            double diffX = 40 * Math.Cos(this.Angle * (Math.PI / 180));

                diffX = this.PosX - diffX;

            double diffY = 40 * Math.Sin(this.Angle * (Math.PI / 180));
            diffY = this.PosY - Math.Abs(diffY);

            this.Item = new LineGeometry(new Point(this.PosX, this.PosY), new Point(diffX, diffY));
        }

        public void Move(double angle)
        {
            double dx = -this.CalculatePos().X;
            double dy = this.CalculatePos().Y;

            Transform transform = new TranslateTransform(dx, dy);

            this.Bullet.TransformGeometry(transform);

            Rect bounds = this.Bullet.Item.Bounds;

            if (bounds.Left < 0)
            {
                this.ShootingAngle = 180 - this.ShootingAngle;
            }
            else if (bounds.Right > (this.PosX * 2))
            {
                this.ShootingAngle = 180 - this.ShootingAngle;
            }

        }

        public Point CalculatePos()
        {
            double dx = this.Bullet.Velocity * Math.Cos(this.ShootingAngle * (Math.PI / 180));
            double dy = -1 * (this.Bullet.Velocity * Math.Sin(this.ShootingAngle * (Math.PI / 180)));

            return new Point(dx, dy);
        }
    }
}
