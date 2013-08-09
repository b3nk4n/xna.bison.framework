using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Represents an animated sprite.
    /// </summary>
    public class MultiSprite : Sprite
    {
        /// <summary>
        /// The name of the default animation.
        /// </summary>
        public const string BASE_ANIMATION = "BaseAnimation";

        /// <summary>
        /// The additional animation strips.
        /// </summary>
        private IDictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();

        /// <summary>
        /// The name of the currently acitve animation strip.
        /// </summary>
        protected string currentAnimationName;

        /// <summary>
        /// Creates a new multi sprite instance.
        /// </summary>
        /// <param name="texture">The texture of the base animation.</param>
        /// <param name="frameWidth">The frame width.</param>
        /// <param name="frameHeight">The frame height.</param>
        /// <param name="frameTime">The time for each frame.</param>
        public MultiSprite(Texture2D texture, int frameWidth, int frameHeight, float frameTime)
            : base(texture, frameWidth, frameHeight, frameTime)
        {
            this.currentAnimationName = BASE_ANIMATION;
        }

        /// <summary>
        /// Adds a new non-looped sprite animation.
        /// </summary>
        /// <param name="key">The animation key.</param>
        /// <param name="texture">The animation texture.</param>
        /// <param name="frameTime">The frame time.</param>
        protected void AddAnimation(string key, Texture2D texture, float frameTime)
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
        protected void AddAnimation(string key, Texture2D texture, float frameTime, string nextAnimationKey)
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
        protected void AddAnimation(string key, Texture2D texture, float frameTime, bool loopAnimation)
        {
            addAnimation(
                key, texture,
                frameTime,
                BASE_ANIMATION,
                loopAnimation);
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

        /// <summary>
        /// Play the animation strip with the given name.
        /// </summary>
        /// <param name="name">The animation strips name.</param>
        public void PlayAnimation(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                currentAnimationName = name;
                GetCurrentAnimation().Play();
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
        /// Updates the game objects animation.
        /// </summary>
        /// <param name="gameTime">The elapsed game time since the last frame.</param>
        private void updateAnimation(GameTime gameTime)
        {
            var currentAnimation = GetCurrentAnimation();

            if (currentAnimation.FinishedPlaying)
            {
                PlayAnimation(currentAnimation.NextAnimationKey);
            }
            else
            {
                currentAnimation.Update(gameTime);
            }
        }

        /// <summary>
        /// Gets the current animation.
        /// </summary>
        protected override AnimationStrip GetCurrentAnimation()
        {
            if (animations.ContainsKey(currentAnimationName))
            {
                return this.animations[currentAnimationName];
            }
            
            return base.GetCurrentAnimation();
        }

        /// <summary>
        /// Gets the animations dictionary.
        /// </summary>
        protected IDictionary<string, AnimationStrip> Animations
        {
            get
            {
                return this.animations;
            }
        }

        /// <summary>
        /// Gets the name of the current animation.
        /// </summary>
        public string CurrentAnimationName
        {
            get
            {
                return this.currentAnimationName;
            }
        }
    }
}
