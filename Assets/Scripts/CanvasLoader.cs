using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoader : MonoBehaviour
{
    public static CanvasLoader instance;

    private void Awake()
    {
        if (instance!=null){
            Destroy(instance);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
