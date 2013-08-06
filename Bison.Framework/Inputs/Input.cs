using Microsoft.Devices.Sensors;
using Microsoft.Phone.Applications.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bison.Framework.Inputs
{
    /// <summary>
    /// The accelerometer filter type.
    /// </summary>
    public enum AccelerometerFilterType
    {
        /// <summary>
        /// Raw, unfiltered accelerometer data (acceleration vector in all 3 dimensions) coming directly from sensor.
        /// This is required for updating rapidly reacting UI.
        /// </summary>
        Raw,
        /// <summary>
        /// Filtered and temporally averaged accelerometer data using an arithmetic mean of the last 25 "optimaly filtered" 
        /// samples (see above), so over 500ms at 50Hz on each axis, to virtually eliminate most sensor noise. 
        /// This provides a very stable reading but it has also a very high latency and cannot be used for rapidly reacting UI.
        /// </summary>
        Average,
        /// <summary>
        /// Filtered accelerometer data using a combination of a low-pass and threshold triggered high-pass on each axis to 
        /// elimate the majority of the sensor low amplitude noise while trending very quickly to large offsets (not perfectly
        /// smooth signal in that case), providing a very low latency. This is ideal for quickly reacting UI updates.
        /// </summary>
        OptimalyFiltered,
        /// <summary>
        /// Filtered accelerometer data using a 1 Hz first-order low-pass on each axis to elimate the main sensor noise
        /// while providing a medium latency. This can be used for moderatly reacting UI updates requiring a very smooth signal.
        /// </summary>
        LowPassFiltered
    }

    /// <summary>
    /// The directions for swipe or accelerometer inputs.
    /// </summary>
    public enum InputDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Represents any game input using keyboard, gamepad, touch screen or accelerometer.
    /// </summary>
    public class Input
    {
        #region Members

        /// <summary>
        /// The defined keyboard inputs.
        /// </summary>
        private Dictionary<Keys, bool> keyboardDefinedInputs = new Dictionary<Keys, bool>();

        /// <summary>
        /// The defined button inputs.
        /// </summary>
        private Dictionary<Buttons, bool> buttonDefinedInputs = new Dictionary<Buttons, bool>();

        /// <summary>
        /// The defined touch tap inputs.
        /// </summary>
        private Dictionary<Rectangle, bool> touchTapDefinedInputs = new Dictionary<Rectangle, bool>();

        /// <summary>
        /// The defined swipe inputs.
        /// </summary>
        private Dictionary<InputDirection, float> swipeDefinedInputs = new Dictionary<InputDirection, float>();

        /// <summary>
        /// The defined gesture inputs.
        /// </summary>
        private Dictionary<int, GestureDefinition> gestureDefinedInputs = new Dictionary<int, GestureDefinition>();

        /// <summary>
        /// The defined accelerometer inputs.
        /// </summary>
        private Dictionary<InputDirection, KeyValuePair<float, AccelerometerFilterType>> accelerometerDefinedInputs = new Dictionary<InputDirection, KeyValuePair<float, AccelerometerFilterType>>();

        /// <summary>
        /// The current button state.
        /// </summary>
        public static GamePadState CurrentButtonState;

        /// <summary>
        /// The previous button state.
        /// </summary>
        public static GamePadState PreviousButtonState;
        
        /// <summary>
        /// The current keyboard state.
        /// </summary>
        public static KeyboardState CurrentKeyboardState;

        /// <summary>
        /// The previous keyboard state.
        /// </summary>
        public static KeyboardState PreviousKeyboardState;

        /// <summary>
        /// The current touchscreen state.
        /// </summary>
        public static TouchCollection CurrentTouchLocationState;

        /// <summary>
        /// The previous touchscreen state.
        /// </summary>
        public static TouchCollection PreviousTouchLocationState;

        /// <summary>
        /// The detected gestures.
        /// </summary>
        private static List<GestureDefinition> detectedGestures = new List<GestureDefinition>();

        /// <summary>
        /// The accelerometer.
        /// </summary>
        private static AccelerometerHelper accelerometer;

        /// <summary>
        /// The current raw accelerometer reading.
        /// </summary>
        private static Vector3 currentRawAccelerometerReading;

        /// <summary>
        /// The current average accelerometer reading.
        /// </summary>
        private static Vector3 currentAverageAccelerometerReading;

        /// <summary>
        /// The current optimaly filtered accelerometer reading.
        /// </summary>
        private static Vector3 currentOptimalyFilteredAccelerometerReading;

        /// <summary>
        /// The curent low pass filtered accelerometer reading.
        /// </summary>
        private static Vector3 currentLowPassFilteredAccelerometerReading;

        /// <summary>
        /// Indicates whether a pinch gesture is available.
        /// </summary>
        private bool isPinchGestureAvailable = false;

        /// <summary>
        /// The current gesture definition.
        /// </summary>
        private GestureDefinition currentGestureDefinition;

        #endregion

        #region Constructors

        /// <summary>
        /// Createa a new input instance.
        /// </summary>
        public Input()
        {
            // check and setup gamepad connection
            if (CurrentButtonState == null)
            {
                CurrentButtonState = GamePad.GetState(PlayerIndex.One);
                PreviousButtonState = GamePad.GetState(PlayerIndex.One);
            }

            // setup accelerometer
            if (accelerometer == null)
            {
                accelerometer = AccelerometerHelper.Instance;

                accelerometer.CurrentValueChanged += accelerometerCurrentValueChanged;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Begins the input update.
        /// </summary>
        public static void BeginUpdate()
        {
            CurrentButtonState = GamePad.GetState(PlayerIndex.One);

            CurrentKeyboardState = Keyboard.GetState();
            CurrentTouchLocationState = TouchPanel.GetState();

            detectedGestures.Clear();

            if (TouchPanel.EnabledGestures != GestureType.None)
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gesture = TouchPanel.ReadGesture();
                    detectedGestures.Add(new GestureDefinition(gesture));
                }
            }
        }

        /// <summary>
        /// Ends the input update by storing the previous inputs.
        /// </summary>
        public static void EndUpdate()
        {
            PreviousButtonState = CurrentButtonState;

            PreviousKeyboardState = CurrentKeyboardState;
            PreviousTouchLocationState = CurrentTouchLocationState;
        }

        /// <summary>
        /// Adds a new button input.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="isReleasedPreviously">Whether the button must be released previously.</param>
        public void AddButtonInput(Buttons button, bool isReleasedPreviously)
        {
            if (buttonDefinedInputs.ContainsKey(button))
            {
                buttonDefinedInputs[button] = isReleasedPreviously;
                return;
            }
            buttonDefinedInputs.Add(button, isReleasedPreviously);
        }

        /// <summary>
        /// Adds a new keyboard input.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isReleasedPreviously">Whether the button must be released previously.</param>
        public void AddKeyboardInput(Keys key, bool isReleasedPreviously)
        {
            if (keyboardDefinedInputs.ContainsKey(key))
            {
                keyboardDefinedInputs[key] = isReleasedPreviously;
                return;
            }
            keyboardDefinedInputs.Add(key, isReleasedPreviously);
        }

        /// <summary>
        /// Adds a new touch tap input.
        /// </summary>
        /// <param name="touchArea">The touch area.</param>
        /// <param name="isReleasedPreviously">Whether the button must be released previously.</param>
        public void AddTouchTapInput(Rectangle touchArea, bool isReleasedPreviously)
        {
            if (touchTapDefinedInputs.ContainsKey(touchArea))
            {
                touchTapDefinedInputs[touchArea] = isReleasedPreviously;
                return;
            }
            touchTapDefinedInputs.Add(touchArea, isReleasedPreviously);
        }

        /// <summary>
        /// Adds a new swipe input.
        /// </summary>
        /// <param name="direction">The swipe directon.</param>
        /// <param name="slideDistance">The slide distance.</param>
        public void AddSwipeInput(InputDirection direction, float slideDistance)
        {
            if (swipeDefinedInputs.ContainsKey(direction))
            {
                swipeDefinedInputs[direction] = slideDistance;
                return;
            }
            swipeDefinedInputs.Add(direction, slideDistance);
        }

        /// <summary>
        /// Adds a new touch gesture input.
        /// </summary>
        /// <param name="gestureType">The gesture type.</param>
        /// <param name="touchArea">The touch area.</param>
        public void AddTouchGestureInput(GestureType gestureType, Rectangle touchArea)
        {
            // ensure the gesture is enabled
            TouchPanel.EnabledGestures = gestureType | TouchPanel.EnabledGestures;

            gestureDefinedInputs.Add(gestureDefinedInputs.Count, new GestureDefinition(gestureType, touchArea));

            if (gestureType == GestureType.Pinch)
            {
                isPinchGestureAvailable = true;
            }
        }

        /// <summary>
        /// Add an acceleromter input.
        /// </summary>
        /// <param name="direction">The tilt direction.</param>
        /// <param name="tiltThreshold">The tilt threshold.</param>
        /// <param name="filterType">The used filter type.</param>
        public void AddAccelerometerInput(InputDirection direction, float tiltThreshold, AccelerometerFilterType filterType)
        {
            if (!accelerometer.Active)
            {
                accelerometer.Active = true;
            }

            accelerometerDefinedInputs.Add(
                direction,
                new KeyValuePair<float, AccelerometerFilterType>(tiltThreshold, filterType));
        }

        /// <summary>
        /// Removes the accelerometer input.
        /// </summary>
        public void RemoveAccelerometerInput()
        {
            if (accelerometer.Active)
            {
                accelerometer.Active = false;
            }

            accelerometerDefinedInputs.Clear();
        }

        /// <summary>
        /// Checks wheter an input is available.
        /// </summary>
        /// <returns>TRUE, if one of the defined inputs is pressed.</returns>
        public bool IsPressed()
        {
            return IsPressed(null);
        }

        /// <summary>
        /// Checks wheter an input is available.
        /// </summary>
        /// <param name="newGestureDetectionArea">The new gesture detection area.</param>
        /// <returns>TRUE, if one of the defined inputs is pressed.</returns>
        public bool IsPressed(Rectangle? newGestureDetectionArea)
        {
            if (IsKeyboardInputPressed())
            {
                return true;
            }

            if (IsButtonInputPressed())
            {
                return true;
            }

            if (IsTouchTapInputPressed())
            {
                return true;
            }

            if (IsSwipeInputPressed())
            {
                return true;
            }

            if (IsTouchGestureInputPressed(newGestureDetectionArea))
            {
                return true;
            }

            if (IsAccelerometerInputPressed())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the current gesture position.
        /// </summary>
        /// <returns>The current gesture position.</returns>
        public Vector2 CurrentGesturePosition()
        {
            if (currentGestureDefinition == null)
            {
                return Vector2.Zero;
            }

            return currentGestureDefinition.Position;
        }

        /// <summary>
        /// Gets the current second gesture position.
        /// </summary>
        /// <returns>The current second gesture position.</returns>
        public Vector2 CurrentGesturePosition2()
        {
            if (currentGestureDefinition == null)
            {
                return Vector2.Zero;
            }

            return currentGestureDefinition.Position2;
        }

        /// <summary>
        /// Gets the current gesture delta.
        /// </summary>
        /// <returns>The current gesture delta.</returns>
        public Vector2 CurrentGestureDelta()
        {
            if (currentGestureDefinition == null)
            {
                return Vector2.Zero;
            }

            return currentGestureDefinition.Delta;
        }

        /// <summary>
        /// Gets the current second gesture delta.
        /// </summary>
        /// <returns>The current second gesture delta.</returns>
        public Vector2 CurrentGestureDelta2()
        {
            if (currentGestureDefinition == null)
            {
                return Vector2.Zero;
            }

            return currentGestureDefinition.Delta2;
        }

        /// <summary>
        /// Get the touch point for the current location. This doesn't use any
        /// of the gesture information, but the actual touch point on the screen
        /// </summary>
        /// <returns>The current touch position or NULL.</returns>
        public Vector2? CurrentTouchPosition()
        {
            foreach (TouchLocation location in CurrentTouchLocationState)
            {
                switch (location.State)
                {
                    case TouchLocationState.Pressed:
                    case TouchLocationState.Moved:
                        return location.Position;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the touch point for the previous location. This doesn't use any
        /// of the gesture information, but the actual touch point on the screen
        /// </summary>
        /// <returns>The previous touch position or NULL.</returns>
        public Vector2? PreviousTouchPosition()
        {
            foreach (TouchLocation location in PreviousTouchLocationState)
            {
                switch (location.State)
                {
                    case TouchLocationState.Pressed:
                    case TouchLocationState.Moved:
                        return location.Position;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the current accelerometer reading.
        /// </summary>
        /// <param name="filterType">The used filter type.</param>
        /// <returns>The current accelerometer reading.</returns>
        public static Vector3 CurrentAccelerometerReading(AccelerometerFilterType filterType)
        {
            switch (filterType)
            {
                case AccelerometerFilterType.Average:
                    return currentAverageAccelerometerReading;
                case AccelerometerFilterType.OptimalyFiltered:
                    return currentOptimalyFilteredAccelerometerReading;
                case AccelerometerFilterType.LowPassFiltered:
                    return currentLowPassFilteredAccelerometerReading;
                default:
                    return currentRawAccelerometerReading;
            }
        }

        /// <summary>
        /// The accelerometer sensor value changed handler.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The accelerometer reading event args.</param>
        private static void accelerometerCurrentValueChanged(object sender, AccelerometerHelperReadingEventArgs e)
        {
            currentRawAccelerometerReading = e.RawAcceleration;
            currentAverageAccelerometerReading = e.AverageAcceleration;
            currentLowPassFilteredAccelerometerReading = e.LowPassFilteredAcceleration;
            currentOptimalyFilteredAccelerometerReading = e.OptimalyFilteredAcceleration;
        }

        /// <summary>
        /// Checks whether a defined key input was pressed.
        /// </summary>
        /// <returns>TRUE, if a defined key was pressed.</returns>
        private bool IsKeyboardInputPressed()
        {
            foreach (Keys key in keyboardDefinedInputs.Keys)
            {
                if (keyboardDefinedInputs[key]
                    && CurrentKeyboardState.IsKeyDown(key)
                    && !PreviousKeyboardState.IsKeyDown(key))
                {
                    return true;
                }
                else if (!keyboardDefinedInputs[key]
                         && CurrentKeyboardState.IsKeyDown(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether a defined button input was pressed.
        /// </summary>
        /// <returns>TRUE, if a defined button was pressed.</returns>
        private bool IsButtonInputPressed()
        {
            foreach (Buttons button in buttonDefinedInputs.Keys)
            {
                if (buttonDefinedInputs[button]
                    && CurrentButtonState.IsButtonDown(button)
                    && !PreviousButtonState.IsButtonDown(button))
                {
                    return true;
                }
                else if (!buttonDefinedInputs[button]
                         && CurrentButtonState.IsButtonDown(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether a defined key input was pressed.
        /// </summary>
        /// <returns>TRUE, if a defined touch tap was detected.</returns>
        private bool IsTouchTapInputPressed()
        {
            foreach (Rectangle touchArea in touchTapDefinedInputs.Keys)
            {
                if (touchTapDefinedInputs[touchArea]
                    && touchArea.Intersects(CurrentTouchRectangle)
                    && PreviousTouchPosition() == null)
                {
                    return true;
                }
                else if (!touchTapDefinedInputs[touchArea]
                         && touchArea.Intersects(CurrentTouchRectangle))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether a defined swipe input was pressed.
        /// </summary>
        /// <returns>TRUE, if a defined swipe gesture was detected.</returns>
        private bool IsSwipeInputPressed()
        {
            foreach (InputDirection slideDirection in swipeDefinedInputs.Keys)
            {
                if (CurrentTouchPosition() != null
                    && PreviousTouchPosition() != null)
                {
                    switch (slideDirection)
                    {
                        case InputDirection.Up:
                            if (CurrentTouchPosition().Value.Y
                                + swipeDefinedInputs[slideDirection] < PreviousTouchPosition().Value.Y)
                            {
                                return true;
                            }
                            break;
                        case InputDirection.Down:
                            if (CurrentTouchPosition().Value.Y
                                - swipeDefinedInputs[slideDirection] > PreviousTouchPosition().Value.Y)
                            {
                                return true;
                            }
                            break;
                        case InputDirection.Left:
                            if (CurrentTouchPosition().Value.X
                                + swipeDefinedInputs[slideDirection] < PreviousTouchPosition().Value.X)
                            {
                                return true;
                            }
                            break;
                        case InputDirection.Right:
                            if (CurrentTouchPosition().Value.X
                                - swipeDefinedInputs[slideDirection] > PreviousTouchPosition().Value.X)
                            {
                                return true;
                            }
                            break;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether a defined touch gesture input was pressed.
        /// </summary>
        /// <param name="newGestureDetectionArea">The new gesture detection area.</param>
        /// <returns></returns>
        private bool IsTouchGestureInputPressed(Rectangle? newGestureDetectionArea)
        {
            // Clear the current gesture eacht to that there is always the most recent stored
            currentGestureDefinition = null;

            // If no gestures were detected, just exit
            if (detectedGestures.Count == 0)
            {
                return false;
            }

            // Check to see if any of the gestures have been fired
            foreach (GestureDefinition userDefinedGesture in gestureDefinedInputs.Values)
            {
                foreach (GestureDefinition detectedGesture in detectedGestures)
                {
                    if (detectedGesture.Type == userDefinedGesture.Type)
                    {
                        // If a rectangle area to check against has been bassed in, use that one.
                        // Otherwise, use the one the Input was originally set up with
                        Rectangle areaToCheck = userDefinedGesture.GestureArea;

                        if (newGestureDetectionArea != null)
                        {
                            areaToCheck = newGestureDetectionArea.Value;
                        }

                        // If the gesture detected was made in the area where user was interested in Input,
                        // then a gesture input is considered detected
                        if (detectedGesture.GestureArea.Intersects(areaToCheck))
                        {
                            if (currentGestureDefinition == null)
                            {
                                currentGestureDefinition = new GestureDefinition(detectedGesture.Gesture);
                            }
                            else
                            {
                                // Gestures like freeDrag or Flick are registered many times in a single update frame.
                                // Because we have only one variable stored, add all additional gesture values. So we
                                // have a composite of all the gesture information in currentGesture
                                currentGestureDefinition.Delta += detectedGesture.Delta;
                                currentGestureDefinition.Delta2 += detectedGesture.Delta2;
                                currentGestureDefinition.Position += detectedGesture.Position;
                                currentGestureDefinition.Position2 += detectedGesture.Position2;
                            }
                        }
                    }
                }
            }

            if (currentGestureDefinition != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether a defined accelerometer input was pressed.
        /// </summary>
        /// <returns>TRUE, if a defined direction was detected.</returns>
        private bool IsAccelerometerInputPressed()
        {
            foreach (KeyValuePair<InputDirection, KeyValuePair<float, AccelerometerFilterType>> input in accelerometerDefinedInputs)
            {
                float tiltThreshold = input.Value.Key;
                Vector3 currentAccelerometerReading = CurrentAccelerometerReading(input.Value.Value);

                switch (input.Key)
                {
                    case InputDirection.Up:
                        if (Math.Abs(currentAccelerometerReading.Y) > tiltThreshold
                            && currentAccelerometerReading.Y < 0)
                        {
                            return true;
                        }
                        break;
                    case InputDirection.Down:
                        if (Math.Abs(currentAccelerometerReading.Y) > tiltThreshold
                            && currentAccelerometerReading.Y > 0)
                        {
                            return true;
                        }
                        break;
                    case InputDirection.Left:
                        if (Math.Abs(currentAccelerometerReading.X) > tiltThreshold
                            && currentAccelerometerReading.X < 0)
                        {
                            return true;
                        }
                        break;
                    case InputDirection.Right:
                        if (Math.Abs(currentAccelerometerReading.X) > tiltThreshold
                            && currentAccelerometerReading.X > 0)
                        {
                            return true;
                        }
                        break;
                }
            }

            return false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether a pinch gesture is available or not.
        /// </summary>
        public bool IsPinchGestureAvailable
        {
            get
            {
                return this.isPinchGestureAvailable;
            }
        }

        /// <summary>
        /// Gets the accelerometer.
        /// </summary>
        public static AccelerometerHelper Accelerometer
        {
            get
            {
                return accelerometer;
            }
        }

        /// <summary>
        /// Gets the current touch rectangle sized 10x10 pixels.
        /// </summary>
        private Rectangle CurrentTouchRectangle
        {
            get
            {
                Vector2? touchPosition = CurrentTouchPosition();

                if (touchPosition == null)
                {
                    return Rectangle.Empty;
                }

                return new Rectangle((int)touchPosition.Value.X - 5,
                                     (int)touchPosition.Value.Y - 5,
                                     10,
                                     10);
            }
        }

        #endregion
    }
}
