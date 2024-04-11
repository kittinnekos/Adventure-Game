using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    public class SpeakerNameTextManager : MonoBehaviour
    {
        [SerializeField] Text speakerNameTextObject;

        public void DisplaySpeakerNameText(string speakerName)
        {
            speakerNameTextObject.text = speakerName;
        }
    }
}
