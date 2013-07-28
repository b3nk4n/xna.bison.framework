using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    /// <summary>
    /// Text control to display some text.
    /// </summary>
    public class Text : IGameDrawable
    {
        #region Members

        /// <summary>
        /// The supported text alignments.
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
        private const float SQRT2 = 0.707167812f;

        /// <summary>
        /// The text location.
        /// </summary>
        private Vector2 location;

        /// <summary>
        /// The text color.
        /// </summary>
        private Color color;

        /// <summary>
        /// The text font.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// The display text.
        /// </summary>
        private string displayText;

        /// <summary>
        /// The calculated text size.
        /// </summary>
        private Vector2 textSize;

        /// <summary>
        /// The current text aligment.
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
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        public Text(SpriteFont font, string text, Vector2 location)
            : this(font, text, location, Color.White, Color.Transparent, 0, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        public Text(SpriteFont font, string text, Vector2 location, Color color)
            : this(font, text, location, color, Color.Transparent, 0, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public Text(SpriteFont font, string text, Vector2 location, Color color, Aligment alignment, Rectangle displayArea)
            : this(font, text, location, color, Color.Transparent, 0, alignment, displayArea)
        {
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        public Text(SpriteFont font, string text, Vector2 location, Color color, Color outlineColor)
            : this(font, text, location, color, outlineColor, 1, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        public Text(SpriteFont font, string text, Vector2 location, Color color, Color outlineColor, int outlineWidth)
            : this(font, text, location, color, outlineColor, outlineWidth, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public Text(SpriteFont font, string text, Vector2 location, Color color, Color outlineColor, Aligment alignment, Rectangle displayArea)
            : this(font, text, location, color, outlineColor, 1, alignment, displayArea)
        {
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public Text(SpriteFont font, string text, Vector2 location,
            Color color, Color outlineColor, int outlineWidth, Aligment alignment, Rectangle displayArea)
        {
            this.font = font;
            this.location = location;
            this.color = color;
            this.outlineColor = outlineColor;
            this.outlineWidth = outlineWidth;
            this.alignment = alignment;
            this.displayArea = displayArea;

            DisplayText = text;

            this.isVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the text control
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                // render outline
                if (IsOutlined)
                {
                    drawTextOutline(batch);
                }

                // render display text
                batch.DrawString(
                    font,
                    displayText,
                    location,
                    color);
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
                                     (int)textSize.X,
                                     (int)textSize.Y);
            }
        }

        /// <summary>
        /// Renders the outline text.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        private void drawTextOutline(SpriteBatch batch)
        {
            // horizontal / verical
            batch.DrawString(
                font,
                displayText,
                location + new Vector2(0, outlineWidth),
                outlineColor);

            batch.DrawString(
                font,
                displayText,
                location + new Vector2(0, -outlineWidth),
                outlineColor);

            batch.DrawString(
                font,
                displayText,
                location + new Vector2(outlineWidth, 0),
                outlineColor);

            batch.DrawString(
                font,
                displayText,
                location + new Vector2(-outlineWidth, 0),
                outlineColor);

            // diagonal
            batch.DrawString(
                font,
                displayText,
                location + new Vector2(outlineWidth * SQRT2, outlineWidth * SQRT2),
                outlineColor);

            batch.DrawString(
                font,
                displayText,
                location + new Vector2(-outlineWidth * SQRT2, outlineWidth * SQRT2),
                outlineColor);

            batch.DrawString(
                font,
                displayText,
                location + new Vector2(outlineWidth * SQRT2, -outlineWidth * SQRT2),
                outlineColor);

            batch.DrawString(
                font,
                displayText,
                location + new Vector2(-outlineWidth * SQRT2, -outlineWidth * SQRT2),
                outlineColor);
        }

        /// <summary>
        /// Updates the text and its position according the alignment.
        /// </summary>
        /// <param name="text">The n</param>
        private void updateText()
        {
            this.textSize = font.MeasureString(this.displayText);

            this.updateAlignment();
        }

        /// <summary>
        /// Updates the texts position according the alignment.
        /// </summary>
        private void updateAlignment()
        {
            switch (alignment)
            {
                case Aligment.Horizontal:
                    this.location.X = (int)((displayArea.Width / 2) - (textSize.X / 2)) + displayArea.X;
                    break;
                case Aligment.Vertical:
                    this.location.Y = (int)((displayArea.Height / 2) - (textSize.Y / 2)) + displayArea.Y;
                    break;
                case Aligment.Both:
                    this.location.X = (int)((displayArea.Width / 2) - (textSize.X / 2)) + displayArea.X;
                    this.location.Y = (int)((displayArea.Height / 2) - (textSize.Y / 2)) + displayArea.Y;
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
        /// Gets or sets the display text.
        /// </summary>
        public string DisplayText
        {
            get
            {
                return this.displayText;
            }
            set
            {
                this.displayText = value;
                this.updateText();
            }
        }

        /// <summary>
        /// Gets or sets the text location.
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
        /// Gets or sets the text color.
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
        /// Gets or sets the text outline color.
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
        /// Gets or sets the text alignment.
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
        /// Gets whether the text is outlined or not.
        /// </summary>
        public bool IsOutlined
        {
            get
            {
                return outlineWidth == 0 || outlineColor != Color.Transparent;
            }
        }

        #endregion
    }
}
