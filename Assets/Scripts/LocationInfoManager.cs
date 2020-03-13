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

    private List<GameObject> borderlist;
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
        currentScene = SceneManager.GetActiveScene();
        borderlist = new List<GameObject>();


    }

    void Start()
    {
        TextMeshProUGUI placeholderObject = Resources.Load<TextMeshProUGUI>("Prefabs/LocationName");
        locationText = Instantiate(placeholderObject, myCanvas.transform);
        blackBackdrop.gameObject.SetActive(true);
        blackBackdrop.rectTransform.SetAsLastSibling();

        playerObject = GameObject.FindWithTag("Player");

        Invoke("MomentDelay", .5f);
    }
    

    
    // Update is called once per frame
    void Update()
    {
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

    void setStage()
    {
        if (borderlist != null)
        {
            borderlist.Clear();
        }
        
        
        placeholderArray = GameObject.FindGameObjectsWithTag("Border");
        borderlist = placeholderArray.ToList();
        aDoor = borderlist[0];
        myFPEDoorway = aDoor.GetComponent<FPEDoorway>();
        startTransform = GameObject.Find("PlayerStartLocation").GetComponent<Transform>();
        Debug.Log("The name of aDoor is " + aDoor.name);
        
        locationText.color = fullyOpaque;
        blackBackdrop.DOFade(0f, .5f).OnComplete(() => blackBackdrop.rectTransform.SetAsFirstSibling());
        int sceneIndex = currentScene.buildIndex;
        Debug.Log("SceneIndex is " + sceneIndex);
       
        StartCoroutine(ShowLocationText(locationStrings[sceneIndex -1]));
        
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
    
    private void MomentDelay()
    {
        setStage();
    }

    public void YesMoveScene()
    {
        if (currentScene.buildIndex == 1)
        {
            FPESwitchSceneMenu.instance.deactivateMenu();
            myFPEDoorway.AllowedToTransition = true;
            myFPEDoorway.OnTriggerEnter(playerObject.GetComponent<Collider>());
        }

        if (currentScene.buildIndex == 2)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void NoMoveScene()
    {
        
        playerObject.transform.position = new Vector3(startTransform.position.x, startTransform.position.y, startTransform.position.z);
        FPESwitchSceneMenu.instance.deactivateMenu();
    }
}
