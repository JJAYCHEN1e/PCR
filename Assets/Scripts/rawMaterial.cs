﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rawMaterial : MonoBehaviour
{
    public static bool employed;
    public static string type = "";
    Transform s,m,l,hat;
    Vector3 originSpos,originMpos,originLpos,originHatpos;
    bool startCheck = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        employed = false;
        s = GameObject.Find("2.5").transform;
        m = GameObject.Find("10").transform;
        l = GameObject.Find("100").transform;
        hat = GameObject.Find("原料试剂盖").transform;
        originSpos = s.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(type == "2.5") {
            if(s.position != GameObject.Find("2.5备份").transform.position) startCheck = true;
            if(startCheck && s.position == GameObject.Find("2.5备份").transform.position){
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
#if !UNITY_EDITOR
        if(GameObject.Find("AR Session Origin").GetComponent<PlaceOnPlane>().IsConformed() == false) {
            SpeechController.Speak("请放置实验器材");
            return;
        }
#endif
        if(type != "") return;
        
        if(buffer.type == "" && polymerase.type== "" && primer.type == "" && template.type == ""){
            type = "2.5";
            Sequence se = DOTween.Sequence();
            se.Append(s.DOMove(GameObject.Find("枪头").transform.position,2f));
            SpeechController.Speak("移液枪装枪头");
            DOTween.To(() => timer, a => timer = a, 1, 2f).OnComplete(() => SpeechController.Speak("打开装有原料的试管"));
            se.Append(hat.DOMove(GameObject.Find("原料开盖").transform.position,2f));
            se.Append(s.DOMove(GameObject.Find("原料上方").transform.position,2f));
            DOTween.To(() => timer, a => timer = a, 1, 7f).OnComplete(() => SpeechController.Speak("采集原料"));
            se.Append(s.DOLocalRotate(new Vector3(28.333f, 0f, 0f), 1f, RotateMode.WorldAxisAdd));
            se.Append(s.DOMove(GameObject.Find("原料采样").transform.position,2f));
            se.Append(s.DOMove(GameObject.Find("原料上方").transform.position,2f));
            se.Append(s.DOLocalRotate(new Vector3(-28.333f, 0f, 0f), 1f, RotateMode.WorldAxisAdd));
            se.Append(s.DOMove(GameObject.Find("试管内").transform.position,2f));
            se.Append(hat.DOMove(GameObject.Find("原料备份盖").transform.position ,2f));
            DOTween.To(() => timer, a => timer = a, 1, 18f).OnComplete(() => SpeechController.Speak("废弃枪头"));
            se.Append(s.DOMove(GameObject.Find("垃圾桶上").transform.position,2f));
            se.Append(s.DOMove(GameObject.Find("垃圾桶内").transform.position ,2f));
            se.Append(s.DOMove(GameObject.Find("2.5备份").transform.position,1.5f));  
        }
        // else if(m.position == originMpos){
        //     type = "10";

        //     Sequence se = DOTween.Sequence();
        //     se.Append(m.DOMove(GameObject.Find("原料试剂").transform.position + new Vector3(0.02f , 0.2f,0),1.5f));
        //     se.Append(m.DOMove(GameObject.Find("原料试剂").transform.position + new Vector3(0 , 0.05f,0),1.5f));
        //     se.Append(m.DOMove(GameObject.Find("试管").transform.position + new Vector3(0f , 0.08f,0.03f),2.5f));
        //     se.Append(m.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(-0.1f , 0.15f,0),1.5f));
        //     se.Append(m.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(0f , 0.1f,0),1.5f));
        //     se.Append(m.DOMove(originMpos,1.5f));
        // } 
        // else if(l.position == originLpos){
        //     type = "100";

        //     Sequence se = DOTween.Sequence();
        //     se.Append(l.DOMove(GameObject.Find("原料试剂").transform.position + new Vector3(0.02f , 0.2f,0),1.5f));
        //     se.Append(l.DOMove(GameObject.Find("原料试剂").transform.position + new Vector3(0 , 0.05f,0),1.5f));
        //     se.Append(l.DOMove(GameObject.Find("试管").transform.position + new Vector3(0f , 0.08f,0.03f),2.5f));
        //     se.Append(l.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(-0.1f , 0.15f,0),1.5f));
        //     se.Append(l.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(0f , 0.1f,0),1.5f));
        //     se.Append(l.DOMove(originLpos,1.5f));
        // } 
    }
}
