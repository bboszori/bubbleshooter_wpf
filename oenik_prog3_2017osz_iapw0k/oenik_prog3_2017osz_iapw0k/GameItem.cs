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

    public class GameItem
    {
        public Geometry Item { get; protected set; }

        public bool CollidesWith(GameItem gItem)
        {
            return Geometry.Combine(this.Item, gItem.Item, GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

        public void TransformGeometry(Transform transform)
        {
            Geometry clone = this.Item.Clone();
            clone.Transform = transform;
            this.Item = clone.GetFlattenedPathGeometry();
        }

        

    }
}
