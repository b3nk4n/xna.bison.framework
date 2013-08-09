using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Represents a simple sprite.
    /// </summary>
    public class Sprite : GameObject, IGameDrawable
    {
        /// <summary>
        /// The base animation strip.
        /// </summary>
        private AnimationStrip baseAnimation;

        /// <summary>
        /// Indicates whether the game object is visible or not.
        /// </summary>
        private bool isVisible;

        /// <summary>
        /// The flip state of teh game object.
        /// </summary>
        private SpriteEffects flipped = SpriteEffects.None;

        /// <summary>
        /// The tint color.
        /// </summary>
        private Color tintColor = Color.White;

        /// <summary>
        /// Creates a new sprite instance.
        /// </summary>
        public Sprite(Texture2D texture, int frameWidth, int frameHeight, float frameTime)
        {
            this.baseAnimation = new AnimationStrip(texture, frameWidth, frameHeight, frameTime);
            this.baseAnimation.LoopAnimation = true;

            this.isVisible = true;
        }

        /// <summary>
        /// Renders the game object if it is visible.
        /// </summary>
        /// <param name="batch">The sprite batch</param>
        public virtual void Draw(SpriteBatch batch)
        {
            if (IsVisible)
            {
                var currentAnimation = GetCurrentAnimation();

                batch.Draw(
                    currentAnimation.Texture,
                    Camera.WorldToScreen(Center),
                    currentAnimation.FrameRectangle,
                    tintColor,
                    Rotation,
                    new Vector2(SourceWidth / 2, SourceHeight / 2),
                    Scale,
                    flipped,
                    layerDepth);

#if DEBUG
                if (CanCircleCollide)
                {
                    foreach (var circle in CollisionCircles)
                    {
                        batch.DrawCircle(
                            circle.Center,
                            circle.Radius);
                    }
                }

                if (BoundingBox != SourceBoundingBox)
                {
                    batch.DrawRectangle(BoundingBox);
                }
                batch.DrawRectangle(SourceBoundingBox);
#endif
            }
        }

        /// <summary>
        /// Gets the current animation.
        /// </summary>
        protected virtual AnimationStrip GetCurrentAnimation()
        {
            return this.baseAnimation;
        }

        /// <summary>
        /// Gets or sets whether the game object is visible or not.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        /// <summary>
        /// Gets or sets the tint color.
        /// </summary>
        public Color TintColor
        {
            get
            {
                return this.tintColor;
            }
            set
            {
                this.tintColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the sprites flip effects.
        /// </summary>
        public SpriteEffects Flipped
        {
            get
            {
                return this.flipped;
            }
            set
            {
                this.flipped = value;
            }
        }

        /// <summary>
        /// Gets the sources unscaled width.
        /// </summary>
        public override int SourceWidth
        {
            get
            {
                return this.GetCurrentAnimation().FrameWidth;
            }
        }

        /// <summary>
        /// Gets the sources unscaled height.
        /// </summary>
        public override int SourceHeight
        {
            get
            {
                return this.GetCurrentAnimation().FrameHeight;
            }
        }
    }
}
