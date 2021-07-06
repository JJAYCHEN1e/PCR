using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bianxing : MonoBehaviour
{
    Button btn;
    public static bool programCache=false,temperatureCache=false;
    // Start is called before the first frame update
    void Start()
    {
        btn = GameObject.Find("变性").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void click()
    {
        setProgram.name = "变性";
        GameObject.Find("首页").GetComponent<Text>().text = "";
        GameObject.Find("程序顺序").GetComponent<Text>().text = "";
        GameObject.Find("程序时间").GetComponent<Text>().text = "";
        GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(programCache) {
            GameObject.Find("变性").GetComponent<Toggle>().isOn = true;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else{
            SpeechController.Speak("请选择该阶段的程序");
            GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if(temperatureCache){
            GameObject.Find("95").GetComponent<Toggle>().isOn = true;
            GameObject.Find("程序时间").GetComponent<Text>().text = "30";
        }
        else {
            if(programCache) SpeechController.Speak("请选择温度");
            GameObject.Find("程序时间").GetComponent<Text>().text = "";
        }
    }
    // public void selectProgram()
    // {
    //     GameObject.Find("变性程序文本").GetComponent<Text>().text = "变性";
    //     GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
    //     GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
    //     GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
    //     programCache = true;
    // }
    
    // public void selectTemperature()
    // {
    //     temperatureCache = true;
    //     GameObject.Find("程序时间").GetComponent<Text>().text = "30";
    // }
    
}

