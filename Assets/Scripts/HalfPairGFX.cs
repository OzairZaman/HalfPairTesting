using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HalfPairGFX : MonoBehaviour
{
    #region VARIABLES
    public Transform[] waypoints;
    public Transform waypointParent;

    Transform originalTarget;
    private AIPath aiPath;
    private AIDestinationSetter destSetter;
    private FieldOfView fov;

    private bool isChasing = false;
    float stopRange;
    private int currentIndex = 1;
    private float stoppingDistance = 1f;
    #endregion




    void Start()
    {
        // get children of waypointParent
        waypoints = waypointParent.GetComponentsInChildren<Transform>();

        destSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        originalTarget = destSetter.target;

        fov = GetComponent<FieldOfView>();
        //aiPath.canSearch = false;
    }

    void Update()
    {

        #region EVERY CYCLE
        // range we want the enemy to stop
        stopRange = (originalTarget.position - transform.position).magnitude;
        //fov.viewAngleDirection = aiPath.desiredVelocity.x > 0 ? 90 : -90; ---> not good because we want to not do anything at 0
        //using if / else we can ignore aiPath.desiredVelocity.x = 0

        if (fov.targetIsVisible)
        {
            isChasing = true;
        }

        if (aiPath.desiredVelocity.x > 0)
        {
            fov.viewAngleDirection = 90;
        }
        else if (aiPath.desiredVelocity.x < 0)
        {
            fov.viewAngleDirection = -90;
        }

        //Debug.Log(aiPath.desiredVelocity.magnitude);
        if (aiPath.desiredVelocity.magnitude != 0)
        {
            GetComponent<SpriteRenderer>().flipX = aiPath.desiredVelocity.x > 0;
        }
        #endregion


        #region NOT CHASE MODE
        if (!isChasing)
        {
            Patrol();
        }
        #endregion


        #region CHASE MODE
        if (isChasing)
        {
            if (stopRange < 0.5f)
            {
                aiPath.maxSpeed = 0;
                destSetter.target = this.transform;

                //transform.Translate(-0.02f,0,0);
            }
            else
            {
                destSetter.target = originalTarget;
                aiPath.maxSpeed = 3;
                //aiPath.SearchPath();
                //Debug.Log("steering: " + aiPath.steeringTarget);
            }
        }

        #endregion


        #region METHODS
        void Patrol()
        {
            // get the current way point
            Transform point = waypoints[currentIndex];
            // get distance to way point
            float distance = Vector3.Distance(this.transform.position, point.position);

            // if close to waypoint
            if (distance < stoppingDistance)
            {
                // switch to waypoint
                currentIndex++;

                if (currentIndex >= waypoints.Length)
                {
                    currentIndex = 1;
                }
            }
            destSetter.target = point.transform;
        }
        #endregion
    }
}
