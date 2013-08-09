using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bison.Framework
{
    /// <summary>
    /// Class which defines a basic game object.
    /// </summary>
    public abstract class GameObject : IGameUpdateable
    {
        #region Members

        /// <summary>
        /// The location inside the world.
        /// </summary>
        private Vector2 location;

        /// <summary>
        /// The current velocity.
        /// </summary>
        private Vector2 velocity;

        /// <summary>
        /// The game objects rotation in radian. The neutral look direction is to the right.
        /// </summary>
        private float rotation;

        /// <summary>
        /// The game objects scale.
        /// </summary>
        private float scale = 1.0f;

        /// <summary>
        /// The layer depth for the rendering.
        /// </summary>
        protected float layerDepth;
        
        /// <summary>
        /// Indicates whether the game object is active or not.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// Defines the collision circles where its center is used for the displacement
        /// relative to the center of the game object without rotation.
        /// </summary>
        private Circle[] neutralCollisionCircles;

        /// <summary>
        /// Defines the collision circles in world coordiantes regarding
        /// the game objects current rotation.
        /// </summary>
        private Circle[] currentCollisionCircles;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new game object instance.
        /// </summary>
        public GameObject()
        {
            this.isActive = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the collision circles where the circles are in neutral position.
        /// </summary>
        /// <param name="circles">The collision circles relative to the game objects center.</param>
        public void SetupCollisionCircles(Circle[] circles)
        {
            this.neutralCollisionCircles = circles;
            updateCurrentCollisonCircles();
        }

        /// <summary>
        /// Updates the current collision circles, when the position or orientation
        /// of the game object has been modified.
        /// </summary>
        private void updateCurrentCollisonCircles()
        {
            if (neutralCollisionCircles != null)
            {
                // ensure array size
                if (currentCollisionCircles == null ||
                    currentCollisionCircles.Length != neutralCollisionCircles.Length)
                {
                    currentCollisionCircles = new Circle[neutralCollisionCircles.Length];
                }

                for (int i = 0; i < neutralCollisionCircles.Length; ++i)
                {
                    var circle = neutralCollisionCircles[i];

                    currentCollisionCircles[i].Center = (this.Center + circle.Center * Scale).Rotate(Center, Rotation);
                    currentCollisionCircles[i].Radius = circle.Radius * Scale;
                }
            }
        }

        /// <summary>
        /// Rotates the game object to the given location in world coordinates.
        /// </summary>
        /// <param name="worldLocation">The world location to face to</param>
        public void RotateTo(Vector2 worldLocation)
        {
            Vector2 direction = worldLocation - this.location;
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
        public bool IsBoundingBoxColliding(Rectangle otherBox)
        {
            if (rotation != 0.0f)
            {
                Debug.WriteLine("Box collision should not be used if the object is rotated");
            }

            return this.BoundingBox.Intersects(otherBox);
        }

        /// <summary>
        /// Checks the boundig box collision.
        /// </summary>
        /// <param name="other">The other game object</param>
        /// <returns>TRUE, of both bounding boxes are intersecting</returns>
        public bool IsBoundingBoxColliding(GameObject other)
        {
            if (other.Rotation != 0.0f)
            {
                Debug.WriteLine("Box collision should not be used if the object is rotated");
            }

            return IsBoundingBoxColliding(other.BoundingBox);
        }

        /// <summary>
        /// Checks the circle collision.
        /// </summary>
        /// <param name="otherCicle">The other objects circle.</param>
        /// <returns>TRUE, if both collision circles are intersecting.</returns>
        public bool IsCircleColliding(Circle otherCircle)
        {
            if (currentCollisionCircles == null)
            {
                foreach (var circle in currentCollisionCircles)
                {
                    // calculation using squared values for optimization
                    if (Vector2.DistanceSquared(this.Center, otherCircle.Center)
                        < (circle.Radius + otherCircle.Radius) * (circle.Radius + otherCircle.Radius))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the circle collision.
        /// </summary>
        /// <param name="other">THe other game object</param>
        /// <returns>TRUE, if both collision circles are intersecting</returns>
        public bool IsCircleColliding(GameObject other)
        {
            foreach (var otherCircle in other.CollisionCircles)
            {
                if (IsCircleColliding(otherCircle))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates the game objects position.
        /// </summary>
        /// <param name="gameTime">The elapes game time since the last frame</param>
        public virtual void Update(GameTime gameTime)
        {
            if (isActive)
            {
                updatePosition(gameTime);
            }
        }

        /// <summary>
        /// Updates the game objects position.
        /// </summary>
        /// <param name="gameTime">The elapes game time since the last frame</param>
        private void updatePosition(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveDelta = velocity * elapsed;

            Vector2 newPosition = location + moveDelta;
            newPosition = new Vector2(
                MathHelper.Clamp(
                    newPosition.X,
                    0,
                    Camera.WorldRectangle.Width - Width),
                MathHelper.Clamp(
                    newPosition.Y,
                    0,
                    Camera.WorldRectangle.Height - Width));

            location = newPosition;
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
        /// Gets or sets the unscaled location in world coordinates.
        /// </summary>
        public Vector2 Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
                updateCurrentCollisonCircles();
            }
        }

        /// <summary>
        /// Gets the game objects unscaled width.
        /// </summary>
        public abstract int SourceWidth { get; }

        /// <summary>
        /// Gets the game objects unscaled height.
        /// </summary>
        public abstract int SourceHeight { get; }

        /// <summary>
        /// The current width.
        /// </summary>
        public float Width
        {
            get
            {
                return SourceWidth * scale;
            }
        }

        /// <summary>
        /// The current height.
        /// </summary>
        public float Height
        {
            get
            {
                return SourceHeight * scale;
            }
        }

        /// <summary>
        /// Gets the center in world coordinates.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                return new Vector2(
                    (int)location.X + (int)(SourceWidth / 2),
                    (int)location.Y + (int)(SourceHeight / 2));
            }
        }

        /// <summary>
        /// Gets the collision cicles.
        /// </summary>
        public Circle[] CollisionCircles
        {
            get
            {
                return this.currentCollisionCircles;
            }
        }

        /// <summary>
        /// Gets whether the game object can circle collide.
        /// </summary>
        public bool CanCircleCollide
        {
            get
            {
                return this.IsActive &&
                    this.CollisionCircles != null &&
                    this.CollisionCircles.Length > 0;
            }
        }

        /// <summary>
        /// Gets or sets the collision bounding box in world coordinates.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(location.X - (Width - SourceWidth) / 2),
                                     (int)(location.Y - (Height - SourceHeight) / 2),
                                     (int)Width,
                                     (int)Height);
            }
        }

        /// <summary>
        /// Gets or sets the collision bounding box in world coordinates without scaling.
        /// </summary>
        public Rectangle SourceBoundingBox
        {
            get
            {
                return new Rectangle((int)(location.X),
                                     (int)(location.Y),
                                     (int)SourceWidth,
                                     (int)SourceHeight);
            }
        }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        /// <summary>
        /// Gets or sets the direction by keeping the current speed.
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                if (this.velocity == Vector2.Zero)
                {
                    return Vector2.Zero;
                }

                return this.velocity / velocity.Length();
            }
            set
            {
                var normalizedDirection = value;
                if (normalizedDirection != Vector2.Zero)
                {
                    normalizedDirection.Normalize();
                }
                this.velocity = normalizedDirection * Speed;
            }
        }

        /// <summary>
        /// Gets or sets the speed by keeping the current direction.
        /// </summary>
        public float Speed
        {
            get
            {
                return this.velocity.Length();
            }
            set
            {
                Velocity = Direction * value;
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
                updateCurrentCollisonCircles();
            }
        }

        /// <summary>
        /// Gets or sets the game objects scale.
        /// </summary>
        public float Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
                updateCurrentCollisonCircles();
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
