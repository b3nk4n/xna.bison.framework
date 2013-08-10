using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Represents an animated sprite.
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        #region Members

        /// <summary>
        /// The base animation strip.
        /// </summary>
        private AnimationStrip baseAnimation;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new sprite instance.
        /// </summary>
        public AnimatedSprite(Texture2D texture, int frameWidth, int frameHeight, float frameTime)
            : base(texture, frameWidth, frameHeight)
        {
            this.baseAnimation = new AnimationStrip(texture, frameWidth, frameHeight, frameTime);
            this.baseAnimation.LoopAnimation = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current frame source.
        /// </summary>
        protected override Rectangle GetCurrentFrame()
        {
            return baseAnimation.FrameRectangle;
        }

        /// <summary>
        /// Gets the current frame texture.
        /// </summary>
        protected override Texture2D GetCurrentTexture()
        {
            return baseAnimation.Texture;
        }

        #endregion
    }
}
