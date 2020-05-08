using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Whilefun.FPEKit;
using TMPro;

public class InventoryPromptTrigger : MonoBehaviour
{
    public Image inventoryPrompt;
    private TextMeshProUGUI promptTextObject;
    public string promptText;

    // Start is called before the first frame update
    void Start()
    {
        promptTextObject = inventoryPrompt.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        inventoryPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        inventoryPrompt.gameObject.SetActive(true);
        promptTextObject.text = promptText;
        FPEInteractionManagerScript.Instance.disableMovement();
        FPEInteractionManagerScript.Instance.disableMouseLook();
        FPEInteractionManagerScript.Instance.setCursorVisibility(true);

    }

    public void okayButtonClick()
    {
        inventoryPrompt.gameObject.SetActive(false);
        FPEInteractionManagerScript.Instance.enableMovement();
        FPEInteractionManagerScript.Instance.enableMouseLook();
        FPEInteractionManagerScript.Instance.setCursorVisibility(false);

        //finally, disable this game object to prevent instructions popping up again
        gameObject.SetActive(false);

    }
}
