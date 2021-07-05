using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tuihuo : MonoBehaviour
{
    public static bool programCache=false,temperatureCache=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void click()
    {
        setProgram.name = "退火";
        GameObject.Find("首页").GetComponent<Text>().text = "";

        if(bianxing.programCache && bianxing.temperatureCache)
        {
            GameObject.Find("程序顺序").GetComponent<Text>().text = "";

            SpeechController.Speak("请选择这一阶段的程序");
        }
        else {
            GameObject.Find("程序顺序").GetComponent<Text>().text = "请按顺序添加程序!";
            UnityToast.ShowAlert("操作失误", "请按顺序添加程序!");
            SpeechController.Speak("操作失误，请按顺序添加程序!");
            
            GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if(programCache) {
            GameObject.Find("退火").GetComponent<Toggle>().isOn = true;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else{
            GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if(temperatureCache){
            GameObject.Find("55").GetComponent<Toggle>().isOn = true;
            GameObject.Find("程序时间").GetComponent<Text>().text = "30";
        }
        else if(programCache){
            GameObject.Find("程序时间").GetComponent<Text>().text = "";
            SpeechController.Speak("请选择温度");
        }
    }
    // public void selectProgram()
    // {
    //     GameObject.Find("退火程序文本").GetComponent<Text>().text = "退火";
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
