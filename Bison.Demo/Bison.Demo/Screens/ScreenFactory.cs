using Bison.Framework.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Demo.Screens
{
    public class ScreenFactory : IScreenFactory
    {
        /// <summary>
        /// The singleton screen factory instance.
        /// </summary>
        private static ScreenFactory instance;

        #region Game screens

        /// <summary>
        /// The game starting screen.
        /// </summary>
        public const string SplashScreen = "SplashScreen";

        /// <summary>
        /// The game screen.
        /// </summary>
        public const string GameScreen = "GameScreen";

        /// <summary>
        /// The in game menu screen.
        /// </summary>
        public const string InMenuScreen = "InMenuScreen";

        #endregion

        /// <summary>
        /// Creates a screen factory instance.
        /// </summary>
        private ScreenFactory()
        {
        }

        /// <summary>
        /// Creates a new screen.
        /// </summary>
        /// <param name="screenName">The screen name.</param>
        /// <param name="changeScreen">The change screen handler.</param>
        /// <returns>The created screen.</returns>
        public IScreen CreateScreen(string screenName, ChangeScreenHandler changeScreen)
        {
            switch (screenName)
            {
                case ScreenFactory.SplashScreen:
                    return new SplashScreen(changeScreen);

                case ScreenFactory.GameScreen:
                    return new GameScreen(changeScreen);

                case ScreenFactory.InMenuScreen:
                    return new InMenuScreen(changeScreen);

                default:
                    throw new ArgumentException("The given screen name does not exist.");
            }
        }

        /// <summary>
        /// Gets the singleton screen factory instance.
        /// </summary>
        public static ScreenFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenFactory();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the initial screen name.
        /// </summary>
        public string InitialScreenName
        {
            get 
            {
                return ScreenFactory.SplashScreen;
            }
        }
    }
}
