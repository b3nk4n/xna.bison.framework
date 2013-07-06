using Bison.Framework.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Audio
{
    /// <summary>
    /// The audio manager class.
    /// </summary>
    public class AudioManager
    {
        #region Members

        /// <summary>
        /// The audio manager singleton instance.
        /// </summary>
        private static AudioManager instance;

        /// <summary>
        /// The managed audio effects.
        /// </summary>
        private Dictionary<string, AudioEffect> audioEffects = new Dictionary<string, AudioEffect>();

        /// <summary>
        /// The managed audio songs.
        /// </summary>
        private Dictionary<string, AudioSong> audioSongs = new Dictionary<string, AudioSong>();

        /// <summary>
        /// The currently active song.
        /// </summary>
        private AudioSong activeAudioSong;

        /// <summary>
        /// The next song to play.
        /// </summary>
        private AudioSong nextAudioSong;

        /// <summary>
        /// The games audio settings.
        /// </summary>
        private IAudioSettings audioSettings;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new audio manager instance.
        /// </summary>
        private AudioManager() { }

        #endregion

        #region Methods

        public void Initialize(IAudioSettings settings)
        {
            this.audioSettings = settings;
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Adds a new audio effect.
        /// </summary>
        /// <param name="key">The key of the audio effect.</param>
        /// <param name="audioEffect">The audio effect.</param>
        public void AddAudioEffect(string key, AudioEffect audioEffect)
        {
            if (audioEffects.ContainsKey(key))
            {
                throw new Exception("They given key is already in use: " + key);
            }

            audioEffects.Add(key, audioEffect);
        }

        /// <summary>
        /// Plays the audio effects sound with in settings defined volume.
        /// </summary>
        /// <param name="key">The key of the audio effect to play.</param>
        public void PlayAudioEffect(string key)
        {
            PlayAudioEffect(key, audioSettings.SoundEffectVolume);
        }

        /// <summary>
        /// Play the audio effect with the given volume.
        /// </summary>
        /// <param name="key">The key of the audio effect to play.</param>
        /// <param name="volume">The sound volume.</param>
        public void PlayAudioEffect(string key, float volume)
        {
            if (!audioEffects.ContainsKey(key))
            {
                throw new KeyNotFoundException("There is no audio effect for the key: " + key);
            }

            audioEffects[key].Play(volume);
        }

        /// <summary>
        /// Adds a new audio song.
        /// </summary>
        /// <param name="key">The key of the audio song.</param>
        /// <param name="audioEffect">The audio song.</param>
        public void AddAudioSong(string key, AudioSong audioSong)
        {
            if (audioSongs.ContainsKey(key))
            {
                throw new Exception("They given key is already in use:" + key);
            }

            audioSongs.Add(key, audioSong);
        }

        /// <summary>
        /// Play the audio song.
        /// </summary>
        /// <param name="key">The key of the audio song to play.</param>
        public void PlayAudioSong(string key)
        {
            if (!audioSongs.ContainsKey(key))
            {
                throw new KeyNotFoundException("There is no audio song for the key: " + key);
            }

            if (activeAudioSong == null)
            {
                activeAudioSong = audioSongs[key];
                activeAudioSong.VolumeTransitionIn();
            }
            else
            {
                nextAudioSong = audioSongs[key];
                activeAudioSong.VolumeTransitionOut();
            }
        }

        /// <summary>
        /// Updates all sound effects and songs.
        /// </summary>
        /// <param name="gameTime">The game time since the last update.</param>
        public void Update(GameTime gameTime)
        {
            // update all audio effects
            foreach (var audioEffect in audioEffects.Values)
            {
                audioEffect.Update(gameTime);
            }

            // update music song
            if (activeAudioSong != null)
            {
                // switch music song, when the transition is over
                if (nextAudioSong != null)
                {
                    if (!activeAudioSong.IsVolumeTransitionActive)
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                        {
                            MediaPlayer.Stop();
                        }
                        activeAudioSong = nextAudioSong;
                        activeAudioSong.Reset();
                        activeAudioSong.VolumeTransitionIn();
                        MediaPlayer.Play(activeAudioSong.Song);
                        nextAudioSong = null;
                    }
                }

                // adjust music player volume
                if (activeAudioSong.IsVolumeTransitionActive)
                {
                    // TODO: check if the frequent volume update causes performance problems
                    MediaPlayer.Volume = audioSettings.MusicVolume * activeAudioSong.CurrentAudioVolumeFactor;
                }

                activeAudioSong.Update(gameTime);
            }
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets the singleton instance of the audio manager.
        /// </summary>
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }

                return instance;
            }
        }

        #endregion
    }
}
