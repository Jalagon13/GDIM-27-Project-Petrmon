using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectPetrmon
{
    public class AudioManager : Singleton<AudioManager>
    {
        private readonly static int _audioSourceNum = 10;
        private Queue<AudioSource> _audioSources = new Queue<AudioSource>();

        private void OnEnable()
        {
            if (transform.childCount > 0) return;

            for (int i = 0; i < _audioSourceNum; i++)
            {
                var audioSourceGameObject = new GameObject("AudioSource " + i);
                audioSourceGameObject.transform.SetParent(transform);
                var audioSource = audioSourceGameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                _audioSources.Enqueue(audioSource);
            }
        }

        public void PlayClip(AudioClip clip, bool looping, bool randPitch, float volume = 0.5f, float pitch = 1f)
        {
            if (clip == null) throw new Exception("Cannot PLAY " + clip.name + " because it's null");

            foreach (Transform audioSource in transform)
            {
                var source = audioSource.GetComponent<AudioSource>();

                if (source.clip == null)
                {
                    source.clip = clip;
                    AudioHandle(looping, randPitch, source, clip, volume, pitch);
                    return;
                }
                else if (source.clip == clip)
                {
                    AudioHandle(looping, randPitch, source, clip, volume, pitch);
                    return;
                }
            }

            throw new Exception("Cannot PLAY " + clip.name + " because there are no available Audio Sources to play from");
        }

        public void StopClip(AudioClip clipToStop)
        {
            if (clipToStop == null) return;

            foreach (Transform audioSource in transform)
            {
                var source = audioSource.GetComponent<AudioSource>();

                if (source.clip == clipToStop)
                {
                    if (source.isPlaying)
                        source.Stop();
                    source.clip = null;
                    return;
                }
            }
        }

        public void StopAllClips()
        {
            foreach (Transform audioSource in transform)
            {
                var source = audioSource.GetComponent<AudioSource>();
                if (source.isPlaying) source.Stop();
            }
        }

        public void UpdateClipVolume(AudioClip clipToModify, float volume)
        {
            if (clipToModify == null) return;

            foreach (Transform audioSource in transform)
            {
                var source = audioSource.GetComponent<AudioSource>();

                if (source.clip == clipToModify)
                {
                    source.volume = volume;
                    return;
                }
            }
        }

        public void UpdateAllLoopingClipVolume(float volume)
        {
            foreach (Transform audioSource in transform)
            {
                var src = audioSource.GetComponent<AudioSource>();
                // Looping clips are bgm, which should be updated
                if (src.loop) src.volume = volume;
            }
        }

        private void AudioHandle(bool looping, bool randPitch, AudioSource source, AudioClip clip, float volume, float pitch)
        {
            pitch = randPitch ? Random.Range(pitch - 0.2f, pitch + 0.7f) : pitch;

            if (looping)
            {
                source.volume = Mathf.Min(1, volume);
                source.pitch = Mathf.Min(3, pitch);
                source.loop = looping;
                if (!source.isPlaying)
                    source.Play();
            }
            else
            {
                StartCoroutine(ClipHandle(source, clip, source.clip.length, pitch, volume));
            }
        }

        private IEnumerator ClipHandle(AudioSource source, AudioClip clip, float clipLength, float pitch, float volume)
        {
            source.pitch = Mathf.Min(3, pitch);
            source.PlayOneShot(clip, Mathf.Min(1, volume));
            yield return new WaitForSeconds(clipLength);
            source.clip = null;
        }
    }
}
