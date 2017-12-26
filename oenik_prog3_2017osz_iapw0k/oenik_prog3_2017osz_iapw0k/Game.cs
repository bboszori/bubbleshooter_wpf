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

        public Bubble Bullet { get; set; }

        public List<Bubble[]> Bubbles { get; set; }

        private void ToStartingState()
        {
            this.Bullet = new Bubble(this.screenHeight, this.screenWidth);
            this.ThePlayer = new Player(this.screenWidth / 2, this.screenHeight - 25);

            this.Bubbles = new List<Bubble[]>();
            for (int i = 0; i < 5; i++)
            {
                Bubble[] bubbleRow = new Bubble[10];
                for (int j = 0; j < 10; j++)
                {
                    bubbleRow[j] = new Bubble(this.screenHeight, this.screenWidth, i, j);
                }

                this.Bubbles.Add(bubbleRow);
            }

            this.dx = 5;
            this.dy = 0;
        }

        private void Shoot()
        {
                
        }
    }
}
