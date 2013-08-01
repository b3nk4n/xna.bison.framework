using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Graphics
{
    /// <summary>
    /// Factory class which creates solid color texures.
    /// </summary>
    public static class Texture2DFactory
    {
        /// <summary>
        /// Creates a 1x1 pixel white texture.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <returns>The 1x1 pixel texure.</returns>
        public static Texture2D Create(GraphicsDevice graphicsDevice)
        {
            return Create(graphicsDevice, 1, 1, Color.White);
        }

        /// <summary>
        /// Creates a 1x1 pixel white texture.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="color">The texture color.</param>
        /// <returns>The 1x1 pixel texture.</returns>
        public static Texture2D Create(GraphicsDevice graphicsDevice, Color color)
        {
            return Create(graphicsDevice, 1, 1, color);
        }

        /// <summary>
        /// Creates a NxM pixel texure.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="width">The texture width.</param>
        /// <param name="height">The texutre height.</param>
        /// <param name="color">The texture color.</param>
        /// <returns>The NxM pixel texture.</returns>
        public static Texture2D Create(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(
                graphicsDevice,
                width,
                height,
                false,
                SurfaceFormat.Color);

            // Fill the texture with a solid color.
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color; // new Color(color.ToVector3()); ??? 
            }
            texture.SetData(colors);

            return texture;
        }
    }
}
