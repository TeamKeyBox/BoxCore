using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticObjs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            var now = DateTime.Now;
            ScreenCapture.CaptureScreenshot("scrshot_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + "_" + now.Millisecond + ".png");
            Debug.Log("ScreenShot Captured!");
        }
    }
}
