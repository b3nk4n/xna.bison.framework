using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Animation
{
    class FadeAnimation : Animation
    {
        private bool increase;
        private float fadeSpeed;
        TimeSpan defaultTime;
        TimeSpan timer;
        bool startTimer;
        float activateValue;
        bool stopUpdating;
        float defaultOpacity;

        public override void LoadContent(ContentManager content, Texture2D texture, string text, Vector2 position)
        {
            base.LoadContent(content, texture, text, position);
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);
            timer = defaultTime;
            activateValue = 0.0f;
            stopUpdating = false;
            defaultOpacity = opacity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsActive)
            {
                if (increase)
                {
                    opacity += fadeSpeed * elapsed;
                } else
                {
                    opacity -= fadeSpeed * elapsed;
                }

                opacity = MathHelper.Clamp(opacity, 0.0f, 1.0f);
            }

            if (opacity == activateValue)
            {
                stopUpdating = true;
                timer -= gameTime.ElapsedGameTime;
                if (timer.TotalSeconds <= 0)
                {
                    increase = !increase;
                    timer = defaultTime;
                    stopUpdating = false;
                }
            }
        }

        public override float Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;

                if (opacity == 1.0)
                {
                    increase = false;
                }
                else if (opacity == 0.0f)
                {
                    increase = true;
                }
            }
        }

        public float ActivateValue
        {
            get;
            set;
        }

        public float FadeSpeed
        {
            get;
            set;
        }

        public TimeSpan Timer
        {
            get
            {
                return this.timer;   
            }
            set
            {
                this.defaultTime = value;
                this.timer = defaultTime;
            }
        }
    }
}
