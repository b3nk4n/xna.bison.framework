using Microsoft.Xna.Framework.Content;

namespace Bison.Framework
{
    /// <summary>
    /// Interface for managed content.
    /// </summary>
    public interface IManagedContent
    {
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content manager.</param>
        void LoadContent(ContentManager content);

        /// <summary>
        /// Unloads the content.
        /// </summary>
        void UnloadContent();
    }
}
