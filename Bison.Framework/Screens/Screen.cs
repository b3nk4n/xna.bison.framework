using Bison.Framework.Audio;
using Bison.Framework.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bison.Framework.Screens
{
    /// <summary>
    /// The game screens base class.
    /// </summary>
    public abstract class Screen : IScreen
    {
        #region Members

        /// <summary>
        /// The game screens type for the back button behavior.
        /// </summary>
        private readonly ScreenType screenType;

        /// <summary>
        /// The content manager.
        /// </summary>
        private ContentManager content;

        /// <summary>
        /// The screen background coloe.
        /// </summary>
        protected Color backgroundColor;

        /// <summary>
        /// The game audio manager.
        /// </summary>
        protected readonly AudioManager audioManager = AudioManager.Instance;

        /// <summary>
        /// The game input manager.
        /// </summary>
        protected readonly InputManager inputManager = InputManager.Instance;

        /// <summary>
        /// The change screen hander method.
        /// </summary>
        private readonly ChangeScreenHandler changeScreen;

        /// <summary>
        /// Indicates wether the screen handles inputs.
        /// </summary>
        private bool acceptInputs;

        /// <summary>
        /// Indicates whether the screen is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Indicates whether the screen is visible.
        /// </summary>
        private bool isVisible;

        /// <summary>
        /// The screens random number generator.
        /// </summary>
        protected Random random = new Random();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new game screen instance.
        /// </summary>
        /// <param name="changeScreen">The change screen handler.</param>
        /// <param name="screenType">The sreen type.</param>
        public Screen(ChangeScreenHandler changeScreen, ScreenType screenType)
        {
            this.changeScreen = changeScreen;
            this.screenType = screenType;

            SetupInputs();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the screens content service provider and loads the content.
        /// </summary>
        /// <param name="content">The parents content manager.</param>
        public virtual void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
        }

        /// <summary>
        /// Unloads the screens content.
        /// </summary>
        public virtual void UnloadContent()
        {
            this.content.Unload();
        }

        /// <summary>
        /// Sets up the screen inputs.
        /// </summary>
        public abstract void SetupInputs();

        /// <summary>
        /// Handles the user inputs.
        /// </summary>
        public abstract void HandleInputs();

        /// <summary>
        /// Activates the screen.
        /// </summary>
        public virtual void Activate()
        {
            this.acceptInputs = true;
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screenName">The name of the requested screen.</param>
        protected void ChangeScreen(string screenName)
        {
            changeScreen(screenName);
        }

        /// <summary>
        /// Updates the screen and the games input manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            audioManager.Update(gameTime);

            // handle input if accepted
            if (AcceptInputs)
            {
                HandleInputs();
            }
#if DEBUG
            // touchIndicator.Update(gameTime, content);
#endif
            UpdateScreen(gameTime);
        }

        /// <summary>
        /// Renders the screen and the debug draw informations of the input manager.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            DrawScreen(batch);

#if DEBUG
            // touchIndicator.Draw(batch, content);
#endif
        }

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void UpdateScreen(GameTime gameTime);

        /// <summary>
        /// Renders the screen.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        protected abstract void DrawScreen(SpriteBatch batch);

        #endregion

        #region Properties

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager Content
        {
            get
            {
                return this.content;
            }
        }

        /// <summary>
        /// Gets or sets whether the game screen is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the game screen is visible or not.
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
        /// Gets or sets whether the screen accepts user inputs or not.
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
        /// Gets the screen type.
        /// </summary>
        public ScreenType ScreenType
        {
            get
            {
                return this.screenType;
            }
        }

        #endregion
    }
}
