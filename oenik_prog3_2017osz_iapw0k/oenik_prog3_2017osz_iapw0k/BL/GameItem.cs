// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;

    /// <summary>
    /// Játékelemekhez tartozó metódusokat tartalmazza.
    /// </summary>
    internal class GameItem
    {
        /// <summary>
        /// Gets or sets of the geometry of the gameitem.
        /// A játék elemhez tartozó Geometry.
        /// </summary>
        public Geometry Item { get; set; }

        /// <summary>
        /// Ellenörzi, hogy össze ütközött-e a játékelemünk a megadott másik játékelemmel.
        /// </summary>
        /// <param name="gItem">A másik játékelem.</param>
        /// <returns> Igazzal tér vissza, ha a két játékelem összeütközött.</returns>
        public bool CollidesWith(GameItem gItem)
        {
            return Geometry.Combine(this.Item, gItem.Item, GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

        /// <summary>
        /// A játékelem mozgatása.
        /// </summary>
        /// <param name="transform">A transzformáció adatai.</param>
        public void TransformGeometry(Transform transform)
        {
            Geometry clone = this.Item.Clone();
            clone.Transform = transform;
            this.Item = clone.GetFlattenedPathGeometry();
        }
    }
}
