using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestLineIntersection : MonoBehaviour
{
    public Line l1;
    public Line l2;

    public static bool GetIntersectionPercent(Line line1, Line line2, out float2 intersectionPercent)
    {
        var pOffSet = line2.point - line1.point;
        float2x2 matrix = new float2x2(line1.dir.x, - line2.dir.x, line1.dir.y, -line2.dir.y);
        intersectionPercent = math.mul(math.inverse(matrix), pOffSet);

        return !float.IsNaN(intersectionPercent.x) && !float.IsNaN(intersectionPercent.y);
    }

    private void OnDrawGizmos() {
        if(GetIntersectionPercent(l1, l2, out var interSectionPoint))
        {
            var p = l1.point + l1.dir * interSectionPoint.x;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ToVector3(p), 1);
        }
        
        Gizmos.color = Color.white;
        DrawLine(l1);
        DrawLine(l2);

    }

    public void DrawLine(Line l)
    {
        var min = l.point + l.dir * short.MinValue;
        var max = l.point + l.dir * short.MaxValue;
        Gizmos.DrawLine(ToVector3(min), ToVector3(max));
    }

    Vector3 ToVector3(float2 p)
    {
        return new Vector3(p.x, p.y, 0);
    }
}
