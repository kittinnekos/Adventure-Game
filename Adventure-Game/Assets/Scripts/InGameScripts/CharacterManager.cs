using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class CharacterManager : MonoBehaviour
    {/*※のある部分はこのクラスやCharacterBrightnessSwitcherクラスに追加があった時に必ずチェックすること*/
        // キャラ画像を表示させるオブジェクト
        [SerializeField] GameObject charImageObj1;
        [SerializeField] GameObject charImageObj2;
    
        //※ キャラ画像
        [SerializeField] List<Sprite> blackHoodSpriteList; //黒フード画像リスト
        [SerializeField] List<Sprite> figureSpriteList; // 人影画像リスト
        [SerializeField] List<Sprite> heroSpriteList; // 勇者画像リスト

        //※ どのキャラ画像を取り出すかを示す定数ナンバー
        public readonly int BLACK_HOOD = 0, FIGURE = 1, HELO = 2;

        //※ 表情差分を取り出す用の定数ナンバー
        public readonly int NO_EXPRESSION = 0, CLOSE_EYE = 1, TEXT_NUM = 2;

        private CharacterBrightnessSwitcher characterBrightnessSwitcher;
                
        // 確認用キャラ画像オブジェクトリスト
        List<GameObject> charImageObjList;

        // 確認用キャラ画像二次元リスト
        List<List<Sprite>> charSprite2DList;

        Dictionary<string, GameObject> textToObject;
        Dictionary<string, List<Sprite>> textToCharSpriteList;


        void Awake()
        {
            characterBrightnessSwitcher = GetComponent<CharacterBrightnessSwitcher>();

            // 確認用リストの初期化
            charImageObjList = new List<GameObject>();
            charImageObjList.Add(charImageObj1);
            charImageObjList.Add(charImageObj2);

            // ※
            charSprite2DList = new List<List<Sprite>>();
            charSprite2DList.Add(blackHoodSpriteList);
            charSprite2DList.Add(figureSpriteList);
            charSprite2DList.Add(heroSpriteList);


            // ディキショナリーの初期化
            textToObject = new Dictionary<string, GameObject>();
            textToObject.Add("1", charImageObj1);
            textToObject.Add("2", charImageObj2);

            // ※
            textToCharSpriteList = new Dictionary<string, List<Sprite>>();
            textToCharSpriteList.Add("hero",heroSpriteList);
            textToCharSpriteList.Add("brackHood", blackHoodSpriteList);
            textToCharSpriteList.Add("figure",figureSpriteList);
        }
        void Start()
        {
            // 話し手の名前によってキャラの明暗度を変える関数
            characterBrightnessSwitcher.CharBrightnessSwitcher(charImageObjList, charSprite2DList);
        }
        void Update()
        {
            characterBrightnessSwitcher.CharBrightnessSwitcher(charImageObjList, charSprite2DList);
        }

        // キャラ画像を変更し、オブジェクトをアクティブにして表示させる
        public void ChangeCharacterImage(string charObjNum, string imageName, string expression = "No")// デフォルト引数で表情を変えられるようにする
        {
            if(textToObject[charObjNum].activeSelf == false) textToObject[charObjNum].SetActive(true);
            Image image = textToObject[charObjNum].GetComponent<Image>();
            if(expression == "No")
            {
                image.sprite = textToCharSpriteList[imageName][NO_EXPRESSION];
            }
            else
            {
                switch(expression)
                {
                    case "closeEye":
                        image.sprite = textToCharSpriteList[imageName][CLOSE_EYE];
                        break;
                    case "testNum":
                        image.sprite = textToCharSpriteList[imageName][TEXT_NUM];
                        break;
                    default:
                        break;
                }
            }
        }

        // キャラ画像をリセットし、オブジェクトを非アクティブにして非表示にさせる
        public void NonActiveCharacterImage(string charObjNum)
        {
            Image image = textToObject[charObjNum].GetComponent<Image>();
            image.sprite = null;
            textToObject[charObjNum].SetActive(false);
        }
    }
}
