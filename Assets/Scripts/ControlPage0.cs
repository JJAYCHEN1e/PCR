using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Crosstales.RTVoice.Tool;
public class ControlPage0 : MonoBehaviour
{
    private Text uiText;
    //储存中间值
    private string words;
    //每个字符的显示速度
    private float timer;

    private bool canceled = false;
    
    CanvasGroup canvasGroup;
    
    // Use this for initialization
    void Start()
    {
    }

    private void OnEnable()
    {
        
        canceled = false;
        MissionController.sceneSwitchedEvent += () => canceled = true;
        
        Debug.Log("load_page0");
        //ControlMedals.ShowMedalInfo();
        CustomNativeView.InitExitAppButton();
        canvasGroup = GameObject.Find("Canvas/page0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup = GameObject.Find("Canvas/page0/layer0").GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, 2);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        uiText = GameObject.Find("Canvas/page0/layer0/Text-bg/Text").GetComponent<Text>();
        words = "\u3000\u3000某城市突然爆发流感，医生通过鼻咽拭子获取了患者身上的病毒株，为了判断该病毒是否是普通的流行性感冒病毒，医生需要将其遗传物质与已知的流感病毒进行比对。\n\u3000\u3000但直接从患者病毒样本提取的核酸（DNA/RNA）往往不足以进行比对或者测序，这就需要进行遗传物质的数量的扩增，称为聚合酶链式反应（Polymerase Chain Reaction, PCR）";
        SpeechController.Speak("某城市突然爆发流感，医生通过鼻咽拭子获取了患者身上的病毒株，为了判断该病毒是否是普通的流行性感冒病毒，医生需要将其遗传物质与已知的流感病毒进行比对。但直接从患者病毒样本提取的核酸往往不足以进行比对或者测序，这就需要进行遗传物质数量的扩增，称为聚合酶链式反应PCR", false);
        //Debug.Log(isPrint);
        uiText.DOText(words, 28);
        
        DOTween.To(() => timer, a => timer = a, 1, 29).OnComplete(() =>
        {
            if (!canceled) SpeechController.Speak("那如何在细胞外实现DNA扩增呢？让我们一起探索吧！");
        });
        DOTween.To(() => timer, a => timer = a, 1, 29).OnComplete(() =>
        {
            if (!canceled)
            {
                showCanvasThree();
            }
        });

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void showCanvasThree()
    {
        GameObject gameObject = GameObject.Find("Canvas/page0/layer3");
        gameObject.transform.SetAsLastSibling();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public void ClearPage()
    {
        canvasGroup= GameObject.Find("Canvas/page0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page0/layer0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        GameObject.Find("Canvas/page0/layer0/Text-bg/Text").GetComponent<Text>().DOText("", 0.5f);
    }

    public void ShowPage()
    {
        ControlMedals.ShowMedalInfo();
        return;
    }
    
    public void enterClick()
    {
        ClearPage();
        DOTween.To(() => timer, a => timer = a, 1, 1).OnComplete(() =>
        {
            if (!canceled)
            {
                showNextPage();
            }
        });
    }
    
    void showNextPage()
    {
        GameObject.Find("MissionController").GetComponent<MissionController>().SwitchMission("2");
    }
}