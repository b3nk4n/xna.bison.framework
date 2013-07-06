using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Audio
{
    /// <summary>
    /// The audio song class, which handles smooth song transitions.
    /// </summary>
    public class AudioSong
    {
        #region Members

        /// <summary>
        /// The song.
        /// </summary>
        private readonly Song song;

        /// <summary>
        /// The current audio volume factor, which will be smoothly 
        /// transformed to the value of preferredVolumeFactor (0.0 - 1.0).
        /// </summary>
        private float currentVolumeFactor;

        /// <summary>
        /// The preferred audio volume factor (0.0 - 1.0).
        /// </summary>
        private float preferredVolumeFactor;

        /// <summary>
        /// The audio transition speed in value change per second.
        /// </summary>
        private float audioTransitionSpeed;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new audio song instance.
        /// </summary>
        /// <param name="song">The audio song.</param>
        public AudioSong(Song song)
            :this(song, 0.33f)
        { }

        /// <summary>
        /// Creates a new audio song instance.
        /// </summary>
        /// <param name="song">The audio song.</param>
        /// <param name="audioTransitionSpeed">The audio transition speed.</param>
        public AudioSong(Song song, float audioTransitionSpeed)
        {
            this.song = song;
            this.audioTransitionSpeed = audioTransitionSpeed;
            this.Reset();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the preferred audio volume.
        /// </summary>
        public void Reset()
        {
            this.preferredVolumeFactor = 0.0f;
            this.currentVolumeFactor = 0.0f;
        }

        /// <summary>
        /// Start the volume transition to full volume.
        /// </summary>
        public void VolumeTransitionIn()
        {
            this.preferredVolumeFactor = 1.0f;
        }

        /// <summary>
        /// Starts the volume transition to no volume.
        /// </summary>
        public void VolumeTransitionOut()
        {
            this.preferredVolumeFactor = 0.0f;
        }

        /// <summary>
        /// Updates the audio song including its transitions.
        /// </summary>
        /// <param name="gameTime">THe game time since the last update.</param>
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (IsVolumeTransitionActive)
            {
                float diff = preferredVolumeFactor - currentVolumeFactor;
                float newTransitionChange = audioTransitionSpeed * elapsed;

                if (Math.Abs(diff) < newTransitionChange)
                {
                    currentVolumeFactor = preferredVolumeFactor;
                }
                else
                {
                    currentVolumeFactor += diff;
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether the music is currently off
        /// </summary>
        public bool IsVolumeTransitionActive
        {
            get
            {
                return this.currentVolumeFactor != this.preferredVolumeFactor;
            }
        }

        /// <summary>
        /// Gets the current audio volume factor of the volume transition.
        /// </summary>
        public float CurrentAudioVolumeFactor
        {
            get
            {
                return this.currentVolumeFactor;
            }
        }

        /// <summary>
        /// Gets the wrapped song.
        /// </summary>
        public Song Song
        {
            get
            {
                return this.song;
            }
        }

        #endregion
    }
}
