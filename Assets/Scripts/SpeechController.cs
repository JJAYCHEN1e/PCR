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
        UnityToast.InitBotEmojiView();
        // UnityToast.ShowToast("这是一段测试用的语音，这是逗号后的部分。这是第二句话。", 5.0f);
        // speechText.Text = "这是一段测试用的语音，这是逗号后的部分。这是第二句话。";
        // speechText.Speak();
        // UnityToast.ShowTopToast("顶部提示");
        // UnityToast.ShowBottomToast("底部提示");
        // UnityToast.ShowAlert("操作失误", "请先加入AAAA。");
    }

    public void Speak(string text)
    {
        speechText.Text = text;
        speechText.Speak();
        UnityToast.ShowBotToast(text, 0.22f * text.Length);
    }
}
