using Bison.Framework.Audio;
using Bison.Framework.Inputs;
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

        protected GameInput gameInput;

        #endregion

        #region Constructors

        public GameScreen()
        {
            gameInput = new GameInput();
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

        public void Update(GameTime gameTime)
        {
            gameInput.BeginUpdate();
#if DEBUG
            // touchIndicator.Update(gameTime, content);
#endif

            UpdateScreen(gameTime);

            gameInput.EndUpdate();
        }

        protected abstract void UpdateScreen(GameTime gameTime);

        public void Draw(SpriteBatch batch)
        {
            batch.Begin();

            DrawScreen(batch);

#if DEBUG
            // touchIndicator.Draw(batch, content);
#endif
            batch.End();
        }

        protected abstract void DrawScreen(SpriteBatch batch);

        #endregion

        #region Properties

        public ContentManager Content
        {
            get
            {
                return this.content;
            }
        }

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
