using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image textBox;
    [HideInInspector] public TextMeshProUGUI textBoxText;
    public Image portraitBackground;
    public Image portraitSprite;
    public Image portraitBorder;
    public Image nameBox;
    public Image nameBoxBorder;
    [HideInInspector] public TextMeshProUGUI nameText;
    [HideInInspector] public Color transparentWhiteColor;
    [HideInInspector] public Color transparentBoxColor;
    [HideInInspector] public Color transparentBlackColor;
    [HideInInspector] public Color transparentPortraitBorderColor;
    public PostProcessVolume myPPV;
    [HideInInspector]
    public Vignette vignette;

    public GameObject startDoor;
    

    private void Awake()
    {
        if (instance!=null){
            Destroy(this.gameObject);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }

        transparentWhiteColor = new Color(255, 255, 255, 0);
        transparentBoxColor = new Color(192, 184, 184, 0);
        transparentBlackColor = new Color(0, 0, 0, 0);
        transparentPortraitBorderColor = new Color(147, 134, 134, 0);

    }

    // Start is called before the first frame update
    void Start()
    {
        SetupAllTextBoxes();
        Invoke("NoGoingBack", 3f);

    }

    private void NoGoingBack()
    {
        Destroy(startDoor);
    }

    public void SetupAllTextBoxes()
    {
        //setup for interview player scripts, which access the text box, text, figure, and PPV set here
        
        //create new colors for transparency functionality
       
        
        //set up the interviewee text box
        portraitBackground.color = transparentBlackColor;
        portraitSprite.color = transparentWhiteColor;
        textBoxText.color = transparentBlackColor;
        textBox.color = transparentBoxColor;
        portraitBorder.color = transparentPortraitBorderColor;
        textBox.gameObject.SetActive(false);
        textBoxText = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = nameBox.GetComponentInChildren<TextMeshProUGUI>();
        

        
        
        myPPV.profile.TryGetSettings(out vignette);

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
