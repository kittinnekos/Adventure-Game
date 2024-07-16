using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class ImageManager : MonoBehaviour
    {
        [System.Serializable]
        public class BackGroundData
        {
            public string name;
            public Sprite sprite;
        }

        public BackGroundData[] backGroundData;

        [SerializeField] Sprite _background1;
        [SerializeField] GameObject _backgroundObject;
        [SerializeField] GameObject _imagePrefab;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToSprite;

        // 操作したいPrefabを指定できるようにするための辞書
        Dictionary<string, GameObject> _textToSpriteObject;

        void Awake()
        {
            _textToSprite = new Dictionary<string, Sprite>();
            _textToSprite.Add("background1", _background1);
            foreach (BackGroundData BGData in backGroundData)
            {
                _textToSprite.Add(BGData.name, BGData.sprite);
            }

            _textToSpriteObject = new Dictionary<string, GameObject>();
        }

        public void PutImage(string imageName)
        {
            Sprite image = _textToSprite[imageName];

            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = _backgroundObject.transform;
            GameObject item = Instantiate(_imagePrefab, position, rotation, parent);
            item.GetComponent<Image>().sprite = image;

            _textToSpriteObject.Add(imageName, item);
        }

        // 画像を削除する
        public void RemoveImage(string imageName)
        {
            Destroy(_textToSpriteObject[imageName]);
        }
    }
}

