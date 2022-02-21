using UnityEngine;


    public class SoundMaster
    {
        /**
         * Simple wrapper for a global Audio Source
         */
        AudioSource Source { get; }
        public float defaultVolume { get; set; } = 0.78f;

        public SoundMaster(AudioSource source)
        {
            this.Source = source;
        }

        public void Play(AudioClip clip)
        {
            Play(clip, defaultVolume);
        }

        public void Play(AudioClip clip, float vol)
        {
            if (clip == null)
                Debug.LogWarning("Sound clip not found");
            else
                Source?.PlayOneShot(clip, vol);
        }
    }
