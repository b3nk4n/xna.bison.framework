using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    class GameTicker
    {
        private readonly float defaultTime;
        private float timer;

        public GameTicker(float defaultTime)
        {
            this.defaultTime = defaultTime;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            timer -= elapsed;
        }

        public void Reset()
        {
            Reset(defaultTime);
        }

        public void Reset(float time)
        {
            timer = time;
        }

        public bool Elapsed
        {
            get
            {
                return timer < 0.0f;
            }
        }
    }
}
