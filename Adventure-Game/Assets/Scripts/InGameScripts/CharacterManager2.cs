using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    public class CharacterManager2 : MonoBehaviour
    {
        // キャラの立ち絵表示位置とナンバーを格納するクラス
        [System.Serializable]
        public class CharSpawnPos
        {
            public string spawnPosNum;
            public GameObject spawnPosObj;
        }

        // キャラの名前とプレハブを格納するクラス
        [System.Serializable]
        public class CharaData
        {
            public string charName;
            public GameObject charprefab;
        }

        [System.NonSerialized] public Dictionary<string, GameObject> NumToGameObject;
        [System.NonSerialized] public Dictionary<string, GameObject> NameToPrefab;

        public CharSpawnPos[] charSpawnPos = new CharSpawnPos[0];
        public CharaData[] charaData = new CharaData[0];

        void Awake()
        {

        }

        void Update()
        {
            
        }
    }
}
