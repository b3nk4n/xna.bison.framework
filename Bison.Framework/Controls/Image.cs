using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    /// <summary>
    /// Class representing an image.
    /// </summary>
    public class Image : IGameDrawable
    {
        #region Members

        /// <summary>
        /// The image texture.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The image location.
        /// </summary>
        private Vector2 location;

        /// <summary>
        /// Indicates whether the image is visible or not.
        /// </summary>
        private bool isVisible;

        /// <summary>
        /// The image scale.
        /// </summary>
        private Vector2 scale;

        /// <summary>
        /// The image rotation.
        /// </summary>
        private float rotation;

        /// <summary>
        /// The image source.
        /// </summary>
        private Rectangle source;

        /// <summary>
        /// The image tint color.
        /// </summary>
        private Color tintColor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an image instance.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="location">The image location.</param>
        public Image(Texture2D texture, Vector2 location)
            : this(texture, location, texture.Bounds)
        {
        }

        /// <summary>
        /// Creates an image instance.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="location">THe image location.</param>
        /// <param name="source">The image source.</param>
        public Image(Texture2D texture, Vector2 location, Rectangle source)
        {
            this.texture = texture;
            this.location = location;
            this.source = source;

            this.scale = Vector2.One;
            this.tintColor = Color.White;
            this.isVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the image.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                batch.Draw(
                    texture,
                    Center,
                    source,
                    tintColor,
                    rotation,
                    new Vector2(source.Width / 2, source.Height / 2),
                    scale,
                    SpriteEffects.None,
                    1.0f);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the location in world coordinates.
        /// </summary>
        public Vector2 Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        /// <summary>
        /// Gets the center in world coordinates.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                return new Vector2(
                    (int)location.X + (int)(source.Width / 2),
                    (int)location.Y + (int)(source.Height / 2));
            }
        }

        /// <summary>
        /// Gets or sets whether the image is visible or not.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = true;
            }
        }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        public float Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
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

        #endregion
    }
}
