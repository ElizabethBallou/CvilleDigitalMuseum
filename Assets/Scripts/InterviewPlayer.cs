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

    private string thisConversationName;
    public InterviewData thisInterview;

    private string transcriptionString;
    private string portraitName;

    private Coroutine intervieweeCoroutine;
    private ConversationInfo thisConversationInfo;

    private int currentChunkIndex = 0;

    private bool alreadyListenedToThis = false;
    private MeshRenderer myHolographRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        myHolographRenderer = gameObject.GetComponent<MeshRenderer>();
        Debug.Assert(TranscriptionDataParser.instance.TranscriptionDataDictionary != null);
        thisConversationName = thisInterview.name;
        thisConversationInfo = TranscriptionDataParser.instance.TranscriptionDataDictionary[thisConversationName];
        assignObjectMaterial();

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
                myAudioSource.PlayOneShot(thisInterview.specificClip);
                triggeredAudioSource = true;
                    if (!GameManager.instance.textBox.IsActive())
                    {
                        ShowTextBox();
                    }
                    
            }

            if (triggeredAudioSource && !myAudioSource.isPlaying)
            {
               // HideTextBox();
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

    private void assignObjectMaterial()
    {
        if (thisConversationInfo.chunks[0].Speaker == Chunk.speakerName.JH)
        {
            myHolographRenderer.material = GameManager.instance.MonacanObjectMat;
        }
        else if (thisConversationInfo.chunks[0].Speaker == Chunk.speakerName.JS ||
                 thisConversationInfo.chunks[0].Speaker == Chunk.speakerName.AD)
        {
            myHolographRenderer.material = GameManager.instance.BAAObjectMat;
        }
        
        else if (thisConversationInfo.chunks[0].Speaker == Chunk.speakerName.PL)
        {
            myHolographRenderer.material = GameManager.instance.JewishObjectMat;
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



        
        intervieweeCoroutine = StartCoroutine(ShowIntervieweeTranscription(thisConversationInfo.chunks));

    }
    
    private IEnumerator ShowIntervieweeTranscription(Chunk[] theseherechunks)
    {
        
        //for each chunk in the chunks
        for (int i = 0; i < theseherechunks.Length; i++)
        {
            
            HandlePortraitActivation(thisConversationInfo.chunks[i].Speaker);

            transcriptionString = thisConversationInfo.chunks[i]
                .speakerText;
        
            
        
            float chunkDuration;
            //if this is not the last chunk...
            if (i < thisConversationInfo.chunks.Length - 1)
            {
                //chunk duration is now set to the time we have between this timestamp and the next time stamp
                chunkDuration = thisConversationInfo.chunks[i + 1].speakerTimestamp -
                                thisConversationInfo.chunks[i].speakerTimestamp;
            }
            else
            {
                //i assume the full time of the interview minus the current time stamp (so the remainder of the interview time)
                chunkDuration = (thisInterview.specificClip.length) - thisConversationInfo.chunks[i].speakerTimestamp;
                Debug.Log("chunkDuration = " + chunkDuration);
                Debug.Log("specificInterviewLength = " + thisInterview.specificClip.length);
            }

            float typeSpeed = chunkDuration / transcriptionString.Length;
            string textToDisplay = "";

            for (int j= 0; j < transcriptionString.Length; j++)
            {
                yield return new WaitForSeconds(typeSpeed);
                if (transcriptionString[j].ToString() == "*")
                {
                    textToDisplay = " ";
                }
                else
                {
                    textToDisplay += transcriptionString[j];
                    GameManager.instance.textBoxText.text = textToDisplay;
                }
            }
 
        }
        HideTextBox();


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

        if (alreadyListenedToThis == false)
        {
            myHolographRenderer.material = GameManager.instance.playedObjectMat;
            alreadyListenedToThis = true;
        }
        
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
