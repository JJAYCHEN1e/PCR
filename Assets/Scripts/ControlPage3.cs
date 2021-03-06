using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ControlPage3 : MonoBehaviour
{
    private Text uiText;
    //储存中间值
    private string words;
    //每个字符的显示速度
    private float timer;
    CanvasGroup canvasGroup;
    Animator dnaAnimator;
    AnimatorStateInfo animatorInfo;
    bool animationPlaying;
    GameObject warningWindow;
    Text warningText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log(MissionController.currentMissionIndex);
        ControlMedals.ShowMedalInfo();
        animationPlaying = false;
        canvasGroup = GameObject.Find("Canvas/page3").GetComponent<CanvasGroup>();
        DOTween.To(() => timer, a => timer = a, 1, 1).OnComplete(() => canvasGroup.DOFade(1, 2));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup = GameObject.Find("Canvas/page3/layer0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup = GameObject.Find("Canvas/page3/Image-Taq").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        dnaAnimator = GameObject.Find("DNA_Line").GetComponent<Animator>();
        warningWindow = GameObject.Find("Canvas/page3/warningWindow");
        warningText = GameObject.Find("Canvas/page3/warningWindow/Image/Text").GetComponent<Text>();
        uiText = GameObject.Find("Canvas/page3/layer0/Image/Text").GetComponent<Text>();
        words = "科学家Saiki等在黄石公园温泉中分离的一株水生嗜热杆菌(thermus\u3000aquaticus,\u3000Taq)中提取到一种耐高温DNA聚合酶。该酶耐高温的性质使其热变性时不会被钝化,不必在每次扩增反应后再加新酶,从而极大地提高了PCR扩增的效率。Taq酶的发现,使得PCR真正变为现实,为其自动化铺平了道路。";
        SpeechController.Speak("科学家Saiki等在黄石公园温泉中分离的一株水生嗜热杆菌(thermus aquaticus,Taq)中提取到一种耐高温DNA聚合酶。该酶耐高温的性质使其热变性时不会被钝化,不必在每次扩增反应后再加新酶,从而极大地提高了PCR扩增的效率。Taq酶的发现，使得PCR真正变为现实，为其自动化铺平了道路。接下来让我们使用Taq酶开始自动化PCR实验的操作吧！", false);
        //Debug.Log(isPrint);
        canvasGroup = GameObject.Find("Canvas/page3/layer0/Image").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        //uiText.text = "\u3000\u3000" + words;
        uiText.DOText("\u3000\u3000" + words, 30);
        DOTween.To(() => timer, a => timer = a, 1, 35).OnComplete(() => showButton());
    }

    void showButton()
    {
        canvasGroup = GameObject.Find("Canvas/page3/Button").GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    // Update is called once per frame
    void Update()
    {
        animatorInfo = GameObject.Find("DNA_Line").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if(animationPlaying && animatorInfo.normalizedTime>1.0f && animatorInfo.IsName("circle3-step3"))
        {
            animationPlaying = false;
            dnaAnimator.speed = 0;
            Debug.Log("aminatorEnd");
            success();
        }
    }
    
    void success()
    {
        warningText.text = "恭喜你使用Taq酶完成了PCR扩增的自动化操作!";
        GameObject.Find("Canvas/page3/warningWindow/Image").GetComponent<Image>().color = new Color(0.6F, 0.7F, 0.9F, 1F);
        canvasGroup = warningWindow.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 1);
    }

    public void ClearPage()
    {
        canvasGroup= GameObject.Find("Canvas/page3/layer0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        canvasGroup= GameObject.Find("Canvas/page3/Image-Taq").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        canvasGroup = GameObject.Find("Canvas/page3/Button").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        
        GameObject.Find("Canvas/page3/layer0/Image/Text").GetComponent<Text>().DOText("", 0.1f);
    }

    public void ShowPage()
    {
        ControlMedals.ShowMedalInfo();
        return;
    }
    
    public void NextClick()
    {
        // ClearPage();
        // SceneManager.LoadScene("Scene2");
        GameObject.Find("MissionController").GetComponent<MissionController>().SwitchMission("6");
    }

}
