using System.Collections;
using System.Collections.Generic;
using AdventureGame;
using UnityEngine;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        public static TitleManager Instance { get; private set; }

        public TitleFadeOutManager titleFadeOutManager;

        void Awake()
        {
            Instance = this;
        }
    }
}
