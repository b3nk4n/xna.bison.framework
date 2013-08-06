using Microsoft.Phone.Applications.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Bison.Framework.Inputs
{
    /// <summary>
    /// The game input manager class.
    /// </summary>
    public class InputManager
    {
        #region Members
        /// <summary>
        /// Collection of all defined inputs.
        /// </summary>
        Dictionary<string, Input> inputs = new Dictionary<string, Input>();

        #endregion

        #region Constructores

        /// <summary>
        /// Creates a new input manager instance.
        /// </summary>
        public InputManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets or created a new input.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The input with the given action name.</returns>
        private Input getInput(string action)
        {
            // Add the action, if it doesn't already exist
            if (!inputs.ContainsKey(action))
            {
                inputs.Add(action, new Input());
            }

            return inputs[action];
        }

        /// <summary>
        /// Begins the input update.
        /// </summary>
        public void BeginUpdate()
        {
            Input.BeginUpdate();
        }

        /// <summary>
        /// Ends the input update.
        /// </summary>
        public void EndUpdate()
        {
            Input.EndUpdate();
        }

        /// <summary>
        /// Verifies whether the given action was pressed.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="newGestureDetectionLocation">The new gesture detection location.</param>
        /// <returns>TRUE, if the action was pressed.</returns>
        public bool IsPressed(string action, Rectangle? newGestureDetectionLocation)
        {
            if (!inputs.ContainsKey(action))
            {
                return false;
            }

            return inputs[action].IsPressed(newGestureDetectionLocation);
        }

        /// <summary>
        /// Verifies whether the given action was pressed.
        /// </summary>
        /// <param name="action">The actin name.</param>
        /// <returns>TRUE, if the action was pressed.</returns>
        public bool IsPressed(string action)
        {
            if (!inputs.ContainsKey(action))
            {
                return false;
            }

            return inputs[action].IsPressed();
        }

        /// <summary>
        /// Adds a new button action.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="button">The button.</param>
        /// <param name="isReleasedPreviously">Whether the button must be released previously to fire the action.</param>
        public void AddButtonInput(string action, Buttons button, bool isReleasedPreviously)
        {
            getInput(action).AddButtonInput(button,
                                            isReleasedPreviously);
        }

        /// <summary>
        /// Adds a new keyboard action.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="key">The keyboard key.</param>
        /// <param name="isReleasedPreviously">Whether the key must be released previously to fire the action.</param>
        public void AddKeyboardInput(string action, Keys key, bool isReleasedPreviously)
        {
            getInput(action).AddKeyboardInput(key,
                                             isReleasedPreviously);
        }

        /// <summary>
        /// Adds a new touch tap action.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="touchArea">The touch area.</param>
        /// <param name="isReleasedPreviously">Whether the finger must be released previously to fire the action.</param>
        public void AddTouchTapInput(string action, Rectangle touchArea, bool isReleasedPreviously)
        {
            getInput(action).AddTouchTapInput(touchArea,
                                             isReleasedPreviously);
        }

        /// <summary>
        /// Adds a new swipe action.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="direction">The slide direction.</param>
        /// <param name="slideDistance">The slide distance to fire the action.</param>
        public void AddSwipeInput(string action, InputDirection direction, float slideDistance)
        {
            getInput(action).AddSwipeInput(direction,
                                               slideDistance);
        }

        /// <summary>
        /// Adds a touch gesture action.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="gesture">The touch gesture.</param>
        /// <param name="gestureArea">The gesture area.</param>
        public void AddTouchGestureInput(string action, GestureType gesture, Rectangle gestureArea)
        {
            getInput(action).AddTouchGestureInput(gesture,
                                                 gestureArea);
        }

        /// <summary>
        /// Adss an accelerometer input action.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <param name="direction">The accelerometer tilt direction.</param>
        /// <param name="tiltThreshold">The tilt threshold.</param>
        public void AddAccelerometerInput(string action, InputDirection direction, float tiltThreshold, AccelerometerFilterType filterType)
        {
            getInput(action).AddAccelerometerInput(
                direction,
                tiltThreshold,
                filterType);
        }

        /// <summary>
        /// The gestures current position.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The current gesture position.</returns>
        public Vector2 CurrentGesturePosition(string action)
        {
            return getInput(action).CurrentGesturePosition();
        }

        /// <summary>
        /// The gestures current delta.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The current gesture delta.</returns>
        public Vector2 CurrentGestureDelta(string action)
        {
            return getInput(action).CurrentGestureDelta();
        }

        /// <summary>
        /// The gestures current second position.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The current gestures second position.</returns>
        public Vector2 CurrentGesturePosition2(string action)
        {
            return getInput(action).CurrentGesturePosition2();
        }

        /// <summary>
        /// The gestures current second delta.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The currend gestures second delta.</returns>
        public Vector2 CurrentGestureDelta2(string action)
        {
            return getInput(action).CurrentGestureDelta2();
        }

        /// <summary>
        /// Gets the current touch point.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The current touch point.</returns>
        public Point CurrentTouchPoint(string action)
        {
            Vector2? currentPosition = getInput(action).CurrentTouchPosition();

            if (currentPosition == null)
            {
                return new Point(-1, -1);
            }

            return new Point((int)currentPosition.Value.X,
                             (int)currentPosition.Value.Y);
        }

        /// <summary>
        /// Gets the current touch position.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The current touch position.</returns>
        public Vector2 CurrentTouchPosition(string action)
        {
            Vector2? currentPosition = getInput(action).CurrentTouchPosition();

            if (currentPosition == null)
            {
                return new Vector2(-1, -1);
            }

            return currentPosition.Value; // ??? does it work?
        }

        /// <summary>
        /// Gets the current gesture scale change of a pinch gesture.
        /// </summary>
        /// <param name="action">The action name.</param>
        /// <returns>The current gesture scale change.</returns>
        public float CurrentGestureScaleChange(string action)
        {
            // if no Pinch esture is activated, return zero
            if (!getInput(action).IsPinchGestureAvailable)
            {
                return 0.0f;
            }

            // Get the current and previous position of the fingers
            Vector2 currentPositionFingerOne = CurrentGesturePosition(action);

            Vector2 previousPositionFingerOne = CurrentGesturePosition(action)
                                                - CurrentGestureDelta(action);

            Vector2 currentPositionFingerTwo = CurrentGesturePosition2(action);

            Vector2 previousPositionFingerTwo = CurrentGesturePosition2(action)
                                                - CurrentGestureDelta2(action);

            //Figure out the distance between current and previous position
            float currentDistance = Vector2.Distance(currentPositionFingerOne,
                                                     currentPositionFingerTwo);

            float previousDistance = Vector2.Distance(previousPositionFingerOne,
                                                      previousPositionFingerTwo);

            // Calculate the diff between both and use it to alter the scale
            float scaleChange = (currentDistance - previousDistance) * 0.01f;

            return scaleChange;
        }

        /// <summary>
        /// Gets the current accelerometer reading.
        /// </summary>
        /// <param name="filterType">The used filter type.</param>
        /// <returns>The current accelerometer reading.</returns>
        public Vector3 CurrentAccelerometerReading(AccelerometerFilterType filterType)
        {
            return Input.CurrentAccelerometerReading(filterType);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the accelerometer.
        /// </summary>
        public static AccelerometerHelper Accelerometer
        {
            get
            {
                return Input.Accelerometer;
            }
        }

        #endregion
    }
}
