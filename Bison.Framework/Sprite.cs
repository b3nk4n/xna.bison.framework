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
        #region Members

        /// <summary>
        /// The sprites texture.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The sprites frame width.
        /// </summary>
        private int frameWidth;

        /// <summary>
        /// The sprites frame height.
        /// </summary>
        private int frameHeight;

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

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new sprite instance.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="frameWidth">The frame width.</param>
        /// <param name="frameHeight">The frame height.</param>
        public Sprite(Texture2D texture, int frameWidth, int frameHeight)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            this.isVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the game object if it is visible.
        /// </summary>
        /// <param name="batch">The sprite batch</param>
        public virtual void Draw(SpriteBatch batch)
        {
            if (IsVisible)
            {
                batch.Draw(
                    GetCurrentTexture(),
                    Camera.WorldToScreen(Center),
                    GetCurrentFrame(),
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
        /// Gets the current frame source.
        /// </summary>
        protected virtual Rectangle GetCurrentFrame()
        {
            return new Rectangle(0, 0, frameWidth, frameHeight);
        }

        /// <summary>
        /// Gets the current frame texture.
        /// </summary>
        protected virtual Texture2D GetCurrentTexture()
        {
            return this.texture;
        }

        #endregion

        #region Properties

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
                return this.GetCurrentFrame().Width;
            }
        }

        /// <summary>
        /// Gets the sources unscaled height.
        /// </summary>
        public override int SourceHeight
        {
            get
            {
                return this.GetCurrentFrame().Height;
            }
        }

        #endregion
    }
}
