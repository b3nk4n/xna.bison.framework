using Bison.Framework.Controls;
using Bison.Framework.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Demo.Screens
{
    public class SplashScreen : GameScreen
    {
        public Image image = new Image();

        public override void SetupInputs()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            image.LoadContent(content);
            image.Position = new Vector2(100, 100);
            image.Opacity = 0.5f;
            image.Scale = new Vector2(0.5f, 1);
            image.AddEffect("FaceEffect", new FadeEffect());
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            image.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            image.Draw(batch);
        }
    }
}
