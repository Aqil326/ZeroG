using System.Collections;
using UnityEngine;

public class HangSawObstacle : Obstacle
{
    [SerializeField]
    private GameObject particles;


    protected const int k_MinObstacleCount = 1;
    protected const int k_MaxObstacleCount = 1;
    protected const int k_LeftMostLaneIndex = -1;
    protected const int k_RightMostLaneIndex = 1;

    public override IEnumerator Spawn(TrackSegment segment, float t, GameObject obj)
    {
        Vector3 offset = new Vector3(-0.7f, 3.5f, 0);
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

    private void Update()
    {
        Trails();
    }
    void Trails()
    {
        if (CharacterInputController.Instance != null && CharacterInputController.Instance.character != null)
        {
            float distance = Vector3.Distance(transform.position, CharacterInputController.Instance.character.transform.position);
            Debug.Log(distance + " Distance");

            if (distance < 9 && distance > 0)
            {
                particles.SetActive(true);
                Debug.Log("Playing.......................");
            }
            else
            {
                particles.SetActive(false);
            }
        }
        else
        {
            // Handle the case where CharacterInputController or its character is null
            // For example, you could disable the particle system in this case
            particles.SetActive(false);
        }
    }

}
