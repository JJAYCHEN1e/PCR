using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class startPCR : MonoBehaviour
{
    public static Text uiText,run,num,reminder,screen;
    public GameObject inputField,submit,chooseTemperature;
    public static bool[] set = new bool[4];
    private string text;
    public float x;
    public float y;
    public bool started,sure;
    public int n;//pcr cycle
    string[] counter ;
    public int timer;//counter of fixedupdate 
    Rect rect;
    
    // Start is called before the first frame update
    void Start()
    {
        set[0] =set[1]=set[2]=set[3]= false;
        text = "";
        started = false;sure=false;
        GameObject.Find("倒计时").GetComponent<Renderer>().enabled = false;
        inputField = GameObject.Find("输入循环次数");
        inputField.GetComponent<CanvasGroup>().alpha = 0;
        inputField.GetComponent<CanvasGroup>().interactable = false;
        inputField.GetComponent<CanvasGroup>().blocksRaycasts = false;
        submit = GameObject.Find("确认");
        submit.GetComponent<CanvasGroup>().alpha = 0;
        submit.GetComponent<CanvasGroup>().interactable = false;
        submit.GetComponent<CanvasGroup>().blocksRaycasts = false;
        chooseTemperature = GameObject.Find("选温度");

        run = GameObject.Find("run").GetComponent<Text>();
        num = GameObject.Find("num").GetComponent<Text>();
        reminder = GameObject.Find("提示").GetComponent<Text>();
        screen = GameObject.Find("屏幕").GetComponent<Text>();
        
        counter = new string[36]{"0","1","2","3","4","5","6","7","8","9","10" ,"11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35"};
        timer = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            if(timer < 60) 
            {
                timer++;
            }
            else timer = 0;
        }
    } 
    void FixedUpdate()
    {
        
    }
    void OnMouseover()
    {
        
    }
    void OnMouseUp()
    {
        if(!primer.employed || !template.employed || !polymerase.employed || !rawMaterial.employed)
        {
            reminder.text = "请将所有试剂准备完成\n再启动PCR仪";
            return;
        }
        if(run.text == "剩余循环次数") 
        {
            Debug.Log("running");
            reminder.text = "仪器正在运行中\n请耐心等待";
        }
        else
        {
            set[0] = true;
        }
    }
    void OnMouseExit()
    {
        if(reminder.text == "请将所有试剂准备完成\n再启动PCR仪"|| reminder.text == "仪器正在运行中\n请耐心等待")reminder.text="";
    }
            
    void OnGUI()
    {
        if(set[0]==true)
        {
            //变性
            screen.text = "变性";
            reminder.text = "请选择变性温度\n选错则无法继续哦";
            chooseTemperature.GetComponent<CanvasGroup>().alpha = 1;
            chooseTemperature.GetComponent<CanvasGroup>().interactable = true;
            chooseTemperature.GetComponent<CanvasGroup>().blocksRaycasts = true;
            if(GameObject.Find("95").GetComponent<Toggle>().isOn == true)
            {
                set[1]=true;
                set[0]=false;
            }
        }
        if(set[1]==true)
        {
            screen.text = "退火";
            reminder.text = "请选择退火温度\n选错则无法继续哦";
            if(GameObject.Find("55").GetComponent<Toggle>().isOn == true)
            {
                set[2]=true;
                set[1]=false;
            }
        }
        if(set[2]==true)
        {
            screen.text = "延伸";
            reminder.text = "请选择延伸温度\n选错则无法继续哦";
            if(GameObject.Find("72").GetComponent<Toggle>().isOn == true)
            {
                set[3]=true;
                set[2]=false;
            }
        }     
        if(set[3]==true)  
        {
            screen.text = "";
            reminder.text = "设置循环次数\n请输入25～35之间的整数";
            chooseTemperature.GetComponent<CanvasGroup>().alpha = 0;
            chooseTemperature.GetComponent<CanvasGroup>().interactable = false;
            chooseTemperature.GetComponent<CanvasGroup>().blocksRaycasts = false;
            inputField.GetComponent<CanvasGroup>().alpha = 1;
            inputField.GetComponent<CanvasGroup>().interactable = true;
            inputField.GetComponent<CanvasGroup>().blocksRaycasts = true;
            submit.GetComponent<CanvasGroup>().alpha = 1;
            submit.GetComponent<CanvasGroup>().interactable = true;
            submit.GetComponent<CanvasGroup>().blocksRaycasts = true;
            text = GameObject.Find("cycles").GetComponent<Text>().text;
            List<string> valid = new List<string>(){"25","26","27","28","29","30","31","32","33","34","35"};
            if(sure)
            {
                sure = false;
                if(valid.Contains(text))
                {
                    reminder.text = "";
                    inputField.GetComponent<CanvasGroup>().alpha = 0;
                    inputField.GetComponent<CanvasGroup>().interactable = false;
                    inputField.GetComponent<CanvasGroup>().blocksRaycasts = false;
        
                    submit.GetComponent<CanvasGroup>().alpha = 0;
                    submit.GetComponent<CanvasGroup>().interactable = false;
                    submit.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    started = true;

                    screen.DOText("运行中",3);
                    n = int.Parse(text);
                    set[3]=false;
                }
            }  
        }           
    
        if(started)
        {
            GameObject.Find("倒计时").GetComponent<Renderer>().enabled = true;
            run.text = "剩余循环次数";
            if(timer == 60) {
                n--;
                timer = 0;
            }
            if(n>=0){
                num.text = counter[n];
            }
            if(n<0){
                started = false;
                screen.text="";
                GameObject.Find("倒计时").GetComponent<Renderer>().enabled = false;
                run.text = "";
                num.text = "";
                reminder.text = "";
                uiText = GameObject.Find("电泳结果").GetComponent<Text>();
                uiText.DOText("\u3000\u3000" + "电泳结果", 2);
                GameObject.Find("电泳").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void clickSure()
    {
        sure = true;
    }
}
