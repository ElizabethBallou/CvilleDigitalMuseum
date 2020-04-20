using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitClicker : MonoBehaviour
{
    private MeshRenderer myMeshRenderer;

    public ArtisticStatementClipPlayer myClipPlayer;

    private float cutoffValue = .1f;
    // Start is called before the first frame update
    void Start()
    {
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnMouseDown()
    {
        if (myClipPlayer.triggeredAudioSource && cutoffValue <= 1)
        {
            Debug.Log("It's working! It's working!");
            Debug.Log(myMeshRenderer.material.name);
            myMeshRenderer.material.SetFloat("_Cutoff", cutoffValue);
            cutoffValue = cutoffValue + .1f;
        }
    }
}
