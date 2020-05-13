using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Whilefun.FPEKit;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startDoor;

    public GameObject myMinimalHUD;

    [HideInInspector] public int lawnRecordsFound = 0;

    [HideInInspector] public int downtownRecordsFound = 0;

    public int lawnRecordsTotal = 0;

    public int downtownRecordsTotal = 0;

    public TextMeshProUGUI foundRecordsText;

    private bool scoringHasCompleted = false;
    

    private void Awake()
    {
        if (instance!=null){
            Destroy(this.gameObject);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }

        /*transparentWhiteColor = new Color(255, 255, 255, 0);
        transparentBoxColor = new Color(192, 184, 184, 0);
        transparentBlackColor = new Color(0, 0, 0, 0);
        transparentPortraitBorderColor = new Color(147, 134, 134, 0);*/

    }

    // Start is called before the first frame update
    void Start()
    {
        //SetupAllTextBoxes();
        Invoke("NoGoingBack", 3f);

        myMinimalHUD = GameObject.Find("FPEMinimalHUD(Clone)");
        myMinimalHUD.GetComponent<FPEMinimalHUD>().HUDEnabled = true;
        foundRecordsText.gameObject.SetActive(false);

    }

    private void NoGoingBack()
    {
        Destroy(startDoor);
    }

    /*public void SetupAllTextBoxes()
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

    }*/
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFoundRecordCount(int sceneNo)
    {
        if (!scoringHasCompleted)
        {
            if (sceneNo == 2)
            {
                //then it's the Lawn # that needs to be updated
                lawnRecordsFound++;
                foundRecordsText.text = "Lawn Records Found: " + lawnRecordsFound + "/" + lawnRecordsTotal;
            }

            if (sceneNo == 3)
            {
                //then it's the downtown mall
                downtownRecordsFound++;
                foundRecordsText.text = "Downtown Records Found: " + downtownRecordsFound + "/" + downtownRecordsTotal;
            }

            scoringHasCompleted = true;
            Invoke("FixScoringComplete", 2f);
        }
        
    }

    public void FixScoringComplete()
    {
        scoringHasCompleted = false;
    }
    
}
