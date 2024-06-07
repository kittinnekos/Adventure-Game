using System.Collections;
using System.Collections.Generic;
using AdventureGame;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Image backGroundImage;

    [System.Serializable]
    public class EndSpriteData
    {
        public int endNum;
        public Sprite sprite;
    }
    [SerializeField] EndSpriteData[] endSpriteDatas;

    private Dictionary<int, Sprite> endSpriteDictionary = new Dictionary<int, Sprite>();

    void Awake()
    {
        foreach(EndSpriteData endSpriteData in endSpriteDatas)
        {
            endSpriteDictionary.Add(endSpriteData.endNum, endSpriteData.sprite);
        }
    }

    void Start()
    {
        backGroundImage.sprite = endSpriteDictionary[MasterData.Instance.EndingNumber];
    }
}
