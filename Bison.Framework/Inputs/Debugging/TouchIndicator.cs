using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Inputs.Debugging
{
    /// <summary>
    /// Represents a visible touch position or gesture.
    /// </summary>
    public class TouchIndicator
    {
        #region Members 

        /// <summary>
        /// The current opacity.
        /// </summary>
        private float opacity = 1.0f;

        /// <summary>
        /// The touch ID of the indiator.
        /// </summary>
        private readonly int touchId;

        /// <summary>
        /// The current touch positions.
        /// </summary>
        private List<Vector2> touchPositions = new List<Vector2>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new touch indicatior instance.
        /// </summary>
        /// <param name="touchId">The touch ID.</param>
        public TouchIndicator(int touchId)
        {
            this.touchId = touchId;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the users touch input visualizations.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        /// <param name="touches">All user touch inputs.</param>
        public void Update(GameTime gameTime, TouchCollection touches)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2? currentPosition = touchPosition(touches);

            if (currentPosition == null)
            {
                if (touchPositions.Count > 0)
                {
                    opacity -= elapsed;

                    if (opacity <= 0.0f)
                    {
                        touchPositions.Clear();
                        opacity = 1.0f;
                    }
                }
            }
            else
            {
                if (opacity < 1.0f)
                {
                    touchPositions.Clear();
                    opacity = 1.0f;
                }

                touchPositions.Add(currentPosition.Value);
            }
        }

        /// <summary>
        /// Draws the touches using a line.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            drawTouchLine(
                batch,
                touchPositions,
                Color.Red * opacity);
        }

        /// <summary>
        /// Renders a touch line with highlighted start and end point.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="touchPositions">The touch positions.</param>
        /// <param name="color">The color of the line.</param>
        private void drawTouchLine(SpriteBatch batch, IList<Vector2> touchPositions, Color color)
        {
            if (touchPositions.Count > 0)
            {
                Vector2 previousTouchPosition = touchPositions[0];

                batch.DrawCircle(previousTouchPosition, 15, color, 3);

                foreach (Vector2 touchPosition in touchPositions)
                {
                    if (touchPosition != previousTouchPosition)
                    {
                        batch.DrawLine(previousTouchPosition, touchPosition, color, 3);
                        previousTouchPosition = touchPosition;
                    }
                    
                }

                batch.DrawCircle(previousTouchPosition, 15, color, 3);
                
            }
        }

        /// <summary>
        /// Gets the touch Position of this touch.
        /// </summary>
        /// <param name="touches">The touch collection.</param>
        /// <returns>The vector position of this touch or NULL.</returns>
        private Vector2? touchPosition(TouchCollection touches)
        {
            TouchLocation touchLocation;

            if (touches.FindById(touchId, out touchLocation))
            {
                return touchLocation.Position;
            }

            return null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the touch identity number.
        /// </summary>
        public int TouchId
        {
            get
            {
                return this.touchId;
            }
        }

        #endregion
    }
}
