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

    private int sceneNum;
    public enum IntervieweeName
    {
        Andrea,
        Jalane,
        Phyllis,
        Jeffrey,
        Elizabeth
    }

    public static Dictionary<IntervieweeName, bool> MetInterviewees;
    
    public Image textBox;
    public TextMeshProUGUI textBoxText;
    public Image portraitBackground;
    public Image portraitSprite;
    public Image portraitBorder;
    private Color transparentWhiteColor;
    private Color transparentBoxColor;
    private Color transparentBlackColor;
    private Color transparentPortraitBorderColor;
    public PostProcessVolume myPPV;
    [HideInInspector]
    public Vignette vignette;
    
    public float typeSpeed;
    
    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        TextBoxSetup();
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        MetInterviewees = new Dictionary<IntervieweeName, bool>();
        foreach (IntervieweeName thisName in Enum.GetValues(typeof(IntervieweeName)))
        {
            MetInterviewees.Add(thisName, false);
        }
        
        
    }

    public void TextBoxSetup()
    {
        //setup for interview player scripts, which access the text box, text, figure, and PPV set here
        transparentWhiteColor = new Color(255, 255, 255, 0);
        transparentBlackColor = new Color(0, 0, 0, 0);
        transparentBoxColor = new Color(192, 184, 184, 0);
        transparentPortraitBorderColor = new Color(147, 134, 134, 0);
        portraitBackground.color = transparentBlackColor;
        portraitSprite.color = transparentWhiteColor;
        textBoxText.color = transparentBlackColor;
        textBox.color = transparentBoxColor;
        portraitBorder.color = transparentPortraitBorderColor;
        
        textBox.gameObject.SetActive(false);
        myPPV.profile.TryGetSettings(out vignette);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneNum);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

   /*  public void SelectTranscription(string knot, string stitch)
    {
        var concatString = knot + "." + stitch;
        story.ChoosePathString(concatString);
    } */
    
    
}
