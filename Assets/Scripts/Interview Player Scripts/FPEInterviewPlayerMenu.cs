using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whilefun.FPEKit;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class FPEInterviewPlayerMenu : FPEMenu
{
    public static FPEInterviewPlayerMenu instance;
    public Image textBox;
    [HideInInspector] public TextMeshProUGUI textBoxText;
    public Image portraitBackground;
    //public Image portraitSprite;
    public Image portraitBorder;
    public Image nameBox;
    public Image nameBoxBorder;
    [HideInInspector] public TextMeshProUGUI nameText;
   

    public float boxFadeTime = 1f;

    public PostProcessVolume myPPV;
    public Vignette vignette;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

       
        textBoxText = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = nameBox.GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Start()
    {
        SetupAllTextBoxes();
    }

    public void SetupAllTextBoxes()
    {
        //set up the interviewee text box
      
        textBox.gameObject.SetActive(false);
        myPPV.profile.TryGetSettings(out vignette);

    }

    public void ShowTextBox()
    {
        FPEInteractionManagerScript.Instance.disableMovement();

        if (vignette != null)
            DOTween.To(() => vignette.intensity.value,
                x => vignette.intensity.value = x, .45f, boxFadeTime);
        textBox.gameObject.SetActive(true);
        textBox.DOFade(.82f, boxFadeTime);
        textBoxText.DOFade(1f, boxFadeTime);
        portraitBackground.DOFade(1f, boxFadeTime);
        portraitBorder.DOFade(1f, boxFadeTime);
        //portraitSprite.DOFade(1f, boxFadeTime);
        nameBox.DOFade(1f, boxFadeTime);
        nameBoxBorder.DOFade(1f, boxFadeTime);
        nameText.DOFade(1f, boxFadeTime);

        foreach (var portrait in PortraitInfo.instance.portraitArray)
        {
            portrait.DOFade(1, boxFadeTime);
        }

        GameManager.instance.myMinimalHUD.GetComponent<FPEMinimalHUD>().HUDEnabled = false;

    }

    public void HideTextBox()
    {
        FPEInteractionManagerScript.Instance.enableMovement();

        if (vignette != null)
            DOTween.To(() => vignette.intensity.value,
                x => vignette.intensity.value = x, 0, boxFadeTime);
        portraitBackground.DOFade(0f, boxFadeTime);
        portraitBorder.DOFade(0f, boxFadeTime);
        //portraitSprite.DOFade(0f, boxFadeTime);
        textBox.DOFade(0f, boxFadeTime).OnComplete(() => textBox.gameObject.SetActive(false));
        textBoxText.DOFade(0f, boxFadeTime);
        nameBox.DOFade(0f, boxFadeTime);
        nameText.DOFade(0f, boxFadeTime);
        nameBoxBorder.DOFade(0f, boxFadeTime);

        foreach (var portrait in PortraitInfo.instance.portraitArray)
        {
            portrait.DOFade(0f, boxFadeTime);
        }
        GameManager.instance.AddFoundRecordCount(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.myMinimalHUD.GetComponent<FPEMinimalHUD>().HUDEnabled = true;


    }


}