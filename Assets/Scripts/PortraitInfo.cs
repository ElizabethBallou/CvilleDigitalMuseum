using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitInfo : MonoBehaviour
{
    public static PortraitInfo instance;
    public Image[] portraitArray;
    public Chunk.speakerName[] SpeakerNames;

    public Dictionary<Chunk.speakerName, Image> portraitDictionary;

    private void Awake()
    {
        instance = this;
        
        portraitDictionary = new Dictionary<Chunk.speakerName, Image>();
        for (int i = 0; i < SpeakerNames.Length; i++)
        {
            portraitDictionary.Add(SpeakerNames[i], portraitArray[i]);
            Debug.Log(portraitDictionary[SpeakerNames[i]]);
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
