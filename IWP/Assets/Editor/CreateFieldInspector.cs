using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateField))]

public class CreateFieldInspector : Editor
{
   public CreateField current//casts the targeted object correctly
   {
        get
        {
            return (CreateField)target;
        }
   }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear"))
            current.clear();
        if (GUILayout.Button("Increase"))
            current.increaseTilesize();
        if (GUILayout.Button("Decrease"))
            current.decreaseTilesize();
        if (GUILayout.Button("Increase Field"))
            current.IncreaseField();
        if (GUILayout.Button("Decrease Field"))
            current.DecreaseField();
        if (GUILayout.Button("Save"))
            current.save();
        if (GUILayout.Button("Load"))
            current.Load();

        if (GUI.changed)
            current.updateIndicator();
    }

}
