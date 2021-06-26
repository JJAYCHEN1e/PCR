using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlide : MonoBehaviour
{
    public Slider timeSlider;
    public Animator animator;
    public AnimatorStateInfo animatorStateInfo;
    public Text speedText;
    public GameObject mei;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("dna").GetComponent<Animator>();
        timeSlider = GetComponent<Slider>();
        timeSlider.onValueChanged.AddListener(SliderOnValueChanged);
        speedText = GameObject.Find("CurrentSpeed").GetComponent<Text>();
        mei = GameObject.Find("Capsule");
    }

    // Update is called once per frame
    void Update()
    {
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animator.speed > 0f)
        {
            timeSlider.value = animatorStateInfo.normalizedTime;
        }
        if (animatorStateInfo.normalizedTime>1.05f)
        {
            timeSlider.value = 0;
        }
        //if(animatorStateInfo.normalizedTime>0.5f)
        //{
        //    mei.SetActive(false);
        //}
        //else
        //{
        //    mei.SetActive(true);
        //}
        
    }
    void SliderOnValueChanged(float value)
    {
        animator.Play("dna", 0, value);
    }

    public void MouseDown()
    {
        animator.speed = 0f;
    }

    public void MouseUp()
    {
        if (GameObject.Find("pauseText").GetComponent<Text>().text.Equals("Pause"))
            animator.speed = float.Parse(speedText.text);
    }
}
