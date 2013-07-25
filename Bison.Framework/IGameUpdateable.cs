using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bison.Framework
{
    /// <summary>
    /// The interface for updateable objects.
    /// </summary>
    public interface IGameUpdateable
    {
        /// <summary>
        /// Updates the game component.
        /// </summary>
        /// <param name="gameTime">The time since the last update.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Indicates whether the object is active.
        /// </summary>
        bool IsActive { get; }
    }
}
