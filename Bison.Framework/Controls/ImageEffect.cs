using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Controls
{
    public class ImageEffect
    {
        protected OldImage image;
        public bool IsActive = false;

        public ImageEffect()
        {

        }

        public virtual void LoadContent(ref OldImage image)
        {
            this.image = image;
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
