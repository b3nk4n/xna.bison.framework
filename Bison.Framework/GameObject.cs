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
    public abstract class GameObject : IGameUpdateable, IGameDrawable
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
        /// The collision radius.
        /// </summary>
        protected float collisionRadius;                        // TODO: support multipe circles relative to center, which will also be rotated with (use Rectlangle class ?)

        /// <summary>
        /// The bounding box padding.
        /// </summary>
        protected Vector2 boundingBoxPadding;

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

        /// <summary>
        /// The game objects rotation in radian. The neutral look direction is to the right.
        /// </summary>
        protected float rotation;

        /// <summary>
        /// The game objects scale.
        /// </summary>
        protected Vector2 scale = Vector2.One;

        /// <summary>
        /// The layer depth for the rendering.
        /// </summary>
        protected float layerDepth;

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
        /// Rotates the game object to the given location in world coordinates.
        /// </summary>
        /// <param name="worldPosition">The world location to face to</param>
        public void RotateTo(Vector2 worldLocation)
        {
            Vector2 direction = worldLocation - this.worldLocation;
            RotateToDirection(direction);
        }

        /// <summary>
        /// Rotates the game object to the given look direction.
        /// </summary>
        /// <param name="direction">The look direction</param>
        public void RotateToDirection(Vector2 direction)
        {
            this.Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        /// <summary>
        /// Checks the boundig box collision.
        /// </summary>
        /// <param name="otherBox">The other bounding box</param>
        /// <returns>TRUE, of both bounding boxes are intersecting</returns>
        public bool IsBoxColliding(Rectangle otherBox)
        {
            return this.BoundingBox.Intersects(otherBox);
        }

        /// <summary>
        /// Checks the boundig box collision.
        /// </summary>
        /// <param name="other">The other game object</param>
        /// <returns>TRUE, of both bounding boxes are intersecting</returns>
        public bool IsBoxColliding(GameObject other)
        {
            return IsBoxColliding(other.BoundingBox);
        }

        /// <summary>
        /// Checks the circle collision.
        /// </summary>
        /// <param name="otherCenter">The other objects center</param>
        /// <param name="otherRadius">The other objects radius</param>
        /// <returns>TRUE, if both collision circles are intersecting</returns>
        public bool IsCircleColliding(Vector2 otherCenter, float otherRadius)
        {
            if (Vector2.Distance(this.WorldCenter, otherCenter) < this.CollisionRadius + otherRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the circle collision.
        /// </summary>
        /// <param name="other">THe other game object</param>
        /// <returns>TRUE, if both collision circles are intersecting</returns>
        public bool IsCircleColliding(GameObject other)
        {
            return this.IsCircleColliding(other.WorldCenter, other.CollisionRadius);
        }

        /// <summary>
        /// Play the animation strip with the given name.
        /// </summary>
        /// <param name="name">The animation strips name</param>
        public void PlayAnimation(string name)
        {
            if (!string.IsNullOrEmpty(name) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[currentAnimation].Play();
            }
        }

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
                        Camera.WorldToScreen(WorldCenter),
                        animations[currentAnimation].FrameRectangle,
                        tintColor,
                        rotation,
                        new Vector2(frameWidth / 2, frameHeight / 2),
                        scale,
                        flipped,
                        layerDepth);
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
        /// Gets or sets the collision radius.
        /// </summary>
        public float CollisionRadius
        {
            get
            {
                return this.collisionRadius;
            }
            set
            {
                this.collisionRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the bounding box padding.
        /// </summary>
        public Vector2 BoundingBoxPadding
        {
            get
            {
                return this.boundingBoxPadding;
            }
            set
            {
                this.boundingBoxPadding = value;
            }
        }

        /// <summary>
        /// Gets or sets the collision bounding box in world coordinates.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(worldLocation.X + boundingBoxPadding.X),
                                     (int)(worldLocation.Y + boundingBoxPadding.Y),
                                     frameWidth - ((int)(2 * boundingBoxPadding.X)),
                                     frameHeight - ((int)(2 * boundingBoxPadding.Y)));
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

        /// <summary>
        /// Gets or sets the game objects rotation.
        /// </summary>
        public float Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        /// <summary>
        /// Gets or sets the game objects scale.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
            }
        }

        /// <summary>
        /// Gets or sets the layer depth.
        /// </summary>
        public float LayerDepth
        {
            get
            {
                return this.layerDepth;
            }
            set
            {
                this.layerDepth = value;
            }
        }

        #endregion
    }
}
