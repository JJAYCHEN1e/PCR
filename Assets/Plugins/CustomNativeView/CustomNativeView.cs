using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CustomNativeView : MonoBehaviour
{
    public static void InitSceneTwoRuleView(){
#if UNITY_IOS && !UNITY_EDITOR
        _initSceneTwoRuleView();
#endif
    }
    
    public static void InitExitAppButton(){
#if UNITY_IOS && !UNITY_EDITOR
        _initExitAppButton();
#endif
    }

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void _initSceneTwoRuleView();
#endif
    
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void _initExitAppButton();
#endif
}
