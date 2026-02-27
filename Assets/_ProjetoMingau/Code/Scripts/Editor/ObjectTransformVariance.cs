using UnityEditor;
using UnityEngine;

public class ObjectTransformVariance : EditorWindow
{
    [MenuItem("Tools/Prefab/Object variance")]
    public static void ShowWindow()
    {
        GetWindow<ObjectTransformVariance>("Add object transform variance");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Y rot variance"))
        {
            AddYRotVariance();
        }

        if (GUILayout.Button("Reset Y rot variance"))
        {
            ResetYRotVariance();
        }
        EditorGUILayout.EndHorizontal();
    }

    void AddYRotVariance()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            Vector3 rot = new Vector3(
                selectedObject.transform.localRotation.x,
                selectedObject.transform.localRotation.y + Random.Range(-180f, 180f),
                selectedObject.transform.localRotation.z
                );

            selectedObject.transform.localEulerAngles = rot;
        }
    }

    void ResetYRotVariance()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            Vector3 rot = new Vector3(
                selectedObject.transform.localRotation.x,
                0f,
                selectedObject.transform.localRotation.z
                );

            selectedObject.transform.localEulerAngles = rot;
        }
    }
}
