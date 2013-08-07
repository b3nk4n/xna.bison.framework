using Bison.Framework.Inputs;
using Bison.Framework.Inputs.Debug;
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
    public class ScreenManager : IManagedContent
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
        /// The initial start screen of the game.
        /// </summary>
        private Screen initialScreen;

        /// <summary>
        /// Stacks the overlapped game screens.
        /// </summary>
        private Stack<Screen> screenStack = new Stack<Screen>();

        /// <summary>
        /// Stacks the screen history.
        /// </summary>
        private Stack<Screen> screenHistoryStack = new Stack<Screen>();

        /// <summary>
        /// The dimension of all screens.
        /// </summary>
        private Vector2 screenDimension;

        /// <summary>
        /// The game input manager.
        /// </summary>
        private readonly InputManager inputManager = new InputManager();

#if DEBUG
        /// <summary>
        /// The touch indicator manager for tocuh screen debugging.
        /// </summary>
        private readonly TouchIndicatorManager touchIndicatorManager = TouchIndicatorManager.Instance;
#endif

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
        /// <param name="initialScreen">The games initial screen.</param>
        public void Initialize(Game game, Screen initialScreen)
        {
            this.game = game;

            this.initialScreen = initialScreen;

            this.setupInputs();
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

#if DEBUG
            touchIndicatorManager.Update(gameTime);
#endif

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

#if DEBUG
            touchIndicatorManager.Draw(batch);
#endif

            batch.End();
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screen">The screen to change to.</param>
        public static void ChangeScreen(Screen screen)
        {
            Instance.changeScreen(screen, true);
        }

        /// <summary>
        /// Adds the screen.
        /// </summary>
        /// <param name="screen">The screen to add to the screen stack.</param>
        public static void AddScreen(Screen screen)
        {
            Instance.addScreen(screen);
        }

        /// <summary>
        /// Goes back to the last screen by using the History.
        /// </summary>
        public static void GoBack()
        {
            Instance.changeScreen(
                Instance.screenHistoryStack.Pop(),
                false);
        }

        /// <summary>
        /// Closes the top most screen and exits the game, if there is no more
        /// active screen left. 
        /// </summary>
        public static void CloseScreen()
        {
            Instance.screenStack.Pop();

            if (Instance.screenStack.Count == 0)
            {
                Instance.Game.Exit();
            }
        }

        /// <summary>
        /// Sets up the back button input action.
        /// </summary>
        private void setupInputs()
        {
            inputManager.AddButtonInput(
                ActionBack,
                Buttons.Back,
                true);
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screen">The screen to change to.</param>
        /// <param name="toHistory">Indicates whether the last screen should be added to the history.</param>
        private void changeScreen(Screen screen, bool toHistory)
        {
            screen.LoadContent(this.content);

            var lastScreen = screenStack.Peek();
            clearScreenStack();

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
        private void addScreen(Screen screen)
        {
            screen.LoadContent(this.content);

            screenStack.Push(screen);
            screen.Activate();
        }

        private void clearScreenStack()
        {
            while (screenStack.Count > 0)
            {
                var screen = screenStack.Pop();
                screen.UnloadContent();
            }
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
                        CloseScreen();
                        break;
                    case AutomatedBackButtonBehavior.GoBack:
                        GoBack();
                        break;
                    case AutomatedBackButtonBehavior.Manual:
                        ActiveScreen.OnBackButtonPressed();
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
                return this.game.GraphicsDevice.Viewport.Bounds;
            }
        }

        /// <summary>
        /// Gets the currently active and top most screen.
        /// </summary>
        public Screen ActiveScreen
        {
            get
            {
                return screenStack.Peek();
            }
        }

        #endregion
    }
}
