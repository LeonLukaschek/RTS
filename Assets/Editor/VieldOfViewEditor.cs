﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(VieldOfView))]
public class VieldOfViewEditor : Editor
{

    void OnSceneGUI()
    {
        VieldOfView fow = (VieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTargets in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTargets.position);
        }
    }

}
