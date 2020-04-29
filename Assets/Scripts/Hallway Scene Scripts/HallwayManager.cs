using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Whilefun.FPEKit;

public class HallwayManager : MonoBehaviour
{
    public static HallwayManager instance;
    private Vector3 debugVector = new Vector3(0.1f, -0.8978958f, 32.9f);
    public FPEDoorway myFPEDoorway;
    private TextAsset hallwayTextLines;
    [HideInInspector] public string[] hallwayLineArray;
    [HideInInspector] public int hallwayLineArrayIndex = 0;
    public Light[] hallwayLightArray;
    [HideInInspector] public int hallwayLightArrayIndex = 0;
    public Material[] materialArray;
    public TextMeshProUGUI movementTips;
    private float movementTipsDisappear = 0;
    
    public TextMeshPro clickText;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        myFPEDoorway.AllowedToTransition = true;
        hallwayTextLines = Resources.Load<TextAsset>("hallwayTextLines");
        hallwayLineArray = hallwayTextLines.text.Split('\n');

    }

    void Update()
    {
        movementTipsDisappear += Time.deltaTime;
        if (movementTipsDisappear >= 5f)
        {
            movementTips.DOFade(0f, 2f);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("the r key has been pressed");
            useDebugReset();
        }
        
    }

    private void useDebugReset()
    {
        Debug.Log("Debug reset called");
        FPEPlayer.Instance.gameObject.transform.localPosition = debugVector;
    }

    public void increaseIndexCount()
    {
        hallwayLineArrayIndex++;
    }
}
