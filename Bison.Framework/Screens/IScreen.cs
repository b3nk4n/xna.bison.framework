using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Screens
{
    /// <summary>
    /// The automated Back button behavior controled by the framework.
    /// </summary>
    public enum AutomatedBackButtonBehavior
    {
        Close,
        GoBack,
        None
    }

    /// <summary>
    /// The interface for screens.
    /// </summary>
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
        /// Custom Back button handler.
        /// </summary>
        void OnBackButtonPressed();

        /// <summary>
        /// Gets the screen type. Used for Back button behavior.
        /// </summary>
        AutomatedBackButtonBehavior AutomatedBackButtonBehavior { get; }
    }
}
