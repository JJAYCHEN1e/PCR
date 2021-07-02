﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab : MonoBehaviour
{
    public bool showTip;
    public static Text hideOrShow;
    public static string a;

    private GameObject rule;
    // Start is called before the first frame update
    void Start()
    {
        showTip = true;
        rule = GameObject.Find("实验规范");
        hideOrShow = GameObject.Find("hideOrshow").GetComponent<Text>();
        CustomNativeView.InitSceneTwoRuleView();
        CustomNativeView.InitExitAppButton();
        
        SpeechController.Speak("双指单击平面以放置、拖动实验器材");
        UnityToast.ShowBotToast("双指单击平面以放置、拖动实验器材", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void click()
    {
        if(hideOrShow.text == "隐藏")
        {
            rule.SetActive(false);
            hideOrShow.text = "显示实验规范";
        }
        else
        {
            rule.SetActive(true);
            hideOrShow.text = "隐藏";
        }
    }
    // void OnGUI()
    // {
    //     GUI.Label(new Rect(280 , 15, 80,20),"实验规范");
    //     string text = "hide";
    //     GUIStyle s = new GUIStyle();
    //     s.normal.textColor = new Color(256f/256f, 256f/256f, 256f/256f, 256f/256f);
        
    //     GUI.Label(new Rect(280 , 35, 400,35),"1.采样。\n点击模板、原料等试剂盒，移液枪会自动将试剂采样到试管中。",s);
    //     GUI.Label(new Rect(280 , 70, 400,20),"2.打开PCR仪的盖子，将采样完的试管放入PCR仪中。",s);
    //     GUI.Label(new Rect(280 , 90, 400,20),"3.设置循环次数。",s);
    //     GUI.Label(new Rect(280 , 110, 400,20),"4.耐心等待。",s);
    //     GUI.Label(new Rect(280 , 130, 400,20),"5.查看电泳图片",s);
    // }
}
