using System.Collections;
using System.Collections.Generic;
using Crosstales.RTVoice.Tool;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    public SpeechText speechText;
    
    // Start is called before the first frame update
    void Start()
    {
        // speechText.Text = "这是一段测试用的语音，这是逗号后的部分。这是第二句话。";
        // speechText.Speak();
    }

    public void Speak(string text)
    {
        speechText.Text = text;
        speechText.Speak();
    }
}
