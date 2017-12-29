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

    class GameGrid
    {
        private double bubbleSize = 28.5;
        private double rowheight = 25;
        private double screenWidth = 300;
        private double screenHeight = 400;

        public GameGrid()
        {
            this.LastRow = 4;
            this.bubbleSize = 28.5;
        }

        public double BubbleSize { get { return this.bubbleSize; } }

        public double ScreenWidth { get; }

        public double ScreenHeight { get; }

        public double LastRow { get; set; }

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
