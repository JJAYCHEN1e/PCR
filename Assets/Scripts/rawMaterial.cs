using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rawMaterial : MonoBehaviour
{
    public static bool employed;
    public string type = "";
    Transform s,m,l;
    Vector3 originSpos,originMpos,originLpos;
    bool startCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        employed = false;
        s = GameObject.Find("2.5").transform;
        m = GameObject.Find("10").transform;
        l = GameObject.Find("100").transform;
        originSpos = s.position;
        originMpos = m.position;
        originLpos = l.position;
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
        else if(type == "10") {
            if(m.position != originMpos) startCheck = true;
            if( startCheck && m.position == originMpos){
                type = "";employed = true;startCheck=false;
            }
        }
        else if(type == "100") {
            if(l.position != originLpos) startCheck = true;
            if( startCheck && l.position == originLpos){
                type = "";employed = true;startCheck = false;
            }
        }
    }
   
    void OnMouseUp()
    {
        if(type != "") return;
        
        if(true){
            type = "2.5";
            Sequence se = DOTween.Sequence();
            se.Append(s.DOMove(GameObject.Find("原料试剂").transform.position + new Vector3(0.02f , 0.2f,0) * 0.5f,1.5f));
            se.Append(s.DOMove(GameObject.Find("原料试剂").transform.position + new Vector3(0 , 0.05f,0) * 0.5f,1.5f));
            se.Append(s.DOMove(GameObject.Find("试管").transform.position + new Vector3(0f , 0.08f,0.03f) * 0.5f,2.5f));
            se.Append(s.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(-0.1f , 0.15f,0) * 0.5f,1.5f));
            se.Append(s.DOMove(GameObject.Find("医疗垃圾桶").transform.position + new Vector3(0f , 0.1f,0) * 0.5f,1.5f));
            se.Append(s.DOMove(originSpos,1.5f));
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
