using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HistoricalFactIndividualHandler : MonoBehaviour
{
    public string historyType;
    
    private Image myFactholderImage;
    private Image myBorderImage;

    private TextMeshProUGUI myText;

    private Button myButton;
    
    private Vector3 correctSize = .66f * Vector3.one;

    private Vector3 largerSize = .7f * Vector3.one;

    // Start is called before the first frame update
    void Start()
    {


        gameObject.transform.localScale = correctSize;
        
        //note to self: order matters here! Underneath the parent gameobject, the children MUST be in this order: border, square background sprite, then text.
        myBorderImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        
        myFactholderImage = gameObject.transform.GetChild(1).GetComponent<Image>();
        
        myText = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
        myButton = gameObject.GetComponentInChildren<Button>();
        
        // perform the newly spawned "pop" animation

        gameObject.transform.DOScale(largerSize, .15f)
            .OnComplete(() => gameObject.transform.DOScale(correctSize, .15f));

        
        if (IntroScript.instance.firstFactSpawned)
        {
            if (historyType == "overwritten")
            {
                myFactholderImage.DOFade(0f, 2f);
                myBorderImage.DOFade(0f, 2f);
                myText.DOFade(0f, 2f);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onObjectClick()
    {
        IntroScript.instance.ActivateFactPair();
        if (!IntroScript.instance.firstFactSpawned)
        {
            if (historyType == "overwritten")
            {
                myFactholderImage.DOFade(0f, 2f);
                myBorderImage.DOFade(0f, 2f);
                myText.DOFade(0f, 2f);
            }

            IntroScript.instance.firstFactSpawned = true;
        }
      
    }
}
