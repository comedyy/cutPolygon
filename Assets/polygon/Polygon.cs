using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct PolygonPoint
{
    public float2 point;
}

public class Polygon : MonoBehaviour
{
    public List<PolygonPoint> allPoints = new List<PolygonPoint>();

    public List<LineSegment> allLineSegment = new List<LineSegment>();

    private void OnValidate() 
    {
        allLineSegment.Clear();

        for(int i = 0; i < allPoints.Count; i++)
        {
            var nextP = allPoints[(i + 1) % allPoints.Count].point;
            var currentP = allPoints[i].point;
            allLineSegment.Add(new LineSegment(){
                line = new Line(){ point = allPoints[i].point, dir = nextP - currentP},
                maxValue = math.length(nextP - currentP)
            });
        }
    }
}
