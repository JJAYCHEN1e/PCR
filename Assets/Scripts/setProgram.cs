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
        if(name!="变性")return;
        GameObject.Find("变性程序文本").GetComponent<Text>().text = "变性";
        GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        bianxing.programCache = true;
    }
    
    public void select95()
    {
        if(name!="变性")return;
        bianxing.temperatureCache = true;
        GameObject.Find("程序时间").GetComponent<Text>().text = "30";
    }
    public void selecttuihuo()
    {
        if(name!="退火")return;
        GameObject.Find("退火程序文本").GetComponent<Text>().text = "退火";
        GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        tuihuo.programCache = true;
    }
    
    public void select55()
    {
        if(name!="退火")return;
        tuihuo.temperatureCache = true;
        GameObject.Find("程序时间").GetComponent<Text>().text = "30";
    }
    public void selectyanshen()
    {
        if(name!="延伸")return;
        GameObject.Find("延伸程序文本").GetComponent<Text>().text = "延伸";
        GameObject.Find("选温度").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("选温度").GetComponent<CanvasGroup>().blocksRaycasts = true;
        yanshen.programCache = true;
    }
    
    public void select72()
    {
        if(name!="延伸")return;
        yanshen.temperatureCache = true;
        GameObject.Find("程序时间").GetComponent<Text>().text = "60";
    }
}
