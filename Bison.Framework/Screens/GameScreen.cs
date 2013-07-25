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
    public abstract class GameScreen : IScreen
    {
        #region Members

        protected ContentManager content;

        protected Color backgroundColor;

        protected AudioManager audioManager = AudioManager.Instance;

        #endregion

        #region Constructors

        public GameScreen()
        {
            
        }

        #endregion

        #region Methods

        public virtual void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            this.content.Unload();
        }

        public abstract void SetupInputs();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch batch);

        #endregion

        #region Properties

        public bool IsActive
        {
            get
            {
                return true;
            }
        }


        public bool IsVisible
        {
            get 
            {
                return true;
            }
        }

        #endregion
    }
}
