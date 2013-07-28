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
    class DemoScreen : GameScreen
    {
        private const string SCREEN_TAP = "tap";
        bool tapped = false;
        SpriteFont font;

        public DemoScreen(ChangeScreenHandler changeScreen)
            : base(changeScreen)
        {

        }

        public override void SetupInputs()
        {
            gameInput.AddTouchTapInput(
                SCREEN_TAP,
                ScreenManager.Instance.Viewport,
                true);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            font = Content.Load<SpriteFont>(@"Fonts/TestFont");
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            if (gameInput.IsPressed(SCREEN_TAP))
            {
                tapped = true;
            }
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            string text = "tap me!";
            if (tapped)
            {
                text = "thx!";
            }

            batch.DrawString(
                font,
                text,
                Vector2.Zero,
                Color.White);
        }
    }
}
