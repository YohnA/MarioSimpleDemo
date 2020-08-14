using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonTool;

namespace MarioSimpleDemo
{
    public class AudioController : Singleton<AudioController>
    {
        private float bgmVolume = 0.1f;
        private float fxVolume = 1f;

        private AudioClip bgmClip;
        private AudioSource bgmSource;

        private List<AudioSource> usingFxSources = new List<AudioSource>();
        private Queue<AudioSource> usableFxSources = new Queue<AudioSource>();

        private AudioClips clips;

        private void Awake()
        {
            this.gameObject.AddComponent<AudioListener>();
            bgmSource = this.gameObject.AddComponent<AudioSource>();

            clips = ScriptableObjectUtil.GetScriptableObject<AudioClips>();
        }

        /// <summary>
        /// 播放背景音效
        /// </summary>
        public void PlayBgm(string name,bool isLoop = true)
        {
            AudioClip clip = clips.GetClip(name);
            if (clip == null)
            {
                bgmSource.Stop();
                return;
            }

            bgmSource.loop = isLoop;
            bgmSource.volume = bgmVolume;
            //判断背景音乐有没有改变 没有则继续播放(不用从头开始播放)
            if (clip == bgmClip)
                return;
            
            bgmClip = clip;
            bgmSource.clip = clip;
            bgmSource.Play();
        }

        /// <summary>
        /// 获取AudioSource
        /// </summary>
        private AudioSource GetAudioSource()
        {
            if (usableFxSources != null && usableFxSources.Count > 0)
                return usableFxSources.Dequeue();
            else
                return this.gameObject.AddComponent<AudioSource>();
        }

        /// <summary>
        /// 播放道具特定音效
        /// </summary>
        public void PlayFx(string name)
        {
            AudioClip clip = clips.GetClip(name);
            if (clip == null) return;

            AudioSource source = GetAudioSource();
            source.clip = clip;
            source.volume = fxVolume;
            source.loop = false;
            source.Play();
            
            StartCoroutine(CleanFxSource(source));
        }

        /// <summary>
        /// 协程处理使用完的audiosource，使其可重复利用
        /// </summary>
        private IEnumerator CleanFxSource(AudioSource source)
        {
            usingFxSources.Add(source);
            while (source.isPlaying)
            {
                yield return 0;
            }

            source.Stop();
            source.clip = null;
            usingFxSources.Remove(source);
            usableFxSources.Enqueue(source);
        }
    }
}