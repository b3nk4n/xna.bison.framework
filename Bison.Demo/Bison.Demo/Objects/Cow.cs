using Bison.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Demo.Objects
{
    class Cow : GameObject
    {
        public Cow(Texture2D tex1, Texture2D tex2, Texture2D tex3, Texture2D tex4)
            : base (64, 64)
        {
            AddAnimation("cow1", tex1, 0.25f);

            AddAnimation("cow2", tex2, 0.25f);
            Animations["cow2"].NextAnimation = "cow1";
            Animations["cow2"].LoopAnimation = false;

            AddAnimation("cow3", tex3, 0.25f);
            Animations["cow3"].NextAnimation = "cow1";
            Animations["cow3"].LoopAnimation = false;

            AddAnimation("cow4", tex4, 0.25f);
            Animations["cow4"].NextAnimation = "cow2";
            Animations["cow4"].LoopAnimation = false;
        }
    }
}
