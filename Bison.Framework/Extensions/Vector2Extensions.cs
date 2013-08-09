using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Extension methods for Microsoft.Xna.Framework.Vector2 of standard XNA.
    /// </summary>
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 vector, Vector2 center, float rotation)
        {
            return Vector2.Transform(vector - center, Matrix.CreateRotationZ(rotation)) + center;
        } 
    }
}
