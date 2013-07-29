using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bison.Framework.Extensions;

namespace Bison.Framework.Controls
{
    /// <summary>
    /// Display control to show Int32 numbers.
    /// </summary>
    public class NumberDisplay : ContentDisplay
    {
        #region Members

        /// <summary>
        /// The display number.
        /// </summary>
        private int number;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location)
            : this(font, number, location, Color.White, Color.Transparent, 0, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location, Color color)
            : this(font, number, location, color, Color.Transparent, 0, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location, Color color, Aligment alignment, Rectangle displayArea)
            : this(font, number, location, color, Color.Transparent, 0, alignment, displayArea)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location, Color color, Color outlineColor)
            : this(font, number, location, color, outlineColor, 1, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location, Color color, Color outlineColor, int outlineWidth)
            : this(font, number, location, color, outlineColor, outlineWidth, Aligment.None, Rectangle.Empty)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location, Color color, Color outlineColor, Aligment alignment, Rectangle displayArea)
            : this(font, number, location, color, outlineColor, 1, alignment, displayArea)
        {
        }
        
        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The text location.</param>
        /// <param name="color">The text color.</param>
        /// <param name="outlineColor">The text outline color.</param>
        /// <param name="outlineWidth">The text outline width.</param>
        /// <param name="alignment">The text alignment.</param>
        /// <param name="displayArea">The text display area</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location,
            Color color, Color outlineColor, int outlineWidth, Aligment alignment, Rectangle displayArea)
            : base(font, location, color, outlineColor, outlineWidth, alignment, displayArea)
        {
            Number = number;
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
            batch.DrawInt32(
                    Font,
                    number,
                    location,
                    color);
        }

        /// <summary>
        /// Mesures the content.
        /// </summary>
        /// <returns>The content dimensions.</returns>
        protected override Vector2 measureContent()
        {
            return Font.MeasureString(this.number.ToString());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the display number.
        /// </summary>
        public int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                this.updateContent();
            }
        }

        #endregion
    }
}
