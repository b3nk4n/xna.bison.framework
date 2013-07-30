using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    public abstract class TextControl : IGameDrawable
    {
        #region Members

        /// <summary>
        /// The supported horizontal content alignments.
        /// </summary>
        public enum HorizontalAligments
        {
            Left,
            Center,
            Right,
        }

        /// <summary>
        /// The supported vertical content alignments.
        /// </summary>
        public enum VerticalAligments
        {
            Top,
            Center,
            Bottom,
        }

        /// <summary>
        /// The sqrt(2) constant to improve performance.
        /// </summary>
        protected const float SQRT2 = 0.707167812f;

        /// <summary>
        /// The content color.
        /// </summary>
        private Color color;

        /// <summary>
        /// The text font.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// The calculated content size.
        /// </summary>
        private Vector2 contentSize;

        /// <summary>
        /// The current horizontal content aligment.
        /// </summary>
        private HorizontalAligments horizontalAlignment;

        /// <summary>
        /// The current vertical content aligment.
        /// </summary>
        private VerticalAligments verticalAlignment;

        /// <summary>
        /// The render location of the content.
        /// </summary>
        private Vector2 location;

        /// <summary>
        /// The display area rectangle.
        /// </summary>
        private Rectangle displayArea;

        /// <summary>
        /// The outline color.
        /// </summary>
        private Color outlineColor = Color.Transparent;

        /// <summary>
        /// The outline width.
        /// </summary>
        private int outlineWidth;

        /// <summary>
        /// Indicates whether the control is visible or not.
        /// </summary>
        private bool isVisible;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new content display instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="location">The content display location.</param>
        /// <param name="color">The content color.</param>
        /// <param name="outlineColor">The content outline color.</param>
        /// <param name="outlineWidth">The content outline width.</param>
        public TextControl(SpriteFont font, Vector2 location, Color color, Color outlineColor,
            int outlineWidth)
            : this(font, new Rectangle((int)location.X, (int)location.Y, 0, 0), HorizontalAligments.Left, VerticalAligments.Top, color, outlineColor, outlineWidth)
        {
        }

        /// <summary>
        /// Creates a new content display instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="location">The content display location.</param>
        /// <param name="horizontalAlignment">The horizontal content alignment.</param>
        /// <param name="verticalAlignment">The vertical content alignment.</param>
        /// <param name="color">The content color.</param>
        /// <param name="outlineColor">The content outline color.</param>
        /// <param name="outlineWidth">The content outline width.</param>
        public TextControl(SpriteFont font, Vector2 location, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment, Color color, Color outlineColor,
            int outlineWidth)
            : this(font, new Rectangle((int)location.X, (int)location.Y, 0, 0), horizontalAlignment, verticalAlignment, color, outlineColor, outlineWidth)
        {
        }

        /// <summary>
        /// Creates a new content display instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="color">The content color.</param>
        /// <param name="outlineColor">The content outline color.</param>
        /// <param name="outlineWidth">The content outline width.</param>
        public TextControl(SpriteFont font, Rectangle displayArea, Color color, Color outlineColor,
            int outlineWidth)
            : this(font, displayArea, HorizontalAligments.Left, VerticalAligments.Top, color, outlineColor, outlineWidth)
        {
        }

        /// <summary>
        /// Creates a new content display instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="displayArea">The content display area.</param>
        /// <param name="horizontalAlignment">The horizontal content alignment.</param>
        /// <param name="color">The content color.</param>
        /// <param name="outlineColor">The content outline color.</param>
        /// <param name="outlineWidth">The content outline width.</param>
        public TextControl(SpriteFont font, Rectangle displayArea, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment, Color color, Color outlineColor, 
            int outlineWidth)
        {
            this.font = font;
            this.color = color;
            this.outlineColor = outlineColor;
            this.outlineWidth = outlineWidth;
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            this.displayArea = displayArea;

            this.isVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the content control.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                // render outline
                if (IsOutlined)
                {
                    // horizontal / verical
                    DrawContent(batch, location + new Vector2(0, OutlineWidth), outlineColor);
                    DrawContent(batch, location + new Vector2(0, -OutlineWidth), outlineColor);
                    DrawContent(batch, location + new Vector2(OutlineWidth, 0), outlineColor);
                    DrawContent(batch, location + new Vector2(-OutlineWidth, 0), outlineColor);

                    // diagonal
                    float diagonalOutlineWidth = OutlineWidth * SQRT2;
                    DrawContent(batch, location + new Vector2(diagonalOutlineWidth, diagonalOutlineWidth), outlineColor);
                    DrawContent(batch, location + new Vector2(-diagonalOutlineWidth, diagonalOutlineWidth), outlineColor);
                    DrawContent(batch, location + new Vector2(diagonalOutlineWidth, -diagonalOutlineWidth), outlineColor);
                    DrawContent(batch, location + new Vector2(-diagonalOutlineWidth, -diagonalOutlineWidth), outlineColor);
                }

                DrawContent(batch, location, color);
            }
        }

        /// <summary>
        /// Renders the content.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="location">The content location.</param>
        /// <param name="color">The content color.</param>
        protected abstract void DrawContent(SpriteBatch batch, Vector2 location, Color color);

        /// <summary>
        /// Updates the content and its position according the alignment.
        /// </summary>
        protected void UpdateContent()
        {
            this.contentSize = MeasureContent();

            this.updateAlignment();
        }

        /// <summary>
        /// Mesures the content.
        /// </summary>
        /// <returns>The content dimensions.</returns>
        protected abstract Vector2 MeasureContent();

        /// <summary>
        /// Updates the content position according the alignment.
        /// </summary>
        private void updateAlignment()
        {
            updateHorizontalAlignment();
            updateVerticalAlignment();
        }

        /// <summary>
        /// Updates the content position according the horizontal alignment.
        /// </summary>
        private void updateHorizontalAlignment()
        {
            switch (horizontalAlignment)
            {
                case HorizontalAligments.Left:
                    this.location.X = displayArea.X;
                    break;
                case HorizontalAligments.Center:
                    this.location.X = displayArea.X + (int)((displayArea.Width / 2) - (contentSize.X / 2));
                    break;
                case HorizontalAligments.Right:
                    this.location.X = displayArea.X + (int)(displayArea.Width - contentSize.X);
                    break;
            }
        }

        /// <summary>
        /// Updates the content position according the vertical alignment.
        /// </summary>
        private void updateVerticalAlignment()
        {
            switch (verticalAlignment)
            {
                case VerticalAligments.Top:
                    this.location.Y = displayArea.Y;
                    break;
                case VerticalAligments.Center:
                    this.location.Y = displayArea.Y + (int)((displayArea.Height / 2) - (contentSize.Y / 2));
                    break;
                case VerticalAligments.Bottom:
                    this.location.Y = displayArea.Y + (int)(displayArea.Height - contentSize.Y);
                    break;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the control is visible or not.
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
        /// Gets the used font.
        /// </summary>
        public SpriteFont Font
        {
            get
            {
                return this.font;
            }
        }

        /// <summary>
        /// Gets or sets the content color.
        /// </summary>
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets or sets the outline color.
        /// </summary>
        public Color OutlineColor
        {
            get
            {
                return this.outlineColor;
            }
            set
            {
                this.outlineColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the outline width.
        /// </summary>
        public int OutlineWidth
        {
            get
            {
                return this.outlineWidth;
            }
            set
            {
                this.outlineWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the content horizontal alignment.
        /// </summary>
        public HorizontalAligments HorizontalAligment
        {
            get
            {
                return this.horizontalAlignment;
            }
            set
            {
                this.horizontalAlignment = value;
                this.updateHorizontalAlignment();
            }
        }

        /// <summary>
        /// Gets or sets the content vertical alignment.
        /// </summary>
        public VerticalAligments VerticalAlignment
        {
            get
            {
                return this.verticalAlignment;
            }
            set
            {
                this.verticalAlignment = value;
                this.updateVerticalAlignment();
            }
        }

        /// <summary>
        /// Gets or sets the display area.
        /// </summary>
        public Rectangle DisplayArea
        {
            get
            {
                return this.displayArea;
            }
            set
            {
                this.displayArea = value;
                this.updateAlignment();
            }
        }

        /// <summary>
        /// Gets whether the content is outlined or not.
        /// </summary>
        public bool IsOutlined
        {
            get
            {
                return outlineWidth == 0 || outlineColor != Color.Transparent;
            }
        }

        /// <summary>
        /// Gets or sets the bounding box in world coordinates.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(location.X),
                                     (int)(location.Y),
                                     (int)contentSize.X,
                                     (int)contentSize.Y);
            }
        }

        #endregion
    }
}
