using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class CharacterBrightnessSwitcher : MonoBehaviour
    {/*※のある部分はこのクラスやCharacterManagerクラスに追加があった時に必ずチェックすること*/
        // 話し手の名前テキストが入ったオブジェクト
        [SerializeField] GameObject speakerNameTextObj;

        //※ どのキャラ画像を取り出すかを示すナンバー
        private int BLACK_HOOD, FIGURE, HELO;

        //※ 表情差分を取り出す用のナンバー
        private int NO_EXPRESSION, CLOSE_EYE, TEXT_NUM;

        // 表示時の明るい色
        private Color clear = new Color(1.0f, 1.0f, 1.0f);
        // 非表示時の暗い色
        private Color dark = new Color(0.5f, 0.5f, 0.5f);
        void Awake()
        {
            // 初期化
            BLACK_HOOD = GameManager.Instance.characterManager.BLACK_HOOD;
            FIGURE = GameManager.Instance.characterManager.FIGURE;
            HELO = GameManager.Instance.characterManager.HELO;

            NO_EXPRESSION = GameManager.Instance.characterManager.NO_EXPRESSION;
            CLOSE_EYE = GameManager.Instance.characterManager.CLOSE_EYE;
            TEXT_NUM = GameManager.Instance.characterManager.TEXT_NUM;
        }

        // 話し手の名前によってキャラの明暗度を変える
        public void CharBrightnessSwitcher(List<GameObject> charImageObjList, List<List<Sprite>> charImageList)
        {
            Text nameText = speakerNameTextObj.GetComponent<Text>();

            foreach (GameObject charImageObj in charImageObjList)
            {
                if(charImageObj == null) Debug.LogError("Object null Error");

                if(charImageObj.activeSelf == false) continue;

                Image image = charImageObj.GetComponent<Image>();

                //※ 話し手の名前が入る
                switch(nameText.text)
                {
                    case "　":
                        image.material.color = dark;
                        break;
                    case "私":
                        // リストのスプライトとimageのスプライトが同じ場合は画像を明るくする(表情あり、なし両方)。違う場合は画像を暗くする
                        // 人影
                        if(charImageList[FIGURE][NO_EXPRESSION] == image.sprite)
                        {image.material.color = clear;}

                        // 勇者
                        else if(charImageList[HELO][NO_EXPRESSION] == image.sprite||charImageList[HELO][CLOSE_EYE] == image.sprite)
                        {image.material.color = clear;} 

                        else {image.material.color = dark;}
                        break;
                    case "???":
                    case "黒フード":
                        if(charImageList[BLACK_HOOD][NO_EXPRESSION] == image.sprite){image.material.color = clear;}
                        else {image.material.color = dark;}
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
