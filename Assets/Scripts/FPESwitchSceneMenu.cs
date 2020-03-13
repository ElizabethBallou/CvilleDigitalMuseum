using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whilefun.FPEKit;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FPESwitchSceneMenu : FPEMenu
{
    public static FPESwitchSceneMenu instance;
    public GameObject Toggler;
    private TextMeshProUGUI togglerText;

    public string location1;
    public string location2;


    public override void Awake()
    {
        if (instance!=null){
            Destroy(gameObject);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        togglerText = Toggler.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //handle the UI for the move scene pop-up
       Toggler.SetActive(false);
    }
    
    
    public override void activateMenu()
    {
        if (!menuActive)
        {
            Toggler.SetActive(true);
            menuActive = true;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                togglerText.text = "Would you like to move " + location1 + "?";
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
                togglerText.text = "Would you like to move " + location2 + "?";
                
            FPEInteractionManagerScript.Instance.disableMovement();
            FPEInteractionManagerScript.Instance.disableMouseLook();
            FPEInteractionManagerScript.Instance.setCursorVisibility(true);
        }

    }

    public override void deactivateMenu()
    {

        if (menuActive)
        {
           Toggler.SetActive(false);
            menuActive = false;
            FPEInteractionManagerScript.Instance.enableMovement();
            FPEInteractionManagerScript.Instance.enableMouseLook();
            FPEInteractionManagerScript.Instance.setCursorVisibility(true);
        }

    }
}
