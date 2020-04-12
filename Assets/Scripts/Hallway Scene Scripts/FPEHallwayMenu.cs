using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Whilefun.FPEKit;
using UnityEngine.UI;
using TMPro;


public class FPEHallwayMenu : FPEMenu
{
    public static FPEHallwayMenu instance;
    [HideInInspector] public Image HallwayTextBox;
    [HideInInspector] public TextMeshProUGUI hallwayTextBoxText;
    [HideInInspector] public bool hallwayTextHidden;
    [HideInInspector] public Color transparentBoxColor;
    [HideInInspector] public Color transparentBlackColor;
    

    public override void Awake()
    {
        instance = this;
        
        transparentBoxColor = new Color(192, 184, 184, 0);
        transparentBlackColor = new Color(0, 0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //set up the hallway text box
        HallwayTextBox = gameObject.GetComponent<Image>();
        hallwayTextBoxText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        HallwayTextBox.color = transparentBoxColor;
        hallwayTextBoxText.color = transparentBlackColor;
        HallwayTextBox.gameObject.SetActive(false);
    }
    
}
