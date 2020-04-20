using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Whilefun.FPEKit;


public class ArtisticStatementClipPlayer : MonoBehaviour
{
    public AudioClip myClip;
    [HideInInspector] public bool triggeredAudioSource = false;
    private AudioSource myAudioSource;
    public float boxFadeTime = 1f;

    private bool alreadyListenedToThis = false;
    
    //Variables for calculating typing-out. Since all calculations take place inside update, i needed to define these here. 
    private bool _isTypingOut;
    private float _lerpPercent;
    private float _currentClipDuration;
    private float _timer;
    private int _cutIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        _currentClipDuration = myClip.length;
    }

    public void BeginClip()
    {
        Debug.Log("BeginClip is being called");
        if (!myAudioSource.isPlaying && triggeredAudioSource == false)
            //if the clip hasn't already started playing...
        {
            if (!alreadyListenedToThis)
                //...and if I haven't already listened to this...
            {

                myAudioSource.PlayOneShot(myClip);
                triggeredAudioSource = true;
                if (!FPEHallwayMenu.instance.HallwayTextBox.IsActive())
                {
                    ShowHallwayTextBox();
                    Debug.Log("The hallway text box is being set with text from " + this.gameObject);
                    _isTypingOut = true; //set a flag to tell us to start the typing-out algorithm!

                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        //HANDLE TYPING OUT TEXT---------------------------------------------------------------------------------------------------------------
          if (_isTypingOut)
          {
              _timer += Time.deltaTime; //increase timer so we know how much time is elapsed

              //_timer = 0; //reset the timer to 0. (timer is only used to keep track of elapsed time between each chunk, which is why it should be 0 when we first set a new chunk)
              //_cutIndex = 0; //reset cut index (used for substring calculations later)


          }

          //Ok here's where we type out the text. This algorithm runs every frame.

            //calculate what percent "done" we are with this chunk in terms of timing.
            //So if chunk is 10 seconds long, and 5 seconds has elapsed since we started the chunk, we are 50% done.
            _lerpPercent = _timer / _currentClipDuration;
            
            //get the current index in the string we should be at, based on the percent "done" we are with this chunk
            //So if we are 50% "done" with this chunk, and the transcriptionText has 200 characters, then the first 100 characters should be typed out.
            int charIndex = (int)Mathf.Lerp(0, HallwayManager.instance.hallwayLineArray[HallwayManager.instance.hallwayLineArrayIndex].Length, _lerpPercent);

            //Get all the characters we should have typed out so far based on the charIndex.
            //So if charIndex is 100, then totalStringSoFar will be a string with the first 100 characters of the total string
            string totalStringSoFar = HallwayManager.instance.hallwayLineArray[HallwayManager.instance.hallwayLineArrayIndex].Substring(0, charIndex);
            if (totalStringSoFar == HallwayManager.instance.hallwayLineArray[HallwayManager.instance.hallwayLineArrayIndex] && FPEHallwayMenu.instance.hallwayTextHidden == false)
            {
                Invoke("HideHallwayTextBox", 1f);
                Invoke("turnOffLights", 1f);
                FPEHallwayMenu.instance.hallwayTextHidden = true;
            }

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
                FPEHallwayMenu.instance.hallwayTextBoxText.text = totalStringSoFar.Substring(_cutIndex, charCount);
            }
            

        
    }
    
    private void ShowHallwayTextBox()
    {
        //prevent the player from moving
        FPEInteractionManagerScript.Instance.disableMovement();
        FPEHallwayMenu.instance.hallwayTextHidden = false;
        Debug.Log("ShowHallwayTextBox() is being called by " + gameObject);
        FPEHallwayMenu.instance.HallwayTextBox.gameObject.SetActive(true);
        FPEHallwayMenu.instance.HallwayTextBox.DOFade(.82f, boxFadeTime);
        FPEHallwayMenu.instance.hallwayTextBoxText.DOFade(1f, boxFadeTime);

    }

    private void HideHallwayTextBox()
    {
        Debug.Log("HideHallwayTextBox() is being called by " + gameObject);
        FPEHallwayMenu.instance.HallwayTextBox.DOFade(0f, boxFadeTime).OnComplete(() => gameObject.SetActive(false));
        FPEHallwayMenu.instance.hallwayTextBoxText.DOFade(0f, boxFadeTime).OnComplete(() => FPEHallwayMenu.instance.HallwayTextBox.gameObject.SetActive(false));
        Invoke("RefreshHallwayTextBox", boxFadeTime);
        
        //let the player move and look again
        FPEInteractionManagerScript.Instance.enableMovement();
        
    }
    
    private void RefreshHallwayTextBox()
    {
        Debug.Log("RefreshHallwayTextBox() is being called by " + gameObject);
        FPEHallwayMenu.instance.hallwayTextBoxText.text = "";
        
        //increase the index of the array in HallwayMiscFunctions
        HallwayManager.instance.increaseIndexCount();
    }

    private void turnOffLights()
    {
        HallwayManager.instance.hallwayLightArray[HallwayManager.instance.hallwayLightArrayIndex].gameObject.SetActive(false);
        HallwayManager.instance.hallwayLightArrayIndex++;
    }
    
}
