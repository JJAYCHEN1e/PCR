using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class buffer : MonoBehaviour
{
    float timer;
    public static bool employed;
    public static string type = "";
    Transform s,m,l,hat;
    Vector3 originSpos,originMpos,originLpos,originHatpos;
    bool startCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        employed = false;
        s = GameObject.Find("2.5").transform;
        m = GameObject.Find("10").transform;
        l = GameObject.Find("100").transform;
        hat = GameObject.Find("缓冲液试剂盖").transform;
        originSpos = s.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(type == "2.5") {
            if(s.position != originSpos) startCheck = true;
            if(startCheck && s.position == originSpos){
                type = "";employed = true;startCheck = false;
            }
        }
        // else if(type == "10") {
        //     if(m.position != originMpos) startCheck = true;
        //     if( startCheck && m.position == originMpos){
        //         type = "";employed = true;startCheck=false;
        //     }
        // }
        // else if(type == "100") {
        //     if(l.position != originLpos) startCheck = true;
        //     if( startCheck && l.position == originLpos){
        //         type = "";employed = true;startCheck = false;
        //     }
        // }
    }
   
    void OnMouseUp()
    {
        if(GameObject.Find("ARSessionOrigin").GetComponent<PlaceOnPlane>().IsConformed() == false) {
            SpeechController.Speak("请放置实验器材");
            return;
        }
        if(type != "") return;
        if(primer.type == "" && polymerase.type== "" && rawMaterial.type == "" && template.type == ""){
        // if(s.position == originSpos){
            type = "2.5";

            Sequence se = DOTween.Sequence();            
            
            se.Append(s.DOMove(GameObject.Find("枪头").transform.position,2f));
            SpeechController.Speak("移液枪装枪头");
            DOTween.To(() => timer, a => timer = a, 1, 2f).OnComplete(() => SpeechController.Speak("打开装有缓冲液的试管"));
            se.Append(hat.DOMove(GameObject.Find("缓冲液开盖").transform.position,2f));
            DOTween.To(() => timer, a => timer = a, 1, 7f).OnComplete(() => SpeechController.Speak("采集缓冲液"));
            se.Append(s.DOMove(GameObject.Find("缓冲液上方").transform.position,2f));
            se.Append(s.DOLocalRotate(new Vector3(15f, 0f, 0f), 1f, RotateMode.WorldAxisAdd));
            se.Append(s.DOMove(GameObject.Find("缓冲液采样").transform.position ,2f));
            se.Append(s.DOMove(GameObject.Find("缓冲液上方").transform.position,2f));
            se.Append(s.DOLocalRotate(new Vector3(-15f, 0f, 0f), 1f, RotateMode.WorldAxisAdd));
            DOTween.To(() => timer, a => timer = a, 1, 18f).OnComplete(() => SpeechController.Speak("废弃枪头"));
            se.Append(s.DOMove(GameObject.Find("试管内").transform.position ,2f));
            //if(GameObject.Find("液体").GetComponent<MeshRenderer>().enabled == false) GameObject.Find("液体").GetComponent<MeshRenderer>().enabled = true;
            se.Append(hat.DOMove(GameObject.Find("缓冲液备份盖").transform.position ,2f));
            se.Append(s.DOMove(GameObject.Find("垃圾桶上").transform.position,2f));
            se.Append(s.DOMove(GameObject.Find("垃圾桶内").transform.position ,2f));
            se.Append(s.DOMove(GameObject.Find("2.5备份").transform.position,1.5f));  
        } 
        // else if(m.position == originMpos){
        //     type = "10";

        //     Sequence se = DOTween.Sequence();
        //     se.Append(m.DOMove(pos + new Vector3(0.02f , 0.2f,0),1.5f));
        //     se.Append(m.DOMove(GameObject.Find("引物试剂").transform.position + new Vector3(0 , 0.05f,0),1.5f));
        //     se.Append(m.DOMove(GameObject.Find("试管").transform.position + new Vector3(0f , 0.08f,0.03f),2.5f));
        //     se.Append(m.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(-0.1f , 0.15f,0),1.5f));
        //     se.Append(m.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(0f , 0.1f,0),1.5f));
        //     se.Append(m.DOMove(originMpos,1.5f));
        // } 
        // else if(l.position == originLpos){
        //     type = "100";

        //     Sequence se = DOTween.Sequence();
        //     se.Append(l.DOMove(GameObject.Find("引物试剂").transform.position + new Vector3(0.02f , 0.2f,0),1.5f));
        //     se.Append(l.DOMove(GameObject.Find("引物试剂").transform.position + new Vector3(0 , 0.05f,0),1.5f));
        //     se.Append(l.DOMove(GameObject.Find("试管").transform.position + new Vector3(0f , 0.08f,0.03f),2.5f));
        //     se.Append(l.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(-0.1f , 0.15f,0),1.5f));
        //     se.Append(l.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(0f , 0.1f,0),1.5f));
        //     se.Append(l.DOMove(originLpos,1.5f));
        // } 
    }
}
