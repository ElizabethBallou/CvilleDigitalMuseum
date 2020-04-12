using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetter : MonoBehaviour
{
    //the purpose of this script is that it runs whenever a new scene (the Lawn or the Downtown Mall) is loaded. It calls
    //setStage in the location manager, which is essential for re-finding borders and allowing scene transitions.
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("callSetStage", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void callSetStage()
    {
        LocationInfoManager.instance.setStage();
    }
}
