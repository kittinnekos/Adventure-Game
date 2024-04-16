using System.Collections.Generic;
using AdventureGame;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonManager : MonoBehaviour
{
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;

    private int activeButtonNum;
    private List<string> selectTextList;
    private List<GameObject> buttonList;

    void Awake()
    {
        buttonList = new List<GameObject>();

        // SelectManagerのint変数とリストを受け取り、ボタンに適応させる
        activeButtonNum = GameManager.Instance.selectManager.GetActiveSelectButtonNum();
        selectTextList = GameManager.Instance.selectManager.GetSelectTextList();

        buttonList.Add(button1); buttonList.Add(button2); buttonList.Add(button3);
    }
    
    void Start()
    {
        string[] selectText = selectTextList[0].Split('　');

        if(buttonList.Count >= activeButtonNum && buttonList.Count >= selectText.Length)
        {
            for(int i = 0; i < activeButtonNum; i++)
            {
                buttonList[i].SetActive(true);

                // ボタンオブジェクトの子であるテキストオブジェクトを取得し、テキストを変更している
                buttonList[i].transform.Find("Text").gameObject.GetComponent<Text>().text = selectText[i];
            }
        }
        else
        {
            Debug.LogError("Length Error");
        }
    }
}
