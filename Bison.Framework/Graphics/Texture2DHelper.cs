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
    public static class Texture2DHelper
    {
        /// <summary>
        /// The internal sprite batch to render.
        /// </summary>
        private static SpriteBatch batch;

        /// <summary>
        /// Creates a 1x1 pixel white texture.
        /// </summary>
        /// <returns>The 1x1 pixel texure.</returns>
        public static Texture2D Create()
        {
            return Create(
                1,
                1,
                Color.White);
        }

        /// <summary>
        /// Creates a 1x1 pixel white texture.
        /// </summary>
        /// <param name="color">The texture color.</param>
        /// <returns>The 1x1 pixel texture.</returns>
        public static Texture2D Create(Color color)
        {
            return Create(
                1,
                1,
                color);
        }

        /// <summary>
        /// Creates a NxM pixel texure.
        /// </summary>
        /// <param name="width">The texture width.</param>
        /// <param name="height">The texutre height.</param>
        /// <param name="color">The texture color.</param>
        /// <returns>The NxM pixel texture.</returns>
        public static Texture2D Create(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(
                BisonGame.GraphicsDeviceManager.GraphicsDevice,
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

        /// <summary>
        /// Cuts a Texture2D out of a larger Texture2D
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Texture2D Cut(Texture2D texture, Vector2 offset, int width, int height)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(
                BisonGame.GraphicsDeviceManager.GraphicsDevice,
                width,
                height);
            BisonGame.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(renderTarget);
            BisonGame.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);
            Batch.Begin();
            Batch.Draw(
                texture,
                Vector2.Zero,
                new Rectangle(
                    (int)offset.X,
                    (int)offset.Y,
                    width,
                    height),
                Color.White);
            Batch.End();
            BisonGame.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);

            return renderTarget;
        }

        /// <summary>
        /// Gets the internal sprite batch.
        /// </summary>
        private static SpriteBatch Batch
        {
            get
            {
                if (batch == null)
                {
                    batch  = new SpriteBatch(BisonGame.GraphicsDeviceManager.GraphicsDevice);
                }

                return batch;
            }
        }
    }
}
