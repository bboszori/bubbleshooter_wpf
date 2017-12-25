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
            ToStartingState();
        }

        public Bubble Bullet { get; set; }

        public List<Bubble[]> Bubbles { get; set; }

        private void ToStartingState()
        {
            Bullet = new Bubble(screenHeight, screenWidth, 0, 0)
            {
                Location = new Point(screenWidth, screenHeight / 2)
            };

            Bubbles = new List<Bubble[]>();
            for (int i = 0; i < 5; i++)
            {
                Bubble[] bubbleRow = new Bubble[10];
                for (int j = 0; j < 10; j++)
                {
                    bubbleRow[j] = new Bubble(screenHeight, screenWidth, i, j);
                }

                Bubbles.Add(bubbleRow);
            }

            dx = 5;
            dy = 0;
        }
    }
}
