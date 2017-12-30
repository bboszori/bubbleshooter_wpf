namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Scores
    {
        private static int point = 10;

        public Scores()
        {
            this.Score = 0;
        }

        public int Score { get; set; }

        public void AddtoScore(int nr)
        {
            if (nr < 5)
            {
                this.Score += nr * point;
            }
            else if (nr < 10)
            {
                this.Score += 2 * nr * point;
            }
            else
            {
                this.Score += 3 * nr * point;
            }
        }

        public void ResetScore()
        {
            this.Score = 0;
        }
    }
}
