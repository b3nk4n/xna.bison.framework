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

        /// <summary>
        /// 
        /// </summary>
        private int minDigits;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The number location.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location)
            : this(font, number, location, Color.White, Color.Transparent, 0)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The number location.</param>
        /// <param name="color">The number color.</param>
        /// <param name="outlineColor">The number outline color.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location,
            Color color, Color outlineColor)
            : this(font, number, location, color, outlineColor, 1)
        {
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The number location.</param>
        /// <param name="color">The number color.</param>
        /// <param name="outlineColor">The number outline color.</param>
        /// <param name="outlineWidth">The number outline width.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location,
            Color color, Color outlineColor, int outlineWidth)
            : base(font, location, color, outlineColor, outlineWidth)
        {
            Number = number;
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="location">The number display location.</param>
        /// <param name="horizontalAlignment">The horizontal number alignment.</param>
        /// <param name="verticalAlignment">The vertical number alignment.</param>
        /// <param name="color">The number color.</param>
        /// <param name="outlineColor">The number outline color.</param>
        /// <param name="outlineWidth">The number outline width.</param>
        public NumberDisplay(SpriteFont font, int number, Vector2 location, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment,
            Color color, Color outlineColor, int outlineWidth)
            : base(font, location, horizontalAlignment, verticalAlignment, color, outlineColor, outlineWidth)
        {
            Number = number;
        }

        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="displayArea">The number display area</param>
        /// <param name="horizontalAlignment">The horizontal number alignment.</param>
        /// <param name="verticalAlignment">The vertical number alignment.</param>
        /// <param name="color">The number color.</param>
        /// <param name="outlineColor">The number outline color.</param>
        public NumberDisplay(SpriteFont font, int number, Rectangle displayArea, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment,
            Color color, Color outlineColor)
            : this(font, number, displayArea, horizontalAlignment, verticalAlignment, color, outlineColor, 1)
        {
        }
        
        /// <summary>
        /// Creates a new number instance.
        /// </summary>
        /// <param name="font">The sprite font.</param>
        /// <param name="number">The display number.</param>
        /// <param name="displayArea">The number display area</param>
        /// <param name="horizontalAlignment">The horizontal number alignment.</param>
        /// <param name="verticalAlignment">The vertical number alignment.</param>
        /// <param name="color">The number color.</param>
        /// <param name="outlineColor">The number outline color.</param>
        /// <param name="outlineWidth">The number outline width.</param>
        public NumberDisplay(SpriteFont font, int number, Rectangle displayArea, HorizontalAligments horizontalAlignment, VerticalAligments verticalAlignment,
            Color color, Color outlineColor, int outlineWidth)
            : base(font, displayArea, horizontalAlignment, verticalAlignment, color, outlineColor, outlineWidth)
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
        protected override void DrawContent(SpriteBatch batch, Vector2 location, Color color)
        {
            batch.DrawInt32(
                    Font,
                    number,
                    location,
                    color,
                    this.minDigits);
        }

        /// <summary>
        /// Mesures the content.
        /// </summary>
        /// <returns>The content dimensions.</returns>
        protected override Vector2 MeasureContent()
        {
            return Font.MeasureString(number.ToString("D" + this.minDigits));
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
                if (this.number != value)
                {
                    this.number = value;
                    this.UpdateContent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimal number of digits. The rest is padded with leading zeros.
        /// </summary>
        public int MinDigits
        {
            get
            {
                return this.minDigits;
            }
            set
            {
                if (this.minDigits != value)
                {
                    this.minDigits = value;
                    this.UpdateContent();
                }
            }
        }

        #endregion
    }
}
