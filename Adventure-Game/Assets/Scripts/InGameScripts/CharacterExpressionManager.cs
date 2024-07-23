using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class CharacterExpressionManager : MonoBehaviour
    {
        // カラーアルファ値の最大値と最低値
        private float maxAlphaNum = 1f;
        private float minAlphaNum = 0f;

        public float fadeIn_OutExpressionTimer = 0.1f;

        // アクティブな子オブジェクト（表情）を取得する（最初にアクティブな物のみ）
        public GameObject FindActiveChildObject(RectTransform parentRT)
        {
            foreach(RectTransform childRT in parentRT)
            {
                if(childRT.gameObject.activeSelf)
                {
                    return childRT.gameObject;
                }
            }
            Debug.Log("Not Active ChildObject");
            return null;
        }

        // 全てのアクティブな子オブジェクト（表情）を取得する
        public List<GameObject> FindActiveChildObjects(RectTransform parentRT)
        {
            List<GameObject> activeChildObjects = new List<GameObject>();
            foreach(RectTransform childRT in parentRT)
            {
                if(childRT.gameObject.activeSelf)
                {
                    activeChildObjects.Add(childRT.gameObject);
                }
            }

            if(activeChildObjects.Count == 0)
            {
                Debug.Log("Not Active ChildObject");
            }
            return activeChildObjects;
        }

        // 子オブジェクト(表情)のフェードイン
        public IEnumerator FadeInExpression(GameObject childObject)
        {
            Image image = childObject.GetComponent<Image>();
            Color color = image.material.color;
            color.a = 0;
            childObject.SetActive(true);

            while(!Mathf.Approximately(color.a, maxAlphaNum))// 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/fadeIn_OutExpressionTimer;
                color.a = Mathf.MoveTowards(color.a, maxAlphaNum, changePerFrame);
                image.material.color = color;
                yield return null;
            }
        }

        // 子オブジェクト(表情)のフェードアウト
        public IEnumerator FadeOutExpression(RectTransform parentRT)
        {
            GameObject activeChildObj = FindActiveChildObject(parentRT);
            Image actImage = activeChildObj.GetComponent<Image>();
            Color actColor = actImage.material.color;
            while(!Mathf.Approximately(actColor.a, minAlphaNum))// 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/fadeIn_OutExpressionTimer;
                actColor.a = Mathf.MoveTowards(actColor.a, minAlphaNum, changePerFrame);
                actImage.material.color = actColor;
                yield return null;
            }
            activeChildObj.SetActive(false);
        }

        // 子オブジェクト(表情)の切り替え
        public IEnumerator ChangeExpression(RectTransform parentRT , GameObject childObject)
        {
            // 既にアクティブだった時コルーチンから抜ける
            if(childObject.activeSelf == true) yield break;
            
            // アクティブな子オブジェクトをフェードアウト
            StartCoroutine(FadeOutExpression(parentRT));
            yield return null;
            // 新たな子オブジェクトをフェードイン
            StartCoroutine(FadeInExpression(childObject));
        }
    }
}
