using UnityEngine;

public class AI_Soldier_01_Enemy : MonoBehaviour
/*
*TODO
*Add spawningn of new waypoints
*/
{
    public GameObject target;
    public GameObject wayPointPref;

    public Transform[] wayPoints;

    private NavMeshAgent agent;

    private int currentPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
        agent.autoRepath = true;
    }

    private void Update()
    {
        if (!target)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        agent.destination = wayPoints[currentPoint].position;

        if (agent.remainingDistance <= .5f)
        {
            currentPoint++;
            if (wayPoints.Length <= currentPoint)
            {
                currentPoint = 0;
            }
        }
    }
}