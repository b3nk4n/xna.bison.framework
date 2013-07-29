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
        Text outlinedText;
        
        Cow cow;
        Cow rotatedCow;
        Cow scaledCow;

        public GameScreen(ChangeScreenHandler changeScreen)
            : base(changeScreen, ScreenType.InGame)
        {

        }

        public override void SetupInputs()
        {
            inputManager.AddTouchTapInput(
                SCREEN_TAP,
                ScreenManager.Instance.Viewport,
                true);
        }

        public override void HandleInputs()
        {
            if (inputManager.IsPressed(SCREEN_TAP))
            {
                cow.PlayAnimation("cow4");
                outlinedText.DisplayText = "Test1234";
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            outlinedText = new Text(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "SampleText",
                new Vector2(25, 25),
                Color.Black,
                Color.White,
                5);

            cow = new Cow(
                Content.Load<Texture2D>(@"Textures/cowSheet1"),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            cow.Location = new Vector2(100, 200);
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

            outlinedText.OutlineWidth = 3 + (int)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 3);
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            outlinedText.Draw(batch);

            cow.Draw(batch);
            rotatedCow.Draw(batch);
            scaledCow.Draw(batch);
        }
    }
}
