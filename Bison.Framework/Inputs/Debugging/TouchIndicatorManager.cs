using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bison.Framework.Inputs.Debugging
{
    /// <summary>
    /// Manages a collection of touch indicators for touchscreen debugging.
    /// </summary>
    public class TouchIndicatorManager
    {
        #region Members

        private static TouchIndicatorManager instance;

        /// <summary>
        /// The list of touch indicators.
        /// </summary>
        private List<TouchIndicator> touchIndicators = new List<TouchIndicator>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new touch indicator manager instance.
        /// </summary>
        private TouchIndicatorManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the touch indicators.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public void Update(GameTime gameTime)
        {
            TouchCollection touches = TouchPanel.GetState();

            foreach (TouchLocation touch in touches)
            {
                bool touchIdAleadyStored = false;

                foreach (TouchIndicator indicator in touchIndicators)
                {
                    if (touch.Id == indicator.TouchId)
                    {
                        touchIdAleadyStored = true;
                        break;
                    }
                }

                if (!touchIdAleadyStored)
                {
                    Debug.WriteLine("New touch with ID {0} was detected at {1}", touch.Id, touch.Position);
                    touchIndicators.Add(new TouchIndicator(touch.Id));
                }
            }

            // Update all touch indicators
            foreach (TouchIndicator indicator in touchIndicators)
            {
                indicator.Update(
                    gameTime,
                    touches);
            }
        }

        /// <summary>
        /// Renders the touch indicators.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            foreach (TouchIndicator indircator in touchIndicators)
            {
                indircator.Draw(batch);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton touch indicator manager instance.
        /// </summary>
        public static TouchIndicatorManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TouchIndicatorManager();
                }

                return instance;
            }
        }

        #endregion
    }
}
