using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Ink.Runtime;

public class InterviewPlayer : MonoBehaviour
{
    private bool triggeredAudioSource = false;
    private AudioSource myAudioSource;
    private Transform playerTransform;

    public float distanceFromPlayer = 2;
    public float boxFadeTime = 1f;
    public float typeSpeed = .005f;

    public InterviewData thisInterview;

    private string inkTranscriptionString;
    private string portraitName;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        var concatString = thisInterview.inkKnot + "." + thisInterview.inkStitch;
        GameManager.instance.story.ChoosePathString(concatString);
        GameManager.instance.story.Continue();
        portraitName = EvaluateInkTag();
        HandlePortraitActivation(portraitName);
        inkTranscriptionString = GameManager.instance.story.currentText;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float dist = Vector3.Distance(playerTransform.position, transform.position);
        if ( dist <= distanceFromPlayer)
            // if the player is within the declared distance...
        {
            if (!myAudioSource.isPlaying && triggeredAudioSource == false)
                // ...and if the clip hasn't already started playing...
            {
                myAudioSource.PlayOneShot(thisInterview.clip);
                triggeredAudioSource = true;
                    if (!GameManager.instance.textBox.IsActive())
                    {
                        ShowTextBox();
                    }
                    
            }

            if (triggeredAudioSource && !myAudioSource.isPlaying)
            {
                HideTextBox();
            }
        }
        else
        {
            if (myAudioSource.isPlaying)
            {
                myAudioSource.Stop();
                HideTextBox();
                triggeredAudioSource = false;
            }

            
        }
    }

    private void ShowTextBox()
    {
        
        if (GameManager.instance.vignette != null)
            DOTween.To(() => GameManager.instance.vignette.intensity.value,
                x => GameManager.instance.vignette.intensity.value = x, .45f, boxFadeTime);
        
        GameManager.instance.textBox.gameObject.SetActive(true);
        GameManager.instance.textBox.DOFade(.82f, boxFadeTime);
        GameManager.instance.textBoxText.DOFade(1f, boxFadeTime);
        GameManager.instance.portraitBackground.DOFade(1f, boxFadeTime);
        GameManager.instance.portraitBorder.DOFade(1f, boxFadeTime);
        GameManager.instance.portraitSprite.DOFade(1f, boxFadeTime);



        
        StartCoroutine(ShowIntervieweeTranscription(inkTranscriptionString));

    }
    
    private IEnumerator ShowIntervieweeTranscription(string transcription)
    {
        for (int i = 0; i < transcription.Length; i++)
        {
            string wordsBeingPrinted = transcription.Substring(0, i);
            GameManager.instance.textBoxText.text = wordsBeingPrinted;
            yield return new WaitForSeconds(typeSpeed);
            
            if (transcription[i].ToString() == "%")
            {
                transcription = transcription.Remove(i, 1);
                GameManager.instance.textBoxText.text = " ";
            }
        }

    }

    private void HideTextBox()
    {
        
        if (GameManager.instance.vignette != null)
            DOTween.To(() => GameManager.instance.vignette.intensity.value,
                x => GameManager.instance.vignette.intensity.value = x, 0, boxFadeTime);
        GameManager.instance.portraitBackground.DOFade(0f, boxFadeTime);
        GameManager.instance.portraitBorder.DOFade(0f, boxFadeTime);
        GameManager.instance.portraitSprite.DOFade(0f, boxFadeTime);
        GameManager.instance.textBox.DOFade(0f, boxFadeTime).OnComplete(() => GameManager.instance.textBox.gameObject.SetActive(false));
        GameManager.instance.textBoxText.DOFade(0f, boxFadeTime).OnComplete(() => RefreshTextBox());
        
    }

    private void RefreshTextBox()
    {
        GameManager.instance.textBoxText.text = "";
    }

    private void HandlePortraitActivation(string speakerName)
    {
        Debug.Log("The speaker name is " + speakerName);
        if (speakerName != "")
        {
            Debug.Log("We here");
            Debug.Log(PortraitInfo.instance.portraitDictionary.Count);
            foreach (string name in PortraitInfo.instance.portraitDictionary.Keys)
            {
                Debug.Log("WE HERE");
                PortraitInfo.instance.portraitDictionary[name].gameObject.SetActive(false);
            }
            
            PortraitInfo.instance.portraitDictionary[speakerName.Trim()].gameObject.SetActive(true);
        }
    }
    
    private string EvaluateInkTag()
    {
        foreach (string s in GameManager.instance.story.currentTags)
        {
            return s;
        }

        return "";
    }
    
}
