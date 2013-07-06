using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bison.Framework
{
    /// <summary>
    /// The game component interface.
    /// </summary>
    public interface IGameComponent
    {
        /// <summary>
        /// Initializes the game component.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content manager.</param>
        void LoadContent(ContentManager content);

        /// <summary>
        /// Unloads the content.
        /// </summary>
        void UnloadContent();

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
