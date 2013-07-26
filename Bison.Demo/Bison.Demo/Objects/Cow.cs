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
        {
            animations.Add(
                "cow1",
                new AnimationStrip(
                    tex1,
                    "cow1",
                    tex1.Height,
                    0.25f));

            animations.Add(
                "cow2",
                new AnimationStrip(
                    tex2,
                    "cow2",
                    tex1.Height,
                    0.25f));
            animations["cow2"].NextAnimation = "cow1";
            animations["cow2"].LoopAnimation = false;

            animations.Add(
                "cow3",
                new AnimationStrip(
                    tex3,
                    "cow3",
                    tex1.Height,
                    0.25f));
            animations["cow3"].NextAnimation = "cow1";
            animations["cow3"].LoopAnimation = false;

            animations.Add(
                "cow4",
                new AnimationStrip(
                    tex4,
                    "cow4",
                    tex1.Height,
                    0.25f));
            animations["cow4"].NextAnimation = "cow2";
            animations["cow4"].LoopAnimation = false;
        }
    }
}
