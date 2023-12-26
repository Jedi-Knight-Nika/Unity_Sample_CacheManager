using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class AttachGameObjects : EditorWindow
{
    private string targetGameObjectName = "StartPanel";
    private string searchString = "gunshot";

    [MenuItem("Tools/Attach Objects")]
    private static void OpenAttachWindow()
    {
        GetWindow<AttachGameObjects>("Attach Objects");
    }

    void OnGUI()
    {
        GUILayout.Label("Attach GameObjects", EditorStyles.boldLabel);
        targetGameObjectName = EditorGUILayout.TextField("Target GameObject Name", targetGameObjectName);
        searchString = EditorGUILayout.TextField("Search String", searchString);

        if (GUILayout.Button("Attach Objects"))
        {
            AttachObjectsToScript(targetGameObjectName, searchString);
        }
    }

    private static void AttachObjectsToScript(string targetName, string searchStr)
    {
        GameObject targetGameObject = GameObject.Find(targetName);

        MonoBehaviourScript script = targetGameObject?.GetComponent<MonoBehaviourScript>();

        if (script != null)
        {
            var objectsToAttach = GameObject.FindObjectsOfType<GameObject>()
                                            .Where(obj => obj.name.Contains(searchStr))
                                            .ToArray();

            List<GameObject> currentObjects = new List<GameObject>(script.gunshotWounds ?? new GameObject[0]);
            currentObjects.AddRange(objectsToAttach);

            script.gunshotWounds = currentObjects.ToArray();

            EditorUtility.SetDirty(script);
        }
        else
        {
            Debug.LogError($"MonoBehaviourScript script not found on the GameObject '{targetName}'.");
        }
    }
}