using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitInfo : MonoBehaviour
{
    public static PortraitInfo instance;
    public Image[] portraitArray;
    public string[] speakerStringArray;

    public Dictionary<string, Image> portraitDictionary;

    private void Awake()
    {
        instance = this;
        
        portraitDictionary = new Dictionary<string, Image>();
        for (int i = 0; i < speakerStringArray.Length; i++)
        {
            portraitDictionary.Add(speakerStringArray[i], portraitArray[i]);
            Debug.Log(portraitDictionary[speakerStringArray[i]]);
        }
        Debug.Log(portraitDictionary.Count);

        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
