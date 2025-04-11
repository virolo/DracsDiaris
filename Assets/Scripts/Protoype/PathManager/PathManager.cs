using System;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] public List<Vector3> pathPoints;


    private LineRenderer _lineRenderer;

    public List<Vector3> GetPathPoints()
    {
        List<Vector3> worldPoints = new List<Vector3>();

        foreach (Vector3 localPoint in pathPoints)
        {
            worldPoints.Add(transform.TransformPoint(localPoint));
        }

        return worldPoints;
    }


    private void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;

        _lineRenderer.positionCount = pathPoints.Count;
        _lineRenderer.SetPositions(pathPoints.ToArray());

        _lineRenderer.startWidth = _lineRenderer.endWidth = 0.2f;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.black;
        _lineRenderer.endColor = Color.black;

        _lineRenderer.alignment = LineAlignment.View;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
            return;

        DrawPathGizmos();
    }

    private void DrawPathGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < pathPoints.Count; i++)
        {
            Vector3 worldPos = transform.TransformPoint(pathPoints[i]);
            Gizmos.DrawSphere(worldPos, 0.15f);

            if (i < pathPoints.Count - 1)
            {
                Vector3 nextWorldPos = transform.TransformPoint(pathPoints[i + 1]);
                Gizmos.DrawLine(worldPos, nextWorldPos);
            }
        }
    }
#endif
}