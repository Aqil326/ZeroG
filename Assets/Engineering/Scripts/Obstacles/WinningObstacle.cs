using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnigObstacles : Obstacle
{
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem glow;
    [SerializeField] private List<GameObject> winningObs;

   public CharacterCollider character;

    private void Start()
    {
        int i =  TrackManager.instance.m_CurrentLevel - 1;
        winningObs[i].SetActive(true);
    }

    public override IEnumerator Spawn(TrackSegment segment, float t, GameObject obj)
    {
        // Copied from All Lane obstacle
        Vector3 position = segment.pathParent.GetChild(1).position; // spawning at the exit point of this segment
        Quaternion rotation = Quaternion.identity;
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
        obj = Instantiate(obj, position, rotation);
        obj.transform.SetParent(segment.objectRoot, true);

        //TODO : remove that hack related to #issue7
        Vector3 oldPos = obj.transform.position;
        obj.transform.position += Vector3.back;
        obj.transform.position = oldPos;

    }

    private void OnTriggerEnter(Collider other)
    {
        //husnain add this code for player fly in sky after level win
        CharacterInputController.Instance.m_TargetPosition.y = 500;
        CharacterCollider.Instance.thrustParticle.Play();
        
        //end

        //int i = TrackManager.instance.m_CurrentLevel - 1;

        //if (i <= 4)
        //{
        //    glow.Play();
        //}
        //else
        //{
        //    fire.Play();
        //}
    }
}


