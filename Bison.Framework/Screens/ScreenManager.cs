using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Screens
{
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
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the screen manager with the initial screen.
        /// </summary>
        /// <param name="initialScreen"></param>
        public void Initialize(IScreen initialScreen)
        {
            this.currentScreen = initialScreen;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            this.currentScreen.LoadContent(content);
        }

        public void UnloadContent()
        {
            this.content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            currentScreen.Draw(batch);
        }

        /// <summary>
        /// Adds a new screen.
        /// </summary>
        /// <param name="screen">The new screen.</param>
        public void AddScreen(GameScreen screen)
        {
            screenStack.Push(screen);
            currentScreen.UnloadContent();
            currentScreen = screen;
            currentScreen.LoadContent(this.content);
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

        /// <summary>
        /// Gets the new screen.
        /// </summary>
        public IScreen NewScreen
        {
            get
            {
                return this.screenStack.Peek();
            }
        }

        #endregion
    }
}
