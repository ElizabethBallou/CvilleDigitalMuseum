using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroTextData : MonoBehaviour
{

    private TextMeshProUGUI factText;

    private Button continueButton1;
    private Button continueButton2;

    public string factWords;

    public string historicalNarrative;

    


    // Start is called before the first frame update
    void Start()
    {
        factText = gameObject.GetComponent<TextMeshProUGUI>();
        continueButton1 = gameObject.GetComponent<Button>();
        continueButton2 = gameObject.transform.GetChild(1).GetComponent<Button>();
        continueButton1.interactable = false;
        continueButton2.gameObject.SetActive(false);
    }
    
   

    public void OnContinueButtonPress()
    {
        if (historicalNarrative == "TJ")
        {
            factText.DOFade(.5f, 1f);
            continueButton1.image.DOFade(.5f, 1f);
            continueButton1.interactable = false;
            continueButton2.gameObject.SetActive(false);
        }
        else if (historicalNarrative == "other")
        {
            factText.DOFade(.1f, 1f);
            continueButton1.image.DOFade(.1f, 1f);
            continueButton1.interactable = false;
            continueButton2.gameObject.SetActive(false);
        }
        else if (historicalNarrative == "UTR")
        {
            for (int i = 0; i < IntroScript.instance.myIntroTexts.Length; i++)
            {
                IntroScript.instance.myIntroTexts[i].gameObject.SetActive(false);
            }
            IntroScript.instance.followupText();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator PrintText()
    {

        
        string wordsToBeTyped = "";
        for (int i = 0; i < factWords.Length; i++)
        {
            wordsToBeTyped = factWords.Substring(0, i);
            factText.text = wordsToBeTyped;
            yield return new WaitForSeconds(IntroScript.instance.typeSpeed);
        }

        if (factText.text == wordsToBeTyped)
        {
            factText.ForceMeshUpdate();
            continueButton1.interactable = true;
            continueButton2.gameObject.SetActive(true);

        }


    }


    
}
