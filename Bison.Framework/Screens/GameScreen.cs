using Bison.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bison.Framework.Screens
{
    /// <summary>
    /// The game screens base class.
    /// </summary>
    public abstract class GameScreen : IGameComponent
    {
        protected ContentManager content;

        protected Color backgroundColor;

        protected AudioManager audioManager = AudioManager.Instance;

        [XmlIgnore]
        public Type Type;

        public GameScreen()
        {
            Type = this.GetType();
        }

        public void Initialize()
        {
            this.SetupInputs();
        }

        /// <summary>
        /// Sets up the inputs of the game screen.
        /// </summary>
        public abstract void SetupInputs();

        public virtual void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            this.content.Unload();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch batch);
    }
}
