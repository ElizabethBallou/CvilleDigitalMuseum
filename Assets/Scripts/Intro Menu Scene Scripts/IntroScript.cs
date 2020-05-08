using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using Whilefun.FPEKit;
using Random = UnityEngine.Random;

public class IntroScript : MonoBehaviour
{

    public static IntroScript instance;
    public FPEMainMenu myFPEMainMenu;
    public Button beginButton;
    private TextMeshProUGUI beginButtonText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subhedText;
    public Image charlottesvilleMap;
    public Image typeWriter;
    public Button spacebarButton;
    private AudioSource spacebarBell;
    public AudioClip bellSound;
    private AudioSource jeffersonTypingAudioSource;
    private AudioSource buriedTypingAudioSource;
    public AudioClip[] typingSoundArray;

    //Fact and typing info
    private TextAsset jeffersonFacts;
    private TextAsset buriedFacts;
    private string[] jeffersonFactArray;
    private string[] buriedFactArray;
    private int jeffersonFactArrayIndex = -1;
    private int buriedFactArrayIndex = 0;
    public TextMeshProUGUI JeffersonianFactText;
    public TextMeshProUGUI BuriedFactText;
    public float typeTime = .1f;
    private bool isTypingBuriedFact = false;
    private bool isJeffersonRunning = false;
    private bool isGettingDarker = false;
    private bool isGettingLighter = false;

    public Button giveUpButton;
    private TextMeshProUGUI giveUpButtonText;

    public float factDisappearTime = 2f;
    [HideInInspector]

    private bool beginButtonPressed;

    private float continueTimer = 0f;
    private float timeUntilJeffersonFact = 0f;
    private float spacebarInteractableTimer = 0f;
    private float colorChangeTimer = 0f;

    private Color clearWhite = new Color(255, 255, 255, 0);
    private Color graySpaceBarColor = new Color(128, 128, 128, 255);

    public GameObject myMinimalHUD;

   


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        giveUpButtonText = giveUpButton.GetComponentInChildren<TextMeshProUGUI>();
        giveUpButton.image.color = clearWhite;
        giveUpButtonText.color = clearWhite;
        giveUpButton.gameObject.SetActive(false);

        typeWriter.color = clearWhite;
        spacebarButton.image.color = clearWhite;
        typeWriter.gameObject.SetActive(false);
        spacebarButton.gameObject.SetActive(false);

        myMinimalHUD = GameObject.Find("FPEMinimalHUD(Clone)");
        myMinimalHUD.GetComponent<FPEMinimalHUD>().HUDEnabled = false;

        jeffersonFacts = Resources.Load<TextAsset>("JeffersonFacts");
        jeffersonFactArray = jeffersonFacts.text.Split('\n');
        buriedFacts = Resources.Load<TextAsset>("BuriedFacts");
        buriedFactArray = buriedFacts.text.Split('\n');

        spacebarBell = spacebarButton.gameObject.GetComponent<AudioSource>();
        jeffersonTypingAudioSource = JeffersonianFactText.gameObject.GetComponent<AudioSource>();
        buriedTypingAudioSource = BuriedFactText.gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       

        if (isTypingBuriedFact)
        {
            timeUntilJeffersonFact += Time.deltaTime;
            if (timeUntilJeffersonFact >= 7f)
            {
                fadeBuriedFact();
                JeffersonianFactText.DOFade(1f, .1f);
                StartCoroutine(TypeJeffersonFact(jeffersonFactArray[jeffersonFactArrayIndex]));
                timeUntilJeffersonFact = 0;
                isTypingBuriedFact = false;
            }
        }
        if (beginButtonPressed)
        {
            continueTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R))
            {
                continueTimer = 29f;
            }
            if (continueTimer >= 80f)
            {
                giveUpButton.gameObject.SetActive(true);
                giveUpButton.image.DOFade(1f, 2f);
                giveUpButtonText.DOFade(1f, 2f);
            }
        }

        if (isJeffersonRunning || isTypingBuriedFact)
        {
            spacebarButton.interactable = false;
        }
        else
        {
            spacebarButton.interactable = true;
        }
        if (spacebarButton.interactable == true)
        {
            if (isGettingDarker)
            {
                colorChangeTimer += Time.deltaTime;
                spacebarButton.image.DOFade(0f, 1f);
                if (colorChangeTimer >= 1f)
                {
                    isGettingDarker = false;
                    isGettingLighter = true;
                    colorChangeTimer = 0f;
                }
            }
            else if (isGettingLighter)
            {
                colorChangeTimer += Time.deltaTime;
                spacebarButton.image.DOFade(1f, 1f);
                Debug.Log("Fading in...");
                if (colorChangeTimer >= 1f)
                {
                    isGettingDarker = true;
                    isGettingLighter = false;
                    colorChangeTimer = 0f;
                }
            }
        }
       

    }

    public void playButtonPressed()
    {
        beginButtonPressed = true;
        beginButton.image.DOFade(0f, 1f);
        beginButton.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 1f);
        subhedText.DOFade(0f, 1f);
        titleText.DOFade(0f, 1f).OnComplete(() => backgroundAudioManager.instance.myAudioSource.Play());

        typeWriter.gameObject.SetActive(true);
        spacebarButton.gameObject.SetActive(true);
        typeWriter.DOFade(1f, 1f).SetDelay(1f);
        spacebarButton.image.DOFade(1f, 1f).SetDelay(1f).OnComplete(() => isGettingDarker = true);
    }

    public void LoadNextScene()
    {
        /* if (FPESaveLoadManager.Instance.SavedGameExists())
        {
            Debug.Log("I'm trying to continue the game, which is weird as hell");
            myFPEMainMenu.continueGame();
        }*/
        myFPEMainMenu.startNewGame();
    }

    public void OnSpacebarPress()
    {
        JeffersonianFactText.DOFade(0f, .1f);
        BuriedFactText.DOFade(1f, .1f);
        StartCoroutine(TypeBuriedFact(buriedFactArray[buriedFactArrayIndex]));
        isTypingBuriedFact = true;
        spacebarBell.PlayOneShot(bellSound);
        jeffersonFactArrayIndex++;
        if (jeffersonFactArrayIndex == jeffersonFactArray.Length)
        {
            jeffersonFactArrayIndex = -1;
        }
    }

    IEnumerator TypeJeffersonFact(string jeffersonFact)
    {
        for (int i = 0; i < jeffersonFact.Length; i++)
        {
            isJeffersonRunning = true;
            JeffersonianFactText.text = jeffersonFact.Substring(0, i);
            var thisTypingSound = typingSoundArray[Random.Range(0, typingSoundArray.Length)];
            jeffersonTypingAudioSource.pitch = Random.Range(.9f, 1.1f);
            jeffersonTypingAudioSource.PlayOneShot(thisTypingSound);
            yield return new WaitForSeconds(typeTime);
        }

        isJeffersonRunning = false;
    }

    IEnumerator TypeBuriedFact(string buriedFact)
    {
        for (int i = 0; i < buriedFact.Length; i++)
        {
            BuriedFactText.text = buriedFact.Substring(0, i);
            var thisTypingSound = typingSoundArray[Random.Range(0, typingSoundArray.Length)];
            buriedTypingAudioSource.pitch = Random.Range(.9f, 1.1f);
            if (isTypingBuriedFact)
            {
                buriedTypingAudioSource.volume = .1f;
            }
            else
            {
                buriedTypingAudioSource.volume = 0;
            }
            buriedTypingAudioSource.PlayOneShot(thisTypingSound);
            yield return new WaitForSeconds(typeTime);
        }
    }

    public void fadeBuriedFact()
    {
        BuriedFactText.DOFade(0f, 10f).OnComplete(() => BuriedFactText.text = " ");
        buriedFactArrayIndex++;
        if (buriedFactArrayIndex == buriedFactArray.Length)
        {
            buriedFactArrayIndex = 0;
        }
    }

   
}
