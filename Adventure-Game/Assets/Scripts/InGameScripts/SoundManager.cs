using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    public class SoundManager : MonoBehaviour
    {
        // ディキショナリーのキーとバリューをインスペクター上で設定
        [System.Serializable]
        public class SoundData
        {
            public string name;
            public AudioClip audioClip;
        }

        [SerializeField] SoundData[] soundDataBGM;
        private AudioSource audioSourceBGM = new AudioSource();

        [SerializeField] SoundData[] soundDataSE;

        // 同時に鳴らしたい音の数だけ配列の要素数を増やす
        private AudioSource[] audioSourceSEList = new AudioSource[10];

        private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

        private float maxVolume = 0.4f;
        private float minVolume = 0;

        void Awake()
        {
            audioSourceBGM = gameObject.AddComponent<AudioSource>();
            audioSourceBGM.volume = maxVolume;

            // ディキショナリーに追加
            foreach(SoundData soundData in soundDataBGM)
            {
                soundDictionary.Add(soundData.name, soundData);
            }

            // 配列の要素数だけ自分自身にオーディオソースを追加する
            for(int i = 0;i < audioSourceSEList.Length;i++)
            {
                audioSourceSEList[i] = gameObject.AddComponent<AudioSource>();
                audioSourceSEList[i].volume = maxVolume;
            }

            // ディキショナリーに追加
            foreach(SoundData soundData in soundDataSE)
            {
                soundDictionary.Add(soundData.name, soundData);
            }
        }

        // TODO BGM関数群
        // 未使用のオーディオソースを取得　使用中の場合はnullを返却
        private AudioSource GetUnusedAudioSourceBGM()
        {
            if(audioSourceBGM.isPlaying == false) return audioSourceBGM;
            return null;
        }

        private void PlayBGM(AudioClip clip)
        {
            AudioSource audioSource = GetUnusedAudioSourceBGM();
            if(audioSource == null) return;
            audioSource.clip = clip;
            audioSource.Play();
        }

        // ディキショナリーのキーを指定して再生
        public void PlayBGM(string name)
        {
            if(soundDictionary.TryGetValue(name, out SoundData soundData)) // ディキショナリーからキーで検索
            {
                PlayBGM(soundData.audioClip);
            }
            else
            {
                Debug.LogWarning($"{name} is no Dictionary");
            }
        }
        public void ChangeBGM(string name)
        {
            if(soundDictionary.TryGetValue(name, out SoundData soundData)) // ディキショナリーからキーで検索
            {
                // BGMをフェードアウトさせ、BGMを切り替える
                StartCoroutine(ChangeBGM(soundData));
            }
            else
            {
                Debug.LogWarning($"{name} is no Dictionary");
            }
        }

        public void RePlayBGM()
        {
            StartCoroutine(FadeInBGM_Play());
        }

        public void StopBGM()
        {
            StartCoroutine(FadeOutBGM_Stop());
        }

        public void UnPauseBGM()
        {
            StartCoroutine(FadeInBGM_UnPause());
        }

        public void PauseBGM()
        {
            StartCoroutine(FadeOutBGM_Pause());
        }

        // SE関数群
        // 未使用のオーディオソースを取得　全て使用中の場合はnullを返却
        private AudioSource GetUnusedAudioSourceSE()
        {
            for(int i = 0;i < audioSourceSEList.Length;i++)
            {
                if(audioSourceSEList[i].isPlaying == false) return audioSourceSEList[i];
            }
            return null;
        }

        // オーディオクリップを指定して再生
        private void PlaySE(AudioClip clip)
        {
            AudioSource audioSource = GetUnusedAudioSourceSE();
            if(audioSource == null) return;
            audioSource.clip = clip;
            audioSource.Play();
        }

        // ディキショナリーのキーを指定して再生
        public void PlaySE(string name)
        {
            if(soundDictionary.TryGetValue(name, out SoundData soundData)) // ディキショナリーからキーで検索
            {
                PlaySE(soundData.audioClip);
            }
            else
            {
                Debug.LogWarning($"{name} is no Dictionary");
            }
        }

        // BGMを切り替えるコルーチン
        private IEnumerator ChangeBGM(SoundData soundData)
        {
            if(audioSourceBGM.isPlaying)
            {
                while(!Mathf.Approximately(audioSourceBGM.volume, minVolume))
                {
                    float changePerFrame = Time.deltaTime/0.5f;
                    audioSourceBGM.volume = Mathf.MoveTowards(audioSourceBGM.volume, minVolume, changePerFrame);
                    yield return null;
                }
                audioSourceBGM.Stop();
            }
            audioSourceBGM.clip = soundData.audioClip;
            StartCoroutine(FadeInBGM_Play());
        }

        // BGMをストップ・プレイするフェードイン、アウトコルーチン
        private IEnumerator FadeInBGM_Play()
        {
            audioSourceBGM.volume = 0f;
            audioSourceBGM.Play();
            while(!Mathf.Approximately(audioSourceBGM.volume, maxVolume))
            {
                float changePerFrame = Time.deltaTime/0.5f;
                audioSourceBGM.volume = Mathf.MoveTowards(audioSourceBGM.volume, maxVolume, changePerFrame);
                yield return null;
            }
        }
        private IEnumerator FadeOutBGM_Stop()
        {
            while(!Mathf.Approximately(audioSourceBGM.volume, minVolume))
            {
                float changePerFrame = Time.deltaTime/0.5f;
                audioSourceBGM.volume = Mathf.MoveTowards(audioSourceBGM.volume, minVolume, changePerFrame);
                yield return null;
            }
            audioSourceBGM.Stop();
        }

        // BGMをポーズ・アンポーズするフェードイン、アウトコルーチン
        public IEnumerator FadeOutBGM_Pause()
        {
            while(!Mathf.Approximately(audioSourceBGM.volume, minVolume))
            {
                float changePerFrame = Time.deltaTime/0.5f;
                audioSourceBGM.volume = Mathf.MoveTowards(audioSourceBGM.volume, minVolume, changePerFrame);
                yield return null;
            }
            audioSourceBGM.Pause();
        }

        public IEnumerator FadeInBGM_UnPause()
        {
            audioSourceBGM.volume = 0f;
            audioSourceBGM.UnPause();
            while(!Mathf.Approximately(audioSourceBGM.volume, maxVolume))
            {
                float changePerFrame = Time.deltaTime/0.5f;
                audioSourceBGM.volume = Mathf.MoveTowards(audioSourceBGM.volume, maxVolume, changePerFrame);
                yield return null;
            }
        }
    }
}
