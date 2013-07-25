using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    public class FadeEffect : ImageEffect
    {
        /*public float FadeSpeed;
        public bool Increase;
        private Image image;

        public FadeEffect(Image image)
        {
            this.image = image;
            FadeSpeed = 1;
            Increase = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (image.IsActive)
            {
                if (Increase)
                {
                    image.Opacity += FadeSpeed * elapsed;
                }
                else
                {
                    image.Opacity -= FadeSpeed * elapsed;
                }

                if (image.Opacity < 0.0f)
                {
                    Increase = true;
                    image.Opacity = 0.0f;
                }
                else if (image.Opacity > 1.0f)
                {
                    Increase = true;
                    image.Opacity = 1.0f;
                }
            }
            else
            {
                image.Opacity = 1.0f;
            }
        }*/
    }
}
