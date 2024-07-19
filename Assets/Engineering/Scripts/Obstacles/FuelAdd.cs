using UnityEngine;
using System.Collections;
public class FuelAdd : Obstacle
{
    protected const int k_MinObstacleCount = 1;
    protected const int k_MaxObstacleCount = 1;
    protected const int k_LeftMostLaneIndex = -1;
    protected const int k_RightMostLaneIndex = 1;

    public override IEnumerator Spawn(TrackSegment segment, float t, GameObject obj)
    {

//        Debug.Log("Fuel is Added");

        //the tutorial very firts barricade need to be center and alone, so player can swipe safely in bother direction to avoid it
        bool isTutorialFirst = TrackManager.instance.isTutorial && TrackManager.instance.firstObstacle && segment == segment.manager.currentSegment;

        if (isTutorialFirst)
            TrackManager.instance.firstObstacle = false;

        int count = isTutorialFirst ? 1 : Random.Range(k_MinObstacleCount, k_MaxObstacleCount + 1);
        int startLane = isTutorialFirst ? 0 : Random.Range(k_LeftMostLaneIndex, k_RightMostLaneIndex + 1);

        Vector3 offset = new Vector3(0, 0.4f, 0);
        Vector3 position;

       
        Quaternion rotation;
        segment.GetPointAt(t, out position, out rotation);

        for (int i = 0; i < count; ++i)
        {
            int lane = startLane + i;
            lane = lane > k_RightMostLaneIndex ? k_LeftMostLaneIndex : lane;
            yield return null;
            obj = Instantiate(obj, position += offset, rotation);

            if (obj == null)
                Debug.Log(gameObject.name);
            else
            {
                obj.transform.position += obj.transform.right * lane * segment.manager.laneOffset;

                obj.transform.SetParent(segment.objectRoot, true);

                //TODO : remove that hack related to #issue7
                Vector3 oldPos = obj.transform.position;
                obj.transform.position += Vector3.back;
                obj.transform.position = oldPos;
            }
        }
    }
}
