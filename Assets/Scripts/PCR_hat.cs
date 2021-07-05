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
        int t=90;
        while(t>0){
            this.transform.Rotate(new Vector3(-1,0,0));
            t--;
        }
        DOTween.To(() => timer, a => timer = a, 1, 1f).OnComplete(() => SpeechController.Speak("将混合液放进PCR仪"));
        GameObject.Find("试管").transform.DOMove(GameObject.Find("试管槽").transform.position,2.5f);
        opened = true;
        DOTween.To(() => timer, a => timer = a, 1, 4.5f).OnComplete(() => SpeechController.Speak("请点击PCR仪的盖子，关上盖子"));
    }
    private void close(){
        int t=90;
        while(t>0){
            this.transform.Rotate(new Vector3(1,0,0));
            t--;
        }
        opened = false;
        SpeechController.Speak("请点击PCR仪的屏幕设置程序");
    }
}
