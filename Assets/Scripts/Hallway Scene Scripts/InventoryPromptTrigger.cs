using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Whilefun.FPEKit;

public class InventoryPromptTrigger : MonoBehaviour
{
    public Image inventoryPrompt;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPrompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        inventoryPrompt.gameObject.SetActive(true);
        FPEInteractionManagerScript.Instance.disableMovement();
        FPEInteractionManagerScript.Instance.disableMouseLook();
        FPEInteractionManagerScript.Instance.setCursorVisibility(true);

    }

    public void okayButtonClick()
    {
        Debug.Log("I'm getting clicked!");
        inventoryPrompt.gameObject.SetActive(false);
        FPEInteractionManagerScript.Instance.enableMovement();
        FPEInteractionManagerScript.Instance.enableMouseLook();
        FPEInteractionManagerScript.Instance.setCursorVisibility(false);

        //finally, disable this game object to prevent instructions popping up again
        gameObject.SetActive(false);

    }
}
