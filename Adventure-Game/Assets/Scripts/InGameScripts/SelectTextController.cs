using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class SelectTextController : MonoBehaviour
    {
        // MainTextControllerを真似て汎用性の高いスクリプトを作成する
        [SerializeField] Text _mainTextObject;
        int _displayedSentenceLength;
        int _sentenceLength;
        float _time;
        float _feedTime;
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;
        }

        public bool CanGoToTheNextLine(string textName)
        {
            string sentence = GameManager.Instance.userScriptSelectTextManager.GetCurrentSentence(textName);
            _sentenceLength = sentence.Length;
            return (_displayedSentenceLength > sentence.Length);
        }

        // 次の行へ移動
        public void GoToTheNextLine(string textName)
        {
            _displayedSentenceLength = 0;
            _time = 0f;
            GameManager.Instance.selectTextLineNumber++;
            string sentence = GameManager.Instance.userScriptSelectTextManager.GetCurrentSentence(textName);
            if(GameManager.Instance.userScriptSelectTextManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptSelectTextManager.ExecuteStatement(sentence);
                GoToTheNextLine(textName);
            }
        }

        public void DisplayText(string textName)
        {
            string sentence = GameManager.Instance.userScriptSelectTextManager.GetCurrentSentence(textName);
            string clampedSentence = sentence.Substring(0, Mathf.Min(sentence.Length, _displayedSentenceLength));
            _mainTextObject.text = clampedSentence;
        }

        public IEnumerator ClickToNextLineCoroutine(string textName)
        {
            GameManager.Instance.userScriptSelectTextManager.ResetSelectTextLineNumber();
            bool isAllTextRunning = true;
            bool isFirstprocessing = true;

            while(isAllTextRunning) // シナリオテキストを読み切ったら抜ける
            {
                bool isSpawnSelectPrefab = GameManager.Instance.selectManager.IsSpawnSelectPrefab();
                if(!isSpawnSelectPrefab)
                {
                    int PSNLCount = GameManager.Instance.pickSelectNumberList.Count-1;

                    if(isFirstprocessing) // 最初の行のテキストを表示、または命令を実行
                    {
                        string statement = GameManager.Instance.userScriptSelectTextManager.GetCurrentSentence(textName);
                        if(GameManager.Instance.userScriptSelectTextManager.IsStatement(statement))
                        {
                            GameManager.Instance.userScriptSelectTextManager.ExecuteStatement(statement);
                            GoToTheNextLine(textName);
                        }
                        DisplayText(textName);
                        isFirstprocessing = false;
                    }

                    // 選択肢シナリオテキストの現在の行が、選んだ選択肢テキストの最大行以上になったらwhile文を抜ける
                    if(GameManager.Instance.selectTextLineNumber >= GameManager.Instance.userScriptSelectTextManager.textToSentencesList
                                                                    [textName][GameManager.Instance.pickSelectNumberList[PSNLCount]].Count)
                    {
                        isAllTextRunning = false;
                        continue;
                    }

                    // 文章を一文字ずつ表示する
                    _time += Time.deltaTime;
                    if(_time >= _feedTime)
                    {
                        _time -= _feedTime;
                        if(!CanGoToTheNextLine(textName))
                        {
                            _displayedSentenceLength++;
                        }
                    }

                    if(Input.GetMouseButtonUp(0))
                    {
                        if(CanGoToTheNextLine(textName))
                        {
                            GoToTheNextLine(textName);
                        }
                        else // すべて表示されていないときにクリックされた時、すべて表示する
                        {
                            _displayedSentenceLength = _sentenceLength;
                        }
                    }
                    DisplayText(textName);
                }
                yield return null;
            }
            GameManager.Instance.userScriptMainTextManager.coroutine = null;
            yield break;
        }

        public bool IsCoroutineRunning(Coroutine coroutine)
        {
            return coroutine != null;
        }
    }
}
