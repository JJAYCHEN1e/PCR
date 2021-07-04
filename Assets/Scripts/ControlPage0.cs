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
    
    CanvasGroup canvasGroup;
    //GameObject gameObject;
    //private int text_length = 0;
    //private string Ctext;
    // Use this for initialization
    void Start()
    {
        CustomNativeView.InitExitAppButton();
        canvasGroup = GameObject.Find("Canvas/page0/layer0").GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, 2);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        uiText = GameObject.Find("Canvas/page0/layer0/Text-bg/Text").GetComponent<Text>();
        words = "\u3000\u3000某城市突然爆发流感，医生通过鼻咽拭子获取了患者身上的病毒株，为了判断该病毒是否是普通的流行性感冒病毒，医生需要将其遗传物质与已知的流感病毒进行比对。\n\u3000\u3000但直接从患者病毒样本提取的核酸（DNA/RNA）往往不足以进行比对或者测序，这就需要进行遗传物质的数量的扩增，称为聚合酶链式反应（Polymerase Chain Reaction, PCR）";
        SpeechController.Speak("某城市突然爆发流感，医生通过鼻咽拭子获取了患者身上的病毒株，为了判断该病毒是否是普通的流行性感冒病毒，医生需要将其遗传物质与已知的流感病毒进行比对。但直接从患者病毒样本提取的核酸往往不足以进行比对或者测序，这就需要进行遗传物质数量的扩增，称为聚合酶链式反应PCR", false);
        //Debug.Log(isPrint);
        uiText.DOText(words, 28);
        //isPrint = true;
        //Debug.Log(isPrint);

        //speaker.Text = "某城市突然爆发流感，医生通过鼻咽拭子获取了患者身上的病毒株,为了判断该病毒是否是普通的流行性感冒病毒，医生需要将其遗传物质与已知的流感病毒进行比对。但直接从患者病毒样本提取的核酸（DNA/RNA）往往不足以进行比对或者测序，这就需要进行遗传物质的数量的扩增，这就是聚合酶链式反应";

        //speaker.Speak();
        //Debug.Log(Crosstales.RTVoice.Speaker.Cultures.Count);
        DOTween.To(() => timer, a => timer = a, 1, 28).OnComplete(() => SpeechController.Speak("那如何在细胞外实现DNA扩增呢？让我们一起探索吧！"));
        DOTween.To(() => timer, a => timer = a, 1, 28).OnComplete(() => showCanvasThree());
        // DOTween.To(() => timer, a => timer = a, 1, 5).OnComplete(() => showCanvasOne());
        // DOTween.To(() => timer, a => timer = a, 1, 7).OnComplete(() => showCanvasTwo());
        // DOTween.To(() => timer, a => timer = a, 1, 9).OnComplete(() => showCanvasThree());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   

    private void showCanvasOne()
    {
        GameObject gameObject = GameObject.Find("Canvas/page0/layer1");
        
        gameObject.transform.SetAsLastSibling();
        canvasGroup =gameObject.GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2);

    }
    private void showCanvasTwo()
    {
        GameObject gameObject = GameObject.Find("Canvas/page0/layer2");
        gameObject.transform.SetAsLastSibling();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2);
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
        canvasGroup.DOFade(0, 1);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowPage()
    {
        return;
    }
    
    public void enterClick()
    {
        ClearPage();
        DOTween.To(() => timer, a => timer = a, 1, 1).OnComplete(() => showNextPage());
    }
    
    void showNextPage()
    {
        // gameObject.GetComponent<ControlPage0>().enabled = false;
        // ControlPage1 controlPage1 = GameObject.Find("Canvas/page1").GetComponent<ControlPage1>();
        // controlPage1.enabled = true;
        // controlPage1.ShowPage();
        GameObject.Find("MissionController").GetComponent<MissionController>().SwitchMissionInSceneOne(2);
    }
}