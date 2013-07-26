using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// The game ticker class to simplify timed actions.
    /// </summary>
    public class GameTicker : IGameUpdateable
    {
        #region Members

        /// <summary>
        /// The default reset time of the timer.
        /// </summary>
        private float defaultTime;

        /// <summary>
        /// The current time of the timer.
        /// </summary>
        private float timer;

        /// <summary>
        /// Flag which shows that the timer is active or not.
        /// </summary>
        private bool isActive;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new game ticker instance.
        /// </summary>
        /// <param name="defaultTime">The default reset time of the timer.</param>
        public GameTicker(float defaultTime)
        {
            DefaultTime = defaultTime;
            Reset();

            this.isActive = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the game ticker if active.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

                this.timer -= elapsed;
            }
        }

        /// <summary>
        /// Resets the game timer to its default reset time.
        /// </summary>
        public void Reset()
        {
            Reset(defaultTime);
        }

        /// <summary>
        /// Resets the game time to the given reset time.
        /// </summary>
        /// <param name="time">The reset time.</param>
        public void Reset(float time)
        {
            this.timer = time;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default reset time.
        /// </summary>
        public float DefaultTime
        {
            get
            {
                return this.defaultTime;
            }
            set
            {
                // verify that the reset time is larger than zero
                this.defaultTime = Math.Max(value, 0.0001f);

                if (this.timer > defaultTime)
                {
                    this.timer = defaultTime;
                }
            }
        }

        /// <summary>
        /// Gets the progress of the timer in percent.
        /// </summary>
        public float Progress
        {
            get
            {
                return MathHelper.Clamp(timer / defaultTime, 0.0f, 1.0f);
            }
        }

        /// <summary>
        /// Indicates whether the game ticker is elapsed or not.
        /// </summary>
        public bool Elapsed
        {
            get
            {
                return this.timer < 0.0f;
            }
        }

        /// <summary>
        /// Gets or sets whether the game ticker is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        #endregion
    }
}
