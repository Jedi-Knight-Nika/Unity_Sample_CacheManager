using UnityEngine;
using UnityEditor;

public class RenameBruiseGameObjects : EditorWindow
{
    private string oldName = "";
    private string newName = "";

    [MenuItem("Tools/Rename GameObjects")]
    private static void OpenRenameWindow()
    {
        GetWindow<RenameBruiseGameObjects>("Rename GameObjects");
    }

    void OnGUI()
    {
        GUILayout.Label("Rename GameObjects", EditorStyles.boldLabel);
        oldName = EditorGUILayout.TextField("Old Name", oldName);
        newName = EditorGUILayout.TextField("New Name", newName);

        if (GUILayout.Button("Rename"))
        {
            RenameGameObjects(oldName, newName);
        }
    }

    private static void RenameGameObjects(string oldName, string newName)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (var obj in allObjects)
        {
            if (obj.name == oldName)
            {
                Undo.RecordObject(obj, $"Rename '{oldName}' to '{newName}'");
                obj.name = newName;
                count++;
            }
        }

        Debug.Log($"{count} '{oldName}' GameObjects renamed to '{newName}'.");
    }
}