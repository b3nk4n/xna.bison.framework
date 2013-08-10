using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Represents an animated sprite with multiple animations like a storyboar.
    /// </summary>
    public class StoryboardSprite : Sprite
    {
        #region Members

        /// <summary>
        /// The name of the default animation.
        /// </summary>
        public const string BASE_ANIMATION = "BaseAnimationKey";

        /// <summary>
        /// The additional animation strips.
        /// </summary>
        private IDictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();

        /// <summary>
        /// The key name of the currently acitve animation strip.
        /// </summary>
        protected string currentAnimationKey;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new multi sprite instance.
        /// </summary>
        /// <param name="texture">The texture of the base animation.</param>
        /// <param name="frameWidth">The frame width.</param>
        /// <param name="frameHeight">The frame height.</param>
        /// <param name="frameTime">The time for each frame.</param>
        public StoryboardSprite(Texture2D texture, int frameWidth, int frameHeight, float frameTime)
            : base(texture, frameWidth, frameHeight)
        {
            var baseAnimation = new AnimationStrip(
                    texture,
                    frameWidth,
                    frameHeight,
                    frameTime);
            baseAnimation.LoopAnimation = true;
            this.animations.Add(
                BASE_ANIMATION,
                baseAnimation);
            this.currentAnimationKey = BASE_ANIMATION;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new non-looped sprite animation.
        /// </summary>
        /// <param name="key">The animation key.</param>
        /// <param name="texture">The animation texture.</param>
        /// <param name="frameTime">The frame time.</param>
        public void AddAnimation(string key, Texture2D texture, float frameTime)
        {
            addAnimation(
                key, texture,
                frameTime,
                BASE_ANIMATION,
                false);
        }

        /// <summary>
        /// Adds a new non-looped sprite animation.
        /// </summary>
        /// <param name="key">The animation key.</param>
        /// <param name="texture">The animation texture.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="nextAnimationKey">The key of the next animation.</param>
        public void AddAnimation(string key, Texture2D texture, float frameTime, string nextAnimationKey)
        {
            addAnimation(
                key, texture,
                frameTime,
                nextAnimationKey,
                false);
        }

        /// <summary>
        /// Adds a new sprite animation.
        /// </summary>
        /// <param name="key">The animation key.</param>
        /// <param name="texture">The animation texture.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="loopAnimation">Whether the animatin is looped or not.</param>
        public void AddAnimation(string key, Texture2D texture, float frameTime, bool loopAnimation)
        {
            addAnimation(
                key, texture,
                frameTime,
                BASE_ANIMATION,
                loopAnimation);
        }

        /// <summary>
        /// Play the animation strip with the given name.
        /// </summary>
        /// <param name="key">The animation strips key name.</param>
        public void PlayAnimation(string key)
        {
            if (!string.IsNullOrEmpty(key) && animations.ContainsKey(key))
            {
                currentAnimationKey = key;
                animations[currentAnimationKey].Play();
            }
        }

        /// <summary>
        /// Updates the game objects animation and position.
        /// </summary>
        /// <param name="gameTime">The elapes game time since the last frame.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsActive)
            {
                updateAnimation(gameTime);
            }
        }

        /// <summary>
        /// Gets the current frame source.
        /// </summary>
        protected override Rectangle GetCurrentFrame()
        {
            if (animations.ContainsKey(currentAnimationKey))
            {
                return this.animations[currentAnimationKey].FrameRectangle;
            }

            return base.GetCurrentFrame();
        }

        /// <summary>
        /// Gets the current frame texture.
        /// </summary>
        protected override Texture2D GetCurrentTexture()
        {
            if (animations.ContainsKey(currentAnimationKey))
            {
                return this.animations[currentAnimationKey].Texture;
            }

            return base.GetCurrentTexture();
        }

        /// <summary>
        /// Updates the game objects animation.
        /// </summary>
        /// <param name="gameTime">The elapsed game time since the last frame.</param>
        private void updateAnimation(GameTime gameTime)
        {
            if (animations[currentAnimationKey].FinishedPlaying)
            {
                PlayAnimation(animations[currentAnimationKey].NextAnimationKey);
            }
            else
            {
                animations[currentAnimationKey].Update(gameTime);
            }
        }

        /// <summary>
        /// Adds a new sprite animation.
        /// </summary>
        /// <param name="key">The animation key.</param>
        /// <param name="texture">The animation texture.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="nextAnimationKey">The key of the next animation.</param>
        /// <param name="loopAnimation">Whether the animatin is looped or not.</param>
        private void addAnimation(string key, Texture2D texture, float frameTime, string nextAnimationKey, bool loopAnimation)
        {
            var animation = new AnimationStrip(
                texture,
                SourceWidth,
                SourceHeight,
                frameTime);
            animation.NextAnimationKey = nextAnimationKey;
            animation.LoopAnimation = loopAnimation;

            animations.Add(key, animation);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the current animation.
        /// </summary>
        public string CurrentAnimationName
        {
            get
            {
                return this.currentAnimationKey;
            }
        }

        #endregion
    }
}
