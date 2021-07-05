using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setProgram : MonoBehaviour
{
    public static string name = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectbianxing()
    {
        if(name!="变性"){
            SpeechController.Speak("请重新选择");
            return;
        }
        GameObject.Find("变性程序文本").GetComponent<Text>().text = "变性";
        GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        bianxing.programCache = true;
        SpeechController.Speak("选择正确");
    }
    
    public void select95()
    {
        if(name!="变性"){
            SpeechController.Speak("请重新选择");
            return;
        }
        bianxing.temperatureCache = true;
        GameObject.Find("程序时间").GetComponent<Text>().text = "30";
        SpeechController.Speak("选择正确，变性时间一般为30秒。点击左侧加号选择下一阶段的程序");
    }
    public void selecttuihuo()
    {
        if(name!="退火"){
            SpeechController.Speak("请重新选择");
            return;
        }
        GameObject.Find("退火程序文本").GetComponent<Text>().text = "退火";
        GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        tuihuo.programCache = true;
        SpeechController.Speak("选择正确");
    }
    
    public void select55()
    {
        if(name!="退火"){
            SpeechController.Speak("请重新选择");
            return;
        }
        tuihuo.temperatureCache = true;
        GameObject.Find("程序时间").GetComponent<Text>().text = "30";
        SpeechController.Speak("选择正确，退火时间一般为30秒。点击左侧加号选择下一阶段的程序");
    }
    public void selectyanshen()
    {
        if(name!="延伸"){
            SpeechController.Speak("请重新选择");
            return;
        }
        GameObject.Find("延伸程序文本").GetComponent<Text>().text = "延伸";
        GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        yanshen.programCache = true;
        SpeechController.Speak("选择正确");
    }
    
    public void select72()
    {
        if(name!="延伸"){
            SpeechController.Speak("请重新选择");
            return;
        }
        yanshen.temperatureCache = true;
        GameObject.Find("程序时间").GetComponent<Text>().text = "60";
        SpeechController.Speak("选择正确，延伸时间一般为60秒。接下来请选择循环次数");
    }
}
