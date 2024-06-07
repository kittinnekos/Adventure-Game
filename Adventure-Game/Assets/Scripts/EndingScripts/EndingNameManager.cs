using System.Collections;
using System.Collections.Generic;
using AdventureGame;
using UnityEngine;
using UnityEngine.UI;

public class EndingNameManager : MonoBehaviour
{
    [SerializeField] Text endingNametext;

    [System.Serializable]
    public class EndNameData
    {
        public int endNum;
        public string endName;
    }

    [SerializeField] EndNameData[] endNameDatas;

    private Dictionary<int, string> endNameDictionary = new Dictionary<int, string>();
    void Awake()
    {
        foreach(EndNameData endNameData in endNameDatas)
        {
            endNameDictionary.Add(endNameData.endNum, endNameData.endName);
        }
    }

    void Start()
    {
        endingNametext.text = endNameDictionary[MasterData.Instance.EndingNumber];
    }
}
