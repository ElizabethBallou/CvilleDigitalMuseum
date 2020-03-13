using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{

    public static IntroScript instance;
    public Button playButton;
    private TextMeshProUGUI playButtonText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subhedText;
    public Image charlottesvilleMap;
    public IntroTextData[] myIntroTexts;
    private int IntroTextArrayIndex = 0;
    public float typeSpeed = .1f;
    public float timeBetweenFacts = 2f;
    
    public TextMeshProUGUI followUpTextObject;
    public Button beginButton;
    private TextMeshProUGUI beginButtonText;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        beginButtonText = beginButton.GetComponentInChildren<TextMeshProUGUI>();
        beginButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButtonPressed()
    {
        playButton.image.DOFade(0f, 1f);
        playButton.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 1f);
        subhedText.DOFade(0f, 1f);
        charlottesvilleMap.DOFade(0f, 1f).OnComplete(() => StartCoroutine(TextController()));
        titleText.DOFade(0f, 1f).OnComplete(() => backgroundAudioManager.instance.myAudioSource.Play());

    }

    private IEnumerator TextController()
    {

        for (int i = 0; i < myIntroTexts.Length; i++)
        {
            myIntroTexts[IntroTextArrayIndex].StartCoroutine("PrintText");
            yield return new WaitForSeconds(timeBetweenFacts);
            IntroTextArrayIndex++;
            Debug.Log("IntroTextArray is " + IntroTextArrayIndex);


        }

    }
    
    public void followupText()
    {
        beginButton.gameObject.SetActive(true);
        followUpTextObject.DOFade(1f, 4f);
        beginButton.image.DOFade(1f, 7f);
        beginButtonText.DOFade(1f, 7f);
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    

}
