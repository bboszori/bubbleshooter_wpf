// <copyright file="Scores.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A Scores osztály tartalmazza azokat az információkat és funkcionalitásokat, amik az elért pontszám kezeléséhez szükséges.
    /// </summary>
    internal class Scores
    {
        private static int point = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scores"/> class.
        /// Scores objektum létrehozása.
        /// </summary>
        public Scores()
        {
            this.Score = 0;
        }

        /// <summary>
        /// Gets or sets the actual score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Megnöveli az elért pontszámot, annak függvényében, hogy hány buborékot sikerült kilőni.
        /// </summary>
        /// <param name="nr">A kilőtt buborékok száma.</param>
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

        /// <summary>
        /// Lenulláza az aktuális pontszámot.
        /// </summary>
        public void ResetScore()
        {
            this.Score = 0;
        }
    }
}
