using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HallwayAudioTrigger : MonoBehaviour
{
    
    private SpriteRenderer audioSprite;
    private Transform playerTransformToAudio;
    public float minimumOpacityDistance = 7;
    public float maximumOpacityDistance = 4;
    private float dist;
    private Camera cameraToLookAt;
    public Material[] materialArray;
    public ArtisticStatementClipPlayer myClipPlayer;
    private bool clickedObject = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSprite = gameObject.GetComponent<SpriteRenderer>();
        audioSprite.material = materialArray[0];
        playerTransformToAudio = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cameraToLookAt = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        audioSprite.color = Color.clear;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraToLookAt.transform);
        dist = Vector3.Distance(playerTransformToAudio.position, transform.position);
        float lerpAmt = 1.0f - Mathf.Clamp01((dist - maximumOpacityDistance) / (minimumOpacityDistance - maximumOpacityDistance));
        if (dist <= minimumOpacityDistance)
        {
            audioSprite.color = Color.white;
            Color opacityColor = audioSprite.color;
            opacityColor.a = lerpAmt;
            audioSprite.color = opacityColor;
            
        }

    }

    private void OnMouseOver()
    {
        if (dist <= minimumOpacityDistance && clickedObject == false)
        {
            audioSprite.material = materialArray[1];
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("I'm no longer mousing over");
            audioSprite.material = materialArray[0];
    }

    private void OnMouseDown()
    {
        clickedObject = true;
        audioSprite.material = materialArray[0];

        Debug.Log("I'm clicking on this object");
        if (audioSprite.color.a >= .3f)
        {
            Debug.Log("I'm triggering BeginClip");
            myClipPlayer.BeginClip();
            audioSprite.DOFade(0f, .5f).OnComplete(() => gameObject.SetActive(false));
        }
    }
    
}
