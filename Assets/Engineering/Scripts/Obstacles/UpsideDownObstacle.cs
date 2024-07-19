using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDownObstacle : Obstacle
{
    public override IEnumerator Spawn(TrackSegment segment, float t, GameObject obj)
    {
        Vector3 offset = new Vector3(0, 0, 0);
        Quaternion rotationOffset = Quaternion.Euler(0, -90, 0);
        Vector3 position;
        Quaternion rotation;
        segment.GetPointAt(t, out position, out rotation);
        // **my changes started
        //Debug.LogError("NAME: " + gameObject.name);
        // AsyncOperationHandle op = Addressables.InstantiateAsync(gameObject.name, position, rotation);
        // yield return op;
        // if (op.Result == null || !(op.Result is GameObject))
        // {
        //     Debug.LogWarning(string.Format("Unable to load obstacle {0}.", gameObject.name));
        //     yield break;
        // }
        yield return null;
        obj = Instantiate(obj, position + offset, rotation * rotationOffset);
        obj.transform.SetParent(segment.objectRoot, true);
        //position.z -= 5;
        obj.transform.position = position;
        //TODO : remove that hack related to #issue7
        Vector3 oldPos = obj.transform.position;
        obj.transform.position += Vector3.back;
        obj.transform.position = oldPos;
    }
}
