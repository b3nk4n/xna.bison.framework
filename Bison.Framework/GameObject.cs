using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Class which defines a basic game object.
    /// </summary>
    public class GameObject : IGameUpdateable, IGameDrawable
    {
        #region Members

        /// <summary>
        /// The location inside the world.
        /// </summary>
        protected Vector2 worldLocation;

        /// <summary>
        /// The current velocity.
        /// </summary>
        protected Vector2 velocity;

        /// <summary>
        /// The frame width.
        /// </summary>
        protected int frameWidth;

        /// <summary>
        /// The frame heigth.
        /// </summary>
        protected int frameHeight;

        /// <summary>
        /// Indicates whether the game object is active or not.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Indicates whether the game object is visible or not.
        /// </summary>
        private bool isVisible;

        /// <summary>
        /// The flip state of teh game object.
        /// </summary>
        protected SpriteEffects flipped = SpriteEffects.None;

        /// <summary>
        /// The collision rectangle of the game object.
        /// </summary>
        protected Rectangle collisionRectangle;

        /// <summary>
        /// The animation strips.
        /// </summary>
        protected Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();

        /// <summary>
        /// The name of the currently acitve animation strip.
        /// </summary>
        protected string currentAnimation;

        /// <summary>
        /// The tint color.
        /// </summary>
        protected Color tintColor = Color.White;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new game object instance.
        /// </summary>
        public GameObject()
        {
            this.isActive = true;
            this.isVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the game objects animation and position.
        /// </summary>
        /// <param name="gameTime">The elapes game time since the last frame</param>
        public virtual void Update(GameTime gameTime)
        {
            if (isActive)
            {
                updateAnimation(gameTime);
                updatePosition(gameTime);
            }
        }

        /// <summary>
        /// Renders the game object if it is visible.
        /// </summary>
        /// <param name="batch">The sprite batch</param>
        public virtual void Draw(SpriteBatch batch)
        {
            if (IsVisible)
            {
                if (animations.ContainsKey(currentAnimation))
                {
                    batch.Draw(
                        animations[currentAnimation].Texture,
                        Camera.WorldToScreen(WorldRectangle),
                        animations[currentAnimation].FrameRectangle,
                        tintColor,
                        0.0f,
                        Vector2.Zero,
                        flipped,
                        0.0f);
                }
            }
        }

        /// <summary>
        /// Updates the game objects position.
        /// </summary>
        /// <param name="gameTime">The elapes game time since the last frame</param>
        public void updatePosition(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveDelta = velocity * elapsed;

            Vector2 newPosition = worldLocation + moveDelta;
            newPosition = new Vector2(
                MathHelper.Clamp(
                    newPosition.X,
                    0,
                    Camera.WorldRectangle.Width - frameWidth),
                MathHelper.Clamp(
                    newPosition.Y,
                    0,
                    Camera.WorldRectangle.Height - frameHeight));

            worldLocation = newPosition;
        }

        /// <summary>
        /// Updates the game objects animation.
        /// </summary>
        /// <param name="gameTime">The elapes game time since the last frame</param>
        private void updateAnimation(GameTime gameTime)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Play the animation strip with the given name.
        /// </summary>
        /// <param name="name">The animation strips name</param>
        private void PlayAnimation(string name)
        {
            if (!string.IsNullOrEmpty(name) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[currentAnimation].Play();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the game object is active or not.
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

        /// <summary>
        /// Gets or sets whether the game object is visible or not.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.IsVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        /// <summary>
        /// Gets or sets the location in world coordinates.
        /// </summary>
        public Vector2 WorldLocation
        {
            get
            {
                return this.worldLocation;
            }
            set
            {
                this.worldLocation = value;
            }
        }

        /// <summary>
        /// Gets the center in world coordinates.
        /// </summary>
        public Vector2 WorldCenter
        {
            get
            {
                return new Vector2(
                    (int)worldLocation.X + (int)(frameWidth / 2),
                    (int)worldLocation.Y + (int)(frameHeight / 2));
            }
        }

        /// <summary>
        /// Gets the rectangle in world coordinates.
        /// </summary>
        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X,
                    (int)worldLocation.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        /// <summary>
        /// Gets or sets the collision rectangle in world coordinates.
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X + collisionRectangle.X,
                    (int)worldLocation.Y + collisionRectangle.Y,
                    collisionRectangle.Width,
                    collisionRectangle.Height);
            }
            set
            {
                this.collisionRectangle = value;
            }
        }

        /// <summary>
        /// Gets or sets the tint color.
        /// </summary>
        public Color TintColor
        {
            get
            {
                return this.tintColor;
            }
            set
            {
                this.tintColor = value;
            }
        }

        #endregion
    }
}
