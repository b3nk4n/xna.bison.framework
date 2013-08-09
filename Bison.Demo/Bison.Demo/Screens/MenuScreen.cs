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
    class MenuScreen : Screen
    {
        #region Members

        Text title;

        Button startButton;
        Button settingsButton;
        Button exitButton;

        #endregion

        #region Constructors

        public MenuScreen()
            : base(AutomatedBackButtonBehavior.Close)
        {

        }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            title = new Text(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Bison Demo Game",
                ScreenManager.Instance.Viewport,
                TextControl.HorizontalAligments.Center,
                TextControl.VerticalAligments.Top,
                Color.White,
                Color.Red,
                2);

            startButton = new Button(
                Content.Load<Texture2D>(@"Textures/Button"),
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Start",
                new Vector2(200, 150));
            startButton.Text.OutlineColor = Color.Black;
            startButton.Text.OutlineWidth = 2;

            settingsButton = new Button(
                Content.Load<Texture2D>(@"Textures/Button"),
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Settings",
                new Vector2(200, 250));

            exitButton = new Button(
                Texture2DHelper.Create(400, 80, Color.DarkBlue),
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Exit",
                new Vector2(200, 350));
            exitButton.Text.Color = Color.Black;
            exitButton.Text.OutlineColor = Color.White;
            exitButton.Text.OutlineWidth = 2;
        }

        protected override void SetupInputs()
        {
            
        }

        protected override void HandleInputs()
        {
            if (startButton.IsPressed())
            {
                ScreenManager.ChangeScreen(new GameScreen());
            }
            else if (settingsButton.IsPressed())
            {
                ScreenManager.ChangeScreen(new SettingsScreen());
            }
            else if (exitButton.IsPressed())
            {
                ScreenManager.Instance.Game.Exit();
            }
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            title.Draw(batch);

            startButton.Draw(batch);
            settingsButton.Draw(batch);
            exitButton.Draw(batch);
        }

        #endregion

        #region Properties

        #endregion
    }
}
