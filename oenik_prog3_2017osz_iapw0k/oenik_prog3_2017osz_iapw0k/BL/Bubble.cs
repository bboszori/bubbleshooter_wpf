// <copyright file="Bubble.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A játékot alkotó buborékot reprezentálja.
    /// </summary>
    internal class Bubble : GameItem
    {
        private static Random rand = new Random();
        private int screenheight;
        private int screenwidth;
        private double posX;
        private double posY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bubble"/> class.
        /// Egy új Bubble objektum létrehozása.
        /// </summary>
        /// <param name="location">Az sor és oszlopszáma a buboréknak.</param>
        /// <param name="bubbleSize">A buborék nagysága.</param>
        /// <param name="nrofColors">Az alkalmazható színek nagysága.</param>
        public Bubble(Point location, double bubbleSize, int nrofColors)
        {
            this.Location = location;
            this.Item = new EllipseGeometry(this.Location, bubbleSize / 2, bubbleSize / 2);
            this.ColorNumber = rand.Next(0, nrofColors);
            this.Processed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bubble"/> class.
        /// Egy új Bubble objektum létrehozása.
        /// </summary>
        /// <param name="screenHeight">A pálya hossza.</param>
        /// <param name="screeWidth">A pálya szélessége.</param>
        /// <param name="bubbleSize">A buborék nagysága.</param>
        /// <param name="nrofColors">Az alkalmazható színek nagysága.</param>
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

        /// <summary>
        /// Gets or sets of bullet location.
        /// A játékos helyzete.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets or sets of bullet's velocity.
        /// A kilőtt buborék sebessége.
        /// </summary>
        public int Velocity { get; set; }

        /// <summary>
        /// Gets or sets the color of actual bubble.
        /// A buborék színének száma.
        /// </summary>
        public int ColorNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets if a bubble is processed, or not.
        /// Tárolja, hogy a buborékot, már feldolgoztuk-e.
        /// </summary>
        public bool Processed { get; set; }
    }
}
