using System.Collections;
using System.Collections.Generic;
using Crosstales.RTVoice;
using Crosstales.RTVoice.Tool;
using UnityEngine;

public class SpeechController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        UnityToast.InitBotEmojiView();
        // UnityToast.ShowToast("这是一段测试用的语音，这是逗号后的部分。这是第二句话。", 5.0f);
        // speechText.Text = "这是一段测试用的语音，这是逗号后的部分。这是第二句话。";
        // speechText.Speak();
        // UnityToast.ShowTopToast("顶部提示");
        // UnityToast.ShowBottomToast("底部提示");
        // UnityToast.ShowAlert("操作失误", "请先加入AAAA。");
    }
    
    public static void Silence()
    {
        SpeechText speechText = GameObject.Find("SpeechController/SpeechText").GetComponent<SpeechText>();
        if (speechText)
        {
            speechText.Silence();
        }
    }

    public static void Speak(string text, bool toast = true)
    {
        SpeechText speechText = GameObject.Find("SpeechController/SpeechText").GetComponent<SpeechText>();
        if (speechText)
        {
            speechText.Text = text;
            speechText.Silence();
            speechText.Speak();
            if (toast) UnityToast.ShowBotToast(text, 0.22f * text.Length);
        }
    }
    
    public static void Speak(string text, float duration, bool toast = true)
    {
        SpeechText speechText = GameObject.Find("SpeechController/SpeechText").GetComponent<SpeechText>();
        if (speechText)
        {
            speechText.Text = text;
            speechText.Silence();
            speechText.Speak();
            if (toast) UnityToast.ShowBotToast(text, duration);
        }
    }
}
