using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdventureGame
{
    public class ChangeEndingSceneManager : MonoBehaviour
    {
        private List<List<int>> targetSequenceList = new List<List<int>>();
        void Awake()
        {
            // 0が一つ目の選択肢。三回選択をする
            targetSequenceList.Add(new List<int>{1,0,0}); // エンド1
            targetSequenceList.Add(new List<int>{1,0,1}); // エンド2
            targetSequenceList.Add(new List<int>{1,1,0}); // エンド3
            targetSequenceList.Add(new List<int>{1,1,1}); // エンド4
            targetSequenceList.Add(new List<int>{1,2,0}); // エンド5
            targetSequenceList.Add(new List<int>{1,2,1}); // エンド6
        }
        // TODO エンディングシーンの切り替えとエンドの保管を別々の処理にする
        public void ChangeEndingScene()
        {
            SceneManager.LoadScene("Ending");
        }
        // 
        private bool CheckOrder(List<int> pickSelectNumberList, List<int> targetSequence)
        {
            return pickSelectNumberList.SequenceEqual(targetSequence);
        } 
        public void Ending()
        {
            // pickSelectNumberListを元にエンディングナンバーを代入する
            int i = 1;
            foreach(List<int> targetSequence in targetSequenceList)
            {
                if(CheckOrder(GameManager.Instance.pickSelectNumberList, targetSequence))
                {
                    MasterData.Instance.EndingNumber = i;
                    Debug.Log("success" + i);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
