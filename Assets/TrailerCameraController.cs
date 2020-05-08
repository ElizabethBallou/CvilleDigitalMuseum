using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerCameraController : MonoBehaviour
{
    public float speed = 3f;
    private Camera trailerCamera;

    public GameObject destination;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        trailerCamera = gameObject.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, destination.transform.position, ref velocity, speed);

    }
}
