using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public Button beginButton;
    public TextMeshProUGUI titleText;

    public TextMeshProUGUI contentWarningText;
    public Button readContentWarning;
    // Start is called before the first frame update
    void Start()
    {
        readContentWarning.gameObject.SetActive(false);
        contentWarningText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginButtonPressed()
    {
        beginButton.image.DOFade(0f, 1f);
        beginButton.GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, 1f);
        titleText.DOFade(0f, 1f);
        readContentWarning.gameObject.SetActive(true);
        readContentWarning.image.DOFade(1f, 1f).SetDelay(1f);
        readContentWarning.GetComponentInChildren<TextMeshProUGUI>().DOFade(1f, 1f).SetDelay(1f);
        contentWarningText.gameObject.SetActive(true);
        contentWarningText.DOFade(1f, 1f).SetDelay(1f);
    }

    public void ContentWarningButtonPressed()
    {
        SceneManager.LoadScene("The Lawn");
    }
}
