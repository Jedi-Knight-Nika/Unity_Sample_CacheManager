using System.Collections;
using UnityEngine;

namespace Nikolla_L
{
    public class AudioManager : MonoBehaviour
    {
        private bool _audioPlaying = false;
        public static bool AudioEnabled { get; set; }

        /// <summary>
        /// Enable all audio sources
        /// </summary>
        public void TurnOn()
        {
            AudioEnabled = true;
        }

        /// <summary>
        /// Disable all audio sources
        /// </summary>
        public void TurnOff()
        {
            AudioEnabled = false;
        }

        /// <summary>
        /// Plays a given audio source
        /// </summary>
        /// <param name="audioSource">AudioSource</param>
        /// <param name="prioritize">If true, other sounds won't be played until this one finishes</param>
        public void PlayAudio(AudioSource audioSource, bool prioritize = false)
        {
            if (AudioEnabled && !_audioPlaying)
            {
                if (prioritize)
                {
                    _audioPlaying = true;
                    audioSource.PlayOneShot(audioSource.clip);
                    StartCoroutine(ResetAudioPlaying(audioSource));
                }
                else
                {
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }

        /// <summary>
        /// Coroutine to reset _audioPlaying after an audio clip has finished playing
        /// </summary>
        /// <param name="audioSource">The AudioSource that was played</param>
        /// <returns></returns>
        private IEnumerator ResetAudioPlaying(AudioSource audioSource)
        {
            if (audioSource != null && audioSource.clip != null)
            {
                yield return new WaitForSeconds(audioSource.clip.length);
                _audioPlaying = false;
            }
        }

        /// <summary>
        /// Plays an audio source after a delay
        /// </summary>
        /// <param name="audioSource">AudioSource to be played</param>
        /// <param name="delay">Time delay before playing the audio</param>
        public void PlayAudioWithDelay(AudioSource audioSource, float delay)
        {
            StartCoroutine(PlayAudioDelayedCoroutine(audioSource, delay));
        }

        /// <summary>
        /// Coroutine to play an audio source after a delay
        /// </summary>
        /// <param name="audioSource">AudioSource to be played</param>
        /// <param name="delay">Time delay before playing the audio</param>
        /// <returns></returns>
        private IEnumerator PlayAudioDelayedCoroutine(AudioSource audioSource, float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayAudio(audioSource);
        }

        /// <summary>
        /// Invoke a given action after an optional delay
        /// </summary>
        private IEnumerator InvokeFunction(System.Action action, float? delay)
        {
            if (delay.HasValue)
            {
                yield return new WaitForSeconds(delay.Value);
            }
            action.Invoke();
        }

        // audio event functions
        // TODO: needs implement
    }
}
