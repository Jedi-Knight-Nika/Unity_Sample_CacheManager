using UnityEngine;
using UnityEditor;

public class FindHighestOrderGameObject : EditorWindow
{
    [MenuItem("Tools/Find Highest Sorting Order")]
    public static void ShowWindow()
    {
        GetWindow<FindHighestOrderGameObject>("Find Highest Sorting Order");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Find Highest Sorting Order GameObject"))
        {
            FindHighestSortingOrderGameObject();
        }
    }

    void FindHighestSortingOrderGameObject()
    {
        int highestOrder = int.MinValue;
        GameObject highestOrderGameObject = null;

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (renderer != null && renderer.sortingOrder > highestOrder)
            {
                highestOrder = renderer.sortingOrder;
                highestOrderGameObject = obj;
            }
        }

        if (highestOrderGameObject != null)
        {
            Debug.Log("Highest Sorting Order: " + highestOrder + ", GameObject: " + highestOrderGameObject.name);
        }
        else
        {
            Debug.Log("No SpriteRenderer found in the scene.");
        }
    }
}

