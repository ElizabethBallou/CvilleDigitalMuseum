﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PortraitClicker : MonoBehaviour
{
    private MeshRenderer myMeshRenderer;

    public ArtisticStatementClipPlayer myClipPlayer;
    private SymbolMouseHover mySymbolInfo;

    //changing from .1f to 0 for trailer purposes
    private float cutoffValue = .1f;
    
    public int guideTextIndicator;

    private int clickNumberTracker = 0;

    public bool trailerfadeout = false;

    private bool hasBeenClickedOnce = false;

    [HideInInspector] public TextMeshPro clickText;
    // Start is called before the first frame update
    void Start()
    {
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        mySymbolInfo = myClipPlayer.transform.GetComponentInChildren<SymbolMouseHover>();
        clickText = gameObject.transform.parent.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

    }



    private void OnMouseDown()
    {
        if (!hasBeenClickedOnce)
        {
            hasBeenClickedOnce = true;
        }

        if (!myClipPlayer.myAudioSource.isPlaying)
        {
            Debug.Log("I'm triggering BeginClip");
            myClipPlayer.BeginClip();
            fadeAudioSprite();
        }
        if (hasBeenClickedOnce)
        {
            if (cutoffValue <= 1.1)
            {
                if (clickNumberTracker == 2)
                {
                    clickText.DOFade(0f, 1f);
                }

                myMeshRenderer.material.SetFloat("_Cutoff", cutoffValue);
                cutoffValue = cutoffValue + .1f;
                clickNumberTracker++;
            }
        }

        
    }

    private void fadeAudioSprite()
    {
        Debug.Log("fading audio sprite...");
        mySymbolInfo.audioSprite.gameObject.SetActive(false);
    }
}
