using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backgroundAudioManager : MonoBehaviour
{
    public static backgroundAudioManager instance;

    public AudioSource myAudioSource;
    public AudioClip[] myBackgroundTracks;
    private bool songSwitched = false;
    
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
        myAudioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (!songSwitched)
            {
                myAudioSource.clip = myBackgroundTracks[1];
                myAudioSource.Play();
                myAudioSource.loop = true;
                songSwitched = true;
            }
        }
    }

}
