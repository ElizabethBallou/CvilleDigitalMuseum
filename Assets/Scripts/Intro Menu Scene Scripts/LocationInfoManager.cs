using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Whilefun.FPEKit;

public class LocationInfoManager : MonoBehaviour
{
    public static LocationInfoManager instance;
    public float typeSpeed = .1f;
    public float dateDisappearTime = 4f;
    private float currentTime;
    public string[] locationStrings;
    private string currentLocationString = "";
    private TextMeshProUGUI locationText;
    public Image blackBackdrop;
    
    private Color fullyOpaque;
    private GameObject myCanvas;
    [HideInInspector]
    public Scene currentScene;

    private bool isTextFadeTriggered = false;

    public List<GameObject> borderlist;
    private GameObject[] placeholderArray;

    private GameObject playerObject;
    private Transform startTransform;
    
    private GameObject aDoor;
    private FPEDoorway myFPEDoorway;

    private void Awake()
    {
        
        if (instance!=null){
            Destroy(this.gameObject);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }
        fullyOpaque = new Color(255, 255, 255, 255);
        myCanvas = GameObject.Find("Canvas");
        borderlist = new List<GameObject>();


    }

    void Start()
    {
        TextMeshProUGUI placeholderObject = Resources.Load<TextMeshProUGUI>("Prefabs/LocationName");
        locationText = Instantiate(placeholderObject, myCanvas.transform);
        blackBackdrop.gameObject.SetActive(true);
        blackBackdrop.rectTransform.SetAsLastSibling();

        playerObject = GameObject.FindWithTag("Player");

    }
    

    
    // Update is called once per frame
    void Update()
    {
        //the logic for fading away the location text
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

    public void setStage()
    {
        //should be called every time the player enters a new scene
        //if there ARE objects in the borderlist, clear them upon entry.
        if (borderlist != null)
        {
            borderlist.Clear();
        }
        
        //fill the placeholder array (so called b/c we only need it for a second) with all the border objects
        placeholderArray = GameObject.FindGameObjectsWithTag("Border");
        //turn that array into a list
        borderlist = placeholderArray.ToList();
        //find the first object in the list
        aDoor = borderlist[0];
        //get the doorway component from that first object
        myFPEDoorway = aDoor.GetComponent<FPEDoorway>();
        //find the place where the player is supposed to spawn in. This is used if the player clicks "no" upon switching scenes.
        startTransform = GameObject.Find("PlayerStartLocation").GetComponent<Transform>();
        
        locationText.color = fullyOpaque;
        blackBackdrop.DOFade(0f, .5f).OnComplete(() => blackBackdrop.rectTransform.SetAsFirstSibling());
        currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
       
        StartCoroutine(ShowLocationText(locationStrings[sceneIndex -2]));
        
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
    
    

    public void YesMoveScene()
    {
        FPESwitchSceneMenu.instance.deactivateMenu();
        myFPEDoorway.AllowedToTransition = true;
        myFPEDoorway.OnTriggerEnter(playerObject.GetComponent<Collider>());
    }

    public void NoMoveScene()
    {
        playerObject.transform.position = new Vector3(startTransform.position.x, startTransform.position.y, startTransform.position.z);
        FPESwitchSceneMenu.instance.deactivateMenu();
    }
}
