using AdventureGame;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = gameObject.GetComponent<Button>(); 

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        GameManager.Instance.selectManager.selectCount++; // 選択肢ボタンが押された回数
        switch(gameObject.name)
        {
            case "Button1":
            GameManager.Instance.pickSelectNumberList.Add(0); // エンディング分岐用のリストに値を追加
            GameManager.Instance.selectManager.DestroySelectPrefab(); // インスタンス化した選択肢ボタンプレハブを破壊
            GameManager.Instance.mainTextController.GoToTheNextLine(); // 選択肢ボタンを押したときに次の行へ進む
            break;

            case "Button2":
            GameManager.Instance.pickSelectNumberList.Add(1);
            GameManager.Instance.selectManager.DestroySelectPrefab();
            GameManager.Instance.mainTextController.GoToTheNextLine();
            break;

            case "Button3":
            GameManager.Instance.pickSelectNumberList.Add(2);
            GameManager.Instance.selectManager.DestroySelectPrefab();
            GameManager.Instance.mainTextController.GoToTheNextLine();
            break;

            default:
            break;
        }
    }
}
