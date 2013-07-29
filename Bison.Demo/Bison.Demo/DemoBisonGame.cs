using Bison.Demo.Screens;
using Bison.Framework;

namespace Bison.Demo
{
    /// <summary>
    /// This is the main class for the bison demo application.
    /// </summary>
    public class DemoBisonGame : BisonGame
    {
        protected override void InitializeScreenManager()
        {
            screenManager.Initialize(
                this,
                this.GraphicsDevice,
                new SplashScreen());
        }
    }
}
