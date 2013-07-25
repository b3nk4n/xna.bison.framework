using Microsoft.Xna.Framework.Graphics;

namespace Bison.Framework
{
    /// <summary>
    /// The interface for drawable objects.
    /// </summary>
    interface IGameDrawable
    {
        /// <summary>
        /// Draws the game component.
        /// </summary>
        /// <param name="batch">The sprite batch.</param>
        void Draw(SpriteBatch batch);

        /// <summary>
        /// Indicates whether the object is visible or not.
        /// </summary>
        bool IsVisible { get; }
    }
}
