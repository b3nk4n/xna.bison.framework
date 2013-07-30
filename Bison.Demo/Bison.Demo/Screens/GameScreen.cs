using Bison.Demo.Objects;
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
    class GameScreen : Screen
    {
        private const string SCREEN_TAP = "tap";
        Number number;
        
        Cow cow;
        Cow rotatedCow;
        Cow scaledCow;

        public GameScreen()
            : base(AutomatedBackButtonBehavior.GoBack)
        {

        }

        public override void SetupInputs()
        {
            InputManager.AddTouchTapInput(
                SCREEN_TAP,
                ScreenManager.Instance.Viewport,
                true);
        }

        public override void HandleInputs()
        {
            if (InputManager.IsPressed(SCREEN_TAP))
            {
                cow.PlayAnimation("cow4");
                ScreenManager.AddScreen(new InMenuScreen());
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            number = new Number(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                12345,
                new Rectangle(400, 240, 400, 240),
                TextControl.HorizontalAligments.Left,
                TextControl.VerticalAligments.Bottom,
                Color.White,
                Color.Red,
                2);
            number.MinDigits = 10;

            cow = new Cow(
                Content.Load<Texture2D>(@"Textures/cowSheet1"),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            cow.Location = new Vector2(0, 0);
            cow.PlayAnimation("cow1");

            rotatedCow = new Cow(
                Content.Load<Texture2D>(@"Textures/cowSheet1"),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            rotatedCow.Location = new Vector2(200, 200);
            rotatedCow.RotateToDirection(new Vector2(0, -1));
            rotatedCow.PlayAnimation("cow1");

            scaledCow = new Cow(
                Content.Load<Texture2D>(@"Textures/cowSheet1"),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            scaledCow.Location = new Vector2(300, 200);
            scaledCow.Scale = new Vector2(2.0f, 0.5f);
            scaledCow.PlayAnimation("cow1");
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            cow.Update(gameTime);
            scaledCow.Scale = new Vector2(2.0f + (float)Math.Sin(gameTime.ElapsedGameTime.TotalSeconds), 1.25f + (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds));
            scaledCow.Update(gameTime);
            rotatedCow.Rotation = (float)gameTime.TotalGameTime.TotalSeconds;
            rotatedCow.Update(gameTime);

            number.OutlineWidth = 3 + (int)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 3);
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            number.Draw(batch);

            cow.Draw(batch);
            rotatedCow.Draw(batch);
            scaledCow.Draw(batch);
        }
    }
}
