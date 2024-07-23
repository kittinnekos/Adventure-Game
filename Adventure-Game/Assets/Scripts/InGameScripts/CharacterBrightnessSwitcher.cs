using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class CharacterBrightnessSwitcher : MonoBehaviour
    {
        // 話し手の名前テキストが入ったオブジェクト
        [SerializeField] GameObject speakerNameTextObj;

        // 表示時の立ち絵の明るい色
        private Color clear = new Color(1.0f, 1.0f, 1.0f);
        // 非表示時の立ち絵の暗い色
        private Color dark = new Color(0.5f, 0.5f, 0.5f);

        // 話し手の名前によって立ち絵と表情の明暗度を変える
        public void CharaBrightnessSwitcher(GameObject[] characterObject)
        {
            Text nameText = speakerNameTextObj.GetComponent<Text>();

            foreach (GameObject charaObj in characterObject)
            {
                if(charaObj == null) continue;
                // 立ち絵から変更用のImageコンポーネントを取得
                Image charaImage = charaObj.GetComponent<Image>();
                Color charaColor = charaImage.color;

                // 話し手の名前と立ち絵のタグが同じ時立ち絵を明るくし、違う時立ち絵を暗くする
                if(nameText.text == charaObj.tag)
                {
                    Color clearColor = clear;   // 元の明るい色を変えないように新たに定義
                    clearColor.a = charaColor.a;// アルファ値は立ち絵の値にする
                    charaImage.material.color = clearColor;
                }
                else
                {
                    Color darkColor = dark;
                    darkColor.a = charaColor.a;
                    charaImage.material.color = darkColor;
                }

                // 全てのアクティブな表情を取得
                RectTransform parentRT = charaObj.GetComponent<RectTransform>();
                List<GameObject> actExpressions = GameManager.Instance.characterExpressionManager.FindActiveChildObjects(parentRT);
                if(actExpressions.Count == 0) continue;

                // アクティブな表情の分ループする
                foreach(GameObject actExpression in actExpressions)
                {
                    // 変更用のImageコンポーネントを取得
                    Image actExpressionImage = actExpression.GetComponent<Image>();
                    Color actExpressionColor = actExpressionImage.color;

                    // 話し手の名前と立ち絵のタグが同じ時表情を明るくし、違う時表情を暗くする。
                    if(nameText.text == charaObj.tag)
                    {
                        Color clearColor = clear;   // 元の明るい色を変えないように新たに定義
                        clearColor.a = actExpressionColor.a;// アルファ値は表情の値にする
                        actExpressionImage.color = clearColor;
                    }
                    else
                    {
                        Color darkColor = dark;
                        darkColor.a = actExpressionColor.a;
                        actExpressionImage.color = darkColor;
                    }
                }
                

            }
        }
    }
}
