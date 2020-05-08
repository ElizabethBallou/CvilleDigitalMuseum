using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SymbolMouseHover : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer audioSprite;
    private bool clickedObject = false;
    private Camera cameraToLookAt;
    public float minimumOpacityDistance = 7;
    public float maximumOpacityDistance = 4;
    private float dist;
    private Transform playerTransformToAudio;
    private ArtisticStatementClipPlayer myClipPlayer;


    // Start is called before the first frame update
    void Start()
    {
        audioSprite = gameObject.GetComponent<SpriteRenderer>();
        myClipPlayer = gameObject.transform.parent.GetComponent<ArtisticStatementClipPlayer>();
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
            audioSprite.material = HallwayManager.instance.materialArray[1];
        }
    }

    private void OnMouseExit()
    {
        audioSprite.material = HallwayManager.instance.materialArray[0];
    }

    private void OnMouseDown()
    {
        clickedObject = true;
        audioSprite.material = HallwayManager.instance.materialArray[0];

        if (audioSprite.color.a >= .3f)
        {
            Debug.Log("I'm triggering BeginClip");
            myClipPlayer.BeginClip();
            audioSprite.DOFade(0f, .5f).OnComplete(() => gameObject.SetActive(false));
        }
    }

}
