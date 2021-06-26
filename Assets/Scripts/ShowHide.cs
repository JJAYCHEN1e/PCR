using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHide : MonoBehaviour
{
    GameObject btn; 
    Vector3 vec;
    Vector3 far;
    bool isShow;
    // Start is called before the first frame update
    void Start()
    {
        btn = GameObject.Find("circle1-step1");
        vec = btn.GetComponent<Transform>().position;
        far = new Vector3(100, 100, 100);
        isShow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (btn.GetComponent<Transform>().position!=far)
            vec = btn.GetComponent<Transform>().position;
    }

    public void ShowClick()
    {
        if (isShow)
        {
            isShow = !isShow;
            btn.GetComponent<Transform>().position = far;
        }
        else
        {
            btn.GetComponent<Transform>().position = vec;
            isShow = !isShow;
        }
    }
}
