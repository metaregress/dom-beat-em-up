using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BasicEnemy : Enemy
{

    private Seeker seeker;
    public GameObject player;
    public float speed;
    private Path path;

    private float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    private bool reachedEndOfPath;

    private Vector3 targetLastPosition;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
        contactDamage = 1;
        seeker = GetComponent<Seeker>();
        seeker.pathCallback = OnPathComplete;

        targetLastPosition = player.transform.position;
        

        seeker.StartPath(transform.position, targetLastPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        DoMovement();        

        // simple AI, no pathfinding yet
        // it seems like a popular pathfinding algorithm is called A*
        // but it will require us to have a pathfinding grid before we can do it


    }

    void DoMovement()
    {
        if(targetLastPosition != player.transform.position)
        {
            targetLastPosition = player.transform.position;
            seeker.StartPath(transform.position, targetLastPosition);
        }
        if (path == null)
        {
            return;
        }

        reachedEndOfPath = false;
        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if(distanceToWaypoint < nextWaypointDistance)
            {
                if(currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed;
        transform.position += velocity * Time.deltaTime;
    }

    public override int GetContactDamage()
    {
        return contactDamage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision detected");
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player)
        {
            player.currentHealth -= GetContactDamage();
        }
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);

        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    public void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }
}
