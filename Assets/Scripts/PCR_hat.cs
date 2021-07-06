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
    float timer;
    void Start()
    {

    }

    void OnMouseUp()
    {
#if !UNITY_EDITOR
        if(GameObject.Find("AR Session Origin").GetComponent<PlaceOnPlane>().IsConformed() == false) {
            SpeechController.Speak("请放置实验器材");
            return;
        }
#endif
        if(opened && GameObject.Find("试管").transform.position == GameObject.Find("试管槽").transform.position) {
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
            UnityToast.ShowAlert("操作失误", "还未采样缓冲液");
            SpeechController.Speak("操作失误，还未采样缓冲液");
                return;
        }
        else if (employed)
        {
            UnityToast.ShowAlert("提示", "已放入,请勿重复操作");
            SpeechController.Speak("已放入,请勿重复操作");

        }
        else{
            open();
            employed = true;
        }
    }
    void OnMouseExit()
    {

    }
    private void open(){
        SpeechController.Speak("混合液移进PCR仪");
        Sequence se = DOTween.Sequence();     
        se.Append(GameObject.Find("PCR仪_ 1").transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 1f, RotateMode.WorldAxisAdd));
        // int t=90;
        // while(t>0){
        //     this.transform.Rotate(new Vector3(-1,0,0));
        //     t--;
        // }
        
        se.Append(GameObject.Find("试管").transform.DOMove(GameObject.Find("试管槽").transform.position,2.5f));
        opened = true;
        DOTween.To(() => timer, a => timer = a, 1, 4f).OnComplete(() => SpeechController.Speak("请点击PCR仪的盖子，关上盖子"));
    }
    private void close(){
        // int t=90;
        // while(t>0){
        //     this.transform.Rotate(new Vector3(1,0,0));
        //     t--;
        // }
        GameObject.Find("PCR仪_ 1").transform.DOLocalRotate(new Vector3(90f, 0f, 0f), 1f, RotateMode.WorldAxisAdd);
        opened = false;
        SpeechController.Speak("请点击PCR仪的屏幕设置程序");
    }
}
