using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class Touch
{

    public static int TouchCount()
    {
        #if UNITY_EDITOR
            return (Input.GetMouseButtonDown(0) == true ? 1 : 0);
        #else
            return (Input.touchCount);
        #endif          
    }
    
    public static Vector2 GetPos()
    {
        #if UNITY_EDITOR
            Debug.Log("Getting mouse position");
            return (Input.mousePosition);
        #else
            return (Input.GetTouch(Input.touchCount - 1).position);
        #endif          
    }

    public static int TouchID(int currentTouch)
    {
        #if UNITY_EDITOR
            return (1);
        #else
            return (Input.GetTouch(currentTouch).fingerId);
        #endif 
    }
   


}
