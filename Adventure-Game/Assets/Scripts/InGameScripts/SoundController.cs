using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] SoundManager soundManager;

        void Start()
        {
            soundManager.PlayBGM("最初のBGM");
        }
    }
}
