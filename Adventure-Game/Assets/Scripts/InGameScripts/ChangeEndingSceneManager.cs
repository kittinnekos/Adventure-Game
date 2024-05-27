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
            targetSequenceList.Add(new List<int>{1,1,1}); // エンド1
            targetSequenceList.Add(new List<int>{1,1,2}); // エンド2
            targetSequenceList.Add(new List<int>{1,2,1}); // エンド3
            targetSequenceList.Add(new List<int>{1,2,2}); // エンド4
            targetSequenceList.Add(new List<int>{1,3,1}); // エンド5
            targetSequenceList.Add(new List<int>{1,3,2}); // エンド6
        }
        // TODO エンディングシーンの切り替えとエンドの保管を別々の処理にする
        private void ChangeEndingScene()
        {
            SceneManager.LoadScene("Ending");
        }
        // 
        private bool CheckOrder(List<int> pickSelectNumberList, List<int> targetSequence)
        {
            if(pickSelectNumberList.Count == 1)
            {
                return true;
            }
            
            return pickSelectNumberList.SequenceEqual(targetSequence);
        } 
        public void Ending()
        {
            // pickSelectNumberListを元にエンディングナンバーを代入してシーンを切り替える
            int i = 0;
            foreach(List<int> targetSequence in targetSequenceList)
            {
                if(CheckOrder(GameManager.Instance.pickSelectNumberList, targetSequence))
                {
                    MasterData.Instance.EndingNumber = i;
                    ChangeEndingScene();
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
