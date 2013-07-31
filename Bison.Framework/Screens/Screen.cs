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
    public enum AutomatedBackButtonBehavior
    {
        Close,
        GoBack,
        Manual
    }

    /// <summary>
    /// The game screens base class.
    /// </summary>
    public abstract class Screen : IManagedContent, IGameUpdateable, IGameDrawable
    {
        #region Members

        /// <summary>
        /// The game screens type for the back button behavior.
        /// </summary>
        private readonly AutomatedBackButtonBehavior automatedBackButtonBehavior;

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
        protected readonly AudioManager AudioManager = AudioManager.Instance;

        /// <summary>
        /// The game input manager.
        /// </summary>
        protected readonly InputManager InputManager = new InputManager();

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
        protected static readonly Random Random = new Random();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new game screen instance.
        /// </summary>
        /// <param name="backButtonBehavior">The back button behavior.</param>
        public Screen(AutomatedBackButtonBehavior backButtonBehavior)
        {
            this.automatedBackButtonBehavior = backButtonBehavior;

            SetupInputs();

            this.isActive = true;
            this.isVisible = true;
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
        protected abstract void SetupInputs();

        /// <summary>
        /// Handles the user inputs.
        /// </summary>
        protected abstract void HandleInputs();

        /// <summary>
        /// Activates the screen.
        /// </summary>
        public virtual void Activate()
        {
            this.acceptInputs = true;
        }

        /// <summary>
        /// Updates the screen and the games input manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            AudioManager.Update(gameTime);

            if (isActive)
            {
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
        }

        /// <summary>
        /// Renders the screen and the debug draw informations of the input manager.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                DrawScreen(batch);

#if DEBUG
                // touchIndicator.Draw(batch, content);
#endif
            }
        }

        /// <summary>
        /// Handle function for manual back button handling. This function is called only
        /// if the screens property AutomatedBackButtonBehavior.Manual is set.
        /// </summary>
        public virtual void OnBackButtonPressed()
        {
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
        public AutomatedBackButtonBehavior AutomatedBackButtonBehavior
        {
            get
            {
                return this.automatedBackButtonBehavior;
            }
        }

        #endregion
    }
}
