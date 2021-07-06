
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EnterExperiment : MonoBehaviour
{
    public GameObject loadingText;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadSceneAsync("Scene1");
            loadingText.GetComponent<Text>().DOText("Loading......", 2);
        }
        
    }

}