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
        /// <param name="screenFactory">The factory class to create the game screens.</param>
        void Initialize(Game game, GraphicsDevice graphicsDevice, IScreenFactory screenFactory);

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
