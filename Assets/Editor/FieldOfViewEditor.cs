using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    //handles in the scene window
    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        // DrawWireArc parameters
        //     : center posion of arc - GameObject the FieldOfView script is attached to
        //     : rotate around forward axis
        //     : start from Up direction
        //     : size of the angle of the arc 360 (full circle)
        //     : radius is public variable in our FieldOfView Script
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Debug.Log(viewAngleA);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
        Handles.color = Color.red;
        foreach (Transform target in fow.visibleTargetList)
        {
            Handles.DrawLine(fow.transform.position, target.position);
            
        }


    }

}
