using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCameraController : MonoBehaviour
{
    public float speed = 3f;
    private Camera trailerCamera;
    private bool isGo = false;

    //public GameObject destination;
    public GameObject[] hallwayDestinations;
    public PortraitClicker[] trailerPortraits;
    private int trailerPortraitIndex = 0;
    private int hallwayDestIndex = 0;
    private Vector3 velocity = Vector3.zero;
    private bool waitingDuringFade = false;
    private float fadeTimer = 0;
    private float moveTimer = 0;
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
                Invoke("TriggerTrailerPortraitFade", 2f);
                alreadyPressedR = true;
            }
            if (alreadyPressedR)
            {
                Debug.Log("don't press R again you chucklefuck");
            }
        }

        if (isGo)
        {
            moveTimer += Time.deltaTime;
            Debug.Log("trailerPortraitIndex is " + trailerPortraitIndex);
            transform.position = Vector3.Slerp(transform.position, hallwayDestinations[hallwayDestIndex].transform.position, speed);
            //Debug.Log(transform.position + " , " + hallwayDestinations[hallwayDestIndex].transform.position);
            if (moveTimer >= 4f)
            {
                Debug.Log("isGo is false");
                isGo = false;
                waitingDuringFade = true;
                hallwayDestIndex++;
                Debug.Log("hallwayDestIndex is " + hallwayDestIndex);
                moveTimer = 0f;
                Invoke("TriggerTrailerPortraitFade", 1f);
            }
        }

        if (waitingDuringFade)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer >= 4f)
            {
                Debug.Log("done waiting during fade");
                isGo = true;
                fadeTimer = 0;
                waitingDuringFade = false;
            }
        }

    }

    public void TriggerTrailerPortraitFade()
    {
        Debug.Log("Portrait is fading");
        trailerPortraits[trailerPortraitIndex].trailerfadeout = true;
        trailerPortraitIndex++;

        if (!waitingDuringFade)
        {
            waitingDuringFade = true;
        }
    }

    
}
