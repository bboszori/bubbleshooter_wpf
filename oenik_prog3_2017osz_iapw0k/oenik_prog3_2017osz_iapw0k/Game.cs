// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

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
            this.Scores = new Scores();
        }

        public Player ThePlayer { get; set; }

        public GameGrid Grid { get; set; }

        public Scores Scores { get; set; }

        public List<Bubble[]> Bubbles { get; set; }

        private void ToStartingState()
        {
            this.Grid = new GameGrid();

            this.ThePlayer = new Player(this.screenWidth, this.screenHeight, this.Grid.BubbleSize);



            this.Bubbles = new List<Bubble[]>();
            for (int i = 0; i < 15; i++)
            {
                Bubble[] bubbleRow = new Bubble[10];
                for (int j = 0; j < 10; j++)
                {
                    bubbleRow[j] = new Bubble(this.Grid.GetLocation(i, j), this.Grid.BubbleSize);
                }

                this.Bubbles.Add(bubbleRow);
            }

            for (int i = 5; i < 15; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    this.Bubbles[i][j].ColorNumber = -1;
                }
            }


            this.dx = 5;
            this.dy = 0;
        }

        public void Shoot()
        {

        }

        public void DoTurn()
        {
            if (this.IsItCollides())
            {
                Point mypoint = this.PinBubble();
                List<Bubble> toreset = this.FindSameColors((int)mypoint.X, (int)mypoint.Y);

                if (toreset.Count >= 3)
                {
                    foreach (Bubble item in toreset)
                    {
                        item.ColorNumber = -1;
                        this.Scores.AddtoScore();
                    }

                    this.FindFloatings();
                }

                
            }
           else
            {
                if (!this.ThePlayer.Move())
                {
                    Point mypoint = this.PinBubble();
                    List<Bubble> toreset = this.FindSameColors((int)mypoint.X, (int)mypoint.Y);

                    if (toreset.Count >= 3)
                    {
                        foreach (Bubble item in toreset)
                        {
                            item.ColorNumber = -1;
                            this.Scores.AddtoScore();
                        }

                        this.FindFloatings();
                    }

                    
                }

                
            }

        }

        public Point PinBubble()
        {
            Rect bounds = this.ThePlayer.Bullet.Item.Bounds;

            int row = this.Grid.GetRowIndex(bounds.Y);
            int column = this.Grid.GetColumnIndex(bounds.X, row);
            Point loc = this.Grid.GetLocation(row, column);

            if (this.Bubbles[row][column].ColorNumber == -1)
            {
                this.Bubbles[row][column].ColorNumber = this.ThePlayer.Bullet.ColorNumber;
            }

            this.Bubbles[row][column].Velocity = 0;
            this.ThePlayer.Bullet = null;


            this.ThePlayer.Bullet = this.ThePlayer.NextBullets[1];
            this.ThePlayer.NextBullets.RemoveAt(1);
            this.ThePlayer.NextBullets.Add(this.ThePlayer.NextBullets[0]);
            this.ThePlayer.NextBullets.RemoveAt(0);
            this.ThePlayer.NextBullets.Insert(0, new Bubble(new Point(20, 420), this.Grid.BubbleSize));
            TranslateTransform transform = new TranslateTransform(30, 0);
            this.ThePlayer.NextBullets[1].TransformGeometry(transform);
            this.ToStartingPoint();

            return new Point(row, column);

        }

        public void ToStartingPoint()
        {
            double posX = this.screenWidth / 2;
            double posY = this.screenHeight - (this.Grid.BubbleSize / 2);
            Point location = new Point(posX, posY);
            this.ThePlayer.Bullet.Item = new EllipseGeometry(location, this.Grid.BubbleSize / 2, this.Grid.BubbleSize / 2);

        }

        public bool IsItCollides()
        {

            for (int i = this.Bubbles.Count - 1; i >= 0; i--)
            {
                foreach (Bubble item in this.Bubbles[i])
                {
                    if (item.ColorNumber >= 0)
                    {
                        if (this.ThePlayer.Bullet.CollidesWith(item))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public List<Bubble> FindSameColors(int row, int column)
        {
            this.ResetProcessed();

            List<Bubble> foundBubbles = new List<Bubble>();
            Queue<Bubble> toprocess = new Queue<Bubble>();

            this.Bubbles[row][column].Processed = true;
            int color = this.Bubbles[row][column].ColorNumber;
            toprocess.Enqueue(this.Bubbles[row][column]);

            while (toprocess.Count > 0)
            {
                Bubble current = toprocess.Dequeue();

                if (current.ColorNumber == color)
                {
                    foundBubbles.Add(current);

                    List<Bubble> neighbours = this.GetNeighbours(current);

                    foreach (Bubble item in neighbours)
                    {
                        if (item.Processed == false && item.ColorNumber >= 0)
                        {
                            item.Processed = true;
                            toprocess.Enqueue(item);
                        }
                    }
                }
            }

            return foundBubbles;
        }

        public void FindFloatings()
        {
            this.ResetProcessed();

            foreach (Bubble[] arr in this.Bubbles)
            {
                foreach (Bubble item in arr)
                {

                    if (!item.Processed && item.ColorNumber >= 0)
                    {
                        item.Processed = true;
                        Queue<Bubble> toprocess = new Queue<Bubble>();
                        List<Bubble> cluster = new List<Bubble>();
                        toprocess.Enqueue(item);

                        while (toprocess.Count > 0)
                        {
                            Bubble current = toprocess.Dequeue();
                            cluster.Add(current);
                            List<Bubble> neighbours = this.GetNeighbours(current);
                            foreach (Bubble n in neighbours)
                            {
                                if (!n.Processed && n.ColorNumber >= 0)
                                {
                                    n.Processed = true;
                                    toprocess.Enqueue(n);
                                }
                            }

                        }

                        bool floating = true;
                        foreach (Bubble b in cluster)
                        {
                            Rect bounds = b.Item.Bounds;
                            int row = this.Grid.GetRowIndex(bounds.Y);

                            if (row == 0)
                            {
                                floating = false;
                            }


                        }

                        if (floating)
                        {
                            foreach (Bubble b in cluster)
                            {
                                b.ColorNumber = -1;
                                this.Scores.AddtoScore();
                            }
                        }
                        }

                    }

                }
            }


        public List<Bubble> GetNeighbours(Bubble current)
        {
            List<Bubble> neighbours = new List<Bubble>();
            Rect bounds = current.Item.Bounds;
            int row = this.Grid.GetRowIndex(bounds.Y);
            int column = this.Grid.GetColumnIndex(bounds.X, row);

            if ((column - 1) >= 0)
            {
                neighbours.Add(this.Bubbles[row][column - 1]);
            }
            if ((column + 1) < 10)
            {
                neighbours.Add(this.Bubbles[row][column + 1]);
            }

            if (row % 2 == 0)
            {
                if ((row - 1) >= 0)
                {
                    if ((column - 1) >= 0)
                    {
                        neighbours.Add(this.Bubbles[row - 1][column - 1]);
                    }

                    neighbours.Add(this.Bubbles[row - 1][column]);
                }

                if ((row + 1) < 15)
                {
                    if ((column - 1) >= 0)
                    {
                        neighbours.Add(this.Bubbles[row + 1][column - 1]);
                    }

                    neighbours.Add(this.Bubbles[row + 1][column]);
                }
            }
            else
            {
                if ((row - 1) >= 0)
                {
                    if ((column + 1) <10)
                    {
                        neighbours.Add(this.Bubbles[row - 1][column + 1]);
                    }

                    neighbours.Add(this.Bubbles[row - 1][column]);
                }

                if ((row + 1) < 15)
                {
                    if ((column + 1) <10)
                    {
                        neighbours.Add(this.Bubbles[row + 1][column + 1]);
                    }

                    neighbours.Add(this.Bubbles[row + 1][column]);
                }
            }

            return neighbours;

        }


        public void ResetProcessed()
        {
            foreach (Bubble[] arr in this.Bubbles)
            {
                foreach (Bubble item in arr)
                {
                    item.Processed = false;
                }
            }

        }

    }
}
