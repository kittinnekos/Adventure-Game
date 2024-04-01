using UnityEngine;
using UnityEngine.UI;

namespace NoverGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] Text _mainTextObject;
        int _displayedSentenceLength;
        float _time;
        float _feedTime;
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;
            // 最初の行のテキストを表示、または命令を実行
            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if(GameManager.Instance.userScriptManager.IsStatement(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
                GoToTheNextLine();
            }
            DisplayText();
        }

        void Update()
        {
            // 文章を一文字ずつ表示する
            _time += Time.deltaTime;
            if(_time >= _feedTime)
            {
                _time -= _feedTime;
                if(!CanGoToTheNextLine())
                {
                    _displayedSentenceLength++;
                }
            }

            // クリックされた時、次の行へ移動
            if(Input.GetMouseButtonUp(0))
            {
                if(CanGoToTheNextLine())
                {
                    GoToTheNextLine();
                }
            }
            DisplayText();
        }

        // その行の、すべての文字が表示されていなければ、まだ次の行へ進むことはできない
        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            return (_displayedSentenceLength > sentence.Length);
        }

        // 次の行へ移動
        public void GoToTheNextLine()
        {
            _displayedSentenceLength = 0;
            _time = 0f;
            //_mainTextObject.maxVisibleCharacters = 0;
            GameManager.Instance.lineNumber++;
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if(GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
                GoToTheNextLine();
            }
        }

        // テキストを表示
        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            string clampedSentence = sentence.Substring(0, Mathf.Min(sentence.Length, _displayedSentenceLength));
            _mainTextObject.text = clampedSentence;
        }
    }
}
