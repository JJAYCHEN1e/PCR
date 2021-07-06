using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ControlMedals : MonoBehaviour
{

    public GameObject cup;
    public static CanvasGroup canvasGroup_medals;
    public static CanvasGroup canvasGroup_cup;
    public static CanvasGroup[] canvasGroup_medal_list=new CanvasGroup[3];
    public static float[] medal_list=new float[3] { 0.3f,0.3f,0.3f};
    public static bool task1 = false;
    public static bool task2 = false;
    public static bool task3 = false;
    bool showed = false;
    

    private void Awake()
    {
        canvasGroup_cup = GameObject.Find("cup").GetComponent<CanvasGroup>();
        canvasGroup_medals = GameObject.Find("medals").GetComponent<CanvasGroup>();
        canvasGroup_medal_list[0] = GameObject.Find("medal1").GetComponent<CanvasGroup>();
        canvasGroup_medal_list[1] = GameObject.Find("medal2").GetComponent<CanvasGroup>();
        canvasGroup_medal_list[2] = GameObject.Find("medal3").GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(MissionController.currentMissionIndex);
        if (task1 && task2 && task3 && !showed)
        {
            showed = true;
            var time = 0f;
            DOTween.To(() => time, a => time = a, 1, 3).OnComplete(() => Win());
        }
        
    }

    //用于各页面初始化时控制奖牌显示
    public static void ShowMedalInfo()
    {
        Debug.Log(7052761061);
        if (MissionController.currentMissionIndex < 3)//page0和page1不显示
        {   
            Debug.Log(7052761062);
            canvasGroup_medals.alpha = 0;
        }
        else
        {
            Debug.Log(7052761063);
            canvasGroup_medals.alpha = 1;
            //根据当前任务完成度显示
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(7052761064);
                canvasGroup_medal_list[i].alpha = medal_list[i];
            }
        }
    }

    //完成任务，点亮相应奖牌
    public static void GetMedal(string taskstr)
    {
        int task = int.Parse(taskstr);
        switch (task)
        {
            case 1:
                canvasGroup_medal_list[0].DOFade(1, 2);
                task1 = true;
                medal_list[0] = 1;
                break;
            case 2:
                canvasGroup_medal_list[1].DOFade(1, 2);
                task2 = true;
                medal_list[1] = 1;
                break;
            case 3:
                var timer=0f;
                DOTween.To(() => timer, a => timer = a, 1, 5).OnComplete(() =>
                {
                    canvasGroup_medal_list[2].DOFade(1, 2);
                    medal_list[2] = 1;
                    task3 = true;
                    SpeechController.Speak("恭喜你点亮了一枚奖牌！");
                });
                break;
        }
    }

    public void GetMedalNonStatic(string taskstr)
    {
        ControlMedals.GetMedal(taskstr);
    }

    //通关之后显示奖杯，由小变大
    void Win()
    {
        canvasGroup_cup.alpha = 1;
        Vector3 originalScale = cup.transform.localScale;
        Vector3 targetScale = new Vector3(1, 1, 1);
        DOTween.To(() => originalScale, x => cup.transform.localScale = x, targetScale, 2);
        SpeechController.Speak("恭喜你完成了所有任务，送你一个奖杯，请继续努力！");
    }

}
