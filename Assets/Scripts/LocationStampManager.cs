using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationStampManager : MonoBehaviour
{
    public float typeSpeed = .1f;
    public float dateDisappearTime = 4f;
    private float currentTime;
    public string locationString;
    private string currentLocationString = "";
    private TextMeshProUGUI locationText;
    public Image blackBackdrop;
    private Color fullyOpaque;
    private GameObject myCanvas;
    public GameObject torches;

    private bool isTextFadeTriggered = false;
    private void Awake()
    {
        fullyOpaque = new Color(255, 255, 255, 255);
        myCanvas = GameObject.Find("Canvas");
        
        
    }

    void Start()
    {
        TextMeshProUGUI placeholderObject = Resources.Load<TextMeshProUGUI>("Prefabs/LocationName");
        locationText = Instantiate(placeholderObject, myCanvas.transform);
        blackBackdrop.gameObject.SetActive(true);
        blackBackdrop.rectTransform.SetAsLastSibling();
        Invoke("MomentDelay", 1.0f);
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
            locationText.DOFade(0f, 2f);
            isTextFadeTriggered = false;
            currentTime = 0;
        }
    }

    void setStage(string location)
    {
        locationText.color = fullyOpaque;
        blackBackdrop.DOFade(0f, .5f).OnComplete(() => blackBackdrop.rectTransform.SetAsFirstSibling());
        StartCoroutine(ShowLocationText(location));
        
    }
    
    private IEnumerator ShowLocationText(string inputstring)
    {
        for (int i = 0; i < inputstring.Length; i++)
        {
            currentLocationString = inputstring.Substring(0, i);
            locationText.text = currentLocationString;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTextFadeTriggered = true;

    }
    
    private void MomentDelay()
    {
        setStage(locationString);
    }
}
