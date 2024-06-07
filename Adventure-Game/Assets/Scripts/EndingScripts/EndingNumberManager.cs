using System.Collections;
using System.Collections.Generic;
using AdventureGame;
using UnityEngine;
using UnityEngine.UI;

public class EndingNumberManager : MonoBehaviour
{
    [SerializeField] Text endingNumText;

    void Start()
    {
        string endNumstr = MasterData.Instance.EndingNumber.ToString();
        endingNumText.text = "エンディング " + endNumstr;
    }
}
