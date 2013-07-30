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
            : this(font, text, location, Color.White, Color.Transparent, 0)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location,
            Color color, Color outlineColor)
            : this(font, text, location, color, outlineColor, 1)
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
        public TextDisplay(SpriteFont font, string text, Vector2 location,
            Color color, Color outlineColor, int outlineWidth)
            : base(font, location, color, outlineColor, outlineWidth)
        {
            Text = text;
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="location">The text display location.</param>
        /// <param name="horizontalAlignment">The horizontal text alignment.</param>
        /// <param name="verticalAlignment">The vertical text alignment.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        public TextDisplay(SpriteFont font, string text, Vector2 location, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment,
            Color color, Color outlineColor, int outlineWidth)
            : base(font, location, horizontalAlignment, verticalAlignment, color, outlineColor, outlineWidth)
        {
            Text = text;
        }

        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="displayArea">The text display area</param>
        /// <param name="horizontalAlignment">The horizontal text alignment.</param>
        /// <param name="verticalAlignment">The vertical text alignment.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        public TextDisplay(SpriteFont font, string text, Rectangle displayArea, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment,
            Color color, Color outlineColor)
            : this(font, text, displayArea, horizontalAlignment, verticalAlignment, color, outlineColor, 1)
        {
        }
        
        /// <summary>
        /// Creates a new text instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="text">The display text.</param>
        /// <param name="displayArea">The text display area</param>
        /// <param name="horizontalAlignment">The horizontal text alignment.</param>
        /// <param name="verticalAlignment">The vertical text alignment.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        public TextDisplay(SpriteFont font, string text, Rectangle displayArea, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment,
            Color color, Color outlineColor, int outlineWidth)
            : base(font, displayArea, horizontalAlignment, verticalAlignment, color, outlineColor, outlineWidth)
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
        protected override void DrawContent(SpriteBatch batch, Vector2 location, Color color)
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
        protected override Vector2 MeasureContent()
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
                if (this.text != value)
                {
                    this.text = value;
                    this.UpdateContent();
                }
            }
        }

        #endregion
    }
}
