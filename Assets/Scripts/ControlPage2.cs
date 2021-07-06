using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ControlPage2 : MonoBehaviour
{
    Animator dnaAnimator;
    Animator polAnimator;
    GameObject warningWindow;
    Text warningText;
    AnimatorStateInfo animatorInfo;
    CanvasGroup canvasGroup;
    //int circle = 1;
    int step = 1;
    float timer;
    bool animationPlaying = false;
    public static bool targetAdded = false;
    public static bool ATCGAdded = false;
    public static bool PrimeAdded = false;
    public static bool DNApolAdded = false;
    Text temp;
    Text circle;
    Button temp5btn;
    Button temp7btn;
    Button temp9btn;
    
    private bool canceled = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        
        canceled = false;
        MissionController.sceneSwitchedEvent += () => canceled = true;

        Debug.Log(MissionController.currentMissionIndex);
        ControlMedals.ShowMedalInfo();
        dnaAnimator = GameObject.Find("DNA_Line").GetComponent<Animator>();
        //dnaAnimator.Play("Target_wait");
        warningWindow = GameObject.Find("Canvas/page2/warningWindow");
        warningText = GameObject.Find("Canvas/page2/warningWindow/Image/Text").GetComponent<Text>();
        temp = GameObject.Find("Canvas/youshangjiao/Temp").GetComponent<Text>();
        circle = GameObject.Find("Canvas/youshangjiao/circle").GetComponent<Text>();
        GameObject.Find("Canvas/page2/nextPageTip/Image/Text").GetComponent<Text>().text =
            "\u3000\u3000恭喜你，完成了3次DNA体外扩增，现在我们获得了目标DNA。但是目的基因量依旧不足，" +
            "需要再次加入DNA聚合酶循环操作。你是否觉得这样的步骤很繁琐，有什么好的方法能简化操作呢？让我们继续探索吧！";
        canvasGroup = GameObject.Find("Canvas/yuanliao").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup = GameObject.Find("Canvas/youshangjiao").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup = GameObject.Find("Canvas/page2").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        GameObject.Find("ATCG").GetComponent<Animator>().enabled = true;
        GameObject.Find("Prime").GetComponent<Animator>().enabled = true;
        polAnimator = GameObject.Find("DNA_pol").GetComponent<Animator>();
        polAnimator.enabled = true;
        temp5btn = GameObject.Find("Temp5").GetComponent<Button>();
        temp7btn = GameObject.Find("Temp7").GetComponent<Button>();
        temp9btn = GameObject.Find("Temp9").GetComponent<Button>();
        
        SpeechController.Speak("在这个任务中，让我们来看看PCR的过程到底是怎样进行的吧！请先加入样品DNA，调节反应体系温度为95°C", "在这个任务中，让我们来看看PCR的过程到底是怎样进行的吧！请先加入样品DNA，条节反应体系温度为95°", 999);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetAdded)
            GameObject.Find("Image-target").GetComponent<ControlDrag>().enabled = false;
        animatorInfo = dnaAnimator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(animationPlaying+":"+animatorInfo.normalizedTime);
        if (animationPlaying && animatorInfo.normalizedTime > 0.85f && animatorInfo.normalizedTime<1.0f)
        {
            //Debug.Log(animatorInfo.normalizedTime);
            Debug.Log("end");
            animationPlaying = false;
            canvasGroup = GameObject.Find("Canvas/page2/layer0").GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(1, 1);
            if (step == 1)
                DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() =>
                {
                    if (!canceled)
                    {
                        step1animatorEnd();
                    }
                });
            else if (step == 2)
                step2animatorEnd();
            else if (step == 3)
                step3animatorEnd();
            else if (step == 4)
                step4animatorEnd();
            else if (step == 5)
                step5animatorEnd();
            else if (step == 6)
                step6animatorEnd();
            else if (step == 7)
                step7animatorEnd();
            else if (step == 8)
                step8animatorEnd();
            else if (step == 9)
                DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() =>
                {
                    if (!canceled)
                    {
                        step9animatorEnd();
                    }
                });
        }
    }

    public void Temp5()
    {
        if (step==2)
        {
            if (!PrimeAdded)
            {
                // warningText.text = "还未加入引物，不能继续！";
                UnityToast.ShowAlert("操作错误", "还未加入引物，不能继续！");
                SpeechController.Speak("还未加入引物，不能继续！", false);
                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
            dnaAnimator.Play("step2");  
        }
        else if (step==5)
        {
            dnaAnimator.Play("circle2-step2");
        }
        else if (step==8)
        {
            dnaAnimator.Play("circle3-step2");
        }
        else if (step==1||step==4||step==7)
        {
            // warningText.text = "请调整至指定温度！";
            UnityToast.ShowAlert("操作错误", "请整至指定温度！");
            SpeechController.Speak("请调整至指定温度！", "请条整至指定温度！", false);
            //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, 1);
            DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
            return;
        }
        temp.text = "当前温度：52°C";
        circle.text = "当前阶段：退火";
        animationPlaying = true;
        temp5btn.interactable = false;
        temp7btn.interactable = true;
        temp9btn.interactable = true;
        canvasGroup = GameObject.Find("Canvas/page2/layer0").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 1);
    }
    public void Temp7()
    {
        if(step==3)
        {
            if (!DNApolAdded&&!ATCGAdded)
            {
                // warningText.text = "还未加入DNA聚合酶和dNTPs反应原料！";
                UnityToast.ShowAlert("操作错误", "还未加入DNA聚合酶和dNTPs反应原料！");
                SpeechController.Speak("还未加入DNA聚合酶和dNTPs反应原料！", false);
                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
            else if(!DNApolAdded)
            {
                // warningText.text = "还未加入DNA聚合酶！";
                UnityToast.ShowAlert("操作错误", "还未加入DNA聚合酶！");
                SpeechController.Speak("还未加入dNTPs反应原料！", false);
                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
            else if (!ATCGAdded)
            {
                // warningText.text = "还未加入dNTPs反应原料！";
                UnityToast.ShowAlert("操作错误", "还未加入dNTPs反应原料！");
                SpeechController.Speak("还未加入dNTPs反应原料！", false);
                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
           
            dnaAnimator.Play("step3");
        }
        else if (step==6)
        {
            if (!DNApolAdded)
            {
                // warningText.text = "DNA聚合酶已失活，请重新加入！";
                SpeechController.Speak("DNA聚合酶已失活，请重新加入！", false);
                UnityToast.ShowAlert("操作失误", "DNA聚合酶已失活，请重新加入！");

                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
            dnaAnimator.Play("circle2-step3");
        }
        else if (step==9)
        {
            if (!DNApolAdded)
            {
                // warningText.text = "DNA聚合酶已失活，请重新加入！";
                SpeechController.Speak("DNA聚合酶已失活，请重新加入！", false);
                UnityToast.ShowAlert("操作失误", "DNA聚合酶已失活，请重新加入！");
                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
            dnaAnimator.Play("circle3-step3");
        }
        else if (step==2||step==5||step==8||step==1)
        {
            // warningText.text =  "请调整至指定温度！";
            UnityToast.ShowAlert("操作错误", "请调整至指定温度！");
            SpeechController.Speak("请调整至指定温度！", "请条整至指定温度！", false);
            //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, 1);
            DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
            return;
        }
        temp.text = "当前温度：72°C";
        circle.text = "当前阶段：延伸";
        animationPlaying = true;
        temp5btn.interactable = true;
        temp7btn.interactable = false;
        temp9btn.interactable = true;
        canvasGroup = GameObject.Find("Canvas/page2/layer0").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 1);
    }
    public void Temp9()
    {
        if (step == 1)
        {
            if (!targetAdded)
            {
                // warningText.text ="还未加入目标DNA，不能继续！";
                UnityToast.ShowAlert("操作错误", "还未加入目标DNA，不能继续！");
                SpeechController.Speak("还未加入目标DNA，不能继续！", false);
                //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
                canvasGroup = warningWindow.GetComponent<CanvasGroup>();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, 1);
                DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
                return;
            }
            dnaAnimator.Play("dna_jielian");
        }
        else if (step==3||step==6||step==9)
        {
            // warningText.text =  "请调整至指定温度！";
            UnityToast.ShowAlert("操作错误", "请调整至指定温度！");
            SpeechController.Speak("请调整至指定温度！", "请条整至指定温度！", false);
            //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
            canvasGroup = warningWindow.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, 1);
            DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
            return;
        }
        else if (step==4)
        {
            dnaAnimator.Play("circle2-step1");
        }
        else if (step==7)
        {
            dnaAnimator.Play("circle3-step1");
        }
        DNApolAdded = false;
        polAnimator.Play("wait");
        temp.text = "当前温度：95°C";
        circle.text = "当前阶段：变性";
        animationPlaying = true;
        temp5btn.interactable = true;
        temp7btn.interactable = true;
        temp9btn.interactable = false;
        canvasGroup = GameObject.Find("Canvas/page2/layer0").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 1);
    }
    
    void step1animatorEnd()
    {
        Debug.Log("step1end");
        step = 2;
        // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请加入引物，并调节反应温度为52°C";
        if (!PrimeAdded)
        {
            // warningText.text = "双链已解开，但操作无法继续，因为新链无法合成，请加入引物!";
            SpeechController.Speak("双链已解开，但操作无法继续，因为新链无法合成，请加入引物，并调节反应温度为52°C", "双链已解开，但操作无法继续，因为新链无法合成，请加入引物，并条节反应温度为52°", 999);
        }
        else
        {
            // warningText.text = "双链已解开，且已加入了引物，请调节反应体系温度!";
            SpeechController.Speak("双链已解开，且已加入了引物，请调节反应体系温度为52°C", "双链已解开，且已加入了引物，请条节反应体系温度为52°", false);
        }
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
    }
    void step2animatorEnd()
    {
        Debug.Log("step2end");
        step = 3;
        if (!DNApolAdded&&!ATCGAdded)
        {
            // warningText.text = "引物已结合，但操作无法继续，因为缺少DNA聚合酶和反应原料!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请加入DNA聚合酶和反应原料，调节反应体系的温度72°C";
            SpeechController.Speak("引物已结合，但操作无法继续，因为缺少DNA聚合酶和反应原料!请加入DNA聚合酶和反应原料，调节反应体系的温度为72°C", "引物已结合，但操作无法继续，因为缺少DNA聚合酶和反应原料!请加入DNA聚合酶和反应原料，条节反应体系的温度为72°", 999);
        }
        else if (!DNApolAdded)
        {
            // warningText.text = "引物已结合，但缺少DNA聚合酶!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请确认已加入DNA聚合酶，并调节反应温度为72°C";
            SpeechController.Speak("引物已结合，但缺少DNA聚合酶!请确认已加入DNA聚合酶，并调节反应温度为72°C", "引物已结合，但缺少DNA聚合酶!请确认已加入DNA聚合酶，并条节反应温度为72°", 999);
        }
        else if (!ATCGAdded)
        {
            // warningText.text = "引物已结合，但缺少反应原理!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请确认已加入反应原料，并条节反应温度为72°C";
            SpeechController.Speak("引物已结合，但缺少反应原理!请确认已加入反应原料，并调节反应温度为72C°", "引物已结合，但缺少反应原理!请确认已加入反应原料，并条节反应温度为72°", 999);
        }
        else
        {
            // warningText.text = "引物已结合，请继续操作!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请加入DNA聚合酶和反应原料，调节反应体系的温度72°C";
            SpeechController.Speak("引物已结合，请继续操作!请加入DNA聚合酶和反应原料，调节反应体系的温度72°C", "引物已结合，请继续操作!请加入DNA聚合酶和反应原料，条节反应体系的温度72°", 999);
        }
        
        
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        
        //DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() => step3());
    }
    void step3animatorEnd()
    {
        Debug.Log("step3end");
        step = 4;
        // warningText.text = "已经成功完成第一轮复制，但还未得到目标DNA，请继续！";
        // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请调节反应体系温度为95°C";
        SpeechController.Speak("已经成功完成第一轮复制，但还未得到目标DNA，请继续！请调节反应体系温度为95°C", "已经成功完成第一轮复制，但还未得到目标DNA，请继续！请条节反应体系温度为95°", 999);
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() => step4());
    }
    void step4animatorEnd()
    {
        Debug.Log("step4end");
        step = 5;
        // warningText.text = "双链已打开，请继续操作!";
        // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请调节反应温度为52°C";
        SpeechController.Speak("双链已打开，请继续操作!请调节反应温度为52°C", "双链已打开，请继续操作!请条节反应温度为52°", 999);
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() => step5());
    }
    void step5animatorEnd()
    {
        Debug.Log("step5end");
        step = 6;
        if (!DNApolAdded)
        {
            // warningText.text = "引物已结合，但操作无法继续，因为DNA聚合酶已在高温下失活!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请重新加入DNA聚合酶，并调节反应温度为72°C";
            SpeechController.Speak("引物已结合，但操作无法继续，因为DNA聚合酶已在高温下失活!请重新加入DNA聚合酶，并调节反应温度为72°C", "引物已结合，但操作无法继续，因为DNA聚合酶已在高温下失活!请重新加入DNA聚合酶，并条节反应温度为72°", 999);
        }
        else
        {
            // warningText.text = "引物已结合，请继续操作!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请确认已重新加入DNA聚合酶，并调节反应温度为72°C";
            SpeechController.Speak("引物已结合，请继续操作!请确认已重新加入DNA聚合酶，并调节反应温度为72°C", "引物已结合，请继续操作!请确认已重新加入DNA聚合酶，并条节反应温度为72°", 999);
        }
        
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //GameObject.Find("Canvas/page2/warningWindow/Button/Text").GetComponent<Text>().text = "重新加入";
        //canvasGroup = GameObject.Find("Canvas/page2/warningWindow/Button").GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 1;
        //canvasGroup.interactable = true;
        //canvasGroup.blocksRaycasts = true;
    }
    void step6animatorEnd()
    {
        Debug.Log("step6end");
        step = 7;
        // warningText.text = "已经成功完成第二轮复制，但还未得到目标DNA，请继续！";
        // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请调节反应体系温度为95°C";
        SpeechController.Speak("已经成功完成第二轮复制，但还未得到目标DNA，请继续！请调节反应体系温度为95°C", "已经成功完成第二轮复制，但还未得到目标DNA，请继续！请条节反应体系温度为95°", 999);
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() => step7());
    }
    void step7animatorEnd()
    {
        Debug.Log("step7end");
        step = 8;
        // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请调节反应温度为52°C";
        SpeechController.Speak("双链已打开，请继续操作!请调节反应温度为52°C", "双链已打开，请继续操作!请条节反应温度为52°", 999);
        // warningText.text = "双链已打开，请继续操作!";
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() => step8());
    }
    void step8animatorEnd()
    {
        Debug.Log("step8end");
        step = 9;
        if (!DNApolAdded)
        {
            // warningText.text = "引物已结合，但操作无法继续，因为DNA聚合酶已在高温下失活!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请重新加入DNA聚合酶，并调节反应温度为72°C";
            SpeechController.Speak("引物已结合，但操作无法继续，因为DNA聚合酶已在高温下失活!请重新加入DNA聚合酶，并条节反应温度为72°C",
                                    "引物已结合，但操作无法继续，因为DNA聚合酶已在高温下失活!请重新加入DNA聚合酶，并条节反应温度为72°", 999);
        }
        else
        {
            // warningText.text = "引物已结合，请继续操作!";
            // GameObject.Find("Canvas/page2/layer0/Text").GetComponent<Text>().text = "请确认已重新加入DNA聚合酶，并调节反应温度为72°C";
            SpeechController.Speak("引物已结合，请继续操作!请确认已重新加入DNA聚合酶，并条节反应温度为72°C",
                                    "引物已结合，请继续操作!请确认已重新加入DNA聚合酶，并条节反应温度为72°", 999);
        }
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.9F, 0.6F, 0.3F, 0.7F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
        DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //GameObject.Find("Canvas/page2/warningWindow/Button/Text").GetComponent<Text>().text = "重新加入";
        //canvasGroup = GameObject.Find("Canvas/page2/warningWindow/Button").GetComponent<CanvasGroup>();
        //canvasGroup.alpha = 1;
        //canvasGroup.interactable = true;
        //canvasGroup.blocksRaycasts = true;
    }
    void step9animatorEnd()
    {
        Debug.Log("step9end");
        //warningText.text = "恭喜你完成了第三轮复制，并且得到了目标DNA！";
        //GameObject.Find("Canvas/page2/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        //canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        ControlMedals.GetMedal("2");
        SpeechController.Speak("恭喜你，完成了3次DNA体外扩增，又点亮了一枚奖牌，现在我们获得了目标DNA。但是目的基因量依旧不足，需要再次加入DNA聚合酶循环操作。你是否觉得这样的步骤很繁琐，有什么好的方法能简化操作呢？让我们继续探索吧！", false);
        canvasGroup = GameObject.Find("Canvas/page2/nextPageTip").GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        canvasGroup = GameObject.Find("Canvas/page2/layer0").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        //dnaAnimator.Play("wait");
        //DOTween.To(() => timer, a => timer = a, 1, 2).OnComplete(() => canvasGroup.DOFade(0, 1));
        //DOTween.To(() => timer, a => timer = a, 1, 3).OnComplete(() => step4());
    }

    public void ClearPage()
    {
        GameObject.Find("DNA_Line").GetComponent<Animator>().Play("wait");
        canvasGroup = GameObject.Find("Canvas/page2/layer1").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page2/warningWindow").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page2").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/yuanliao").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/youshangjiao").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup = GameObject.Find("Canvas/page2/nextPageTip").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;

        GameObject.Find("ATCG").GetComponent<Animator>().Play("wait");
        GameObject.Find("Prime").GetComponent<Animator>().Play("wait");
        GameObject.Find("DNA_pol").GetComponent<Animator>().Play("wait");
        //GameObject.Find("").GetComponent<Animator>().Play("wait");
    }

    public void ShowPage()
    {
        ControlMedals.ShowMedalInfo();
        return;
    }

    public void nextPageClick()
    {
        // ClearPage();
        // gameObject.GetComponent<ControlPage2>().enabled = false;
        // ControlPage3 controlPage3 = GameObject.Find("Canvas/page3").GetComponent<ControlPage3>();
        // controlPage3.enabled = true;
        // controlPage3.ShowPage();
        
        
        
        GameObject.Find("MissionController").GetComponent<MissionController>().SwitchMission("5");
    }
}
