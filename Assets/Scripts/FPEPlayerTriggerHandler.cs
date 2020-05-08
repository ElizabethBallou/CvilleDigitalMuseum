using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPEPlayerTriggerHandler : MonoBehaviour
{

    public AudioClip[] grassFootsteps;

    public AudioClip[] stoneFootsteps;

    public AudioClip[] woodFootsteps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            FPESwitchSceneMenu.instance.activateMenu();
        }
    }
}
