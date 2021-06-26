using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rawMaterial : MonoBehaviour
{
    bool showTip;
    public static bool employed;
    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    // RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        showTip = false;
        employed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnMouseOver(){
        showTip = true;
    }
    
    // void OnGUI()
    // {
    //     GUI.Label(new Rect(447,220,40,40),"原料");
    // }
    void OnMouseUp()
    {
        Transform hat = GameObject.Find("Model/原料/盖").transform;
        Vector3 originHatPos = hat.position;
        Sequence se = DOTween.Sequence();
        se.Append(hat.DOMove(originHatPos + new Vector3(-0.02f,0.05f,0),1.5f));

        Transform pipetee = GameObject.Find("2.5").transform;
        Vector3 originPos = pipetee.position;
        se.Append(pipetee.DOMove(GameObject.Find("原料").transform.position + new Vector3(0.05f , 0.3f,0),1.5f));
        se.Append(pipetee.DOMove(GameObject.Find("原料").transform.position + new Vector3(0 , 0.1f,0),1.5f));
        se.Append(pipetee.DOMove(GameObject.Find("八连管_").transform.position + new Vector3(0f , 0.1f,0),2.5f));
       
        se.Append(pipetee.DOMove(originPos,1.5f));

        se.Append(hat.DOMove(originHatPos,1.5f));
        employed = true;
    }
}
