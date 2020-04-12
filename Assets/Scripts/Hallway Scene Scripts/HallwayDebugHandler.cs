using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whilefun.FPEKit;

public class HallwayDebugHandler : MonoBehaviour
{
    
    private Vector3 debugVector = new Vector3(.1f, 2.3f, -183f);
    public FPEDoorway myFPEDoorway;


    private void Start()
    {
        myFPEDoorway.AllowedToTransition = true;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("the r key has been pressed");
            useDebugReset();
        }
    }

    private void useDebugReset()
    {
        Debug.Log("Debug reset called");
        FPEPlayer.Instance.gameObject.transform.localPosition = debugVector;
    }
}
