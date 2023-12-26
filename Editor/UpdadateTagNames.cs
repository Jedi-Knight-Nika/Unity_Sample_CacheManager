using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UpdateTagNames : EditorWindow
{
    private List<string> oldTags = new List<string>();
    private List<string> newTags = new List<string>();
    private int numberOfTags = 1;

    [MenuItem("Tools/Change Tags")]
    public static void ShowWindow()
    {
        GetWindow<UpdateTagNames>("Change Tags");
    }

    void OnGUI()
    {
        GUILayout.Label("Change Tags", EditorStyles.boldLabel);

        numberOfTags = Mathf.Max(1, EditorGUILayout.IntField("Number of Tags", numberOfTags));

        while (oldTags.Count < numberOfTags) oldTags.Add("");
        while (newTags.Count < numberOfTags) newTags.Add("");
        while (oldTags.Count > numberOfTags) oldTags.RemoveAt(oldTags.Count - 1);
        while (newTags.Count > numberOfTags) newTags.RemoveAt(newTags.Count - 1);

        for (int i = 0; i < numberOfTags; i++)
        {
            oldTags[i] = EditorGUILayout.TextField("Old Tag " + (i + 1), oldTags[i]);
            newTags[i] = EditorGUILayout.TextField("New Tag " + (i + 1), newTags[i]);
        }

        if (GUILayout.Button("Change Tags"))
        {
            for (int i = 0; i < numberOfTags; i++)
            {
                ChangeTag(oldTags[i], newTags[i]);
            }

            Debug.Log("Tags changed successfully.");
        }
    }

    private void ChangeTag(string oldTag, string newTag)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(oldTag))
        {
            obj.tag = newTag;
            EditorUtility.SetDirty(obj);
        }
    }
}