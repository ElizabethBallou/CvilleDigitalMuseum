using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InterviewPlayer : MonoBehaviour
{
    public GameManager.IntervieweeName intervieweeName;

    private bool startedPlaying;

    public AudioClip interviewClip;

    public AudioClip introductionClip;

    private AudioSource myAudioSource;
    private Transform playerTransform;

    public float distanceFromPlayer = 2;
    public float boxFadeTime = 1f;
    public float typeSpeed = .05f;

    public InterviewData thisInterview;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {

            


        }
        float dist = Vector3.Distance(playerTransform.position, transform.position);
        if ( dist <= distanceFromPlayer)
            // if the player is within the declared distance...
        {
            if (!startedPlaying)
                // ...and if the clip hasn't already started playing...
            {
                startedPlaying = true;
                if (!GameManager.MetInterviewees[intervieweeName])
                    // if the player has never encountered this character before...
                {
                    // open the text box and play the intro clip
                    ShowTextBox();
                    StartCoroutine(PlayIntroClip());
                }
                else
                // if the player HAS encountered this character before
                {
                    myAudioSource.PlayOneShot(thisInterview.clip);
                    if (!GameManager.instance.textBox.IsActive())
                    {
                        ShowTextBox();
                    }
                }
            }
        }
        else
        {
            startedPlaying = false;
            if (myAudioSource.isPlaying)
            {
                myAudioSource.Stop();
                HideTextBox();
            }
        }
    }

    private IEnumerator PlayIntroClip()
    {
        myAudioSource.PlayOneShot(introductionClip);
        yield return new WaitForSeconds(introductionClip.length);
        GameManager.MetInterviewees[intervieweeName] = true;
        myAudioSource.PlayOneShot(interviewClip);

    }

    private void ShowTextBox()
    {
        
        if (GameManager.instance.vignette != null)
            DOTween.To(() => GameManager.instance.vignette.intensity.value,
                x => GameManager.instance.vignette.intensity.value = x, .45f, boxFadeTime);
        
        GameManager.instance.textBox.gameObject.SetActive(true);
        GameManager.instance.humanFigure.gameObject.SetActive(true);
        GameManager.instance.textBox.DOFade(.82f, boxFadeTime);
        GameManager.instance.textBoxText.DOFade(1f, boxFadeTime);
        GameManager.instance.humanFigure.DOFade(.75f, boxFadeTime);
        StartCoroutine(ShowIntervieweeTranscription("This is test text"));
    }
    
    private IEnumerator ShowIntervieweeTranscription(string transcription)
    {
        for (int i = 0; i < transcription.Length; i++)
        {
            string wordsBeingPrinted = transcription.Substring(0, i);
            GameManager.instance.textBoxText.text = wordsBeingPrinted;
            yield return new WaitForSeconds(typeSpeed);
        }

    }

    private void HideTextBox()
    {
        
        if (GameManager.instance.vignette != null)
            DOTween.To(() => GameManager.instance.vignette.intensity.value,
                x => GameManager.instance.vignette.intensity.value = x, 0, boxFadeTime);
        
        GameManager.instance.textBox.DOFade(0f, boxFadeTime).OnComplete(() => GameManager.instance.textBox.gameObject.SetActive(false));
        GameManager.instance.textBoxText.DOFade(0f, boxFadeTime).OnComplete(() => RefreshTextBox());
        GameManager.instance.humanFigure.DOFade(0f, boxFadeTime)
            .OnComplete(() => GameManager.instance.humanFigure.gameObject.SetActive(false));
    }

    private void RefreshTextBox()
    {
        GameManager.instance.textBoxText.text = "";
    }
}
