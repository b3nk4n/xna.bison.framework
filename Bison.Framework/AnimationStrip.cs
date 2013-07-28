using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Class which manages an animation strip for sprite animations.
    /// </summary>
    public class AnimationStrip : IGameUpdateable
    {
        #region Members

        /// <summary>
        /// The texture of the animation strip.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// The frame width.
        /// </summary>
        private int frameWidth;

        /// <summary>
        /// The frame height.
        /// </summary>
        private int frameHeight;

        /// <summary>
        /// The number of frames.
        /// </summary>
        private int frameCount;

        /// <summary>
        /// The frame timer.
        /// </summary>
        private GameTicker frameTimer;

        /// <summary>
        /// The current frame index.
        /// </summary>
        private int currentFrameIndex;

        /// <summary>
        /// Indicates whether the animation will be looped or not.
        /// </summary>
        private bool loopAnimation = true;

        /// <summary>
        /// Indicates whether the animation is finished.
        /// </summary>
        private bool finishedPlaying;

        /// <summary>
        /// The name of the animation.
        /// </summary>
        private string name;

        /// <summary>
        /// The name of the next animation, which will be started when this animation is finished.
        /// </summary>
        private string nextAnimation;

        /// <summary>
        /// Indicates whether the animation strip is active or not.
        /// </summary>
        private bool isActive;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new animation strip instance.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="name">The name.</param>
        /// <param name="frameWidth">The frame width.</param>
        /// <param name="frameHeight">The frame height.</param>
        /// <param name="frameTime">The frame delay.</param>
        public AnimationStrip(Texture2D texture, string name, int frameWidth, int frameHeight, float frameTime)
        {
            this.texture = texture;
            this.name = name;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = this.texture.Width / frameWidth;
            this.frameTimer = new GameTicker(frameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the frame animation.
        /// </summary>
        public void Play()
        {
            this.currentFrameIndex = 0;
            this.finishedPlaying = false;
            this.frameTimer.Reset();

            this.isActive = true;
        }

        /// <summary>
        /// Updates the frame animation.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                frameTimer.Update(gameTime);

                if (frameTimer.Elapsed)
                {
                    ++currentFrameIndex;

                    if (currentFrameIndex >= FrameCount)
                    {
                        if (loopAnimation)
                        {
                            currentFrameIndex = 0;
                        }
                        else
                        {
                            currentFrameIndex = FrameCount - 1;
                            finishedPlaying = true;
                        }
                    }

                    frameTimer.Reset();
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the texture.
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
        }

        /// <summary>
        /// Gets the frame height.
        /// </summary>
        public int FrameHeight
        {
            get
            {
                return this.frameHeight;
            }
        }

        /// <summary>
        /// Gets the frame width.
        /// </summary>
        public int FrameWidth
        {
            get
            {
                return this.frameWidth;
            }
        }

        /// <summary>
        /// Gets or sets whether the animation will be looped or not.
        /// </summary>
        public bool LoopAnimation
        {
            get
            {
                return this.loopAnimation;
            }
            set
            {
                this.loopAnimation = value;
            }
        }

        /// <summary>
        /// Gets whether the animation is finished or not.
        /// </summary>
        public bool FinishedPlaying
        {
            get
            {
                return this.finishedPlaying;
            }
        }

        /// <summary>
        /// Gets the number of frames.
        /// </summary>
        public int FrameCount
        {
            get
            {
                return this.frameCount;
            }
        }

        /// <summary>
        /// Gets the frame length in seconds.
        /// </summary>
        public float FrameLength
        {
            get
            {
                return frameTimer.DefaultTime;
            }
            set
            {
                frameTimer.DefaultTime = value;
            }
        }

        /// <summary>
        /// Gets the current frame rectangle.
        /// </summary>
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(
                    currentFrameIndex * frameWidth,
                    0,
                    frameWidth,
                    frameHeight);
            }
        }

        /// <summary>
        /// Gets the name of the animation strip.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets the name of the next animation strip.
        /// </summary>
        public string NextAnimation
        {
            get
            {
                return this.nextAnimation;
            }
            set
            {
                this.nextAnimation = value;
            }
        }

        /// <summary>
        /// Gets whether the animation is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        #endregion
    }
}
