﻿using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    public UnitSelectionComponent uSelect;

    public List<GameObject> soldiers = new List<GameObject>();

    public float offset;
    public int offsetCounter = 1;

    private int soldierCount;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Reset();
            for (int i = 0; i < uSelect.selectedSoldiers.Count; i++)
            {
                soldiers.Add(uSelect.selectedSoldiers[i].gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Formate();
        }
    }

    private void Reset()
    {
        soldiers.Clear();
        offset = 0;
        offsetCounter = 0;
    }

    private void Formate()
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            Debug.Log("Formating i: " + i);
            AddOffset();
            if (i == 0)
            {
                soldiers[i].GetComponent<NavMeshAgent>().stoppingDistance = 0.05f;
                soldiers[i].GetComponent<NavMeshAgent>().destination += new Vector3(offset, 0, 0);
                soldiers[i].transform.rotation = soldiers[i].GetComponent<AI_Soldier_01>().rota;
            }
            if (i != 0)
            {
                soldiers[i].GetComponent<AI_Soldier_01>().AddOffset(offset);
                soldiers[i].GetComponent<NavMeshAgent>().stoppingDistance = 0.05f;
                soldiers[i].GetComponent<NavMeshAgent>().destination += new Vector3(offset, 0, 0);
            }
        }
    }

    private void AddOffset()
    {
        Debug.Log("Adding offset: " + offset);

        if (offsetCounter % 2 == 0)
        {
            offset = offset * (-1);
            offset += 2.5f;
            offsetCounter++;
        }
        else
        {
            offset = offset * (-1);
            offsetCounter++;
        }
    }
}