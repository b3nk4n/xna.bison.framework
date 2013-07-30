using Bison.Framework.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    public class Image
    {
        /*public float Opacity;
        public Vector2 Position;
        public Vector2 Scale;
        public Rectangle SourceRect;

        public Texture2D Texture;
        private Vector2 origion;

        public RenderTarget2D renderTarget;

        public bool IsActive;

        /*private Dictionary<string, ImageEffect> effectList = new Dictionary<string,ImageEffect>();

        public void AddEffect(string effectName, ImageEffect effect)
        {
            effect.IsActive = true;
            var obj = this;
            effect.LoadContent(ref obj);
            effectList.Add(effectName, effect);
            ActivateEffect(effectName);
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public Image(Texture2D texture)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Opacity = 1.0f;
            SourceRect = Rectangle.Empty;
            IsActive = true;
            this.Texture = texture;
            

            Vector2 dimensions = Vector2.Zero;

            if (texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            }
            else
            {
                dimensions.Y = font.MeasureString(Text).Y;
            }
            dimensions.X += font.MeasureString(Text).X;

            if (SourceRect == Rectangle.Empty)
            {
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            renderTarget = new RenderTarget2D(
                ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X,
                (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
            {
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            }
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void Update(GameTime gameTime)
        {
            /*foreach (var effect in effectList.Values)
            {
                if (effect.IsActive)
                {
                    effect.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            origion = new Vector2(SourceRect.Width / 2,
                SourceRect.Height / 2);
            batch.Draw(Texture, Position, SourceRect, Color.White * Opacity,
                0.0f, origion, Scale, SpriteEffects.None, 0.0f);
        }*/
    }
}
