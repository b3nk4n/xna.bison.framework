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
    class SettingsScreen : Screen
    {
        #region Members

        Text title;

        #endregion

        #region Constructors

        public SettingsScreen()
            : base(AutomatedBackButtonBehavior.GoBack)
        {
        }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            title = new Text(Content.Load<SpriteFont>(@"Fonts/TestFont"),
                "Settings",
                ScreenManager.Instance.Viewport,
                TextControl.HorizontalAligments.Center,
                TextControl.VerticalAligments.Top,
                Color.White,
                Color.Red,
                2);
        }

        protected override void SetupInputs()
        {
            
        }

        protected override void HandleInputs()
        {
            
        }

        protected override void UpdateScreen(GameTime gameTime)
        {
            
        }

        protected override void DrawScreen(SpriteBatch batch)
        {
            title.Draw(batch);
        }

        #endregion

        #region Properties

        #endregion
    }
}
