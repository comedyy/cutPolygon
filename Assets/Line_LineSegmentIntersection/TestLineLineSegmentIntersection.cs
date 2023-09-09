using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestLineLineSegmentIntersection : MonoBehaviour
{
    public Line l1;
    public LineSegment segment;

    public static bool GetIntersection(Line line1, LineSegment line2, out float2 intersectionPercent)
    {
        if(!TestLineIntersection.GetIntersectionPercent(line1, line2.line, out intersectionPercent))
        {
            return false;
        }

        return line2.maxValue >= intersectionPercent.y && intersectionPercent.y >= 0;
    }

    private void OnDrawGizmos() {
        if(GetIntersection(l1, segment, out var intersectionPercent))
        {
            var p = l1.point + l1.dir * intersectionPercent.x;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ToVector3(p), 1);
        }

        Gizmos.color = Color.white;
        DrawLineSegment(l1, short.MinValue, short.MaxValue);
        DrawLineSegment(segment.line, 0, segment.maxValue);

    }

    public static void DrawLineSegment(Line l, float minPercent, float maxPercent)
    {
        var min = l.point + l.dir * minPercent;
        var max = l.point + l.dir * maxPercent;
        Gizmos.DrawLine(ToVector3(min), ToVector3(max));
    }

    static Vector3 ToVector3(float2 p)
    {
        return new Vector3(p.x, p.y, 0);
    }
}
