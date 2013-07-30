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
    public class SplashScreen : Screen
    {
        TextDisplay title;
        NumberDisplay number;
        GameTicker splashTimer = new GameTicker(3.0f);

        public SplashScreen()
            : base(AutomatedBackButtonBehavior.Close)
        {
        }

        public override void Activate()
        {
            base.Activate();

            splashTimer.Reset();
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

            title = new TextDisplay(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "SplashScreen",
                new Vector2(400, 240),
                ContentDisplay.HorizontalAligments.Center,
                ContentDisplay.VerticalAligments.Center,
                Color.Red,
                Color.RoyalBlue,
                2);

            number = new NumberDisplay(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                12345,
                new Rectangle(400, 240, 400, 240),
                ContentDisplay.HorizontalAligments.Left,
                ContentDisplay.VerticalAligments.Bottom,
                Color.White,
                Color.Red,
                2);
            number.MinDigits = 10;
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
                ScreenManager.ChangeScreen(new GameScreen());
            }
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            title.Draw(batch);
            number.Draw(batch);
        }
    }
}
