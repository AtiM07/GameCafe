using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AOT;

public class StatisticsJS : MonoBehaviour
{
    [DllImport("example")]
    private static extern IntPtr JSCallbackExample();

    [MonoPInvokeCallback(typeof(Action))]
    public static void DefaultCallback()
    {
        //This fires from javascript
    }
    public static Action GetJSCallback()
    {
        IntPtr ptr = JSCallbackExample();
        Action a = Marshal.GetDelegateForFunctionPointer<Action>(ptr);
        return a;
    }
    private void Start()
    {
        //GetJSCallback();
        Application.ExternalCall("JSCallbackExample");
    }
}
