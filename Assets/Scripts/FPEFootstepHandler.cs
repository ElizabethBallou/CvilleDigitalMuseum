using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPEFootstepHandler : MonoBehaviour
{

    private AudioSource myAudioSource;

    public AudioClip[] woodFootsteps;
    public AudioClip[] grassFootsteps;
    public AudioClip[] stoneFootsteps;
    
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickFootstepType(AudioClip[] clipArray)
    {
        var thisClip = clipArray[Random.Range(0, clipArray.Length)];
        myAudioSource.clip = thisClip;
        myAudioSource.pitch = Random.Range(.9f, 1.1f);
        myAudioSource.PlayOneShot(thisClip);
    }
}
