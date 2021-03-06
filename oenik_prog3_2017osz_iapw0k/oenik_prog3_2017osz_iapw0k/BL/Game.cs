// <copyright file="Game.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A játék fő logikáját tartalmazza.
    /// </summary>
    internal class Game
    {
        private static Random rand = new Random();
        private int screenWidth;
        private int screenHeight;
        private int rounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// A Game objektum létrehozása.
        /// </summary>
        /// <param name="screenWidth">A pálya szélessége.</param>
        /// <param name="screenHeight">A pálya hossza.</param>
        public Game(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.Scores = new Scores();
            this.Level = new Levels();
            this.ToStartingState();
        }

        /// <summary>
        /// Gets or sets of the player instance.
        /// A játékos objektum létrehozása.
        /// </summary>
        public Player ThePlayer { get; set; }

        /// <summary>
        /// Gets or sets of the Levels object.
        ///  Levels objektum létrehozása.
        /// </summary>
        public Levels Level { get; set; }

        /// <summary>
        /// Gets or sets of the GameGrid object.
        /// A GameGrid objektum létrehozása.
        /// </summary>
        public GameGrid Grid { get; set; }

        /// <summary>
        /// Gets or sets of Scores object.
        /// A pontokat kezelő objektum létrehozása.
        /// </summary>
        public Scores Scores { get; set; }

        /// <summary>
        /// Gets or sets of the list of bubbles.
        /// A pályát alkotó buborék tömb.
        /// </summary>
        public List<Bubble[]> Bubbles { get; set; }

        /// <summary>
        /// A játék kezdőállapotba való állítása.
        /// </summary>
        public void ToStartingState()
        {
            this.Grid = new GameGrid();
            this.rounds = 0;

            this.ThePlayer = new Player(this.screenWidth, this.screenHeight, this.Grid.BubbleSize, this.Level.NrofColors);
            this.Bubbles = new List<Bubble[]>();
            for (int i = 0; i < 15; i++)
            {
                Bubble[] bubbleRow = new Bubble[10];
                for (int j = 0; j < 10; j++)
                {
                    bubbleRow[j] = new Bubble(this.Grid.GetLocation(i, j), this.Grid.BubbleSize, this.Level.NrofColors);
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
        }

        /// <summary>
        /// Egy fordulót vezet végig a játékban:
        /// Ellenőrzi, hogy a kilőtt buborék, nem-e ütközött valamibe
        ///  - ha igen, akkor megnézi, hogy van-e találat (3-nál több azonos színű buborék)
        ///     ha van, akkor eltünteti a kilőtt és a lebegő buborékokat,
        ///     ha nem volt találat, akkor hozzácsatolj a tömbhöz a kilőtt buborékot is, és ha szükséges új sort fűz a tömbhöz;
        /// - a nem ütközött semmibe, akkor tovább mozgatja, és megnézi, mozgatható-e még tovább.
        ///     Ha nem (azaz a pálya tetejébe ütközött, akkor megnézi, hogy van-e találat (3-nál több azonos színű buborék)
        ///     ha van, akkor eltünteti a kilőtt és a lebegő buborékokat,
        ///     ha nem volt találat, akkor hozzácsatolj a tömbhöz a kilőtt buborékot is, és ha szükséges új sort fűz a tömbhöz.
        /// </summary>
        public void DoTurn()
        {
            if (this.IsItCollides())
            {
                Point mypoint = this.PinBubble();
                List<Bubble> toreset = this.FindSameColors((int)mypoint.X, (int)mypoint.Y);

                if (toreset.Count >= 3)
                {
                    this.rounds = 0;
                    foreach (Bubble item in toreset)
                    {
                        item.ColorNumber = -1;
                    }

                    this.Scores.AddtoScore(toreset.Count);

                    this.FindFloatings();
                    this.ColorsLeft();
                }
                else
                {
                    this.rounds++;
                    if (this.rounds % this.Level.NrofRounds == 0)
                    {
                        this.AddNewLine();
                        this.ColorsLeft();
                    }
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
                        this.rounds = 0;
                        foreach (Bubble item in toreset)
                        {
                            item.ColorNumber = -1;
                        }

                        this.Scores.AddtoScore(toreset.Count);

                        this.FindFloatings();
                        this.ColorsLeft();
                    }
                    else
                    {
                        this.rounds++;
                        if (this.rounds % this.Level.NrofRounds == 0)
                        {
                            this.AddNewLine();
                            this.ColorsLeft();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy nem-e vesztettük el a játékot (azaz, a legalsó sorban van-e buborék).
        /// </summary>
        /// <returns>Igaz értéket ad vissza, ha vesztettünk.</returns>
        public bool CheckIfLoose()
        {
            int i = 0;

            while (i < 10 && this.Bubbles[this.Bubbles.Count - 1][i].ColorNumber == -1)
            {
                i++;
            }

            return i < 10;
        }

        /// <summary>
        /// Ellenőrzi, hogy nem-e nyertük meg a játékot (azaz, a legfelső sorban van-e buborék).
        /// </summary>
        /// <returns>Igaz értéket ad vissza, ha már nincs több buborék.</returns>
        public bool CheckIfWin()
        {
            int i = 0;

            while (i < 10 && this.Bubbles[0][i].ColorNumber == -1)
            {
                i++;
            }

            return i >= 10;
        }

        /// <summary>
        /// Egy új sort ad hozzá a tömb elejéhez.
        /// </summary>
        public void AddNewLine()
        {
            Bubble[] newLine = new Bubble[10];
            this.ThePlayer.Bullet.Velocity = 0;
            for (int i = 0; i < newLine.Length; i++)
            {
                newLine[i] = new Bubble(this.Grid.GetLocation(0, i), this.Grid.BubbleSize, this.Level.NrofColors);
            }

            this.Bubbles.RemoveAt(this.Bubbles.Count - 1);
            int j = 0;
            foreach (Bubble[] arr in this.Bubbles)
            {
                foreach (Bubble item in arr)
                {
                    if (j % 2 == 0)
                    {
                        TranslateTransform transform = new TranslateTransform(15, 25);
                        item.TransformGeometry(transform);
                    }
                    else
                    {
                        TranslateTransform transform = new TranslateTransform(-15, 25);
                        item.TransformGeometry(transform);
                    }
                }

                j++;
            }

            this.Bubbles.Insert(0, newLine);

            this.FindFloatings();
        }

        /// <summary>
        /// Megcseréli az épp kilőhető buborékot a következővel.
        /// </summary>
        public void SwitchBullets()
        {
            int c;

            c = this.ThePlayer.Bullet.ColorNumber;
            this.ThePlayer.Bullet.ColorNumber = this.ThePlayer.NextBullets[1].ColorNumber;
            this.ThePlayer.NextBullets[1].ColorNumber = c;
        }

        private Point PinBubble()
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
            this.ThePlayer.NextBullets.Insert(0, new Bubble(new Point(20, 420), this.Grid.BubbleSize, this.Level.NrofColors));
            TranslateTransform transform = new TranslateTransform(30, 0);
            this.ThePlayer.NextBullets[1].TransformGeometry(transform);
            this.ToStartingPoint();

            return new Point(row, column);
        }

        private bool IsItCollides()
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

        private List<Bubble> FindSameColors(int row, int column)
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

        private void FindFloatings()
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
                            }

                            this.Scores.AddtoScore(cluster.Count);
                        }
                        }
                    }
                }
            }

        private List<Bubble> GetNeighbours(Bubble current)
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
                    if ((column + 1) < 10)
                    {
                        neighbours.Add(this.Bubbles[row - 1][column + 1]);
                    }

                    neighbours.Add(this.Bubbles[row - 1][column]);
                }

                if ((row + 1) < 15)
                {
                    if ((column + 1) < 10)
                    {
                        neighbours.Add(this.Bubbles[row + 1][column + 1]);
                    }

                    neighbours.Add(this.Bubbles[row + 1][column]);
                }
            }

            return neighbours;
        }

        private void ResetProcessed()
        {
            foreach (Bubble[] arr in this.Bubbles)
            {
                foreach (Bubble item in arr)
                {
                    item.Processed = false;
                }
            }
        }

        private void ColorsLeft()
        {
            List<int> colors = new List<int>();

            foreach (Bubble[] arr in this.Bubbles)
            {
                foreach (Bubble item in arr)
                {
                    if (colors.Count < this.Level.NrofColors)
                    {
                        if (item.ColorNumber >= 0 && !colors.Contains(item.ColorNumber))
                        {
                            colors.Add(item.ColorNumber);
                        }
                    }
                }
            }

            if (colors.Count > 0)
            {
                if (!colors.Contains(this.ThePlayer.Bullet.ColorNumber))
                {
                    int i = rand.Next(0, this.Level.NrofColors);
                    while (!colors.Contains(i))
                    {
                        i = rand.Next(0, this.Level.NrofColors);
                    }

                    this.ThePlayer.Bullet.ColorNumber = i;
                }

                if (!colors.Contains(this.ThePlayer.NextBullets[0].ColorNumber))
                {
                    int i = rand.Next(0, this.Level.NrofColors);
                    while (!colors.Contains(i))
                    {
                        i = rand.Next(0, this.Level.NrofColors);
                    }

                    this.ThePlayer.NextBullets[0].ColorNumber = i;
                }

                if (!colors.Contains(this.ThePlayer.NextBullets[1].ColorNumber))
                {
                    int i = rand.Next(0, this.Level.NrofColors);
                    while (!colors.Contains(i))
                    {
                        i = rand.Next(0, this.Level.NrofColors);
                    }

                    this.ThePlayer.NextBullets[1].ColorNumber = i;
                }
                }
        }

        private void ToStartingPoint()
        {
            double posX = this.screenWidth / 2;
            double posY = this.screenHeight - (this.Grid.BubbleSize / 2);
            Point location = new Point(posX, posY);
            this.ThePlayer.Bullet.Item = new EllipseGeometry(location, this.Grid.BubbleSize / 2, this.Grid.BubbleSize / 2);
        }
    }
}
