using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// The global game camera.
    /// </summary>
    public static class Camera
    {
        #region Members

        /// <summary>
        /// The cameras position.
        /// </summary>
        private static Vector2 position = Vector2.Zero;

        /// <summary>
        /// The cameras view port.
        /// </summary>
        private static Vector2 viewPortSize = Vector2.Zero;

        /// <summary>
        /// The world bounds, in which the camara can be moved.
        /// </summary>
        private static Rectangle worldRectangle = new Rectangle(0, 0, 0, 0);

        #endregion

        #region Methods

        /// <summary>
        /// Moves the camera relatively.
        /// </summary>
        /// <param name="offset">The offset to move</param>
        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

        /// <summary>
        /// Checks whether the object bounds are visible.
        /// </summary>
        /// <param name="bounds">The object bounds</param>
        /// <returns>TRUE if the object is visible on screen</returns>
        public static bool IsObjectVisible(Rectangle bounds)
        {
            return ViewPort.Intersects(bounds);
        }

        /// <summary>
        /// Converts the world coordinates to screen coordinates.
        /// </summary>
        /// <param name="worldLocation">The world coordinates to convert</param>
        /// <returns>The converted screen coodinates</returns>
        public static Vector2 WorldToScreen(Vector2 worldLocation)
        {
            return worldLocation - position;
        }

        /// <summary>
        /// Converts the world rectangle to a screen rectangle.
        /// </summary>
        /// <param name="worldRect">The world rectangle</param>
        /// <returns>The converted screen rectange</returns>
        public static Rectangle WorldToScreen(Rectangle worldRect)
        {
            return new Rectangle(
                worldRect.X - (int)position.X,
                worldRect.Y - (int)position.Y,
                worldRect.Width,
                worldRect.Height);
        }

        /// <summary>
        /// Converts the screen coordinates to world coordinates.
        /// </summary>
        /// <param name="screenLocation">The screen coordinates to convert</param>
        /// <returns>The converted world coodinates</returns>
        public static Vector2 ScreenToWorld(Vector2 screenLocation)
        {
            return screenLocation + position;
        }

        /// <summary>
        /// Converts the screen rectangle to a world rectangle.
        /// </summary>
        /// <param name="screenRect">The screen rectangle</param>
        /// <returns>The converted world rectange</returns>
        public static Rectangle ScreenToWorld(Rectangle screenRect)
        {
            return new Rectangle(
                screenRect.X + (int)position.X,
                screenRect.Y + (int)position.Y,
                screenRect.Width,
                screenRect.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or seths the cameras position, which will be clamped
        /// inside the world rectangle.
        /// </summary>
        public static Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = new Vector2(
                    MathHelper.Clamp(
                        value.X,
                        worldRectangle.X,
                        worldRectangle.Width - ViewPortWidth),
                   MathHelper.Clamp(
                        value.Y,
                        worldRectangle.Y,
                        worldRectangle.Height - ViewPortHeight));

            }
        }

        /// <summary>
        /// Gets or sets the world rectangle.
        /// </summary>
        public static Rectangle WorldRectangle
        {
            get
            {
                return worldRectangle;
            }
            set
            {
                worldRectangle = value;
            }
        }

        /// <summary>
        /// Gets or sets the viewport width.
        /// </summary>
        public static int ViewPortWidth
        {
            get
            {
                return (int)viewPortSize.X;
            }
            set
            {
                viewPortSize.X = value;
            }
        }

        /// <summary>
        /// Gets or sets the viewport height.
        /// </summary>
        public static int ViewPortHeight
        {
            get
            {
                return (int)viewPortSize.Y;
            }
            set
            {
                viewPortSize.Y = value;
            }
        }

        /// <summary>
        /// Gets the viewport.
        /// </summary>
        public static Rectangle ViewPort
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    ViewPortHeight,
                    ViewPortHeight);
            }
        }

        #endregion
    }
}
