using Bison.Framework.Screens;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    public abstract class BisonGame : Game
    {
        /// <summary>
        /// The graphics device manager.
        /// </summary>
        protected GraphicsDeviceManager graphics;

        /// <summary>
        /// The sprite batch to render.
        /// </summary>
        protected SpriteBatch spriteBatch;

        /// <summary>
        /// The screen manager.
        /// </summary>
        protected ScreenManager screenManager = ScreenManager.Instance;

        /// <summary>
        /// Creates a new bison game instance running on 60 fps.
        /// </summary>
        public BisonGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(preparingGraphicsDeviceSettings);

            Content.RootDirectory = "Content";

            // Frame rate is 60 fps
            TargetElapsedTime = TimeSpan.FromTicks(166667);

            // Extend battery life under lock
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            // Initialize phone services
            initializaPhoneServices();
        }

        /// <summary>
        /// Prepares the graphics device settings by setting the frame rate to 60 fps.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The device settings event args.</param>
        private void preparingGraphicsDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.One;
        }

        /// <summary>
        /// Initializes the phone service events.
        /// </summary>
        private void initializaPhoneServices()
        {
            PhoneApplicationService.Current.Activated += new EventHandler<ActivatedEventArgs>(gameActivated);
            PhoneApplicationService.Current.Deactivated += new EventHandler<DeactivatedEventArgs>(gameDeactivated);
            PhoneApplicationService.Current.Closing += new EventHandler<ClosingEventArgs>(gameClosing);
            PhoneApplicationService.Current.Launching += new EventHandler<LaunchingEventArgs>(gameLaunching);
        }

        /// <summary>
        /// Calls the game launching hook.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The launging event args.</param>
        private void gameLaunching(object sender, LaunchingEventArgs e)
        {
            this.OnGameLaunching();
        }

        /// <summary>
        /// The game launching hook.
        /// </summary>
        protected virtual void OnGameLaunching()
        {
        }

        /// <summary>
        /// Calls the game activated hook.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The activated event args.</param>
        private void gameActivated(object sender, ActivatedEventArgs e)
        {
            this.OnGameActivated();
        }

        /// <summary>
        /// The game activated hook.
        /// </summary>
        protected virtual void OnGameActivated()
        {
        }

        /// <summary>
        /// Calls the game deactivated hook.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The deactivated event args.</param>
        private void gameDeactivated(object sender, DeactivatedEventArgs e)
        {
            this.OnGameDeactivated();
        }

        /// <summary>
        /// The game deactivated hook.
        /// </summary>
        protected virtual void OnGameDeactivated()
        {
        }

        /// <summary>
        /// Calls the game closing hook.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The closing event args.</param>
        private void gameClosing(object sender, ClosingEventArgs e)
        {
            this.OnGameClosing();
        }

        /// <summary>
        /// The game closing hook.
        /// </summary>
        protected virtual void OnGameClosing()
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // set up screen manager configuration
            InitializeScreenManager();

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = (int)screenManager.ScreenDimension.X;
            graphics.PreferredBackBufferHeight = (int)screenManager.ScreenDimension.Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// Sets up the initial screen of the screen manager.
        /// </summary>
        protected abstract void InitializeScreenManager();

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenManager.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            screenManager.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            screenManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
