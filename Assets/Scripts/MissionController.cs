using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    private const int sceneOneMissionCount = 5;
    public static int currentMissionIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (currentMissionIndex != 1 && currentMissionIndex != sceneOneMissionCount + 1)
        {
            SwitchMissionInSceneOne(currentMissionIndex);
        }
    }

    public void SwitchMissionInSceneOne(int missionIndexInSceneOne)
    {
        ControlPage0 page0 = GameObject.Find("page0").GetComponent<ControlPage0>();
        ControlPage1 page1 = GameObject.Find("page1").GetComponent<ControlPage1>();
        ControlPage11 page11 = GameObject.Find("page11").GetComponent<ControlPage11>();
        ControlPage2 page2 = GameObject.Find("page2").GetComponent<ControlPage2>();
        ControlPage3 page3 = GameObject.Find("page3").GetComponent<ControlPage3>();
        
        page0.ClearPage();
        page1.ClearPage();
        page11.ClearPage();
        page2.ClearPage();
        page3.ClearPage();
        
        page0.enabled = false;
        page1.enabled = false;
        page11.enabled = false;
        page2.enabled = false;
        page3.enabled = false;

        switch (missionIndexInSceneOne)
        {
            case 1:
                page0.enabled = true;
                page0.ShowPage();
                break;
            case 2:
                page1.enabled = true;
                page1.ShowPage();
                break;
            case 3:
                page11.enabled = true;
                page11.ShowPage();
                break;
            case 4:
                page2.enabled = true;
                page2.ShowPage();
                break;
            case 5:
                page3.enabled = true;
                page3.ShowPage();
                break;
                
        }
    }

    public void SwitchMission(string message)
    {
        // TODO: Medal show-hide control!
        
        
        int missionIndex = int.Parse(message);
        if (missionIndex > 0 && missionIndex <= sceneOneMissionCount)
        {
            if (currentMissionIndex > sceneOneMissionCount)
            {
                currentMissionIndex = missionIndex;
                SceneManager.LoadScene("Scene1");
            }
            else
            {
                currentMissionIndex = missionIndex;
                SwitchMissionInSceneOne(missionIndex);
            }
        } else if (missionIndex >= sceneOneMissionCount + 1 && missionIndex <= sceneOneMissionCount + 1)
        {
            currentMissionIndex = missionIndex;
            SceneManager.LoadScene("Scene2");
        }
    }

    // private bool f = false;
    // void Update()
    // {
    //     if (!f)
    //         if (Input.GetMouseButtonDown(0))
    //         {
    //             f = !f;
    //             SwitchMission("5");
    //         }
    // }
    
}
