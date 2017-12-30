// <copyright file="Levels.cs" company="PlaceholderCompany">
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
    /// A Levels osztály tartalmazza azokat az információkat és funkcionalitásokat, amik a játék szintjeinek kezeléséhez szükséges.
    /// </summary>
    internal class Levels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Levels"/> class.
        /// Levels objektum létrehozása.
        /// </summary>
        public Levels()
        {
            this.Level = 1;
            this.GetNrofColors();
            this.NrofRounds = 5;
        }

        /// <summary>
        /// Gets or sets of actual level of game.
        /// A játék aktuális szintje.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets of the actual number of colors.
        /// Az elérhető színek aktuális száma.
        /// </summary>
        public int NrofColors { get; private set; }

        /// <summary>
        /// Gets the actual number of rounds.
        /// Azon körök aktuális száma, ahányszor rontani lehet, új sor hozzáadása nélkül.
        /// </summary>
        public int NrofRounds { get; private set; }

        /// <summary>
        /// Eggyel növeli az aktuális szintet és kszámolja az elérhető színek és körök számát.
        /// </summary>
        public void LevelUp()
        {
            this.Level++;
            this.GetNrofColors();
            this.GetNrofRounds();
        }

        /// <summary>
        /// Alapállapotba állítja az értékeket.
        /// </summary>
        public void ResetLevel()
        {
            this.Level = 1;
            this.GetNrofColors();
            this.NrofRounds = 5;
        }

        private void GetNrofColors()
        {
            if (this.Level < 5)
            {
                this.NrofColors = 3;
            }
            else if (this.Level < 9)
            {
                this.NrofColors = 4;
            }
            else if (this.Level < 13)
            {
                this.NrofColors = 5;
            }
            else if (this.Level < 17)
            {
                this.NrofColors = 6;
            }
            else
            {
                this.NrofColors = 7;
            }
        }

        private void GetNrofRounds()
        {
            if (this.Level < 17 && this.NrofColors > 2)
            {
                if (this.Level % 5 == 0)
                {
                    this.NrofRounds = 5;
                }
                else
                {
                    this.NrofRounds--;
                }
            }
        }
   }
}
