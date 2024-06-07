using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleFadeOutManager : MonoBehaviour
{
    [SerializeField] GameObject FadePanelObj;
    [SerializeField] Image FadePanel;
    private Color imageColor;

    void Awake()
    {
        imageColor = FadePanel.color;
    }
    public IEnumerator FadeOut(float fadeTime = 1f)
        {
            if(!FadePanelObj.activeSelf)
            {
                FadePanelObj.SetActive(true);
            }
            while(!Mathf.Approximately(imageColor.a, 1f)) // 透明度がほぼ等しくなるまで繰り返す
            {
                float changePerFrame = Time.deltaTime/fadeTime;
                imageColor.a = Mathf.MoveTowards(imageColor.a, 1f, changePerFrame);
                FadePanel.color = imageColor;
                yield return null;
            }
            SceneManager.LoadScene("InGame");
        }
}
