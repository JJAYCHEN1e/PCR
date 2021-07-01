using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cycle : MonoBehaviour
{
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
        GameObject.Find("首页").GetComponent<Text>().text = "";
        GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("变性退火延伸").GetComponent<CanvasGroup>().blocksRaycasts = false;
        if(bianxing.programCache && bianxing.temperatureCache && tuihuo.programCache && tuihuo.temperatureCache
        &&yanshen.programCache && yanshen.temperatureCache)
        {
            GameObject.Find("程序顺序").GetComponent<Text>().text = "循环次数在25～35次\n之间为佳";
            GameObject.Find("循环程序").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("循环程序").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("循环程序").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else {
            GameObject.Find("程序顺序").GetComponent<Text>().text = "请先将程序添加完毕!";
            
            GameObject.Find("循环程序").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("循环程序").GetComponent<CanvasGroup>().interactable = false;
            GameObject.Find("循环程序").GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    public void increase()
    {
        int num = int.Parse(GameObject.Find("次数").GetComponent<Text>().text);
        if(num<35){
            num++;
            GameObject.Find("次数").GetComponent<Text>().text = num.ToString();
        }
    }
    public void decrease()
    {
        int num = int.Parse(GameObject.Find("次数").GetComponent<Text>().text);
        if(num>25){
            num--;
            GameObject.Find("次数").GetComponent<Text>().text = num.ToString();
        }
    }
}
