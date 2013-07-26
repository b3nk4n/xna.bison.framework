using Bison.Framework;
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
        //public Image image = new Image();
        SpriteFont font;
        GameTicker splashTimer = new GameTicker(3.0f);

        public override void SetupInputs()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            //image.Position = new Vector2(100, 100);
            //image.Opacity = 0.5f;
            //image.Scale = new Vector2(0.5f, 1);
            //image.AddEffect("FaceEffect", new FadeEffect());

            font = Content.Load<SpriteFont>(@"Fonts/TestFont");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            //image.Update(gameTime);
            splashTimer.Update(gameTime);

            if (splashTimer.Elapsed)
            {
                ScreenManager.Instance.AddScreen(new DemoScreen());
            }
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            //image.Draw(batch);
            batch.DrawString(
                font,
                "SplashScreen",
                Vector2.Zero,
                Color.White);
        }
    }
}
