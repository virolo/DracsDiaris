using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        PathManager pathManager = (PathManager)target;
        Vector3 origin = pathManager.transform.position;

        KeyboardEvents(pathManager);

        for (int i = 0; i < pathManager.pathPoints.Count; i++)
        {
            Vector3 worldPoint = origin + pathManager.pathPoints[i];

            Handles.Label(worldPoint + Vector3.up, "Point: " + (i + 1));

            Handles.color = Color.yellow;
            Handles.SphereHandleCap(i, worldPoint, Quaternion.identity, 0.3f, EventType.Repaint);


            Vector3 newWorldPoint = Handles.PositionHandle(worldPoint, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(pathManager, "Move Path Point");
                pathManager.pathPoints[i] = newWorldPoint - origin;
                EditorUtility.SetDirty(pathManager);
            }
        }

        if (pathManager.pathPoints.Count > 1)
        {
            Handles.color = new Color(255, 155, 70, 255);

            for (int i = 0; i < pathManager.pathPoints.Count - 1; i++)
            {
                Vector3 p1 = origin + pathManager.pathPoints[i];
                Vector3 p2 = origin + pathManager.pathPoints[i + 1];

                Handles.DrawLine(p1, p2);
            }
        }
    }

    private void KeyboardEvents(PathManager manager)
    {
        Event e = Event.current;

        if (e.type == EventType.KeyDown)
        {
            if (e.character == '+')
            {
                Undo.RecordObject(manager, "Add Path Point");

                Vector3 newPoint = Vector3.zero;
                if (manager.pathPoints.Count > 0)
                {
                    newPoint = manager.pathPoints[manager.pathPoints.Count - 1] + Vector3.forward;
                }

                manager.pathPoints.Add(newPoint);

                EditorUtility.SetDirty(manager);
            }
            else if (manager.pathPoints.Count > 0 && e.character == '-')
            {
                Undo.RecordObject(manager, "Remove Last Path Point");
                manager.pathPoints.RemoveAt(manager.pathPoints.Count - 1);
                EditorUtility.SetDirty(manager);
            }
        }
    }
}
