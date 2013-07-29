using Bison.Framework.Controls;
using Bison.Framework.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bison.Demo.Screens
{
    class InMenuScreen : Screen
    {
        Text pauseTitle;
          

        public InMenuScreen(ChangeScreenHandler changeScreen)
            : base(changeScreen, ScreenType.InGameMenu)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            pauseTitle = new Text(
                Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "P A U S E",
                new Vector2(400, 240),
                Color.Green,
                Color.Red,
                Text.Aligment.Both,
                Rectangle.Empty);
        }

        public override void SetupInputs()
        {
            
        }

        public override void HandleInputs()
        {
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            pauseTitle.Draw(batch);
        }
    }
}
