using UnityEngine;

namespace NoverGame
{
    public class GameManager : MonoBehaviour
    {
        // 別のクラスからGameManagerの変数などを使えるようにするためのもの。（変更はできない）
        public static GameManager Instance { get; private set; }

        public UserScriptManager userScriptManager;
        public MainTextController mainTextController;
        public ImageManager imageManager;

        // ユーザースクリプトの、今の行の数値。クリック（タップ）のたびに1ずつ増える
        [System.NonSerialized] public int lineNumber;

        void Awake()
        {
            // これで、別のクラスからGameManagerの変数などを使えるようになる。
            Instance = this;
            lineNumber = 0;
        }
    }
}
