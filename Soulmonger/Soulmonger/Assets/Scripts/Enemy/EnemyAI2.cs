using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; //use pathfinding namespace

public class EnemyAI2 : MonoBehaviour
{
    //declaring pathfinding
    public AIPath aiPath; //declare pathfinding stuff

    //declaring public variables
    public List<Transform> pathsArray;
    public Transform enemyGFX;
    public GameObject detectField;

    public float speed = 200f;
    public float nextWaypointDistance = 0.5f; //same thing as target in last one

    //declaring private variables
    private DetectFieldController dfScript;
    private GameObject playerTarget;
    private Transform playerTransform;
    private Vector3 normalScale;
    private Vector3 flippedScale;

    Transform targetPos;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    //State boolean vars
    bool detectedResponse;
    string enemyState = "patrol";


    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindWithTag("Player");
        playerTransform = playerTarget.transform;

        //give it start position, target position, and function
        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("UpdateState", 0f, 1f);

        targetPos = pathsArray[0];

        rb.inertia = 0;

        //detectedResponse = detectField.GetComponent("DetectFieldController").detected;
        dfScript = (DetectFieldController)detectField.GetComponent(typeof(DetectFieldController));
        Debug.Log(dfScript.DetectStatus);
        //GotoNextPoint();

        //set vector variables for flipping scale
        normalScale = new Vector3(transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        flippedScale = new Vector3(transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
    }

    //UpdatePath Function
    public void UpdatePath()//Transform targetPos
    {
        //Debug.Log("update path is running");
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, targetPos.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdateState()
    {
        //Debug.Log(dfScript.DetectStatus);

        if (dfScript.DetectStatus == true)
        {
            enemyState = "chase";
            //patrol = false;
            //chase = true;
        } else if (dfScript.DetectStatus == false)
        {
            enemyState = "patrol";
            //patrol = true;
            //chase = false;
        }
        Debug.Log("enemyState = " + enemyState);
    }

    // Update is called fixed number of times per second
    void FixedUpdate()
    {
        //set empty null path handling
        if (path == null)
            return;

        //handle flipping directions of objects
        //if the currently plotted path's x is to the right
        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = flippedScale;
        }
        else if (rb.velocity.x <= 0.01f)
        //if currently plotted path is left
        {
            transform.localScale = normalScale;
        }

        //update based on state
        if  (enemyState == "patrol") //(patrol == true)
        {
            //Debug.Log("patrol state");

            int pathsArrayLength = pathsArray.Count;
            int ranIndex = Random.Range(0, pathsArrayLength);
            targetPos = pathsArray[ranIndex];

            //if current waypoint has reached the end of all waypoints in a path
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                ranIndex = Random.Range(0, pathsArrayLength);
                targetPos = pathsArray[ranIndex];
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            //get direction of object by getting current vector path and subtracting current position
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

            //use this direction as a force to apply to the obj
            Vector2 force = direction * speed * Time.deltaTime;

            //add the force to make obj move
            rb.AddForce(force);

            //figure out distance to next waypoint
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            //if we've reached the current waypoint
            if (distance < nextWaypointDistance)
            {
                //increase currentWaypoint by one to change to next one
                currentWaypoint++;
            }
        }
        if  (enemyState == "chase") //(chase == true)
        {
            //Debug.Log("chase state");
            //update path function to set correct waypoints
            targetPos = playerTransform;

            //if current waypoint has reached the end of all waypoints in a path
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            //get direction of object by getting current vector path and subtracting current position
            //it's like pointing an arrow from curr pos (rb.pos) to where we want to be (path.vect)
            //and normalizing it to make length of vector always 1
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

            //use this direction as a force to apply to the obj
            Vector2 force = direction * speed * Time.deltaTime;

            //add the force to make obj move
            rb.AddForce(force);

            //figure out distance to next waypoint
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            //if we've reached the current waypoint
            if (distance < nextWaypointDistance)
            {
                //increase currentWaypoint by one to change to next one
                currentWaypoint++;
            }
        }
        if (enemyState == "attack") //(attack == true)
        {
            Debug.Log("attack state");
        }
        
    }
}
