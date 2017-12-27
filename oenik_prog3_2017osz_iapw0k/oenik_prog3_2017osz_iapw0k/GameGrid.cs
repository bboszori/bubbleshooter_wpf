// <copyright file="GameGrid.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oenik_prog3_2017osz_iapw0k
{
    public class GameGrid
    {
        private double bubbleSize = 28.5;
        private double rowheight = 25;
        private double screenWidth = 300;
        private double screenHeight = 450;

        public GameGrid()
        {
            this.LastRow = 5;
            this.bubbleSize = 28.5;
        }

        public double BubbleSize { get { return this.bubbleSize; } }

        public double ScreenWidth { get; }

        public double ScreenHeight { get; }

        public double LastRow { get; set; }

        public int[] GetIndex(double posX, double posY)
        {
            int[] index = new int[2];
            int offset = 0;

            index[0] = (int)Math.Floor(posY / this.rowheight);

            if (index[0] % 2 != 0)
            {
                offset = 15;
            }

            index[1] = (int)Math.Floor((posY - offset) / this.BubbleSize);

            return index;
        }



    }

}
