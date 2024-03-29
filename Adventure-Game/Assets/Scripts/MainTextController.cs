using UnityEngine;
using UnityEngine.UI;

namespace NoverGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] Text _mainTextObject;
        void Start()
        {
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
            if(Input.GetMouseButtonUp(0))
            {
                GoToTheNextLine();
                DisplayText();
            }
        }

        // 次の行へ移動
        public void GoToTheNextLine()
        {
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
            _mainTextObject.text = sentence;
        }
    }
}
