using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFieldController : MonoBehaviour
{
    private Transform thisTransform;
    //public GameObject parent;

    public bool detected;
    bool countdown = false;
    public float timerCount;
    float timeLeft;
    private int count;

    System.Timers.Timer chasingTimer;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();

        //Initialize timer with 1 second intervals
        chasingTimer = new System.Timers.Timer(1000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //countdown to stopping detected being true
        if (timeLeft >= 0)
        {
            Debug.Log("countdown timer is running");

            //This function decreases chaseTimer every second
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Debug.Log("timer ended");
                detected = false;
            }
        }
    }

    //Detect when player enters detection field
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision is running");
        //if there is a collision with the collider of tag Player, detected = true
        if (other.transform.tag == "Player")
        {
            detected = true;
        }
    }

    //Start timer to stop chasing when player exits field.
    private void OnTriggerExit2D(Collider2D other)
    {
        //if there is a collision with the collider of tag Player, detected = true
        if (other.transform.tag == "Player")
        {
            timeLeft = timerCount;
            //detected = false;
        }
    }

    //say detected status for other scripts
    public bool DetectStatus
    {
        get { return detected; }
        set { detected = value; }
    }
}
