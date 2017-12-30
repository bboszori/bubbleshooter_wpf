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
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A Player osztály tartalmazza azokat az információkat és funkcionalitásokat, amik a játék irányítáához szükségesek.
    /// </summary>
    internal class Player : GameItem
    {
        private double lbound = 10;
        private double ubound = 170;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// A Player objektum léttrehozása.
        /// </summary>
        /// <param name="posX">A pálya szélessége.</param>
        /// <param name="posY">A pálya hossza.</param>
        /// <param name="size">A buborék nagysága.</param>
        /// <param name="nrOfColors">Az alkalmazható színek nagysága.</param>
        public Player(int posX, int posY, double size, int nrOfColors)
        {
            this.PosX = posX / 2;
            this.PosY = posY - 15;
            this.Item = new LineGeometry(new Point(this.PosX, this.PosY), new Point(this.PosX, this.PosY - 40));
            this.Bullet = new Bubble(posY, posX, size, nrOfColors);
            this.NextBullets = new List<Bubble>();
            this.NextBullets.Add(new Bubble(new Point(20, 420), size, nrOfColors));
            this.NextBullets.Add(new Bubble(new Point(50, 420), size, nrOfColors));
            this.ShootingAngle = 0;
        }

        /// <summary>
        /// Gets or sets of X coordinate of the Player.
        /// A játékos elhelyezkedésének X koordinátája.
        /// </summary>
        public double PosX { get; set; }

        /// <summary>
        /// Gets or sets of Y coordinate of the Player.
        /// A játékos elhelyezkedésének Y koordinátája.
        /// </summary>
        public double PosY { get; set; }

        /// <summary>
        /// Gets or sets of the actual bullet.
        /// Az aktuális kilőhető bubble.
        /// </summary>
        public Bubble Bullet { get; set; }

        /// <summary>
        /// Gets or sets of the next 2 bullets.
        /// A 2 következő kilőhető buborék.
        /// </summary>
        public List<Bubble> NextBullets { get; set; }

        /// <summary>
        /// Gets of the angle of actual mouse position.
        /// A játékos egérpozíciójának aktuális szöge.
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        /// Gets or sets of the angle of the shooted bullet.
        /// A kilőtt bubble szöge.
        /// </summary>
        public double ShootingAngle { get; set; }

        /// <summary>
        /// Kiszámolja az aktuális szöget a megadott pozíció alapján.
        /// </summary>
        /// <param name="pX">X pozició</param>
        /// <param name="pY">Y pozició</param>
        public void CalcAngle(double pX, double pY)
        {
            if (pX == this.PosX)
            {
                this.Angle = 90;
            }
            else
            {
                double a = this.PosX - pX;
                double b = this.PosY - pY;

                this.Angle = System.Math.Atan2(b, a) * (180 / System.Math.PI);
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

        /// <summary>
        /// A kilövés irányát jelző vonal forgatása.
        /// </summary>
        public void Rotate()
        {
            double diffX = 40 * Math.Cos(this.Angle * (Math.PI / 180));

                diffX = this.PosX - diffX;

            double diffY = 40 * Math.Sin(this.Angle * (Math.PI / 180));
            diffY = this.PosY - Math.Abs(diffY);

            this.Item = new LineGeometry(new Point(this.PosX, this.PosY), new Point(diffX, diffY));
        }

        /// <summary>
        /// A kilőtt bubble mozgatása.
        /// </summary>
        /// <returns>Mozgatható-e tovább, vagy a pálya tetejébe ütközött.</returns>
        public bool Move()
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

            if (bounds.Top < 0)
            {
                return false;
            }

            return true;
        }

        private Point CalculatePos()
        {
            double dx = this.Bullet.Velocity * Math.Cos(this.ShootingAngle * (Math.PI / 180));
            double dy = -1 * (this.Bullet.Velocity * Math.Sin(this.ShootingAngle * (Math.PI / 180)));

            this.Bullet.Location = new Point(this.Bullet.Location.X - dx, this.Bullet.Location.Y + dy);

            return new Point(dx, dy);
        }
    }
}
