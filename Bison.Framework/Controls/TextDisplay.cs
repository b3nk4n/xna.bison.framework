using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    /// <summary>
    /// Display control to show some text.
    /// </summary>
    public class TextDisplay : ContentDisplay
    {
        #region Members

        /// <summary>
        /// The display text.
        /// </summary>
        private string text;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text location.</param>
        public TextDisplay(SpriteFont font, string text, Vector2 location)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location, Color color)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location, Color color, Aligment alignment, Rectangle displayArea)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location, Color color, Color outlineColor)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location, Color color, Color outlineColor, int outlineWidth)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location, Color color, Color outlineColor, Aligment alignment, Rectangle displayArea)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location,
            Color color, Color outlineColor, int outlineWidth, Aligment alignment, Rectangle displayArea)
            : base(font, location, color, outlineColor, outlineWidth, alignment, displayArea)
        {
            Text = text;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the content.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        /// <param name="location">The content location.</param>
        /// <param name="color">The content color.</param>
        protected override void drawContent(SpriteBatch batch, Vector2 location, Color color)
        {
            batch.DrawString(
                    Font,
                    text,
                    location,
                    color);
        }

        /// <summary>
        /// Mesures the content.
        /// </summary>
        /// <returns>The content dimensions.</returns>
        protected override Vector2 measureContent()
        {
            return Font.MeasureString(this.text);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.updateContent();
            }
        }

        #endregion
    }
}
