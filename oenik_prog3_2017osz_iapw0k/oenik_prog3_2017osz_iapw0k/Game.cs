// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System.Collections.Generic;
    using System.Windows;

    public class Game
    {
        private int screenWidth;
        private int screenHeight;

        private int dx;
        private int dy;

        public Game(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.ToStartingState();
        }

        public Player ThePlayer { get; set; }

        public GameGrid Grid { get; set; }

        public Bubble Bullet { get; set; }

        public List<Bubble[]> Bubbles { get; set; }

        private void ToStartingState()
        {
            this.Grid = new GameGrid();

            this.ThePlayer = new Player(this.screenWidth, this.screenHeight, this.Grid.BubbleSize);



            this.Bubbles = new List<Bubble[]>();
            for (int i = 0; i < 5; i++)
            {
                Bubble[] bubbleRow = new Bubble[10];
                for (int j = 0; j < 10; j++)
                {
                    bubbleRow[j] = new Bubble(this.screenHeight, this.screenWidth, this.Grid.BubbleSize, i, j);
                }

                this.Bubbles.Add(bubbleRow);
            }

            this.dx = 5;
            this.dy = 0;
        }

        public void Shoot()
        {
                
        }

    }
}
