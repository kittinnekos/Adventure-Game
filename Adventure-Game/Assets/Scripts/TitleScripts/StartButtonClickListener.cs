using UnityEngine;
using UnityEngine.UI;

public class StartButtonClickListener : MonoBehaviour
{
    void Start()
    {
        // Buttonコンポーネントがアタッチされているか確認
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // ボタンがクリックされたときに呼び出されるメソッドを設定
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }

        void OnButtonClick()
        {
            Debug.Log("Click1");
        }
    }
}
