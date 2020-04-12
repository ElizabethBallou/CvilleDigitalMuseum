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
    
    //Variables for calculating typing-out. Since all calculations take place inside update, i needed to define these here. 
    private bool _isTypingOut;
    private int _chunkIndex = 0; 
    private bool _setNewChunk;
    private float _lerpPercent;
    private float _currentChunkDuration;
    private float _timer;
    private string _transcriptionText = "";
    private int _cutIndex;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Debug.Assert(TranscriptionDataParser.instance.TranscriptionDataDictionary != null);
        thisConversationName = thisInterview.name;
        thisConversationInfo = TranscriptionDataParser.instance.TranscriptionDataDictionary[thisConversationName];

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

                    //Start typing out text.
                    _isTypingOut = true; //set a flag to tell us to start the typing-out algorithm!
                    _setNewChunk = true; //set a flag to tell us to update to a new chunk
                    _chunkIndex = -1; //reset the chunkIndex to -1. (it will get set to 0 later)
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
        
        //HANDLE TYPING OUT TEXT---------------------------------------------------------------------------------------------------------------
        if (_isTypingOut)
        {
            _timer += Time.deltaTime; //increase timer so we know how much time is elapsed

            //The contents of the If-Statement below handle setting a new chunk. This should not happen every frame, hence
            //why we check the _setNewChunk flag to see if it has been switched on for us to update to the next chunk
            if (_setNewChunk)
            {
                _setNewChunk = false; //now set that boolean back to false. ensure we don't call this bit of code every frame.

                //if we have run out of chunks to display, hide the textbox
                if (_chunkIndex >= thisConversationInfo.chunks.Length - 1)
                {
                    HideTextBox();
                }
                //otherwise, do some setup for the next chunk
                else
                {
                    _chunkIndex++; //move to the next chunk in the conversation
                    _timer = 0; //reset the timer to 0. (timer is only used to keep track of elapsed time between each chunk, which is why it should be 0 when we first set a new chunk)
                    _cutIndex = 0; //reset cut index (used for substring calculations later)

                    HandlePortraitActivation(thisConversationInfo.chunks[_chunkIndex].Speaker); //set new portrait, because someone new is talking

                    _transcriptionText = thisConversationInfo.chunks[_chunkIndex].speakerText; //get a reference to the speakerText in the current chunk

                    //calculate the duration of the current chunk based on timestamps. 
                    if (_chunkIndex < thisConversationInfo.chunks.Length - 1)
                    {
                        //chunk duration is now set to the time we have between this timestamp and the next time stamp
                        _currentChunkDuration = thisConversationInfo.chunks[_chunkIndex + 1].speakerTimestamp -
                                        thisConversationInfo.chunks[_chunkIndex].speakerTimestamp;
                    }
                    else
                    {
                        //i assume the full time of the interview minus the current time stamp (so the remainder of the interview time)
                        _currentChunkDuration = (thisInterview.specificClip.length) - thisConversationInfo.chunks[_chunkIndex].speakerTimestamp;
                    }

                }
            }

            //Ok here's where we type out the text. This algorithm runs every frame.

            //calculate what percent "done" we are with this chunk in terms of timing.
            //So if chunk is 10 seconds long, and 5 seconds has elapsed since we started the chunk, we are 50% done.
            _lerpPercent = _timer / _currentChunkDuration; 

            //get the current index in the string we should be at, based on the percent "done" we are with this chunk
            //So if we are 50% "done" with this chunk, and the transcriptionText has 200 characters, then the first 100 characters should be typed out.
            int charIndex = (int)Mathf.Lerp(0, _transcriptionText.Length, _lerpPercent);

            //Get all the characters we should have typed out so far based on the charIndex.
            //So if charIndex is 100, then totalStringSoFar will be a string with the first 100 characters of the total string
            string totalStringSoFar = _transcriptionText.Substring(0, charIndex);

            //Here is where we need to handle starting the text back at the top when we've reached overflow
            //check to see if there are any * in the text at all
            if (totalStringSoFar.Contains("*"))
            {
                //_cutIndex is a variable I am using to track where the beginning of the paragraph should start
                //so here we set the cut index to the most recent occurance of a * in the string. So the
                //typing-out will start one character after the most recent * found.
                _cutIndex = totalStringSoFar.LastIndexOf('*') + 1;

            }

            //Get the length of how many characters we want displayed on the screen this frame.
            int charCount = charIndex - _cutIndex;

            //Display a substring from the most recent "*" to the last character we should be typing out, based on
            //the percent "finished" we are with this chunk.
            if (_isTypingOut)
            {
                GameManager.instance.textBoxText.text = totalStringSoFar.Substring(_cutIndex, charCount);
                //Debug.Log("_cutIndex is " + _cutIndex + ", and charCount is " + charCount);
            }

            //the _lerpPercent value will be greater than 1 when the _timer is >= the duration of the current chunk.
            //When this happens, we need to set the _setNewChunk flag to true, so that the next frame of Update,
            //we call the if-statement that sets a new chunk
            if (_lerpPercent > 1)
            {
                _setNewChunk = true;
            }

        }
        //---------------------------------------------------------------------------------------------------------------------------------------



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
        GameManager.instance.nameBox.DOFade(1f, boxFadeTime);
        GameManager.instance.nameText.DOFade(1f, boxFadeTime);
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
        GameManager.instance.nameBox.DOFade(0f, boxFadeTime);
        GameManager.instance.nameText.DOFade(0f, boxFadeTime);
        GameManager.instance.nameBoxBorder.DOFade(0f, boxFadeTime);

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

            switch (thisSpeaker)
            {
                case Chunk.speakerName.AD:
                    GameManager.instance.nameText.text = "Dr. Andrea Douglas";
                    break;
                case Chunk.speakerName.EB:
                    GameManager.instance.nameText.text = "Elizabeth Ballou";
                    break;
                case Chunk.speakerName.JH:
                    GameManager.instance.nameText.text = "Dr. Jeffrey Hantman";
                    break;
                case Chunk.speakerName.JS:
                    GameManager.instance.nameText.text = "Dr. Jalane Schmidt";
                    break;
                case Chunk.speakerName.PL:
                    GameManager.instance.nameText.text = "Dr. Phyllis Leffler";
                    break;

            }
        }
    }
    
 
}
