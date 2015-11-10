using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PatrolPath))]
public class PatrolPathEditor : Editor {

    private PatrolPath script {  get { return target as PatrolPath; } }

    SerializedProperty direction;
    SerializedProperty closedPath;
    SerializedProperty waypoints;


    void OnEnable()
    {

        direction = serializedObject.FindProperty("direction");
        closedPath = serializedObject.FindProperty("closedPath");
        waypoints = serializedObject.FindProperty("waypoints");

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //DrawDefaultInspector();

        EditorGUILayout.PropertyField(direction);
        EditorGUILayout.PropertyField(closedPath);

        //EditorGUILayout.PropertyField(waypoints);
        for(int i = 0; i< script.waypoints.Count; i++)
        {
            GUILayout.BeginHorizontal();

            var result = EditorGUILayout.ObjectField(script.waypoints[i], typeof(Transform), true) as Transform ;
            if (GUI.changed)
            {
                script.waypoints[i] = result;
            }

            if (GUILayout.Button("remove" , GUILayout.Width(80f)))
            {
                RemoveWaypoint(i);
            }

            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Waypoint"))
        {
            AddWaypoint();
        }


        serializedObject.ApplyModifiedProperties();
       
    }

    private void RemoveWaypoint(int i)
    {
        DestroyImmediate(script.waypoints[i].gameObject);
        script.waypoints.RemoveAt(i);
    }

    private void AddWaypoint()
    {
        GameObject go = new GameObject("Waypoint");
        go.transform.parent = script.transform;
        go.transform.localPosition = Vector3.zero;

        go.AddComponent<Waypoint>();

        script.waypoints.Add(go.transform);
    }
}
