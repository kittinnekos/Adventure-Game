using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip audioClip;
    }

    [SerializeField] SoundData[] soundDataSE;

    // 同時に鳴らしたい音の数だけ配列の要素数を増やす
    private AudioSource[] audioSourceSEList = new AudioSource[1];

    // TODO クリック音をスタートボタンに適用させる
    void Awake()
    {
        // 配列の要素数だけ自分自身にオーディオソースを追加する
        for(int i = 0;i < audioSourceSEList.Length;i++)
        {
            audioSourceSEList[i] = gameObject.AddComponent<AudioSource>();
        }
    }
}
