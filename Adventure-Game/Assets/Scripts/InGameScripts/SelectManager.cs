using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AdventureGame
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] GameObject selectPrefab;
        [SerializeField] TextAsset selectText0;
        [SerializeField] TextAsset selectText1;
        [SerializeField] TextAsset selectText2;
        [SerializeField] TextAsset selectText3;
        [System.NonSerialized] public int selectCount = 0;

        Dictionary<string, TextAsset> textToTextFile;

        private GameObject instanceObj;
        private int activeButtonNum;
        private List<string> selectTextList;

        void Awake()
        {   
            textToTextFile = new Dictionary<string, TextAsset>();
            textToTextFile.Add("select0", selectText0);
            textToTextFile.Add("select1", selectText1);
            textToTextFile.Add("select2", selectText2);
            textToTextFile.Add("select3", selectText3);

            activeButtonNum = 0;
            selectTextList = new List<string>();
        }

        public void SpawnSelectPrefab(string ActiveButtonNum, string TextName)
        {
            SetActiveSelectButtonNum(ActiveButtonNum);
            SetselectTextList(TextName);
            instanceObj = Instantiate(selectPrefab);
            instanceObj.transform.SetParent(canvas.transform, false);
        }

        private void SetActiveSelectButtonNum(string ActiveButtonNam)
        {
            activeButtonNum = int.Parse(ActiveButtonNam);
        }

        private void SetselectTextList(string TextName)
        {
            TextAsset Text = textToTextFile[TextName];
            StringReader reader = new StringReader(Text.text);
            
            List<string> textList = new List<string>();
            while (reader.Peek() != -1) // テキストが末端になるまで繰り返す
            {
                string line = reader.ReadLine(); // 変数に一行ずつ格納している
                textList.Add(line);
            }
            selectTextList = textList;
        }

        public int GetActiveSelectButtonNum()
        {
            return activeButtonNum;
        }

        public List<string> GetSelectTextList()
        {
            return selectTextList;
        }

        public void DestroySelectPrefab()
        {
            if(instanceObj == null){Debug.LogError("null Error");}
            Destroy(instanceObj);
        }

        public bool IsSpawnSelectPrefab()
        {
            if(instanceObj != null)
            {
                return true;
            }
            return false;
        }
    }
}

