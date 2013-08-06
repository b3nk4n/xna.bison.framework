using Bison.Framework.Graphics;
using Bison.Framework.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Sprite batch extension class for simple primitive rendering.
    /// </summary>
    public static class SpriteBatchPrimitivesExtensions
    {
        /// <summary>
        /// The used white 1x1 pixel texture.
        /// </summary>
        private static readonly Texture2D PixelTexture = Texture2DFactory.Create();

        /// <summary>
        /// Renders a line.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="position1">The line start.</param>
        /// <param name="position2">The line end.</param>
        public static void DrawLine(this SpriteBatch batch, Vector2 position1, Vector2 position2)
        {
            batch.DrawLine(position1, position2, Color.White, 1.0f);
        }

        /// <summary>
        /// Renders a line.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="position1">The line start.</param>
        /// <param name="position2">The line end.</param>
        /// <param name="color">The line color.</param>
        /// <param name="width">The line thickness.</param>
        public static void DrawLine(this SpriteBatch batch, Vector2 position1, Vector2 position2, Color color, float width)
        {
            float angle = (float)Math.Atan2(position2.Y - position1.Y, position2.X - position1.X);
            float length = Vector2.Distance(position1, position2);

            batch.Draw(PixelTexture, position1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        /// <summary>
        /// Renders a circle.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="center">The circles center.</param>
        /// <param name="radius">The circle radius.</param>
        public static void DrawCircle(this SpriteBatch batch, Vector2 center, float radius)
        {
            batch.DrawCircle(center, radius, Color.White, 1.0f);
        }

        /// <summary>
        /// Renders a circle.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="center">The circles center.</param>
        /// <param name="radius">The circle radius.</param>
        /// <param name="color">The line color.</param>
        /// <param name="thickness">The line thickness.</param>
        public static void DrawCircle(this SpriteBatch batch, Vector2 center, float radius, Color color, float thickness)
        {
            //compute how many vertices we want so it looks circular
            int n = (int)(radius / 2);

            batch.DrawNgon(center, radius, n, color, thickness);
        }

        /// <summary>
        /// Renders a circle with n edges.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="center">The ngon center.</param>
        /// <param name="radius">The ngon radius.</param>
        /// <param name="n">The number of edges.</param>
        /// <param name="color">The line color.</param>
        /// <param name="thickness">The line thickness.</param>
        public static void DrawNgon(this SpriteBatch batch, Vector2 center, float radius, int n, Color color, float thickness)
        {
            //szize of angle between each vertex
            float increment = (float)Math.PI * 2 / n;
            Vector2[] certices = new Vector2[n];
            //compute the locations of all the vertices
            for (int i = 0; i < n; i++)
            {
                certices[i].X = (float)Math.Cos(increment * i);
                certices[i].Y = (float)Math.Sin(increment * i);
            }
            //Now draw all the lines
            for (int i = 0; i < n - 1; i++)
            {
                batch.DrawLine(center + certices[i] * radius, center + certices[i + 1] * radius, color, thickness);
            }

            batch.DrawLine(center + radius * certices[0], center + radius * certices[n - 1], color ,thickness);
        }

        /// <summary>
        /// Renders an empty rectangle.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="rectangle">The rectangle.</param>
        public static void DrawRectangle(this SpriteBatch batch, Rectangle rectangle)
        {
            batch.DrawRectangle(new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// Renders an empty rectangle.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="location">The top left location.</param>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        public static void DrawRectangle(this SpriteBatch batch, Vector2 location, float width, float height)
        {
            batch.DrawRectangle(location, width, height, Color.White, 1.0f);
        }

        /// <summary>
        /// Renders an empty rectangle.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The line color.</param>
        /// <param name="thickness">The line thickness.</param>
        public static void DrawRectangle(this SpriteBatch batch, Rectangle rectangle, Color color, float thickness)
        {
            batch.DrawRectangle(new Vector2(rectangle.X, rectangle.Y), rectangle.Width, rectangle.Height, color, thickness);
        }

        /// <summary>
        /// Renders an empty rectangle.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="location">The top left location.</param>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <param name="color">The line color.</param>
        /// <param name="thickness">The line thickness.</param>
        public static void DrawRectangle(this SpriteBatch batch, Vector2 location, float width, float height, Color color, float thickness)
        {
            Vector2 topRight = new Vector2(location.X + width, location.Y);
            Vector2 bottomLeft = new Vector2(location.X, location.Y + height);
            Vector2 bottomRight = new Vector2(location.X + width, location.Y + height);

            batch.DrawLine(
                location,
                topRight,
                color,
                thickness);

            batch.DrawLine(
                location,
                bottomLeft,
                color,
                thickness);

            batch.DrawLine(
                topRight,
                bottomRight,
                color,
                thickness);

            batch.DrawLine(
                bottomLeft,
                bottomRight,
                color,
                thickness);
        }
    }
}
