using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Spawner))]
public class CreatePath : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.HelpBox("Left Ctrl + Left Mouse Button - put decal on surface", MessageType.Info);

    }
    void OnInspectorUpdate()
    {

    }

    void OnSceneGUI()
    {
        Spawner test = (Spawner)target;
        if (Event.current.control)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }

        if (Event.current.control && Event.current.type == EventType.MouseDown)
        {
            
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 500))
            {
                //decal.transform.position = hit.point;
                //decal.transform.forward = -hit.normal;
                test.pathCoords.Add(hit.point);
                EditorUtility.SetDirty(test.gameObject);
                SceneView.RepaintAll();
                HandleUtility.Repaint();
            }
        }
    }
}
