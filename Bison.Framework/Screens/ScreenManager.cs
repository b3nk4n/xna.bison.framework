using Bison.Framework.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Bison.Framework.Screens
{
    /// <summary>
    /// The game screen manager.
    /// </summary>
    public class ScreenManager : IScreenManager
    {
        #region Members

        /// <summary>
        /// The Back button input action.
        /// </summary>
        public const string ActionBack = "Back";

        /// <summary>
        /// The singleton instance of the screen manager.
        /// </summary>
        private static ScreenManager instance;

        /// <summary>
        /// The game instance.
        /// </summary>
        private Game game;

        /// <summary>
        /// Custom content manager.
        /// </summary>
        private ContentManager content;

        /// <summary>
        /// The games graphics device.
        /// </summary>
        private GraphicsDevice graphicsDevice;

        /// <summary>
        /// The screen factory.
        /// </summary>
        private IScreenFactory screenFactory;

        /// <summary>
        /// Stores all game screens.
        /// </summary>
        private Dictionary<string, IScreen> screens = new Dictionary<string, IScreen>();

        /// <summary>
        /// Stacks the overlapped game screens.
        /// </summary>
        private Stack<string> screenStack = new Stack<string>();

        /// <summary>
        /// The currently active screen.
        /// </summary>
        private IScreen currentScreen;

        /// <summary>
        /// The dimension of all screens.
        /// </summary>
        private Vector2 screenDimension;

        /// <summary>
        /// The game input manager.
        /// </summary>
        private readonly InputManager inputManager = InputManager.Instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a screen manager instance.
        /// </summary>
        private ScreenManager() {
            ScreenDimension = new Vector2(800, 480);
            Camera.WorldRectangle = new Rectangle(0, 0, 800, 480);
            Camera.ViewPortHeight = 480; // TODO: screen manager, graphicsDevice and camera should use the same viewport.
            Camera.ViewPortWidth = 800;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the screen manager with the used screen factory.
        /// </summary>
        /// <param name="game">The game instance.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="screenFactory">The factory class to create the game screens.</param>
        public void Initialize(Game game, GraphicsDevice graphicsDevice, IScreenFactory screenFactory)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.screenFactory = screenFactory;

            this.setupInputs();
        }

        /// <summary>
        /// Sets up the back button input action.
        /// </summary>
        private void setupInputs()
        {
            inputManager.AddGamepadInput(
                ActionBack,
                Buttons.Back,
                true);
        }

        /// <summary>
        /// Loads the content and the initial screen.
        /// </summary>
        /// <param name="content">The games content manager.</param>
        public void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            this.ChangeScreen(screenFactory.InitialScreenName);
        }

        /// <summary>
        /// Unloads the game content.
        /// </summary>
        public void UnloadContent()
        {
            this.content.Unload();
        }

        /// <summary>
        /// Updates the active screens.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public void Update(GameTime gameTime)
        {
            inputManager.BeginUpdate();

            this.handleBackButtonInput();

            currentScreen.Update(gameTime);

            inputManager.EndUpdate();
        }

        /// <summary>
        /// Rendres the active screens.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            batch.Begin();

            currentScreen.Draw(batch);

            batch.End();
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screenName">The screen name.</param>
        protected void ChangeScreen(string screenName)
        {
            // check for back button actions at first.

            if (!screens.ContainsKey(screenName))
            {
                screens.Add(
                    screenName,
                    screenFactory.CreateScreen(screenName,
                        new ChangeScreenHandler(ChangeScreen)));
                screens[screenName].LoadContent(this.content);
            }

            currentScreen = screens[screenName];
            currentScreen.Activate();
        }
        /// <summary>
        /// Handles the back button input according the screens bahavior type.
        /// </summary>
        private void handleBackButtonInput()
        {
            if (inputManager.IsPressed(ActionBack))
            {
                switch (currentScreen.ScreenType)
                {
                    case ScreenType.Start:
                        game.Exit();
                        break;
                    case ScreenType.InGame:
                        // TODO: close overlay screen.
                        break;
                    case ScreenType.InGameMenu:
                        // TODO: show overlay screen.
                        break;
                    case ScreenType.Other:
                        // TODO: go back to the last screen.
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the screen manager.
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the game instance.
        /// </summary>
        public Game Game
        {
            get
            {
                return this.game;
            }
        }

        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.graphicsDevice;
            }
        }

        /// <summary>
        /// Gets the dimension of any screen.
        /// </summary>
        public Vector2 ScreenDimension
        {
            get
            {
                return this.screenDimension;
            }
            set
            {
                this.screenDimension = value;
            }
        }

        /// <summary>
        /// Gets the dimension of any screen.
        /// </summary>
        public Rectangle Viewport
        {
            get
            {
                return this.graphicsDevice.Viewport.Bounds;
            }
        }

        #endregion
    }
}
