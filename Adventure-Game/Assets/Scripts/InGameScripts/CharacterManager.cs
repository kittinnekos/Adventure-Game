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

        // TODO テスト部分始め
        // 表情の名前とリスト用の値を保存するクラス
        [System.Serializable]
        public class ExpressionData
        {
            public string expressionName;
            public int expressionNum;
        }

        // インスペクター上で表情差分画像を保存するクラス
        [System.Serializable]
        public class CharSprite
        {
            public int expressionNum; // どの表情差分かを値で指定。ExpressionData内の値と合致させるように
            public Sprite expressionSprite;
        }

        // インスペクター上でキャラの名前に合った画像を表情差分のリストで保存するクラス
        [System.Serializable]
        public class CharaData
        {
            public string charName;
            public CharSprite[] charSprites;
        }
        // 表情差分を増やす、または減らす時この配列の要素数を増減させる
        public ExpressionData[] expressionDatas = new ExpressionData[8];

        // 表示させるキャラを増やす、または減らす時この配列の要素数を増減させる
        public CharaData[] charaDatas = new CharaData[14];
        // TODO テスト部分終わり
    
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
        Dictionary<(string,int), Sprite> testDictionary; // TODO テスト用
        Dictionary<string, int> testExpressionDic; // TODO テスト用


        void Awake()
        {
            characterBrightnessSwitcher = GetComponent<CharacterBrightnessSwitcher>();

            // 確認用リストの初期化
            charImageObjList = new List<GameObject>();
            charImageObjList.Add(charImageObj1);
            charImageObjList.Add(charImageObj2);

            // TODO テスト部分始め
            testDictionary = new Dictionary<(string, int),Sprite>();
            testExpressionDic = new Dictionary<string,int>();
            // インスペクター上に入力された画像の名前とCharSprite配列内の表情の値をキー、CharSprite配列内のspriteをバリューとする
            foreach(CharaData charaData in charaDatas)
            {
                foreach(CharSprite charSprite in charaData.charSprites)
                {
                    Debug.Log(charaData.charName);
                    testDictionary.Add((charaData.charName, charSprite.expressionNum), charSprite.expressionSprite);
                }
            }

            // インスペクター上に入力された表情の名前をキー、表情の値をバリューとする。これは変換用の連想配列
            foreach(ExpressionData expressionData in expressionDatas)
            {
                testExpressionDic.Add(expressionData.expressionName, expressionData.expressionNum);
            }
            // TODO テスト部分終わり

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
            //characterBrightnessSwitcher.CharBrightnessSwitcher(charImageObjList, charSprite2DList);
            characterBrightnessSwitcher.CharBrightnessSwitcher(charImageObjList);
        }
        void Update()
        {
            //characterBrightnessSwitcher.CharBrightnessSwitcher(charImageObjList, charSprite2DList);
            characterBrightnessSwitcher.CharBrightnessSwitcher(charImageObjList);
        }

        // 旧機能　キャラ画像を変更し、オブジェクトをアクティブにして表示させる
        /*public void ChangeCharacterImage(string charObjNum, string imageName, string expression = "NoExpression")// デフォルト引数で表情を変えられるようにする
        {
            if(textToObject[charObjNum].activeSelf == false) textToObject[charObjNum].SetActive(true);
            Image image = textToObject[charObjNum].GetComponent<Image>();
            if(expression == "NoExpression")
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
        */

        // TODO テスト　キャラ画像を変更し、オブジェクトをアクティブにして表示させる
        public void TestChangeCharacterImage(string charObjNum, string imageName, string expression = "表情なし")// デフォルト引数で表情を変えられるようにする
        {
            if(textToObject[charObjNum].activeSelf == false) textToObject[charObjNum].SetActive(true);
            textToObject[charObjNum].tag = imageName;
            Image image = textToObject[charObjNum].GetComponent<Image>();

            // 連想配列のキーに「画像の名前」と「表情(連想配列によりintになる)」を入れてspriteを取得
            image.sprite = testDictionary[(imageName, testExpressionDic[expression])];
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
