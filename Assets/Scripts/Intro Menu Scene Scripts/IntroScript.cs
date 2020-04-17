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
    public float timeBetweenFacts = 2f;
    public GameObject[] overwrittenHistoryPrefabs;
    public int overwritteHistoryPrefabIndex = 0;
    public GameObject[] JeffersonianHistoryPrefabs;
    public int JeffersonianHistoryPrefabIndex = 0;
    public Button giveUpButton;
    private TextMeshProUGUI giveUpButtonText;

    public RectTransform panel;

    public float factDisappearTime = 2f;
    [HideInInspector]
    public bool firstFactSpawned;

    private bool beginButtonPressed;

    private float timer = 0f;

    private float JeffersonHeight = 120f;
    private float JeffersonWidth = 295f;
    private float overwrittenHeight = 140f;
    private float overwrittenWidth = 310f;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        giveUpButtonText = giveUpButton.GetComponentInChildren<TextMeshProUGUI>();
        giveUpButton.image.color = Color.clear;
        giveUpButtonText.color = Color.clear;
        giveUpButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (beginButtonPressed)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R))
            {
                timer = 29f;
            }
            if (timer >= 30f)
            {
                giveUpButton.gameObject.SetActive(true);
                giveUpButton.image.DOFade(1f, 2f);
                giveUpButtonText.DOFade(1f, 2f);
            }
        }
    }

    public void playButtonPressed()
    {
        beginButtonPressed = true;
        beginButton.image.DOFade(0f, 1f);
        beginButton.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 1f);
        subhedText.DOFade(0f, 1f).OnComplete(() => ActivateFirstFact());
        titleText.DOFade(0f, 1f).OnComplete(() => backgroundAudioManager.instance.myAudioSource.Play());

    }

    public void ActivateFirstFact()
    {
        //this is the function that runs after the player clicks the start button
        GameObject prefabToSpawn = overwrittenHistoryPrefabs[0];
        overwritteHistoryPrefabIndex++;
        //Vector3 spawnPosition = Camera.main.ScreenToViewportPoint(new Vector3(Random.Range(0,Screen.width),0,Random.Range(0,Screen.height)));  //Random.Range(xPosMinLimit, xPosMaxLimit);

        float xPos = Random.Range(-overwrittenWidth, overwrittenWidth);
        float yPos = Random.Range(-overwrittenHeight, overwrittenHeight);
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
        GameObject spawnObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, panel.transform);
        spawnObject.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

    }


    public void ActivateFactPair()
    {
        GameObject overwrittenHistoryFact1 = overwrittenHistoryPrefabs[overwritteHistoryPrefabIndex];
        overwritteHistoryPrefabIndex++;
        if (overwritteHistoryPrefabIndex == 6)
        {
            overwritteHistoryPrefabIndex = 0;
        }
        
        float xPos = Random.Range(-overwrittenWidth, overwrittenWidth);
        float yPos = Random.Range(-overwrittenHeight, overwrittenHeight);
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
        GameObject spawnObject = Instantiate(overwrittenHistoryFact1, spawnPosition, Quaternion.identity, panel.transform);
        spawnObject.GetComponent<RectTransform>().anchoredPosition = spawnPosition;


        GameObject JeffersonianHistoryFact1 = JeffersonianHistoryPrefabs[JeffersonianHistoryPrefabIndex];
        JeffersonianHistoryPrefabIndex++;
        if (JeffersonianHistoryPrefabIndex == 4)
        {
            JeffersonianHistoryPrefabIndex = 0;
        }
        
        float xPos2 = Random.Range(JeffersonWidth* -1, JeffersonWidth);
        float yPos2 = Random.Range(JeffersonHeight* -1, JeffersonHeight);
        Vector3 spawnPosition2 = new Vector3(xPos2, yPos2, 0f);
        GameObject spawnObject2 = Instantiate(JeffersonianHistoryFact1, spawnPosition2, Quaternion.identity, panel.transform);
        spawnObject2.GetComponent<RectTransform>().anchoredPosition = spawnPosition2;

    }
    

    public void LoadNextScene()
    {
        /* if (FPESaveLoadManager.Instance.SavedGameExists())
        {
            Debug.Log("I'm trying to continue the game, which is weird as hell");
            myFPEMainMenu.continueGame();
        }*/
        Debug.Log("I am calling startNewGame");
        myFPEMainMenu.startNewGame();
    }

}
