using UnityEngine;
using UnityEngine.UI;

namespace NoverGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] Text _mainTextObject;
        void Start()
        {
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
        }
        // テキストを表示
        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _mainTextObject.text = sentence;
        }
    }
}
