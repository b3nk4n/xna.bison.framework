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
        /// The initial start screen of the game.
        /// </summary>
        private IScreen initialScreen;

        /// <summary>
        /// Stacks the overlapped game screens.
        /// </summary>
        private Stack<IScreen> screenStack = new Stack<IScreen>();

        /// <summary>
        /// Stacks the screen history.
        /// </summary>
        private Stack<IScreen> screenHistoryStack = new Stack<IScreen>();

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
        /// <param name="initialScreen">The games initial screen.</param>
        public void Initialize(Game game, GraphicsDevice graphicsDevice, IScreen initialScreen)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;

            this.initialScreen = initialScreen;

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

            this.addScreen(this.initialScreen);
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

            ActiveScreen.Update(gameTime);

            this.handleBackButtonInput();

            inputManager.EndUpdate();
        }

        /// <summary>
        /// Rendres the active screens.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            batch.Begin();

            foreach (var screen in screenStack)
            {
                screen.Draw(batch);
            }

            batch.End();
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screen">The screen to change to.</param>
        public static void ChangeScreen(IScreen screen)
        {
            instance.changeScreen(screen, true);
        }

        /// <summary>
        /// Adds the screen.
        /// </summary>
        /// <param name="screen">The screen to add to the screen stack.</param>
        public static void AddScreen(IScreen screen)
        {
            instance.addScreen(screen);
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screen">The screen to change to.</param>
        /// <param name="toHistory">Indicates whether the last screen should be added to the history.</param>
        private void changeScreen(IScreen screen, bool toHistory)
        {
            screen.LoadContent(this.content);

            IScreen lastScreen = screenStack.Pop();
            lastScreen.UnloadContent();

            if (screen.AutomatedBackButtonBehavior == AutomatedBackButtonBehavior.GoBack)
            {
                screenHistoryStack.Push(lastScreen);
            }
            else
            {
                screenHistoryStack.Clear();
            }

            screenStack.Push(screen);
            screen.Activate();
        }

        /// <summary>
        /// Adds the screen.
        /// </summary>
        /// <param name="screen">The screen to add to the screen stack.</param>
        private void addScreen(IScreen screen)
        {
            screen.LoadContent(this.content);

            screenStack.Push(screen);
            screen.Activate();
        }

        /// <summary>
        /// Handles the back button input according the screens bahavior type.
        /// </summary>
        private void handleBackButtonInput()
        {
            if (inputManager.IsPressed(ActionBack))
            {
                switch (ActiveScreen.AutomatedBackButtonBehavior)
                {
                    case AutomatedBackButtonBehavior.Close:
                        screenStack.Pop();

                        if (screenStack.Count == 0)
                        {
                            game.Exit();
                        }

                        break;
                    case AutomatedBackButtonBehavior.GoBack:
                        changeScreen(screenHistoryStack.Pop(), false);
                        break;
                    case AutomatedBackButtonBehavior.None:
                        // do nothing, because OnBackButtonPressed is used
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

        /// <summary>
        /// Gets the currently active and top most screen.
        /// </summary>
        public IScreen ActiveScreen
        {
            get
            {
                return screenStack.Peek();
            }
        }

        #endregion
    }
}
