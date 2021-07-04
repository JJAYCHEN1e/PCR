using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab : MonoBehaviour
{
    public static bool exampleMode = false;
    public static Text hideOrShow;
    public static string a;

    private GameObject rule;
    // Start is called before the first frame update
    void Start()
    {
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
    void clickExample()
    {
        exampleMode = true;

    }
}
