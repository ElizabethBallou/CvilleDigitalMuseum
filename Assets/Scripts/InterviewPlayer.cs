using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InterviewPlayer : MonoBehaviour
{
    private bool triggeredAudioSource = false;
    private AudioSource myAudioSource;
    private Transform playerTransform;

    public float distanceFromPlayer = 2;
    public float boxFadeTime = 1f;
    public float typeSpeed = .005f;

    public string conversationName;
    public InterviewData thisInterview;

    private string transcriptionString;
    private string portraitName;

    private Coroutine intervieweeCoroutine;
    private ConversationInfo thisConversationInfo;

    private int currentChunkIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


        thisConversationInfo = TranscriptionDataParser.instance.TranscriptionDataDictionary[conversationName];


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
        transcriptionString = thisConversationInfo.chunks[0]
            .speakerText;
        
        HandlePortraitActivation(thisConversationInfo.chunks[0].Speaker);
        
        if (GameManager.instance.vignette != null)
            DOTween.To(() => GameManager.instance.vignette.intensity.value,
                x => GameManager.instance.vignette.intensity.value = x, .45f, boxFadeTime);
        GameManager.instance.textBox.gameObject.SetActive(true);
        GameManager.instance.textBox.DOFade(.82f, boxFadeTime);
        GameManager.instance.textBoxText.DOFade(1f, boxFadeTime);
        GameManager.instance.portraitBackground.DOFade(1f, boxFadeTime);
        GameManager.instance.portraitBorder.DOFade(1f, boxFadeTime);
        GameManager.instance.portraitSprite.DOFade(1f, boxFadeTime);



        
        intervieweeCoroutine = StartCoroutine(ShowIntervieweeTranscription(transcriptionString));

    }
    
    private IEnumerator ShowIntervieweeTranscription(string transcription)
    {
        float chunkDuration;
        //if this is not the last chunk...
        if (currentChunkIndex < thisConversationInfo.chunks.Length - 1)
        {
            chunkDuration = thisConversationInfo.chunks[currentChunkIndex + 1].speakerTimestamp -
                            thisConversationInfo.chunks[currentChunkIndex].speakerTimestamp;
        }
        else
        {
            chunkDuration = thisInterview.clip.length - thisConversationInfo.chunks[currentChunkIndex].speakerTimestamp;
        }

        float timeElapsed = 0;

        for (int i = 0; i < transcription.Length; i++)
        {
            string wordsBeingPrinted = transcription.Substring(0, i);
            GameManager.instance.textBoxText.text = wordsBeingPrinted;
            timeElapsed += Time.deltaTime;
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
        StopCoroutine(intervieweeCoroutine);
        GameManager.instance.textBoxText.text = "";
    }

    private void HandlePortraitActivation(Chunk.speakerName thisSpeaker)
    {
        if (thisSpeaker != Chunk.speakerName.none)
        {
            foreach (Chunk.speakerName interviewee in PortraitInfo.instance.portraitDictionary.Keys)
            {
                PortraitInfo.instance.portraitDictionary[interviewee].gameObject.SetActive(false);
            }
            
            PortraitInfo.instance.portraitDictionary[thisSpeaker].gameObject.SetActive(true);
        }
    }
    
 
}
