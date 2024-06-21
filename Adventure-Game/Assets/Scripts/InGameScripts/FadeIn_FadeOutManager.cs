using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace AdventureGame
{
    public class FadeIn_FadeOutManager : MonoBehaviour
    {
        [SerializeField] Image FadePanel;
        //private Sprite sp;
        private Color imageColor;

        void Awake()
        {
            imageColor = FadePanel.color;
        }

        // フェードインまたはフェードアウトのどちらかをする
        // targetAlphaを0にするとフェードアウト、1にするとフェードインになる
        public IEnumerator Fade(float targetAlpha, float fadeTime = 1f)
        {
            while(!Mathf.Approximately(imageColor.a, targetAlpha)) // 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/fadeTime;
                imageColor.a = Mathf.MoveTowards(imageColor.a, targetAlpha, changePerFrame);
                FadePanel.color = imageColor;
                yield return null;
            }
        }

        // フェードアウトし、waitTime分待ってからフェードインする
        public IEnumerator FadeOut_FadeIn(float fadeTime = 1f, float waitTime = 1f)
        {
            bool isFadeSuccess = false;
            while(!isFadeSuccess)
            {
                bool isSpawnSelectPrefab = GameManager.Instance.selectManager.IsSpawnSelectPrefab();
                if(isSpawnSelectPrefab)
                {
                    yield return null;
                    continue;
                }

                StartCoroutine(Fade(1, fadeTime));
                yield return new WaitForSeconds(fadeTime);
                GameManager.Instance.isFadeIn = true;
                yield return new WaitForSeconds(waitTime);
                StartCoroutine(Fade(0, fadeTime));
                yield return new WaitForSeconds(fadeTime);
                GameManager.Instance.isFadeIn = false;
                isFadeSuccess = true;
            }
        }
    }
}
