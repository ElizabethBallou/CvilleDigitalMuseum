using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCameraController : MonoBehaviour
{
    //basic functionality
    public float speed = 3f;
    private Camera trailerCamera;
    private bool isGo = false;
    public GameObject destination;

    //hallway functionality
    //public GameObject[] hallwayDestinations;
    //public PortraitClicker[] trailerPortraits;
   // private int trailerPortraitIndex = 0;
   // private int hallwayDestIndex = 0;
    //private Vector3 velocity = Vector3.zero;
    //private bool waitingDuringFade = false;
    //private float fadeTimer = 0;
   // private float moveTimer = 0;
    private bool alreadyPressedR = false;
    // Start is called before the first frame update
    void Start()
    {
        trailerCamera = gameObject.GetComponent<Camera>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("RUNNING RUNNING RUNNING");
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!alreadyPressedR)
            {
                //Invoke("TriggerTrailerPortraitFade", 2f);
                alreadyPressedR = true;
                isGo = true;
            }
            if (alreadyPressedR)
            {
                Debug.Log("don't press R again you chucklefuck");
            }
        }

        if (isGo)
        {
            //moveTimer += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, destination.transform.position, speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, destination.transform.rotation, speed);
            /*if (moveTimer >= 4f)
            {
                Debug.Log("isGo is false");
                isGo = false;
                waitingDuringFade = true;
                hallwayDestIndex++;
                Debug.Log("hallwayDestIndex is " + hallwayDestIndex);
                moveTimer = 0f;
                Invoke("TriggerTrailerPortraitFade", 1f);
            }*/
        }

        /*if (waitingDuringFade)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer >= 4f)
            {
                Debug.Log("done waiting during fade");
                isGo = true;
                fadeTimer = 0;
                waitingDuringFade = false;
            }
        }*/

    }

    /*public void TriggerTrailerPortraitFade()
    {
        Debug.Log("Portrait is fading");
        trailerPortraits[trailerPortraitIndex].trailerfadeout = true;
        trailerPortraitIndex++;

        if (!waitingDuringFade)
        {
            waitingDuringFade = true;
        }
    }*/

    
}
