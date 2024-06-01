using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AdventureGame
{
    public class UserScriptSelectTextManager : MonoBehaviour
    {
        // UserScriptManagerを真似て汎用性の高いスクリプトを作成する
        // TextAsset配列には各選択肢ごとに表示させるシナリオテキストを格納する。
        [SerializeField] TextAsset[] selectTextFile0;
        [SerializeField] TextAsset[] selectTextFile1;
        [SerializeField] TextAsset[] selectTextFile2;
        [SerializeField] TextAsset[] selectTextFile3;

        [System.NonSerialized] public Dictionary<string, List<List<string>>> textToSentencesList;

        // 一行の文が入る二次元リスト
        List<List<string>> sentences0 = new List<List<string>>();
        List<List<string>> sentences1 = new List<List<string>>();
        List<List<string>> sentences2 = new List<List<string>>();
        List<List<string>> sentences3 = new List<List<string>>();

        void Awake()
        {
            // 選択肢０の格納処理
            for(int i = 0;i < selectTextFile0.Length;i++)
            {
                StringReader reader = new StringReader(selectTextFile0[i].text);
                List<string> sentence = new List<string>();
                while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
                {
                    string line = reader.ReadLine(); // 変数に一行ずつ格納している
                    sentence.Add(line);
                }
                sentences0.Add(sentence);
            }

            // 選択肢１の格納処理
            for(int i = 0;i < selectTextFile1.Length;i++)
            {
                StringReader reader = new StringReader(selectTextFile1[i].text);
                List<string> sentence = new List<string>();
                while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
                {
                    string line = reader.ReadLine(); // 変数に一行ずつ格納している
                    sentence.Add(line);
                }
                sentences1.Add(sentence);
            }

            // 選択肢２の格納処理
            for(int i = 0;i < selectTextFile2.Length;i++)
            {
                StringReader reader = new StringReader(selectTextFile2[i].text);
                List<string> sentence = new List<string>();
                while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
                {
                    string line = reader.ReadLine(); // 変数に一行ずつ格納している
                    sentence.Add(line);
                }
                sentences2.Add(sentence);
            }

            // 選択肢３の格納処理
            for(int i = 0;i < selectTextFile3.Length;i++)
            {
                StringReader reader = new StringReader(selectTextFile3[i].text);
                List<string> sentence = new List<string>();
                while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
                {
                    string line = reader.ReadLine(); // 変数に一行ずつ格納している
                    sentence.Add(line);
                }
                sentences3.Add(sentence);
            }

            textToSentencesList = new Dictionary<string, List<List<string>>>();
            textToSentencesList.Add("select0", sentences0);
            textToSentencesList.Add("select1", sentences1);
            textToSentencesList.Add("select2", sentences2);
            textToSentencesList.Add("select3", sentences3);
        }

        // 現在の行の文を取得する
        public string GetCurrentSentence(string textName)
        {
            int PSNLCount = GameManager.Instance.pickSelectNumberList.Count-1;// 選択肢を選んだナンバーを保管するリストの要素数-1
            // 選択肢シナリオテキストの現在の行が、選んだ選択肢テキストの最大行以上になったら空白を返す
            // つまりシナリオテキストを読み切っていたら空白を返す
            if(GameManager.Instance.selectTextLineNumber >= GameManager.Instance.userScriptSelectTextManager.textToSentencesList[textName][GameManager.Instance.pickSelectNumberList[PSNLCount]].Count)
            {
                return " ";
            }
            // [ディキショナリーのキー][選択肢を選んだナンバーを保管するリストの末尾][選択先テキストのラインナンバー]
            return textToSentencesList[textName][GameManager.Instance.pickSelectNumberList[PSNLCount]][GameManager.Instance.selectTextLineNumber];
        }

        public void ResetSelectTextLineNumber()
        {
            GameManager.Instance.selectTextLineNumber = 0;
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
                    break;
                case "&actchar":
                    if(words.Length == 4)
                    {
                        GameManager.Instance.characterManager.ChangeCharacterImage(words[1], words[2], words[3]);
                    }
                    else GameManager.Instance.characterManager.ChangeCharacterImage(words[1], words[2]);
                    break;
                case "&nonactchar":
                    GameManager.Instance.characterManager.NonActiveCharacterImage(words[1]);
                    break;
            }
        }
    }
}
