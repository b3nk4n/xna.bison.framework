using Bison.Demo.Objects;
using Bison.Framework;
using Bison.Framework.Controls;
using Bison.Framework.Graphics;
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
            : base(AutomatedBackButtonBehavior.Manual)
        {

        }

        protected override void SetupInputs()
        {
            InputManager.AddTouchTapInput(
                SCREEN_TAP,
                ScreenManager.Instance.Viewport,
                true);
        }

        protected override void HandleInputs()
        {
            if (InputManager.IsPressed(SCREEN_TAP))
            {
                cow.PlayAnimation("cow4");
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
                Texture2DHelper.Cut(Content.Load<Texture2D>(@"Textures/cowSheet1"), Vector2.Zero, 64, 64),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            cow.Location = new Vector2(0, 0);
            cow.SetupCollisionCircles(
                new[] {
                    new Circle(new Vector2(10, 0), 15.0f),
                    new Circle(new Vector2(-5, -5), 10.0f)
                });
            cow.PlayAnimation("cow1");

            rotatedCow = new Cow(
                Content.Load<Texture2D>(@"Textures/cowSheet1"),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            rotatedCow.Location = new Vector2(200, 200);
            rotatedCow.RotateToDirection(new Vector2(0, -1));
            rotatedCow.SetupCollisionCircles(
                new[] {
                    new Circle(Vector2.Zero, 28.0f)
                });
            rotatedCow.PlayAnimation("cow1");

            scaledCow = new Cow(
                Content.Load<Texture2D>(@"Textures/cowSheet1"),
                Content.Load<Texture2D>(@"Textures/cowSheet2"),
                Content.Load<Texture2D>(@"Textures/cowSheet3"),
                Content.Load<Texture2D>(@"Textures/cowSheet4"));
            scaledCow.Location = new Vector2(300, 200);
            scaledCow.Scale = 2.0f;
            scaledCow.SetupCollisionCircles(
                new[] {
                    new Circle(Vector2.Zero, 15.0f),
                    new Circle(new Vector2(-5, -5), 10.0f)
                });
            scaledCow.PlayAnimation("cow1");
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            cow.Update(gameTime);
            scaledCow.Scale = 2.0f + (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds);
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

        public override void OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            ScreenManager.AddScreen(new InGameMenuScreen());
        }
    }
}
