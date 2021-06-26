using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PCR_hat : MonoBehaviour
{
    // Start is called before the first frame update
    public bool opened;
    
    void Start()
    {
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.name == "PCR仪_ 1")
                {
                    Debug.Log(this.transform.name);
                    if(!opened)
                    {
                        open();
                    }
                    else
                    {
                        close();
                    }
                }
            } 
        }     
    }
    private void open(){
        int t=90;
        while(t>0){
            this.transform.Rotate(new Vector3(-1,0,0));
            t--;
        }
        opened = true;
        Vector3 HatPos = GameObject.Find("PCR仪_ 1").transform.position;
        GameObject.Find("八连管_").transform.DOMove(HatPos + new Vector3(-0.02f,0f,0),2.5f);
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
