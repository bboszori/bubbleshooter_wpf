namespace oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Oenik_prog3_2017osz_iapw0k;

    public class Game
    {
        private int screenWidth;
        private int screenHeight;

        private int dx;
        private int dy;

        public Bubble Bullet { get; set; }

        public List<Bubble[]> Bubbles { get; set; }

        public Game(int screenHeight, int screenWidth)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.ToStartingState();
        }

        private void ToStartingState()
        {
            this.Bullet = new Bubble(this.screenHeight, this.screenWidth)
            {
                Location = new Point(this.screenWidth, this.screenHeight / 2)
            };

            this.Bubbles = new List<Bubble[]>();
            for (int i = 0; i < 5; i++)
            {
                //
            }

            this.dx = 5;
            this.dy = 0;
        }



    }
}
