using UnityEngine;

namespace AdventureGame
{
    public class MasterData : MonoBehaviour
    {
        public static MasterData Instance { get; private set;}
        [System.NonSerialized] public int EndingNumber;
        [System.NonSerialized] public bool[] storageEndingNumber = new bool[6]; // エンディングの保管変数
        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad (this.gameObject);
                EndingNumber = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
