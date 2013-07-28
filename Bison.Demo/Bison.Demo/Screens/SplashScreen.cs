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
        SpriteFont font;
        GameTicker splashTimer = new GameTicker(3.0f);

        public SplashScreen(ChangeScreenHandler changeScreen)
            : base(changeScreen)
        {

        }

        public override void SetupInputs()
        {
            
        }

        public override void HandleInputs()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            font = Content.Load<SpriteFont>(@"Fonts/TestFont");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            splashTimer.Update(gameTime);

            if (splashTimer.Elapsed)
            {
                ChangeScreen("DemoScreen");
            }
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            batch.DrawString(
                font,
                "SplashScreen",
                Vector2.Zero,
                Color.White);
        }
    }
}
