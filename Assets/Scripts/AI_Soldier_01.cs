using UnityEngine;
using System.Collections;

public class AI_Soldier_01 : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] points;

    private int desPoint = 0;

    NavMeshAgent agent;

    bool patrol = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            patrol = false;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            patrol = true;
            GoToNextPoint();
        }

        if (agent.remainingDistance < 0.5f && patrol)
            GoToNextPoint();

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
