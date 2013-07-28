using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Screens
{
    /// <summary>
    /// The screen factory interface, which creates the game screens
    /// used by the screen manager.
    /// </summary>
    public interface IScreenFactory
    {
        /// <summary>
        /// Creates the game screen with the given name.
        /// </summary>
        /// <param name="screenName">The screen to create.</param>
        /// <param name="changeScreen">The change screen handler.</param>
        /// <returns>The requested screen instance.</returns>
        IScreen CreateScreen(string screenName, GameScreen.ChangeScreenHandler changeScreen);

        /// <summary>
        /// Gets the initial screen name.
        /// </summary>
        string InitialScreenName { get; }
    }
}
