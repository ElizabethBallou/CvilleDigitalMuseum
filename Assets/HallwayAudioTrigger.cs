using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayAudioTrigger : MonoBehaviour
{
    
    private SpriteRenderer audioSprite;
    private Transform playerTransformToAudio;
    public float minimumOpacityDistance = 4;
    public float maximumOpacityDistance = 1;
    private Camera cameraToLookAt;


    // Start is called before the first frame update
    void Start()
    {
        audioSprite = gameObject.GetComponent<SpriteRenderer>();
        playerTransformToAudio = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cameraToLookAt = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        audioSprite.color = Color.clear;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraToLookAt.transform);
        float dist = Vector3.Distance(playerTransformToAudio.position, transform.position);
        float lerpAmt = 1.0f - Mathf.Clamp01((dist - maximumOpacityDistance) / (minimumOpacityDistance - maximumOpacityDistance));
        if (dist <= minimumOpacityDistance)
        {
            audioSprite.color = Color.white;
            Color opacityColor = audioSprite.color;
            opacityColor.a = lerpAmt;
            Debug.Log(lerpAmt);
            audioSprite.color = opacityColor;
        }

    }

    private void OnMouseOver()
    {
        audioSprite.color = Color.gray;
    }

    private void OnMouseExit()
    {
        audioSprite.color = Color.white;
    }

    private void OnMouseDown()
    {
        throw new NotImplementedException();
    }
}
