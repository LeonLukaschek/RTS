﻿using System.Collections.Generic;
using UnityEngine;

public class AI_Soldier_01 : MonoBehaviour
{
    public enum SoldierState { Idle, Patroling, MovingToTarget, Attacking }

    [Header("Patrol")]
    public Transform[] points;

    public SoldierState currentState;

    public Quaternion rota;

    public bool isSelected;

    public int closestIndex;

    public float offset;

    private int desPoint = 0;
    private int count;
    private int counter;

    private float lastInList;
    private float closest;

    private NavMeshAgent agent;
    private VieldOfView fow;
    private Transform closeseTransform;

    private bool patrol = false;

    private void Awake()
    {
    }

    private void Start()
    {
        Spawner sp = GameObject.Find("Spawnhouse").GetComponent<Spawner>();
        this.name = "AI_Soldier(" + sp.counter + ")";
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<VieldOfView>();
        agent.autoBraking = true;
    }

    private void Update()
    {
        ControllUnit();

        //If distance to destination in less than 2.5 and patrol is true, go to next point
        if (agent.remainingDistance < 2.5f && patrol)
            GoToNextPoint();

        //Find the closest enemy in fieldoview if there is an enemy
        FindClosestEnemy();
        //Get the size of the list
        foreach (Transform t in fow.visibleTargets)
        {
            counter++;
        }
        //If there are enemys in the list, look at them
        if (counter != 0)
        {
            closeseTransform = fow.visibleTargets[closestIndex];
            this.transform.LookAt(closeseTransform);
        }
    }

    //Move the unit or let it patrol
    private void ControllUnit()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                patrol = false;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                    rota = hit.transform.rotation;
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
    }

    //Finds the closest enemy in the vof.visibleTargets list
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

    public void AddOffset(float increase)
    {
        offset = increase;
    }
}