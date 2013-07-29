using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bison.Framework.Screens
{
    public interface IScreenManager : IManagedContent
    {
        /// <summary>
        /// Initializes the screen manager with the used screen factory.
        /// </summary>
        /// <param name="game">The game instance.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="initialScreen">The initial screen.</param>
        void Initialize(Game game, GraphicsDevice graphicsDevice, IScreen initialScreen);

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
