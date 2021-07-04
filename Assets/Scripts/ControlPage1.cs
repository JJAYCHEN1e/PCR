using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ControlPage1 : MonoBehaviour
{
    static public bool enterPage1;
    bool dnaShow = true;
    float timer;
    Animator dnaAnimator;
    AnimatorStateInfo animatorInfo;
    CanvasGroup canvasGroup;
    GameObject warningWindow;
    Text warningText;
    Text tips;
    //物质+条件
    public static HashSet<string> ChosenCondition = new HashSet<string>();
    // Start is called before the first frame update
    void Start()
    {
        //enterPage1 = false;
        dnaAnimator = GameObject.Find("DNA_Line").GetComponent<Animator>();
        warningWindow = GameObject.Find("Canvas/page1/warningWindow");
        warningText= GameObject.Find("Canvas/page1/warningWindow/Image/Text").GetComponent<Text>();
        tips= GameObject.Find("Canvas/page1/layer0/jieshuoImage/jieshuo").GetComponent<Text>();
        GameObject.Find("Canvas/page1/layer2/Text").GetComponent<Text>().text= "\u3000\u3000"+ "看完DNA体内复制的过程，你是否得到了启发，知道如何构建一个反应体系来实现DNA的体外扩增了呢？来试试吧!";
        enterPage1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enterPage1 && dnaShow)
            DOTween.To(() => timer, a => timer = a, 1, 1).OnComplete(() => showDNAAnimation());
        //Debug.Log(enterPage1);
        //Debug.Log(dnaShow);
        animatorInfo = dnaAnimator.GetCurrentAnimatorStateInfo(0);  
        //Debug.Log(animatorInfo.normalizedTime);
        if (animatorInfo.normalizedTime > 0.8f && animatorInfo.IsName("dnaCopy"))
        {
            //Debug.Log("copy end");
            //dnaAnimator.Play("wait");
            //dnaAnimator.SetBool("dnaCopy", false);
            //canvasGroup = GameObject.Find("DNA_Line").GetComponent<CanvasGroup>();
            //canvasGroup.alpha = 0;
            dnaAnimator.SetBool("dnaCopyFinished", true);
            showCanvasOne();
        }

    }
    void showDNAAnimation()
    {
        enterPage1 = false;
        dnaShow = false;
        //Debug.Log(123);
        //canvasGroup = GameObject.Find("DNA_Line").GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 0;
        dnaAnimator.Play("dnaCopy");
        showText1();
        //dnaAnimator.SetBool("dnaCopy", true);
    }

    private bool f1, f2, f3, f4, f5 = false;
    void showText1()
    {
        
        canvasGroup=GameObject.Find("Canvas/page1/layer0/jieshuoImage").GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, 1);
        // tips.text = "DNA解旋酶解开DNA双链";
        if (!f1)
        {
            f1 = !f1;
            SpeechController.Speak("我们先来观察DNA在细胞内是如何复制的吧！DNA解旋酶解开DNA双链");
            DOTween.To(() => timer, a => timer = a, 1, 7f).OnComplete(() => showText2());
        }
    }
    void showText2()
    {
        // tips.text = "RNA引物结合";
        if (!f2)
        {
            f2 = !f2;
            SpeechController.Speak("引物结合");
            DOTween.To(() => timer, a => timer = a, 1, 4).OnComplete(() => showText3());
        }
    }
    void showText3()
    {
        // tips.text = "DNA聚合酶催化底物dNTP分子聚合形成子代DNA";
        if (!f3)
        {
            f3 = !f3;
            SpeechController.Speak("DNA聚合酶催化底物dNTP分子聚合形成子代DNA");
            DOTween.To(() => timer, a => timer = a, 1, 9).OnComplete(() => showText4());
        }

    }
    void showText4()
    {
        // tips.text = "两条链分别进行半保留复制，其中一条链进行不连续复制";
        if (!f4)
        {
            f4 = !f4;
            SpeechController.Speak("两条链分别进行半保留复制，其中一条链进行不连续复制");
        }
    }
    void showCanvasOne()
    {
        if (!f5)
        {
            f5 = !f5;
            SpeechController.Speak("看完DNA体内复制的过程，你是否得到了启发，知道如何构建一个反应体系来实现DNA的体外扩增了呢？来试试吧!", false);
        }
        
        canvasGroup = GameObject.Find("Canvas/page1/layer0").GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 1);
        canvasGroup = GameObject.Find("Canvas/page1/layer2").GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2);
        canvasGroup= GameObject.Find("Canvas/page1/layer2/Button").GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(1,1));
    }
    
    public void confirmClick()
    {
        if ((ChosenCondition.Count == 3 && ChosenCondition.Contains("模板") && ChosenCondition.Contains("DNA聚合酶")
            && ChosenCondition.Contains("原料(dNTPs)")) || (ChosenCondition.Count == 4 && ChosenCondition.Contains("模板")
            && ChosenCondition.Contains("DNA聚合酶")&& ChosenCondition.Contains("原料(dNTPs)")&& ChosenCondition.Contains("引物")))
        {
            GameObject.Find("Canvas/page1/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
            warningText.text = "DNA双链无法打开，DNA体外扩增还需要高温帮助解旋。请重新选择！";
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1, 1);
            DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0,1));
        }
        else if (ChosenCondition.Count == 4 && ChosenCondition.Contains("模板") && ChosenCondition.Contains("DNA聚合酶")
            && ChosenCondition.Contains("原料(dNTPs)") && ChosenCondition.Contains("高温"))
        {
            GameObject.Find("Canvas/page1/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
            warningText.text = "新链无法合成，DNA体外扩增需要加入引物。请重新选择！";
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1, 1);
            DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        }
        else if (ChosenCondition.Count == 5)
        {
            warningText.text = "恭喜你选择正确，让我们开始做实验吧！";
            GameObject.Find("Canvas/page1/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1, 1);
            DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => showNextPage());
        }
        
        else
        {
            warningText.text = "看来你对DNA复制过程还不太了解哦，让我们再重新来看一遍吧！";
            GameObject.Find("Canvas/page1/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, 1);
            //DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
            canvasGroup = GameObject.Find("Canvas/page1/warningWindow/Button").GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
       
    }
    public void showAnimationAgain()
    {
        canvasGroup = GameObject.Find("Canvas/page1/layer0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup = GameObject.Find("Canvas/page1/layer1").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page1/warningWindow/Button").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        showDNAAnimation();
    }
    public void clickTemplate()
    {
        if (ChosenCondition.Contains("模板") == true)
        {
            ChosenCondition.Remove("模板");
            GameObject.Find("Canvas/page1/layer1/select/template").GetComponent<Image>().color = Color.white;
        }
        else
        {
            ChosenCondition.Add("模板");
            GameObject.Find("Canvas/page1/layer1/select/template").GetComponent<Image>().color = Color.green;
        }
    }
    public void clickPolymerase()
    {
        if (ChosenCondition.Contains("DNA聚合酶") == true)
        {
            ChosenCondition.Remove("DNA聚合酶");
            GameObject.Find("Canvas/page1/layer1/select/polymerase").GetComponent<Image>().color = Color.white;
        }
        else
        {
            ChosenCondition.Add("DNA聚合酶");
            GameObject.Find("Canvas/page1/layer1/select/polymerase").GetComponent<Image>().color = Color.green;
        }
    }
    public void clickRawMaterial()
    {
        if (ChosenCondition.Contains("原料(dNTPs)") == true)
        {
            ChosenCondition.Remove("原料(dNTPs)");
            GameObject.Find("Canvas/page1/layer1/select/rawMaterial").GetComponent<Image>().color = Color.white;
        }
        else
        {
            ChosenCondition.Add("原料(dNTPs)");
            GameObject.Find("Canvas/page1/layer1/select/rawMaterial").GetComponent<Image>().color = Color.green;
        }
    }
    public void clickPrimer()
    {
        if (ChosenCondition.Contains("引物") == true)
        {
            ChosenCondition.Remove("引物");
            GameObject.Find("Canvas/page1/layer1/select/primer").GetComponent<Image>().color = Color.white;
        }
        else
        {
            ChosenCondition.Add("引物");
            GameObject.Find("Canvas/page1/layer1/select/primer").GetComponent<Image>().color = Color.green;
        }
    }
    public void clickHighTemperature()
    {
        if (ChosenCondition.Contains("高温") == true)
        {
            ChosenCondition.Remove("高温");
            GameObject.Find("Canvas/page1/layer1/select/highTemperature").GetComponent<Image>().color = Color.white;
        }
        else
        {
            ChosenCondition.Add("高温");
            GameObject.Find("Canvas/page1/layer1/select/highTemperature").GetComponent<Image>().color = Color.green;
        }
    }

    public void ClearPage()
    {
        canvasGroup = GameObject.Find("Canvas/page1/layer1").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page1/warningWindow").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page1").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowPage()
    {
        canvasGroup = GameObject.Find("Canvas/page1").GetComponent<CanvasGroup>();
        UnityToast.ShowTopToast("首先让我们来观察细胞中DNA的复制过程", 25);
        canvasGroup.DOFade(1, 1);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //ControlPage1.enterPage1 = true;
    }
    
    public void showNextPage()
    {
        // ClearPage();
        // gameObject.GetComponent<ControlPage1>().enabled = false;
        // ControlPage11 controlPage11 = GameObject.Find("Canvas/page11").GetComponent<ControlPage11>();
        // controlPage11.enabled = true;
        // controlPage11.ShowPage();
        GameObject.Find("MissionController").GetComponent<MissionController>().SwitchMissionInSceneOne(3);
    }
}
