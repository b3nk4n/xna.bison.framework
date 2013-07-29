using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    public abstract class ContentDisplay : IGameDrawable
    {
        #region Members

        /// <summary>
        /// The supported content alignments.
        /// </summary>
        public enum Aligment
        {
            None,
            Horizontal,
            Vertical,
            Both
        }

        /// <summary>
        /// The sqrt(2) constant to improve performance.
        /// </summary>
        protected const float SQRT2 = 0.707167812f;

        /// <summary>
        /// The content location.
        /// </summary>
        private Vector2 location;

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
        /// The current content aligment.
        /// </summary>
        private Aligment alignment;

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
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public ContentDisplay(SpriteFont font, Vector2 location, Color color, Color outlineColor, 
            int outlineWidth, Aligment alignment, Rectangle displayArea)
        {
            this.font = font;
            this.location = location;
            this.color = color;
            this.outlineColor = outlineColor;
            this.outlineWidth = outlineWidth;
            this.alignment = alignment;
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
                    drawContent(batch, location + new Vector2(0, OutlineWidth), outlineColor);
                    drawContent(batch, location + new Vector2(0, -OutlineWidth), outlineColor);
                    drawContent(batch, location + new Vector2(OutlineWidth, 0), outlineColor);
                    drawContent(batch, location + new Vector2(-OutlineWidth, 0), outlineColor);

                    // diagonal
                    float diagonalOutlineWidth = OutlineWidth * SQRT2;
                    drawContent(batch, location + new Vector2(diagonalOutlineWidth, diagonalOutlineWidth), outlineColor);
                    drawContent(batch, location + new Vector2(-diagonalOutlineWidth, diagonalOutlineWidth), outlineColor);
                    drawContent(batch, location + new Vector2(diagonalOutlineWidth, -diagonalOutlineWidth), outlineColor);
                    drawContent(batch, location + new Vector2(-diagonalOutlineWidth, -diagonalOutlineWidth), outlineColor);
                }

                drawContent(batch, location, color);
            }
        }

        /// <summary>
        /// Renders the content.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="location">The content location.</param>
        /// <param name="color">The content color.</param>
        protected abstract void drawContent(SpriteBatch batch, Vector2 location, Color color);

        /// <summary>
        /// Updates the content and its position according the alignment.
        /// </summary>
        protected void updateContent()
        {
            this.contentSize = measureContent();

            this.updateAlignment();
        }

        /// <summary>
        /// Mesures the content.
        /// </summary>
        /// <returns>The content dimensions.</returns>
        protected abstract Vector2 measureContent();

        /// <summary>
        /// Updates the content position according the alignment.
        /// </summary>
        private void updateAlignment()
        {
            switch (alignment)
            {
                case Aligment.Horizontal:
                    this.location.X = (int)((displayArea.Width / 2) - (contentSize.X / 2)) + displayArea.X;
                    break;
                case Aligment.Vertical:
                    this.location.Y = (int)((displayArea.Height / 2) - (contentSize.Y / 2)) + displayArea.Y;
                    break;
                case Aligment.Both:
                    this.location.X = (int)((displayArea.Width / 2) - (contentSize.X / 2)) + displayArea.X;
                    this.location.Y = (int)((displayArea.Height / 2) - (contentSize.Y / 2)) + displayArea.Y;
                    break;
                case Aligment.None:
                    // do nothing
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
        /// Gets or sets the content location.
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
                this.updateAlignment();
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
        /// Gets or sets the content alignment.
        /// </summary>
        public Aligment Alignment
        {
            get
            {
                return this.alignment;
            }
            set
            {
                this.alignment = value;
                this.updateAlignment();
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
