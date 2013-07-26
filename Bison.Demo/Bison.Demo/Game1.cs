using Bison.Demo.Screens;
using Bison.Framework;

namespace Bison.Demo
{
    /// <summary>
    /// This is the main class for the bison demo application.
    /// </summary>
    public class Game1 : BisonGame
    {
        protected override void initializeScreenManager()
        {
            screenManager.Initialize(new SplashScreen());
        }
    }
}
