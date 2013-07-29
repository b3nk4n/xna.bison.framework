using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Audio
{
    /// <summary>
    /// The class representing an audio effect with different characteristics.
    /// </summary>
    public class AudioEffect
    {
        /// <summary>
        /// The different sound characteristics of the audio effect.
        /// </summary>
        private List<SoundEffect> soundEffects = new List<SoundEffect>();

        /// <summary>
        /// The used number generator to select a random sound effect.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// The minimum time between one effect to another.
        /// </summary>
        private readonly GameTicker minTimeBetweenSoundEffectTimer;

        /// <summary>
        /// Creates a new audio effect instance.
        /// </summary>
        /// <param name="minTimeBetweenSoundEffect">The minimum time between two of those audio effects.</param>
        public AudioEffect(float minTimeBetweenSoundEffect)
        {
            minTimeBetweenSoundEffectTimer = new GameTicker(minTimeBetweenSoundEffect);
        }

        /// <summary>
        /// Adds a new characteristic sound effect.
        /// </summary>
        /// <param name="soundEffect">The sound effect to add.</param>
        public void AddSoundEffect(SoundEffect soundEffect)
        {
            this.soundEffects.Add(soundEffect);
        }

        /// <summary>
        /// Plays one of the characteristic sound effects.
        /// </summary>
        /// <param name="volume">The sound volume.</param>
        public void Play(float volume)
        {
            if (soundEffects.Count == 0)
            {
                throw new Exception("No sound effect is available");
            }
            if (minTimeBetweenSoundEffectTimer.Elapsed)
            {
                int index = random.Next(0, soundEffects.Count - 1);
                SoundEffectInstance soundInstance = soundEffects[index].CreateInstance();
                soundInstance.Volume = volume;
                soundInstance.Play();

                minTimeBetweenSoundEffectTimer.Reset();
            }
        }

        /// <summary>
        /// Updates the audio effect and its internal timers.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public void Update(GameTime gameTime)
        {
            minTimeBetweenSoundEffectTimer.Update(gameTime);
        }
    }
}
