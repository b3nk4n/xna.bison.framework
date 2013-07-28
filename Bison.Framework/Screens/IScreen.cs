using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Screens
{
    public interface IScreen : IManagedContent, IGameUpdateable, IGameDrawable
    {
        /// <summary>
        /// Sets up the inputs of the screen.
        /// </summary>
        void SetupInputs();

        /// <summary>
        /// Activates the screen.
        /// </summary>
        void Activate();
    }
}
