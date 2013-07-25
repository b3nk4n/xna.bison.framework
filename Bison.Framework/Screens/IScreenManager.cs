using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bison.Framework.Screens
{
    interface IScreenManager : IManagedContent
    {
        /// <summary>
        /// Initializes the screen manager with the initial screen.
        /// </summary>
        /// <param name="initialScreen">The initial screen.</param>
        void Initialize(GameScreen initialScreen);

        /// <summary>
        /// Updates the game component.
        /// </summary>
        /// <param name="gameTime">The time since the last update.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draws the game component.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        void Draw(SpriteBatch batch);
    }
}
