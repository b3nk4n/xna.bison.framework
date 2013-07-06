using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Screens
{
    public class ScreenManager
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
        /// Stores all game screens.
        /// </summary>
        private Dictionary<string, GameScreen> screens = new Dictionary<string, GameScreen>();

        /// <summary>
        /// Stacks the overlapped game screens.
        /// </summary>
        private Stack<GameScreen> screenStack = new Stack<GameScreen>();

        private GameScreen currentScreen;

        public GraphicsDevice GraphicsDevice;

        public SpriteBatch SpriteBatch;

        /// <summary>
        /// The dimension of all screens.
        /// </summary>
        private Vector2 dimension;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a screen manager instance.
        /// </summary>
        private ScreenManager() {
            Dimension = new Vector2(800, 480);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the screen manager with the initial screen.
        /// </summary>
        /// <param name="initialScreen"></param>
        public void Initialize(GameScreen initialScreen)
        {
            this.currentScreen = initialScreen;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            this.currentScreen.LoadContent(content);
        }

        public void UnloadContent(ContentManager content)
        {
            this.content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();

            currentScreen.Draw(batch);

            batch.End();
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

        /// <summary>
        /// Gets the dimension of any screen.
        /// </summary>
        public Vector2 Dimension
        {
            get
            {
                return this.dimension;
            }
            set
            {
                this.dimension = value;
            }
        }

        /// <summary>
        /// Gets the new screen.
        /// </summary>
        public GameScreen NewScreen
        {
            get
            {
                return this.screenStack.Peek();
            }
        }

        #endregion
    }
}
