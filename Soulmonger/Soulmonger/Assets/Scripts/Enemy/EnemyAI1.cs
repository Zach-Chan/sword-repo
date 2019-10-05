using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; //use pathfinding namespace

public class EnemyAI1 : MonoBehaviour
{

    //public Transform target;
    public Transform enemyGFX;

    public float speed = 200f;
    public float nextWaypointDistance = 3f; //same thing as target in last one

    private GameObject playerTarget;
    private Transform playerTransform;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //give it start position, target position, and function
        InvokeRepeating("UpdatePath", 0f, .5f);

        playerTarget = GameObject.FindWithTag("Player");
        playerTransform = playerTarget.transform;
    }

    //UpdatePath Function
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, playerTransform.position, OnPathComplete);
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

    // Update is called fixed number of times per second
    void FixedUpdate()
    {
        if (path == null)
            return;

        //if current waypoint has reached the end of all waypoints in a path
        if(currentWaypoint >= path.vectorPath.Count)
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

        //Flipping EnemyGFX
        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        //if currently plotted path is left
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
