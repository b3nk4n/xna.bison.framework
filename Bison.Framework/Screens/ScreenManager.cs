using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        /// The singleton instance of the screen manager.
        /// </summary>
        private static ScreenManager instance;

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
        private Stack<IScreen> screenStack = new Stack<IScreen>();

        /// <summary>
        /// The currently active screen.
        /// </summary>
        private IScreen currentScreen;

        /// <summary>
        /// The dimension of all screens.
        /// </summary>
        private Vector2 screenDimension;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a screen manager instance.
        /// </summary>
        private ScreenManager() {
            ScreenDimension = new Vector2(800, 480);
            Camera.WorldRectangle = new Rectangle(0, 0, 800, 480);
            Camera.ViewPortHeight = 480;
            Camera.ViewPortWidth = 800;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the screen manager with the used screen factory.
        /// </summary>
        /// <param name="screenFactory">The factory class to create the game screens.</param>
        public void Initialize(IScreenFactory screenFactory)
        {
            this.screenFactory = screenFactory;
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
            currentScreen.Update(gameTime);
        }

        /// <summary>
        /// Rendres the active screens.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        public void Draw(SpriteBatch batch)
        {
            currentScreen.Draw(batch);
        }

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screenName">The screen name.</param>
        private void ChangeScreen(string screenName)
        {
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
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.graphicsDevice;
            }
            set
            {
                this.graphicsDevice = value;
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
