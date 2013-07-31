using Bison.Framework.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    /// <summary>
    /// Representing a button.
    /// </summary>
    public class Button : IGameDrawable
    {
        #region Members

        /// <summary>
        /// The background image.
        /// </summary>
        private Image backgroundImage;

        /// <summary>
        /// The button text.
        /// </summary>
        private Text text;

        /// <summary>
        /// The input manager.
        /// </summary>
        private InputManager inputManager = new InputManager();

        /// <summary>
        /// The button pressed action.
        /// </summary>
        private const string ACTION_BUTTON_PRESSED = "ButtonPressed";

        /// <summary>
        /// Indicates whether the button accepts user input or not.
        /// </summary>
        private bool acceptInputs;

        /// <summary>
        /// Indicates whether the button is visible or not.
        /// </summary>
        private bool isVisible;

        #endregion

        #region Constructors

        public Button(Texture2D backgroundImage, SpriteFont font, string text, Vector2 location)
        {
            this.backgroundImage = new Image(
                backgroundImage,
                location);

            this.text = new Text(
                font,
                text,
                this.backgroundImage.BoundingBox,
                TextControl.HorizontalAligments.Center,
                TextControl.VerticalAligments.Center,
                Color.White);

            setupInputs();

            this.acceptInputs = true;
            this.isVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks whether the button was pressed.
        /// </summary>
        /// <returns>TRUE, if the button was pressed.</returns>
        public bool IsPressed()
        {
            if (acceptInputs)
            {
                return inputManager.IsPressed(ACTION_BUTTON_PRESSED);
            }

            return false;
        }
        
        /// <summary>
        /// Renders the button.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                backgroundImage.Draw(batch);
                text.Draw(batch);
            }
        }

        /// <summary>
        /// Sets up the button touch input.
        /// </summary>
        private void setupInputs()
        {
            inputManager.AddTouchTapInput(
                ACTION_BUTTON_PRESSED,
                this.backgroundImage.BoundingBox,
                true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the button text.
        /// </summary>
        public Text Text
        {
            get
            {
                return this.text;
            }
        }

        /// <summary>
        /// Gets the background image.
        /// </summary>
        public Image BackgroundImage
        {
            get
            {
                return this.backgroundImage;
            }
            set
            {
                this.backgroundImage = value;
            }
        }

        /// <summary>
        /// Gets or sets the bounding box in world coordinates.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return this.backgroundImage.BoundingBox;
            }
        }

        /// <summary>
        /// Gets or sets whether the button accepts user inputs or not.
        /// </summary>
        public bool AcceptInputs
        {
            get
            {
                return this.acceptInputs;
            }
            set
            {
                this.acceptInputs = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the button is visible or not.
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

        #endregion
    }
}
