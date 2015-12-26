using System.Collections.Generic;
using UnityEngine;

public class AI_Soldier_01 : MonoBehaviour
{
    public enum SoldierState { Idle, Patroling, MovingToTarget, Attacking }

    [Header("Patrol")]
    public Transform[] points;

    public SoldierState currentState;

    public bool isSelected;

    public int closestIndex;

    private int desPoint = 0;
    private int count;

    private float lastInList;
    private float closest;

    private NavMeshAgent agent;
    private VieldOfView fow;

    private bool patrol = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<VieldOfView>();
        agent.autoBraking = true;
    }

    private void Update()
    {
        FindClosestEnemy();

        if (isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                patrol = false;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                    currentState = SoldierState.MovingToTarget;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                patrol = true;
                currentState = SoldierState.Patroling;
                GoToNextPoint();
            }
        }

        //If distance to destination in less than 2.5 and patrol is true, go to next point
        if (agent.remainingDistance < 2.5f && patrol)
            GoToNextPoint();
    }

    private void FindClosestEnemy()
    {
        if (fow.visibleTargets.Count > 0)
        {
            foreach (Transform t in fow.visibleTargets)
            {
                float distance = Vector3.Distance(this.transform.position, t.transform.position);
                if (distance < lastInList)
                {
                    closest = distance;
                    closestIndex = count;
                }

                lastInList = distance;
                count++;
            }
            Debug.Log("Closest: " + closestIndex);
            count = 0; 
        }
    }

    private void GoToNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[desPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        desPoint = (desPoint + 1) % points.Length;
    }
}