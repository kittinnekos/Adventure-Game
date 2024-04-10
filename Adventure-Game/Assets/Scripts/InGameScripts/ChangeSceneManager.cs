using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoverGame
{
    public class ChangeSceneManager : MonoBehaviour
    {
        private GameObject MasterDataObj;
        void Start()
        {
            MasterDataObj = GameObject.Find ("MasterDataObject");
        }
        public void ChangeScene(string EndNumber)
        {
            if(EndNumber.All(char.IsDigit))
            {
                MasterDataObj.GetComponent<MasterData>().EndingNumber = int.Parse(EndNumber);
                SceneManager.LoadScene("Ending");
            }
            else
            {
                Debug.LogError("Error");
            }
        }
    }
}
