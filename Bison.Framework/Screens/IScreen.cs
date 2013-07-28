using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Screens
{
    /// <summary>
    /// The change screen delegate.
    /// </summary>
    /// <param name="screenName">The screen name.</param>
    public delegate void ChangeScreenHandler(string screenName);

    public interface IScreen : IManagedContent, IGameUpdateable, IGameDrawable
    {
        /// <summary>
        /// Sets up the inputs of the screen.
        /// </summary>
        void SetupInputs();

        /// <summary>
        /// Handles the user inputs.
        /// </summary>
        void HandleInputs();

        /// <summary>
        /// Gets whether the screen accepts user input or not.
        /// </summary>
        bool AcceptInputs { get; set; }

        /// <summary>
        /// Activates the screen.
        /// </summary>
        void Activate();

        /// <summary>
        /// Changes the screen.
        /// </summary>
        /// <param name="screenName">The name of the requested screen.</param>
        void ChangeScreen(string screenName);
    }
}
