using Bison.Framework.Audio;
using Bison.Framework.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    public abstract class GameScreen : IScreen
    {
        #region Members

        /// <summary>
        /// The content manager.
        /// </summary>
        protected ContentManager content;

        /// <summary>
        /// The screen background coloe.
        /// </summary>
        protected Color backgroundColor;

        /// <summary>
        /// The game audio manager.
        /// </summary>
        protected AudioManager audioManager = AudioManager.Instance;

        /// <summary>
        /// The game input manager.
        /// </summary>
        protected InputManager gameInput = InputManager.Instance;

        /// <summary>
        /// The change screen delegate.
        /// </summary>
        /// <param name="screenName">The screen name.</param>
        public delegate void ChangeScreenHandler(string screenName);

        /// <summary>
        /// The change screen hander method.
        /// </summary>
        private ChangeScreenHandler changeScreen;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new game screen instance.
        /// </summary>
        /// <param name="changeScreen">The change screen handler.</param>
        public GameScreen(ChangeScreenHandler changeScreen)
        {
            this.changeScreen = changeScreen;

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
        /// Activates the screen.
        /// </summary>
        public virtual void Activate()
        {

        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screenName">The name of the requested screen.</param>
        public void ChangeScreen(string screenName)
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

            gameInput.BeginUpdate();
#if DEBUG
            // touchIndicator.Update(gameTime, content);
#endif
            UpdateScreen(gameTime);

            gameInput.EndUpdate();
        }

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void UpdateScreen(GameTime gameTime);

        /// <summary>
        /// Renders the screen and the debug draw informations of the input manager.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            batch.Begin();

            DrawScreen(batch);

#if DEBUG
            // touchIndicator.Draw(batch, content);
#endif
            batch.End();
        }

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
        /// Gets whether the game screen is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets whether the game screen is visible or not.
        /// </summary>
        public bool IsVisible
        {
            get 
            {
                return true;
            }
        }

        #endregion
    }
}
