using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class CharacterExpressionManager : MonoBehaviour
    {
        private float maxAlphaNum = 1f;
        private float minAlphaNum = 0f;

        // アクティブな子オブジェクト（表情）を取得する
        private GameObject FindActiveChildObject(RectTransform parentRT)
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

        // 子オブジェクト(表情)のフェードイン
        public IEnumerator FadeInExpression(GameObject childObject)
        {
            Image image = childObject.GetComponent<Image>();
            Color color = image.material.color;
            color.a = 0;
            childObject.SetActive(true);

            while(!Mathf.Approximately(color.a, maxAlphaNum)) // 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/0.5f;
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
            while(!Mathf.Approximately(actColor.a, minAlphaNum)) // 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/0.5f;
                actColor.a = Mathf.MoveTowards(actColor.a, minAlphaNum, changePerFrame);
                actImage.material.color = actColor;
                yield return null;
            }
            activeChildObj.SetActive(false);
        }

        // 子オブジェクト(表情)の切り替え
        public IEnumerator ExpressionBrightnessSwitcher(RectTransform parentRT , GameObject childObject)
        {
            // アクティブな子オブジェクトをフェードアウト
            StartCoroutine(FadeOutExpression(parentRT));
            yield return null;
            // 新たな子オブジェクトをフェードイン
            StartCoroutine(FadeInExpression(childObject));
        }
    }
}
