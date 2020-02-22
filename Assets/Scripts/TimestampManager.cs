using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimestampManager : MonoBehaviour
{
    public float typeSpeed = .1f;
    public float dateDisappearTime = 4f;
    private float currentTime;
    public string[] timeStringArray;
    private int timeStringIndex;
    private string currentTimeString = "";
    private TextMeshProUGUI timestampText;
    public Image blackBackdrop;
    private Color dayColor;
    private Color nightColor;
    private Color fullyOpaque;
    private GameObject myCanvas;
    public GameObject torches;

    private bool playerAtLatestTime = false;
    private bool playerAtEarliestTime = false;
    private bool isTextFadeTriggered = false;
    private void Awake()
    {
        dayColor = new Color(217, 210, 185, 100);
        nightColor = new Color(24, 18, 65, 100);
        fullyOpaque = new Color(255, 255, 255, 255);
        myCanvas = GameObject.Find("Canvas");
        
        
    }

    void Start()
    {
        TextMeshProUGUI placeholderObject = Resources.Load<TextMeshProUGUI>("Prefabs/Timestamp");
        timestampText = Instantiate(placeholderObject, myCanvas.transform);
        blackBackdrop.gameObject.SetActive(true);
        blackBackdrop.rectTransform.SetAsLastSibling();
        Invoke("MomentDelay", 1.0f);
        //setStage(timeStringArray[0]);
        timeStringIndex = 0;
    }
    

    
    // Update is called once per frame
    void Update()
    {
        if (isTextFadeTriggered)
        {
            currentTime += Time.deltaTime;
        }
        
        if (currentTime >= dateDisappearTime)
        {
            timestampText.DOFade(0f, 2f);
            isTextFadeTriggered = false;
            currentTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RewindButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FastForwardButtonPress();
        }
        
        PlayerAtEarliestTime();
        PlayerAtLatestTime();
    }

    void setStage(string timestamp)
    {
        timestampText.color = fullyOpaque;
        if (timeStringIndex == 0)
        {
            //RenderSettings.fogColor = dayColor;
            //RenderSettings.fogMode = FogMode.Linear;
            torches.SetActive(false);
        }
        else if (timeStringIndex == 1)
        {
            //RenderSettings.fogColor = nightColor;
            //RenderSettings.fogMode = FogMode.ExponentialSquared;
            //RenderSettings.fogDensity = .03f;
            torches.SetActive(true);
        }

        blackBackdrop.DOFade(0f, .5f).OnComplete(() => blackBackdrop.rectTransform.SetAsFirstSibling());
        StartCoroutine(ShowTimeText(timestamp));
    }
    
    private IEnumerator ShowTimeText(string timemarker)
    {
        for (int i = 0; i < timemarker.Length; i++)
        {
            currentTimeString = timemarker.Substring(0, i);
            timestampText.text = currentTimeString;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTextFadeTriggered = true;

    }

    public void FastForwardButtonPress()
    {
        if (!playerAtLatestTime)
        {
            timeStringIndex = timeStringIndex + 1;
            blackBackdrop.rectTransform.SetAsLastSibling();
            blackBackdrop.DOFade(1f, .5f).OnComplete(() => setStage(timeStringArray[timeStringIndex]));
        }
        else
        {
            
        }
    }

    public void RewindButtonPress()
    {
        if (!playerAtEarliestTime)
        {
            timeStringIndex = timeStringIndex - 1;
            blackBackdrop.rectTransform.SetAsLastSibling();
            blackBackdrop.DOFade(1f, .5f).OnComplete(() => setStage(timeStringArray[timeStringIndex]));
        }
        else
        {
            
        }
    }

    private bool PlayerAtEarliestTime()
    {
        return timeStringIndex == 0;
    }

    private bool PlayerAtLatestTime()
    {
        return timeStringIndex == timeStringArray.Length - 1;
    }

    private void MomentDelay()
    {
        setStage(timeStringArray[0]);
    }
}
