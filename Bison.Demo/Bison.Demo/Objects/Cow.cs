using Bison.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Demo.Objects
{
    class Cow : StoryboardSprite
    {
        public Cow(Texture2D tex1, Texture2D tex2, Texture2D tex3, Texture2D tex4)
            : base(tex1, 64, 64, 0.25f)
        {
            AddAnimation("cow2", tex2, 0.25f);
            AddAnimation("cow3", tex3, 0.25f);
            AddAnimation("cow4", tex4, 0.25f, "cow2");
            
        }
    }
}
