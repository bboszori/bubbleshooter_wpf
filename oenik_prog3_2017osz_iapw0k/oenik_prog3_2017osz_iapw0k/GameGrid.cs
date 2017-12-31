// <copyright file="GameGrid.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// A pálya adatainak kezelése.
    /// </summary>
    internal class GameGrid
    {
        private double bubbleSize = 28.5;
        private double rowheight = 25;
        private double screenWidth = 300;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameGrid"/> class.
        /// Új pálya létrehozása.
        /// </summary>
        public GameGrid()
        {
            this.bubbleSize = 28.5;
        }

        /// <summary>
        /// Gets the bubblesize.
        /// A buborék mérete.
        /// </summary>
        public double BubbleSize
        {
            get { return this.bubbleSize; }
        }

        /// <summary>
        /// Gets the screenwidth.
        /// A pálya szélessége.
        /// </summary>
        public double ScreenWidth { get; }

        /// <summary>
        /// Az y koordináta alapján, megadja hogy melyik sorban helyezkedik el az adott buborék.
        /// </summary>
        /// <param name="y">A buborék helyzetének y koordinátája.</param>
        /// <returns>A sor számát adja vissza.</returns>
        public int GetRowIndex(double y)
        {
            if (y < 0)
            {
                y = 0;
            }

            int row = (int)Math.Round(y / this.rowheight);

            if (row > 14)
            {
                return 14;
            }
            else
            {
                return row;
            }
        }

        /// <summary>
        /// Az y koordináta és a sor száma alapján, megadja hogy melyik oszlopban helyezkedik el az adott buborék.
        /// </summary>
        /// <param name="x">>A buborék helyzetének x koordinátája.</param>
        /// <param name="row">A uborék sorának száma.</param>
        /// <returns>Az oszlop számát adja vissza.</returns>
        public int GetColumnIndex(double x, int row)
        {
            if (x < 0)
            {
                x = 0;
            }

            int offset = 15;

            if (row % 2 != 0)
            {
                offset = 30;
            }

            if (x < offset)
            {
                return 0;
            }
            else
            {
                if ((int)Math.Ceiling((x - offset) / this.BubbleSize) > 9)
                {
                    return 9;
                }
                else
                {
                    return (int)Math.Ceiling((x - offset) / this.BubbleSize);
                }
            }
        }

        /// <summary>
        /// A sor és az oszlop szám alapján megadja a buborék koordinátáit.
        /// </summary>
        /// <param name="row">A sor száma.</param>
        /// <param name="column">Az oszlop száma.</param>
        /// <returns>A buborék koorinátáit adja vissza.</returns>
        public Point GetLocation(int row, int column)
        {
            double posX = 0;
            double posY = 0;

            if (row % 2 == 0)
            {
                posX = (column * ((this.screenWidth - 15) / 10)) + 15;
            }
            else
            {
                posX = (column * ((this.screenWidth - 15) / 10)) + 30;
            }

            posY = (row * this.rowheight) + 15;

            return new Point(posX, posY);
        }
    }
}
