using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AdventureGame
{
    public class UserScriptMainTextManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile;

        // 文章中の文（ここでは一行ごと）を入れておくためのリスト
        List<string> _sentences = new List<string>();

        [System.NonSerialized] public Coroutine coroutine;

        void Awake()
        {
            // テキストファイルの中身を、一行ずつリストに入れておく
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
            {
                string line = reader.ReadLine(); // 変数に一行ずつ格納している
                _sentences.Add(line);
            }
        }

        // 現在の行の文を取得する
        public string GetCurrentSentence()
        {
            // テキストの全ての行が現在の行以上になったら、半角スペースを返す
            if(_sentences.Count <= GameManager.Instance.lineNumber) return " ";
            
            return _sentences[GameManager.Instance.lineNumber];
        }

        // 文が命令かどうか
        public bool IsStatement(string sentence)
        {
            if(sentence[0] == '&')
            {
                return true;
            }
            return false;
        }

        // 命令を実行する
        public void ExecuteStatement(string sentence)
        {
            string[] words = sentence.Split(' ');
            switch(words[0])
            {
                case "&img":
                    GameManager.Instance.imageManager.PutImage(words[1], words[2]);
                    break;
                case "&rmimg":
                    GameManager.Instance.imageManager.RemoveImage(words[1]);
                    break;
                case "&name":
                    GameManager.Instance.speakerNameTextManager.DisplaySpeakerNameText(words[1]);
                    break;
                case "&end":
                    GameManager.Instance.changeEndingSceneManager.ChangeEndingScene();
                    break;
                case "&select":
                    GameManager.Instance.selectManager.SpawnSelectPrefab(words[1], words[2]);
                    // selectTextControllerのコルーチンを始める
                    coroutine = GameManager.Instance.userScriptSelectTextManager.StartCoroutine
                    (GameManager.Instance.selectTextController.ClickToNextLineCoroutine(words[2]));
                    break;
                case "&endselect":
                    GameManager.Instance.changeEndingSceneManager.Ending(); // 選択肢を元にエンディングナンバーを保管する
                    Debug.Log(MasterData.Instance.EndingNumber);//デバック
                    GameManager.Instance.selectManager.SpawnSelectPrefab("1", "select3");
                    StartCoroutine(GameManager.Instance.fadeIn_FadeOutManager.FadeOut_FadeIn());
                    // selectTextControllerのコルーチンを始める
                    coroutine = StartCoroutine(GameManager.Instance.selectTextController.ClickToNextLineCoroutine("select3", true));
                    break;
                case "&actchar":
                    if(words.Length == 4)
                    {
                        //GameManager.Instance.characterManager.ChangeCharacterImage(words[1], words[2], words[3]);
                        GameManager.Instance.characterManager.TestChangeCharacterImage(words[1], words[2], words[3]);
                    }
                    else
                    {
                        //GameManager.Instance.characterManager.ChangeCharacterImage(words[1], words[2]);
                        GameManager.Instance.characterManager.TestChangeCharacterImage(words[1], words[2]);
                    }
                    break;
                case "&nonactchar":
                    GameManager.Instance.characterManager.NonActiveCharacterImage(words[1]);
                    break;
                case "&changeBGM":
                    GameManager.Instance.soundManager.ChangeBGM(words[1]);
                    break;
                case "&SE":
                    GameManager.Instance.soundManager.PlaySE(words[1]);
                    break;
            }
        }
    }
}
