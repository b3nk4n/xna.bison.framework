using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Animation
{
    public abstract class Animation
    {
        protected ContentManager content;
        protected Texture2D texture;
        protected string text;
        protected SpriteFont font;
        protected Color color;
        protected float opacity;
        protected Rectangle sourceRect;
        protected float rotation;
        protected float scale;
        protected float axis;
        protected Vector2 origin;
        protected Vector2 position;
        protected bool isActive;

        public virtual void LoadContent(ContentManager content, Texture2D texture, string text, Vector2 position)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            this.texture = texture;
            this.text = text;
            this.position = position;
            if (font == null)
            {
                font = content.Load<SpriteFont>(@"Fonts/AnimationFont");
                color = new Color(114, 77, 255);
            }
            this.opacity = 1.0f;
            if (texture != null)
            {
                this.sourceRect = new Rectangle(
                    0, 0, texture.Width, texture.Height);
            }
            this.rotation = 0.0f;
            this.axis = 0.0f;
            this.scale = 1.0f;
            this.isActive = false;
        }

        public virtual void UnloadContent()
        {
            this.content.Unload();
            this.text = string.Empty;
            this.position = Vector2.Zero;
            this.sourceRect = Rectangle.Empty;
            this.texture = null;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (texture != null)
            {
                this.origin = new Vector2(sourceRect.Width / 2,
                    sourceRect.Height / 2);
                batch.Draw(texture, position + origin, sourceRect,
                    Color.White, rotation, origin, scale,
                    SpriteEffects.None, 0.0f);
            }

            if (text != string.Empty)
            {
                Vector2 messuredText = font.MeasureString(text);
                origin = new Vector2(messuredText.X / 2,
                    messuredText.X / 2);
                batch.DrawString(font, text, position + origin,
                    color, rotation, origin, scale,
                    SpriteEffects.None, 0.0f);
            }
        }

        public bool IsActive
        {
            get;
            set;
        }

        public virtual float Opacity
        {
            get;
            set;
        }
    }
}
