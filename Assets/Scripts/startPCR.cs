using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class startPCR : MonoBehaviour
{
    public static Text remain,reminder;
    public Sprite[] s = new Sprite[11];
    public Transform tube;
    private string instrution;
    public static int cnt = -3;//玩游戏的次数
    public bool started;
    public bool wait = false;//电泳先显示文字，再图片
    public int n;//pcr cycle
    string[] counter ;
    public int timer;//counter of fixedupdate 

    private TextMesh num;
    private Renderer countdown;
    
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        initComps();
        if (countdown != null) countdown.enabled = false;
        counter = new string[36]{"0","1","2","3","4","5","6","7","8","9","10" ,"11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35"};
        timer = 0;
        instrution = GameObject.Find("首页").GetComponent<Text>().text;
        tube = GameObject.Find("试管").transform;
    }

    private void initComps()
    {
        if (countdown == null && GameObject.Find("倒计时")) countdown = GameObject.Find("倒计时").GetComponent<Renderer>();
        if (num == null && GameObject.Find("num")) num = GameObject.Find("num").GetComponent<TextMesh>();
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
        if(wait)
        {
            if(timer < 200) 
            {
                timer++;
            }
        }
    } 
    
    void OnMouseover()
    {
        
    }
    void OnMouseUp()
    {
        if(!PCR_hat.employed)
        {
            UnityToast.ShowAlert("操作失误", "请先将试管放入 PCR 仪，再启动 PCR 仪");
            SpeechController.Speak("操作失误，请先将试管放入 PCR 仪，再启动 PCR 仪");
            return;
        }
        if(GameObject.Find("剩余循环次数").GetComponent<TextMesh>().text == "剩余循环次数") 
        {
            UnityToast.ShowAlert("提示", "仪器正在运行中，请耐心等待");
            SpeechController.Speak("仪器正在运行中，请耐心等待");
        }
        else
        {
            GameObject.Find("程序顺序").GetComponent<Text>().text = "";
            GameObject.Find("设置程序").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("设置程序").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("设置程序").GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameObject.Find("首页").GetComponent<Text>().text = instrution;
        }
    }
    public void click()//确定循环次数后开始扩增
    {
        n = int.Parse(GameObject.Find("次数").GetComponent<Text>().text);
        int index = n - 25;
        Debug.Log(s[index].name);
        GameObject.Find("电泳").GetComponent<SpriteRenderer>().sprite = s[index];
        GameObject.Find("变性程序文本").GetComponent<Text>().text = "+";
        GameObject.Find("退火程序文本").GetComponent<Text>().text = "+";
        GameObject.Find("延伸程序文本").GetComponent<Text>().text = "+";
        GameObject.Find("循环程序").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("循环程序").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("循环程序").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("设置程序").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("设置程序").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("设置程序").GetComponent<CanvasGroup>().blocksRaycasts = false;
        started = true;
        SpeechController.Speak("PCR 仪已启动，一共需要 " + n + " 次循环。");
    }
    void OnMouseExit()
    {
        
    }
            
    void OnGUI()
    {
        if(wait&&timer==200)
        {
            wait = false;timer=0;
            GameObject.Find("电泳").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("电泳结果").GetComponent<TextMesh>().text = "电泳结果";
            UnityToast.ShowAlert("电泳完成", "电泳完成，请查看 PCR 仪上方的电泳结果。");
            SpeechController.Speak("电泳完成，请查看 PCR 仪上方的电泳结果。你可以点击屏幕下方的按钮再次进行实验。");
            
            GameObject.Find("again").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("again").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("again").GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        if(started)
        {
            initComps();
            if (countdown != null) countdown.enabled = true;
            if(timer == 60) {
                n--;
                timer = 0;
            }
            if(n>=0){
                if (num != null)
                {
                    num.text = counter[n];
                    GameObject.Find("运行中").GetComponent<TextMesh>().text = "运行中";
                    GameObject.Find("剩余循环次数").GetComponent<TextMesh>().text = "剩余循环次数";
                }
            }
            if(n<0){
                started = false;
                wait = true;timer = 0;
                if (countdown) countdown.gameObject.SetActive(false);
                num.text = "";
                GameObject.Find("正在进行电泳").GetComponent<TextMesh>().text = "恭喜你完成了DNA扩增！\n下面进行电泳";
                UnityToast.ShowAlert("DNA 扩增完成", "恭喜你完成了 DNA 扩增！\n下面进行电泳。");
                SpeechController.Speak("恭喜你完成了 DNA 扩增！\n下面进行电泳。");
            }
        }
    }
    public void reset()//一次结束，再玩一次
    {
        SceneManager.LoadScene("Scene2");
        cnt++;
        // dianyongRes.text = "";
        // GameObject.Find("电泳").GetComponent<SpriteRenderer>().enabled = false;
        // GameObject.Find("again").GetComponent<CanvasGroup>().alpha = 0;
        // GameObject.Find("again").GetComponent<CanvasGroup>().interactable = false;
        // GameObject.Find("again").GetComponent<CanvasGroup>().blocksRaycasts = false;
        // primer.employed = false;
        // template.employed = false;
        // polymerase.employed = false;
        // rawMaterial.employed = false;
        // PCR_hat.employed = false;
        // bianxing.programCache = false;
        // bianxing.temperatureCache = false;
        // tuihuo.programCache = false;
        // tuihuo.temperatureCache = false;
        // yanshen.programCache = false;
        // yanshen.temperatureCache = false;
        // //
        // Sequence se = DOTween.Sequence();
        // se.Append(tube.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(-0.05f , 0.05f,0),1.5f));
        // //se.Append(GameObject.Find("试管").transform.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(0f , 0.1f,0),1.5f));
        // //se.Pause();
        // se.Append(tube.DOLocalRotate(new Vector3(0, 0, -60), 2f, RotateMode.WorldAxisAdd));
        // se.Append(tube.DOLocalRotate(new Vector3(0, 0, 60), 2f, RotateMode.WorldAxisAdd));
        // se.Append(tube.DOMove(PCR_hat.tubePos,1.5f));
    }
}
