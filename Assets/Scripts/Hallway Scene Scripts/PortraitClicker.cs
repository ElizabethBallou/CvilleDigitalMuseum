using System;
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

    private float cutoffValue = .1f;
    
    public int guideTextIndicator;

    private int clickNumberTracker = 0;
    // Start is called before the first frame update
    void Start()
    {
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        mySymbolInfo = myClipPlayer.transform.GetComponentInChildren<SymbolMouseHover>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


    private void OnMouseDown()
    {
        if (!myClipPlayer.myAudioSource.isPlaying)
        {
            Debug.Log("I'm triggering BeginClip");
            myClipPlayer.BeginClip();
            fadeAudioSprite();
        }
        else
        {
            if (cutoffValue <= 1.1)
            {
                if (clickNumberTracker == 0)
                {
                    if (guideTextIndicator != 0)
                    {
                        HallwayManager.instance.clickText.DOFade(0f, 1f);
                    }
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
        mySymbolInfo.audioSprite.DOFade(0f, .5f);
    }
}
