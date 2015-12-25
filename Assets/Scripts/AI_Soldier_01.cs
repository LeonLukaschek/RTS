using UnityEngine;
using System.Collections;

public class AI_Soldier_01 : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] points;

    public bool isSelected;

    private int desPoint = 0;

    private NavMeshAgent agent;

    bool patrol = false;
    bool hasTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.autoBraking = true;
    }

    void Update()
    {
        float distance = this.transform.position.magnitude - agent.destination.magnitude;

        if (isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                patrol = false;
                hasTarget = true;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                patrol = true;
                hasTarget = false;
                GoToNextPoint();
            }

            //Go to next patrol point if in range
            if (agent.remainingDistance < .5f && patrol)
                GoToNextPoint(); 

        }


    }

    void GoToNextPoint()
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
