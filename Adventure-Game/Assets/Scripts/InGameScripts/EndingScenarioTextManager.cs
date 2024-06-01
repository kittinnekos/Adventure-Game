using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace AdventureGame
{
    public class EndingScenarioTextManager : MonoBehaviour
    {
        // エンディングセレクトの時のみこの機能を使用する
        [SerializeField] List<TextAsset> endScenarios;
        // [SerializeField] TextAsset EndScenario_2;
        // [SerializeField] TextAsset EndScenario_3;
        // [SerializeField] TextAsset EndScenario_4;
        // [SerializeField] TextAsset EndScenario_5;
        // [SerializeField] TextAsset EndScenario_6;

        [System.NonSerialized] public Dictionary<int, List<string>> textToSentencesList;

        private List<List<string>> sentences = new List<List<string>>();
        // private List<string> sentence2;
        // private List<string> sentence3;
        // private List<string> sentence4;
        // private List<string> sentence5;
        // private List<string> sentence6;
        void Awake()
        {
            foreach(TextAsset endScenario in endScenarios)
            {
                StringReader reader = new StringReader(endScenario.text);
                List<string> sentence = new List<string>();
                while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
                {
                    string line = reader.ReadLine(); // 変数に一行ずつ格納している
                    sentence.Add(line);
                }
                sentences.Add(sentence);
            }

            textToSentencesList = new Dictionary<int, List<string>>();
            textToSentencesList.Add(1, sentences[0]);
            textToSentencesList.Add(2, sentences[1]);
            textToSentencesList.Add(3, sentences[2]);
            textToSentencesList.Add(4, sentences[3]);
            textToSentencesList.Add(5, sentences[4]);
            textToSentencesList.Add(6, sentences[5]);
        }

        public string GetCurrentEndSentence()
        {
            if(GameManager.Instance.selectTextLineNumber >= textToSentencesList[MasterData.Instance.EndingNumber].Count)
            {
                GameManager.Instance.changeEndingSceneManager.ChangeEndingScene();
                return " ";
            }
            // [キー（エンドナンバー）][ラインナンバー]
            return textToSentencesList[MasterData.Instance.EndingNumber][GameManager.Instance.selectTextLineNumber];
        }
        public int GetMaxEndSentence()
        {
            return textToSentencesList[MasterData.Instance.EndingNumber].Count;
        }
    }
}
