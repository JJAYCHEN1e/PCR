using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PCR_hat : MonoBehaviour
{
    // Start is called before the first frame update
    public bool opened = false;
    public static bool employed = false;
    public static Vector3 tubePos;
    void Start()
    {
        tubePos = GameObject.Find("试管").transform.position;
    }

    void OnMouseUp()
    {
        if(opened) {
            close();return;
        }
        if(!primer.employed )
        {
            UnityToast.ShowAlert("操作失误", "还未采样引物试剂");
            SpeechController.Speak("操作失误，还未采样引物试剂");
        }
        else if(!template.employed )
        {
            UnityToast.ShowAlert("操作失误", "还未采样模板试剂");
            SpeechController.Speak("操作失误，还未采样模板试剂");
        }
        else if(!polymerase.employed )
        {
            UnityToast.ShowAlert("操作失误", "还未采样 Taq 酶试剂");
            SpeechController.Speak("操作失误，还未采样 Taq 酶试剂");
        }
        else if(!rawMaterial.employed)
        {
            UnityToast.ShowAlert("操作失误", "还未采样原料试剂");
            SpeechController.Speak("操作失误，还未采样原料试剂");
        }
        else if (employed)
        {
            UnityToast.ShowAlert("操作失误", "已放入，请勿重复操作");
            SpeechController.Speak("已放入，请勿重复操作");
        }
        else if(!buffer.employed)
        {
            reminder.text = "还未采样缓冲液";
            return;
        }
        else if(employed) reminder.text = "已放入,请勿重复操作";
        else{
            open();
            employed = true;
        }
    }
    void OnMouseExit()
    {
    }
    private void open(){
        int t=90;
        while(t>0){
            this.transform.Rotate(new Vector3(-1,0,0));
            t--;
        }
        Vector3 HatPos = GameObject.Find("PCR仪_ 1").transform.position;
        GameObject.Find("试管").transform.DOMove(GameObject.Find("移液枪架").transform.position + new Vector3(-0.01f,0.1f,0) * 0.5f,2.5f);
        GameObject.Find("试管").transform.DOMove(HatPos + new Vector3(-0.02f,0f,-0.02f) * 0.5f,2.5f);
        opened = true;
    }
    private void close(){
        int t=90;
        while(t>0){
            this.transform.Rotate(new Vector3(1,0,0));
            t--;
        }
        opened = false;
    }
}
