using UnityEngine;
using UnityEngine.UI;

namespace NoverGame
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
