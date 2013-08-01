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
        Image bisonLogo;
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

        protected override void SetupInputs()
        {
        }

        protected override void HandleInputs()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            bisonLogo = new Image(
                Content.Load<Texture2D>(@"Textures/BisonSplashscreen"),
                new Vector2(200, 165));
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
                ScreenManager.ChangeScreen(new MenuScreen());
            }
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            bisonLogo.Draw(batch);

            batch.DrawLine(new Vector2(0, 0), new Vector2(800, 480), Color.Red, 3.0f);
            batch.DrawCircle(
                new Vector2(100, 100),
                90.0f,
                Color.Red,
                2);
            batch.DrawNgon(
                new Vector2(600, 100),
                50,
                8,
                Color.Blue,
                1);
        }
    }
}
