using Bison.Framework.Controls;
using Bison.Framework.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bison.Demo.Screens
{
    class InGameMenuScreen : Screen
    {
        Text pauseTitle;

        Button exitGameButton;
        Button continueButton;
          

        public InGameMenuScreen()
            : base(AutomatedBackButtonBehavior.Close)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            pauseTitle = new Text(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "P A U S E",
                new Vector2(400, 240),
                TextControl.HorizontalAligments.Center,
                TextControl.VerticalAligments.Center,
                Color.Green,
                Color.Red);

            exitGameButton = new Button(
                Content.Load<Texture2D>(@"Textures/Button"),
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Exit Game",
                new Vector2(0, 350));

            continueButton = new Button(
                Content.Load<Texture2D>(@"Textures/Button"),
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Continue",
                new Vector2(400, 350));
        }

        protected override void SetupInputs()
        {
            
        }

        protected override void HandleInputs()
        {
            if (continueButton.IsPressed())
            {
                ScreenManager.CloseScreen();
            }
            else if(exitGameButton.IsPressed())
            {
                ScreenManager.ChangeScreen(new MenuScreen());
            }
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            pauseTitle.Draw(batch);

            exitGameButton.Draw(batch);
            continueButton.Draw(batch);
        }
    }
}
