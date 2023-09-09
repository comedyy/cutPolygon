using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Polygon))]
public class PolygonEditor : Editor
{
    void OnSceneGUI()
    {
        Polygon polygon = target as Polygon;

        if(polygon.allPoints.Count == 0)
        {
            polygon.allPoints.Add(new PolygonPoint(){point = new float3(0, 0, 0)});
            polygon.allPoints.Add(new PolygonPoint(){point = new float3(1, 0, 0)});
            polygon.allPoints.Add(new PolygonPoint(){point = new float3(0, 1, 0)});
        }

        for(int i = 0; i < polygon.allPoints.Count; i++)
        {
            var p = polygon.allPoints[i];
            var nextP = polygon.allPoints[(i + 1) % polygon.allPoints.Count];
            if(DrawPoint(ref p, nextP.point))
            {
                polygon.allPoints[i] = p;
            }
        }

        Handles.EndGUI();
    }

    private bool DrawPoint(ref PolygonPoint currentPoint, float3 nextPoint)
    {
        EditorGUI.BeginChangeCheck();

        Handles.color = Color.yellow;
        currentPoint.point = Handles.FreeMoveHandle(currentPoint.point, Quaternion.identity, 0.1f, Vector3.zero, Handles.CubeHandleCap);

        Handles.color = Color.white;
        Handles.DrawLine(currentPoint.point, nextPoint);

        return EditorGUI.EndChangeCheck();
    }
}