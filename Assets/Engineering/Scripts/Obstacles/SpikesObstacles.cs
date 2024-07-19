using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesObstacles : Obstacle
{

    protected const int k_MinObstacleCount = 1;
    protected const int k_MaxObstacleCount = 1;
    protected const int k_LeftMostLaneIndex = -1;
    protected const int k_RightMostLaneIndex = 1;

    public override IEnumerator Spawn(TrackSegment segment, float t, GameObject obj)
    {
        Vector3 offset = new Vector3(0, 0.1f, 0);
        Vector3 position;
        Quaternion rotation;
        segment.GetPointAt(t, out position, out rotation);
        yield return null;
        obj = Instantiate(obj, position += offset, rotation);
        obj.transform.SetParent(segment.objectRoot, true);

        Vector3 oldPos = obj.transform.position;
        obj.transform.position += Vector3.back;
        obj.transform.position = oldPos;
    }
}

