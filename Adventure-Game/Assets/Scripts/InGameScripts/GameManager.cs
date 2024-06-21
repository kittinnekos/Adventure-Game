using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    public class GameManager : MonoBehaviour
    {
        // 別のクラスからGameManagerの変数などを使えるようにするためのもの。（変更はできない）
        public static GameManager Instance { get; private set; }

        public UserScriptMainTextManager userScriptMainTextManager;
        public UserScriptSelectTextManager userScriptSelectTextManager;
        public MainTextController mainTextController;
        public SelectTextController selectTextController;
        public ImageManager imageManager;
        public SpeakerNameTextManager speakerNameTextManager;
        public ChangeEndingSceneManager changeEndingSceneManager;
        public FadeIn_FadeOutManager fadeIn_FadeOutManager;
        public EndingScenarioTextManager endingScenarioTextManager;
        public SelectManager selectManager;
        public CharacterManager characterManager;
        public SoundManager soundManager;

        // ユーザースクリプトの、今の行の数値。クリック（タップ）のたびに1ずつ増える
        [System.NonSerialized] public int lineNumber;

        // ユーザースクリプト選択テキストの、今の行の数値。クリック（タップ）のたびに1ずつ増える
        [System.NonSerialized] public int selectTextLineNumber;

        [System.NonSerialized] public bool isFadeIn;

        // エンディング用に選択肢を選んだナンバーを保管するリスト
        [System.NonSerialized] public List<int> pickSelectNumberList;

        void Awake()
        {
            // これで、別のクラスからGameManagerの変数などを使えるようになる。
            Instance = this;
            lineNumber = 0;
            selectTextLineNumber = 0;
            isFadeIn = false;
            pickSelectNumberList = new List<int> ();
        }
    }
}
