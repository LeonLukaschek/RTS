﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
    bool isSelecting = false;
    Vector3 mousePosition1;

    public GameObject selectionCirclePrefab;

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if( Input.GetMouseButtonDown( 0 ) )
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
            {
                if( selectableObject.selectionCircle != null )
                {
                    //Destroy( selectableObject.selectionCircle.gameObject );
                    selectableObject.selectionCircle = null;
                }
            }
        }
        // If we let go of the left mouse button, end selection
        if( Input.GetMouseButtonUp( 0 ) )
        {
            var selectedObjects = new List<SelectableUnitComponent>();
            foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
            {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                {
                    selectedObjects.Add( selectableObject );
                }
                else
                {
                    //Unselect
                    GameObject ai = selectableObject.gameObject;
                    AI_Soldier_01 ai_scirpt = ai.GetComponent<AI_Soldier_01>();
                    ai_scirpt.isSelected = false;
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine( string.Format( "Selecting [{0}] Units", selectedObjects.Count ) );
            foreach( var selectedObject in selectedObjects )
                sb.AppendLine( "-> " + selectedObject.gameObject.name );
            Debug.Log( sb.ToString() );

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if( isSelecting )
        {
            foreach( var selectableObject in FindObjectsOfType<SelectableUnitComponent>() )
            {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                {
                    //Change boolean isSelected to true if the ai is selected
                    GameObject ai = selectableObject.gameObject;
                    AI_Soldier_01 ai_scirpt = ai.GetComponent<AI_Soldier_01>();
                    ai_scirpt.isSelected = true;
                }
            }
        }
    }

    public bool IsWithinSelectionBounds( GameObject gameObject )
    {
        if( !isSelecting )

            return false;

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
        return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
    }

    void OnGUI()
    {
        if( isSelecting )
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect( mousePosition1, Input.mousePosition );
            Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
            Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
        }
    }
}