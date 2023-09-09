using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

class InterSectionPoint
{
    public int segmentIndex;
    public float percentSelfSegment;  // 在自己线段里面的percent
    public float percentCut;        // 在切割的那条线段里的。 
}

class CutRange
{
    public int fromIndex;
    public int endIndex;

    public bool Cut(CutRange segment)
    {
        if(fromIndex == 0 && endIndex == 0) // 初始状态
        {
            return true;
        }
        else if(fromIndex < endIndex)
        {
            if(segment.fromIndex < segment.endIndex)
            {
                return fromIndex <= segment.fromIndex && endIndex >= segment.endIndex;
            }

            return false;
        }
        else
        {
            if(segment.fromIndex < segment.endIndex)
            {
                return fromIndex <= segment.fromIndex || endIndex >= segment.endIndex;
            }

            return false;
        }
    }
}

class CutArea
{
    public List<CutRange> _cutLines = new List<CutRange>();
    public List<CutRange> _normalLines = new List<CutRange>();
}

public class TestLineCutPolygon : MonoBehaviour
{
    public LineSegment segment;
    public Polygon polygon;
    List<InterSectionPoint> _listIntersection = new List<InterSectionPoint>();

    private void OnDrawGizmos() 
    {
        if(polygon == null) return;
        

        TestLineLineSegmentIntersection.DrawLineSegment(segment.line, 0, segment.maxValue);

        _listIntersection.Clear();
        var allLineSegment = polygon.allLineSegment;
        for(int i = 0; i < allLineSegment.Count; i++)
        {
            if(GetIntersection(segment, allLineSegment[i], out var intersectionPercent))
            {
                _listIntersection.Add(new InterSectionPoint(){
                    segmentIndex = i, percentCut = intersectionPercent.x, percentSelfSegment = intersectionPercent.y
                });
            }
        }

        _listIntersection.Sort((m, n)=>m.percentCut.CompareTo(n.percentCut));

        for(int i = 0; i < _listIntersection.Count; i++)
        {
            var currentIntersection = _listIntersection[i];
            var nextIntersection = _listIntersection[ (i + 1) % _listIntersection.Count];
        }
    }

    public static bool GetIntersection(LineSegment line1, LineSegment line2, out float2 intersectionPercent)
    {
        if(!TestLineIntersection.GetIntersectionPercent(line1.line, line2.line, out intersectionPercent))
        {
            return false;
        }

        return line2.maxValue >= intersectionPercent.y && intersectionPercent.y >= 0 
            && line1.maxValue >= intersectionPercent.x && intersectionPercent.x >= 0;
    }
}
