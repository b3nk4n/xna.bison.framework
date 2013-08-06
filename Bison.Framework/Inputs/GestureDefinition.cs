using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace Bison.Framework.Inputs
{
    /// <summary>
    /// Represents a defined gesture.
    /// </summary>
    internal class GestureDefinition
    {
        #region Members

        /// <summary>
        /// The gesture type.
        /// </summary>
        private GestureType type;

        /// <summary>
        /// The gesture area.
        /// </summary>
        private Rectangle gestureArea;

        /// <summary>
        /// The wrapped gesture sample.
        /// </summary>
        private GestureSample gesture;

        /// <summary>
        /// The first touch position of a multi touch screen.
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// The second touch position of a multi touch screen.
        /// </summary>
        private Vector2 position2;

        /// <summary>
        /// The delta of the first touch position of a multi touch screen.
        /// </summary>
        private Vector2 delta;

        /// <summary>
        /// The delta of the second touch position of a multi touch screen.
        /// </summary>
        private Vector2 delta2;

        #endregion

        #region Constructros

        /// <summary>
        /// Creates a new gesture definition.
        /// </summary>
        /// <param name="gestureType">The gesture type.</param>
        /// <param name="gestureArea">The gesture area.</param>
        public GestureDefinition(GestureType gestureType, Rectangle gestureArea)
        {
            this.gesture = new GestureSample(
                gestureType,
                new TimeSpan(0),
                Vector2.Zero,
                Vector2.Zero,
                Vector2.Zero,
                Vector2.Zero);

            this.type = gestureType;
            this.gestureArea = gestureArea;
        }

        /// <summary>
        /// Creates a new gesture definition.
        /// </summary>
        /// <param name="gesture">The gesture sample to wrap.</param>
        public GestureDefinition(GestureSample gesture)
        {
            this.gesture = gesture;
            this.type = gesture.GestureType;
            this.gestureArea = new Rectangle(
                (int)gesture.Position.X,
                (int)gesture.Position.Y,
                5,
                5);
            this.delta = gesture.Delta;
            this.delta2 = gesture.Delta2;
            this.position = gesture.Position;
            this.position2 = gesture.Position2;
        }

        /// <summary>
        /// Gets the gesture type.
        /// </summary>
        public GestureType Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Gets the wrapped gesture.
        /// </summary>
        public GestureSample Gesture
        {
            get
            {
                return this.gesture;
            }
        }

        /// <summary>
        /// Gets the expected gesture area.
        /// </summary>
        public Rectangle GestureArea
        {
            get
            {
                return this.gestureArea;
            }
        }

        /// <summary>
        /// Gets or sets the first touch position of a multi touch screen.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        /// <summary>
        /// Gets or sets the second touch position of a multi touch screen.
        /// </summary>
        public Vector2 Position2
        {
            get
            {
                return this.position2;
            }
            set
            {
                this.position2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the delta of the first touch position of a multi touch screen.
        /// </summary>
        public Vector2 Delta
        {
            get
            {
                return this.delta;
            }
            set
            {
                this.delta = value;
            }
        }

        /// <summary>
        /// Gets or sets the delta of the second touch position of a multi touch screen.
        /// </summary>
        public Vector2 Delta2
        {
            get
            {
                return this.delta2;
            }
            set
            {
                this.delta2 = value;
            }
        }

        #endregion
    }
}
