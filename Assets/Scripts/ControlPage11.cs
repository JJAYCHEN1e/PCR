using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System; 
using Crosstales.RTVoice.Tool;
public class ControlPage11 : MonoBehaviour
{
    public GameObject dropd;
    public GameObject btn;
    Dropdown dpn;
    public static Image thermometer;
    float temp;
    float temp_timer;//计时器示数
    int temp_timer2;//控制温度计液面
    float duration;//计时长度
    bool thermometerUp, thermometerDown;
    float src, tar;
    
    
    CanvasGroup canvasGroup;
    //public SpeechText speaker;
    //GameObject gameObject;
    //private int text_length = 0;
    //private string Ctext;
   
    // Update is called once per frame
    //用于解旋前，上下为一对
    List<GameObject> dna_above = new List<GameObject>();
    List<GameObject> dna_below = new List<GameObject>();
    List<GameObject> keyList = new List<GameObject>();
    


    //用于prefab实例化
    public GameObject A_above;
    public GameObject T_above;
    public GameObject C_above;
    public GameObject G_above;
    public GameObject A_below;
    public GameObject T_below;
    public GameObject C_below;
    public GameObject G_below;
    public GameObject DNA_pol;
    //public Slider slide;
    //所有dna模型的父对象
    GameObject dna_line;
    Transform t;
    Vector3 p;
    //实例化prefab的中间过渡
    GameObject target;
    string prefabNamestr;
    //用于随机生成碱基
    System.Random rd = new System.Random();
    //左右碱基的间隔
    float x_space = 0.03f;
    //上下碱基的间隔
    float y_space = 0.025f;
    //DNA链的扭转角度
    float x_rotation = 20f;
    //DNA链的长度
    int dna_length = 40;
    //引物长度
    //int prime_length = 5;
    //缩放尺寸
    float reduce_scale = 1f;
    //识别每个prefab下的标记为dntp的子对象，用于颤动
    //GameObject[] dntps;
    List<Transform> dntps = new List<Transform>();
    //每个标记为dntp的子对象的原始localPosition
    List<Vector3> positions = new List<Vector3>();
    //颤动的随机系数
    System.Random rd_shake = new System.Random();
    //颤动的基准系数
    float shake_speed = 0.002f;
    // Use this for initialization
    void Start()
    {
        thermometer = GameObject.Find("温度计液面").GetComponent<Image>();
        thermometerUp = thermometerDown = false;

        //up = GameObject.Find("up");
        //down = GameObject.Find("down");
        //fifty.SetActive(false);
        //seventy.SetActive(false);
        //ninety.SetActive(false);
        temp_timer = 0;
        duration = 0;
        temp_timer2 = 0;
        dropd = GameObject.Find("Dropdown");
        dpn = dropd.GetComponent<Dropdown>();
        Dropdown.OptionData data1;
        for (int i=60;i<=95;i+=5)
        {
            data1 = new Dropdown.OptionData();
            data1.text = i.ToString() + "°C";
            //optionDatas.Add(data1);
            dpn.options.Add(data1);
        }
        canvasGroup = GameObject.Find("Canvas/page11").GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, 2);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup = GameObject.Find("Canvas/page11/layer0").GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1, 2);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        dna_line = GameObject.Find("DNA");
        t = dna_line.GetComponent<Transform>();
        //t.localScale=Vector3.one * reduce_scale;
        p = t.position;

        t.transform.position -= Vector3.right * x_space * dna_length / 2;//居于视野中央
        CreateNewLine();
        SpeechController.Speak("第一个小任务，先来探索一下温度对DNA双链的影响吧！你可以设置不同温度来观察DNA的变化情况。");
    }


    

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dntps.Count; i++)
        {
            //xyz轴都设置颤动
            dntps[i].localPosition = positions[i] + new Vector3((float)(rd_shake.NextDouble() * shake_speed), (float)(rd_shake.NextDouble() * shake_speed), (float)(rd_shake.NextDouble() * shake_speed));
        }
       
        
        
    }

    private void FixedUpdate()
    {
        //if (temp_timer < duration )
        //{
        //    temp_timer+=0.4f;
        //    Debug.Log(temp_timer);
            
        //}
        if (thermometerUp)
        {
            if (temp_timer2 < 10)
                temp_timer2++;
            else
            {
                
                temp_timer2 = 0;
                if (src == tar)
                {
                    thermometerUp = false;
                    btn.SetActive(true);
                }
                else
                {
                    src++;
                    thermometer.fillAmount = src/100f;
                    GameObject.Find("temperature").GetComponent<Text>().text = Mathf.Round(src).ToString();
                }
                
            }
            shake_speed = 0.002f * ((src - 55) / 10 + 1);
        }
        if (thermometerDown)
        {
            if (temp_timer2 < 10)
                temp_timer2++;
            else
            {

                temp_timer2 = 0;
                if (src == tar)
                {
                    thermometerDown = false;
                    btn.SetActive(true);
                }
                else
                {
                    src--;
                    thermometer.fillAmount = src / 100f;
                    GameObject.Find("temperature").GetComponent<Text>().text = Mathf.Round(src).ToString();
                }
            }
            shake_speed = 0.002f * ((src - 55) / 10 + 1);
        }
        //Debug.Log(shake_speed);
    }


    public void CreateNewLine()
    {
        //重新生成，销毁之前的对象
        if (keyList.Count != 0)
        {
            for (int i = 0; i < dna_length; i++)
                DestroyImmediate(keyList[i]);
            dna_above.Clear();
            dna_below.Clear();
            keyList.Clear();
        }

        //上下碱基为一对，方便旋转，创建空白的父对象列表
        for (int i = 0; i < dna_length; i++)
        {
            target = new GameObject("key" + i.ToString());
            target.transform.position = t.position + new Vector3(i * x_space, 0f, 0f);
            target.transform.parent = dna_line.transform;
            keyList.Add(target);
        }
        //第一条链，碱基随机生成
        for (int i = 0; i < dna_length; i++)
        {
            int random_dntp = rd.Next(1, 5);
            switch (random_dntp)
            {
                case 1:
                    target = A_above; break;
                case 2:
                    target = T_above; break;
                case 3:
                    target = C_above; break;
                case 4:
                    target = G_above; break;
            }
            //Debug.Log(target);
            dna_above.Add(GameObject.Instantiate(target, t.position + new Vector3(i * x_space, y_space, 0f), Quaternion.Euler(Vector3.zero), keyList[i].transform));
            dntps.Add(dna_above[i].transform.GetChild(0));
            dna_above[i].transform.localScale = Vector3.one * reduce_scale;
            positions.Add(dntps[dntps.Count - 1].localPosition);
        }
        //根据第一条链，生成互补配对的第二条链
        for (int i = 0; i < dna_length; i++)
        {
            prefabNamestr = dna_above[i].name;
            //Debug.Log(prefabNamestr);
            switch (prefabNamestr)
            {
                case "A_above (1)(Clone)":
                    target = T_below;
                    break;
                case "T_above (1)(Clone)":
                    target = A_below;
                    break;
                case "C_above (1)(Clone)":
                    target = G_below;
                    break;
                case "G_above (1)(Clone)":
                    target = C_below;
                    break;
            }
            //target = A_below;
            dna_below.Add(GameObject.Instantiate(target, t.position + new Vector3(i * x_space, -y_space, 0f), Quaternion.Euler(Vector3.zero), keyList[i].transform));
            dntps.Add(dna_below[i].transform.GetChild(0));
            dna_below[i].transform.localScale = Vector3.one * reduce_scale;
            positions.Add(dntps[dntps.Count - 1].localPosition);
        }
        //以从左往右1/3处为中心旋转
        for (int i = 0; i < dna_length; i++)
        {
            keyList[i].transform.Rotate(new Vector3(x_rotation * (i - dna_length / 3), 0f, 0f));
        }

        //获取所有需要颤动的碱基
        //dntps = GameObject.FindGameObjectsWithTag("dntp");
        //for (int i = 0; i < dntps.Length; i++)
        //{
        //    positions.Add(dntps[i].transform.localPosition);
        //}
    }
    public void jielian(float src_temp,float tar_temp,float duration_temp)
    {
        var s = DOTween.Sequence();
        var runtime = 0f;
        var runtime2 = 0f;
        //var timer = 0f;
        //降温，src>tar
        if (thermometerDown)
        {
            //初始温度小于72度，则只涉及旋转
            if(src_temp<=72)
            {
                SpeechController.Speak("正在降温，DNA双螺旋结构逐渐恢复");
                //旋转系数
                float degree = (src_temp - tar_temp) / 17;
                for (int i = 0; i < dna_length; i++)
                {
                    s.Insert(runtime, keyList[i].transform.DORotate(new Vector3(x_rotation *degree * (i - dna_length / 3), 0f, 0f), duration_temp/2+2, RotateMode.LocalAxisAdd));
                }
            }
            //目标温度大于72，只涉及链的合并
            else if (tar_temp>=72)
            {
                SpeechController.Speak("正在降温，DNA双链缓慢合并");
                float degree = (src_temp - tar_temp) / 23;
                for (int i = 0; i < dna_length; i++)
                {
                    s.Insert(runtime, dna_above[i].transform.DOMove(Vector3.down * 0.2f*degree + dna_above[i].transform.position, duration_temp/2+2));
                    s.Insert(runtime, dna_below[i].transform.DOMove(Vector3.up * 0.2f * degree + dna_below[i].transform.position, duration_temp/2+2));
                }
            }
            //初始大于72，目标小于72，先合并，再旋转
            else if(src_temp>72&&tar_temp<72)
            {
                SpeechController.Speak("正在降温，DNA双链先合并，再恢复螺旋结构");
                float degree = (src_temp - 72) / 23;
                float degree2 = (72 - tar_temp) / 17;
                runtime= (src_temp-72) / 5+2;
                runtime2 = (72 - tar_temp) / 5+2;
                for (int i = 0; i < dna_length; i++)
                {
                    s.Insert(0f, dna_above[i].transform.DOMove(Vector3.down * 0.2f * degree + dna_above[i].transform.position, runtime));
                    s.Insert(0f, dna_below[i].transform.DOMove(Vector3.up * 0.2f * degree + dna_below[i].transform.position, runtime));
                    s.Insert(runtime, keyList[i].transform.DORotate(new Vector3(x_rotation * degree2 * (i - dna_length / 3), 0f, 0f), runtime2, RotateMode.LocalAxisAdd));

                }
            }
        }
        //升温，src<tar
        if (thermometerUp)
        {
            //目标温度小于72度，则只涉及旋转
            if (tar_temp <= 72)
            {
                SpeechController.Speak("正在升温，DNA双螺旋结构缓慢解开");
                //旋转系数
                float degree = (tar_temp - src_temp) / 17;
                for (int i = 0; i < dna_length; i++)
                {
                    s.Insert(runtime, keyList[i].transform.DORotate(new Vector3(-x_rotation * degree * (i - dna_length / 3), 0f, 0f), duration_temp / 2+2, RotateMode.LocalAxisAdd));
                }
            }
            //初始温度大于72，只涉及链的分开
            else if (src_temp >= 72)
            {
                SpeechController.Speak("正在升温，DNA双链逐渐分开");
                float degree = (tar_temp- src_temp ) / 23;
                for (int i = 0; i < dna_length; i++)
                {
                    s.Insert(runtime, dna_above[i].transform.DOMove(Vector3.up * 0.2f * degree + dna_above[i].transform.position, duration_temp / 2+2));
                    s.Insert(runtime, dna_below[i].transform.DOMove(Vector3.down * 0.2f * degree + dna_below[i].transform.position, duration_temp / 2+2));
                }
            }
            //初始小于72，目标大于72，先解旋，再分开
            else if (src_temp < 72 && tar_temp > 72)
            {
                SpeechController.Speak("正在升温，DNA双链先解旋，再分开");
                float degree = (72 - src_temp) / 17;
                float degree2 = (tar_temp - 72) / 23;
                runtime = (72-src_temp) / 5+2;
                runtime2 = (tar_temp-72) / 5+2;
                for (int i = 0; i < dna_length; i++)
                {
                    s.Insert(runtime, dna_above[i].transform.DOMove(Vector3.up * 0.2f * degree2 + dna_above[i].transform.position, runtime2));
                    s.Insert(runtime, dna_below[i].transform.DOMove(Vector3.down * 0.2f * degree2 + dna_below[i].transform.position, runtime2));
                    s.Insert(0f, keyList[i].transform.DORotate(new Vector3(-x_rotation * degree * (i - dna_length / 3), 0f, 0f), runtime, RotateMode.LocalAxisAdd));

                }
            }
        }
        //for (int i = 0; i < dna_length; i++)
        //{
        //    //解旋之后上下两条链分开
        //    s.Insert(runtime, keyList[i].transform.DORotate(new Vector3(-x_rotation * (i - dna_length / 3), 0f, 0f), 10f, RotateMode.LocalAxisAdd));
        //    s.Insert(10f, dna_below[i].transform.DOMove(Vector3.down * 0.2f + dna_below[i].transform.position, 2f));
        //    s.Insert(10f, dna_above[i].transform.DOMove(Vector3.up * 0.2f + dna_above[i].transform.position, 2f));
        //}
        //DOTween.To(() => timer, a => timer = a, 1, 12).OnComplete(() => adjust());

    }
    

    //public void switchClick()
    //{
    //    for (int i = 0; i < dna_length; i++)
    //        GameObject.Instantiate(A_above, t.position, t.rotation, t);

    //}

    //void next()
    //{
        
    //}
    public void Drop_select(int n)
    {
        float selectTemp = Convert.ToInt32(dpn.captionText.text.Substring(0, 2));
        
        //print("选择了:" +shakeChange);
        
        string text = GameObject.Find("temperature").GetComponent<Text>().text;
        temp = float.Parse(text);
        temp_timer = 0;
        temp_timer2 = 0;
        if (selectTemp>temp)
        {
            
            duration = (selectTemp - temp) / 5+2;
            thermometerUp = true;
            btn.SetActive(false);
        }
        else
        {
            
            duration = (temp-selectTemp) / 5+2;
            thermometerDown = true;
            btn.SetActive(false);
        }
        src = temp;
        tar = selectTemp;
        Debug.Log(duration);
        jielian(src, tar, duration);
    }

    public void ClearPage()
    {
        canvasGroup = GameObject.Find("Canvas/page11/layer0").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        canvasGroup = GameObject.Find("Canvas/page11").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        DestroyImmediate(dna_line);
    }

    public void ShowPage()
    {
        return;
    }
    
    
    public void showNextPage()
    {
        // ClearPage();
        // gameObject.GetComponent<ControlPage11>().enabled = false;
        // ControlPage2 controlPage2 = GameObject.Find("Canvas/page2").GetComponent<ControlPage2>(); 
        // controlPage2.enabled = true;
        // controlPage2.ShowPage();
        GameObject.Find("MissionController").GetComponent<MissionController>().SwitchMissionInSceneOne(4);
    }

}