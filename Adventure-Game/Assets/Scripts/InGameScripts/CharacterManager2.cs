using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class CharacterManager2 : MonoBehaviour
    {
        // キャラの名前とプレハブを格納するクラス
        [System.Serializable]
        public class CharaData
        {
            public string charaName;
            public GameObject charaPrefab;
        }

        // 表情の名前とナンバーを格納するクラス
        [System.Serializable]
        public class ExpressionData
        {
            public string expressionName;
            public int expressionPosNum;// ナンバーはキャラクターオブジェクトからの位置を元に当てはめる。
        }

        [System.NonSerialized] public Dictionary<string, GameObject> NameToPrefab = new Dictionary<string, GameObject>();

        [System.NonSerialized] public Dictionary<string, GameObject> NameToCharacterObject = new Dictionary<string, GameObject>();

        [System.NonSerialized] public Dictionary<string, int> expressionToNum = new Dictionary<string, int>();

        [SerializeField] GameObject CharacterObjects;

        // インスペクター上で設定
        public CharaData[] charaDatas;
        public ExpressionData[] expressionDatas;

        // インスタンス化したキャラクターオブジェクトを保管する配列(要素数は表示上限)
        private GameObject[] characterObject = new GameObject[4];

        // カラーアルファ値の最大値と最低値
        private float maxAlphaNum = 1f;
        private float minAlphaNum = 0f;

        private float fadeIn_OutExpressionTimer;
        private float fadeIn_OutStandingPictureTimer = 0.2f;

        void Awake()
        {
            fadeIn_OutExpressionTimer = GameManager.Instance.characterExpressionManager.fadeIn_OutExpressionTimer;
            //int i = 0;
            // ディキショナリーの初期化
            foreach(CharaData charaData in charaDatas)
            {
                // TODO 後のデバッグのため一時保存
                // Debug.Log(charaData.charaName);
                // Debug.Log(charaData.charaPrefab);
                // Debug.Log(i);
                // i++;
                NameToPrefab.Add(charaData.charaName, charaData.charaPrefab);
            }
            //int j = 0;
            foreach(ExpressionData expressionData in expressionDatas)
            {
                // TODO 後のデバッグのため一時保存
                // Debug.Log(expressionData.expressionName);
                // Debug.Log(j);
                // j++;
                expressionToNum.Add(expressionData.expressionName, expressionData.expressionPosNum);
            }
        }

        void Update()
        {
            GameManager.Instance.characterBrightnessSwitcher2.CharaBrightnessSwitcher(characterObject);
        }

        // 立ち絵をインスタンス化し、表情を付ける。
        public void SpawnStandingPicture(string charaName, string expression)
        {
            for(int i = 0;i < characterObject.Length;i++)
            {
                Debug.Log("Spawn Object Num." + i);
                if(characterObject[i] != null) continue;
                characterObject[i] = Instantiate(NameToPrefab[charaName], CharacterObjects.transform);

                // ディキショナリーにインスタンス化したオブジェクトを追加
                NameToCharacterObject.Add(charaName, characterObject[i]);
                break;
            }
            RectTransform objectRT = NameToCharacterObject[charaName].GetComponent<RectTransform>();
            GameObject childObject = FindChildObject(objectRT, expression);
            if(childObject == null) return;
            // 立ち絵と表情をフェードインさせるコルーチン
            StartCoroutine(FadeInStandingPictureANDExpression(NameToCharacterObject[charaName], childObject));
        }

        // 表情を変える
        public void ChangeExpression(string charaName, string expression)
        {
            RectTransform objectRT = NameToCharacterObject[charaName].GetComponent<RectTransform>();
            GameObject childObject = FindChildObject(objectRT, expression);
            if(childObject == null) return;
            
            StartCoroutine(GameManager.Instance.characterExpressionManager.ChangeExpression(objectRT, childObject));
        }

        // 立ち絵をフェードアウト&削除
        public void RemoveStandingPicture(string charaName)
        {
            // キャラクターオブジェクト配列内のオブジェクトとディキショナリー内のキャラクターオブジェクトを削除する。
            StartCoroutine(FadeOutANDRemoveStandingPicture(charaName));
        }

        // 立ち絵の子オブジェクト（表情）を取得する。
        private GameObject FindChildObject(RectTransform parentRT, string expression)
        {
            // 指定された表情をキーとし、親オブジェクトから子オブジェクトを取得して返す。
            if(expressionToNum.ContainsKey(expression))
            {
                RectTransform childRT = parentRT.GetChild(expressionToNum[expression]) as RectTransform;
                GameObject childObject = childRT.gameObject;
                return childObject;
            }
            else 
            {
                Debug.LogWarning("Not expression" + expression);
                return null;
            }
        }

        // 立ち絵のフェードイン
        private IEnumerator FadeInStandingPicture(GameObject characterObject)
        {
            Image image = characterObject.GetComponent<Image>();
            Color color = image.material.color;
            color.a = 0;

            while(!Mathf.Approximately(color.a, maxAlphaNum)) // 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/fadeIn_OutStandingPictureTimer;
                color.a = Mathf.MoveTowards(color.a, maxAlphaNum, changePerFrame);
                image.material.color = color;
                yield return null;
            }
        }

        // 立ち絵のフェードアウト
        private IEnumerator FadeOutStandingPicture(GameObject characterObject)
        {
            Image image = characterObject.GetComponent<Image>();
            Color color = image.material.color;

            while(!Mathf.Approximately(color.a, minAlphaNum)) // 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/fadeIn_OutStandingPictureTimer;
                color.a = Mathf.MoveTowards(color.a, minAlphaNum, changePerFrame);
                image.material.color = color;
                yield return null;
            }
        }

        // 立ち絵をフェードインさせて表情をフェードインさせる。
        private IEnumerator FadeInStandingPictureANDExpression(GameObject characterObject, GameObject childObject)
        {
            // 立ち絵のフェードインコルーチン
            StartCoroutine(FadeInStandingPicture(characterObject));
            yield return new WaitForSeconds(fadeIn_OutStandingPictureTimer);// 立ち絵が表示されるまで待機
            // 表情のフェードインコルーチン
            StartCoroutine(GameManager.Instance.characterExpressionManager.FadeInExpression(childObject));
        }

        // 立ち絵をフェードアウトさせて削除する。
        private IEnumerator FadeOutANDRemoveStandingPicture(string charaName)
        {
            for(int i = 0; i < characterObject.Length;i++)
            {
                // 削除する立ち絵を検索
                if(characterObject[i] == null) continue;
                Debug.Log("Object Active");
                if(characterObject[i] != NameToCharacterObject[charaName]) continue;
                Debug.Log("Success");
                // 立ち絵のフェードアウト
                RectTransform parentRT = characterObject[i].GetComponent<RectTransform>();
                StartCoroutine(GameManager.Instance.characterExpressionManager.FadeOutExpression(parentRT));
                yield return new WaitForSeconds(fadeIn_OutExpressionTimer);// 表情をフェードアウトさせるため待機。

                // 立ち絵の削除
                StartCoroutine(FadeOutStandingPicture(characterObject[i]));
                yield return new WaitForSeconds(fadeIn_OutStandingPictureTimer);// 立ち絵をフェードアウトさせるため待機。
                Destroy(characterObject[i]);
                characterObject[i] = null;

                // 配列内のオブジェクトを削除後ディキショナリー内のキャラクターオブジェクトを削除する
                NameToCharacterObject.Remove(charaName);
                Debug.Log(charaName + "is Remove");
                break;
            }
        }
    }
}
