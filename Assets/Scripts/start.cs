using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    public Animator animator;
    public int stepNum;
    public GameObject scamera;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("DNA_Line").GetComponent<Animator>();
        scamera = GameObject.Find("Main Camera");
        stepNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        Debug.Log(scamera.GetComponent<Transform>().position);
        //AlignToCamera();
    }

    void AlignToCamera()
    {
        transform.position = new Vector3(scamera.GetComponent<Transform>().position.x+5, scamera.GetComponent<Transform>().position.y-2.3f, scamera.GetComponent<Transform>().position.z + 6);
    }
    public void btn1Click()
    {
      
        switch(stepNum)
        {
            case 0:
                animator.Play("step1");
                animator.SetBool("start", true);
                Debug.Log("start" + animator.GetBool("start"));
                animator.SetBool("quit", false);
                break;
            case 1:
                animator.Play("step2");
                animator.SetBool("circle1_step1to2", true);
                Debug.Log("circle1_step1to2" + animator.GetBool("circle1_step1to2"));
                animator.SetBool("start", false);
                break;
            case 2:
                animator.Play("step3");
                animator.SetBool("circle1_step2to3", true);
                Debug.Log("circle1_step2to3" + animator.GetBool("circle1_step2to3"));
                animator.SetBool("circle1_step1to2", false);
                break;
            case 3:
                animator.Play("circle2-step1");
                animator.SetBool("circle1to2", true);
                Debug.Log("circle1to2" + animator.GetBool("circle1to2"));
                animator.SetBool("circle1_step2to3", false);
                break;
            case 4:
                animator.Play("circle2-step2");
                animator.SetBool("circle2_step1to2", true);
                Debug.Log("circle2_step1to2" + animator.GetBool("circle2_step1to2"));
                animator.SetBool("circle1to2", false);
                break;
            case 5:
                animator.Play("circle2-step3");
                animator.SetBool("circle2_step2to3", true);
                Debug.Log("circle2_step2to3" + animator.GetBool("circle2_step2to3"));
                animator.SetBool("circle2_step1to2", false);
                break;
            case 6:
                animator.Play("circle3-step1");
                animator.SetBool("circle2to3", true);
                Debug.Log("circle2to3" + animator.GetBool("circle2to3"));
                animator.SetBool("circle2_step2to3", false);
                break;
            case 7:
                animator.Play("circle3-step2");
                animator.SetBool("circle3_step1to2", true);
                Debug.Log("circle3_step1to2" + animator.GetBool("circle3_step1to2"));
                animator.SetBool("circle2to3", false);
                break;
            case 8:
                animator.Play("circle3-step3");
                animator.SetBool("circle3_step2to3", true);
                Debug.Log("circle3_step2to3" + animator.GetBool("circle3_step2to3"));
                animator.SetBool("circle3_step1to2", false);
                break;
            case 9:
                animator.SetBool("quit", true);
                Debug.Log("quit" + animator.GetBool("quit"));
                animator.SetBool("circle3_step2to3", false);
                break;
            default:
                stepNum = 0;
                break;


        }
        stepNum++;
        Debug.Log(stepNum);
        
        
    }
}
