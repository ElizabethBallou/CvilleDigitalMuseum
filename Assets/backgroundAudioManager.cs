using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundAudioManager : MonoBehaviour
{
    public static backgroundAudioManager instance;

    public AudioSource myAudioSource;
    public AudioClip[] myBackgroundTracks;
    
    private void Awake()
    {
        if (instance!=null){
            Destroy(this.gameObject);
        } else {
            instance = this;         
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
        myAudioSource.clip = myBackgroundTracks[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMySong(int audioCip)
    {
        
    }
}
