using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityManager : MonoBehaviour
{

    public bool AutomaticallySetPlayerAsTarget = true;
    public Transform ProximityTarget;
    public bool AutomaticallyGrabControlledObjects = true;
    public List<ProximityManaged> ControlledObjects;
    public float EvaluationFrequency = 0.5f;

    protected float _lastEvaluationAt = 0f;
    // Start is called before the first frame update
    public virtual IEnumerator Start()
    {

        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Player") != null);
        SetPlayerAsTarget();
       // yield return new WaitUntil(() => FindObjectsOfType<ProximityManaged>() != null);
        //GrabControlledObjects();
    }

    protected virtual void GrabControlledObjects()
    {
        if (AutomaticallyGrabControlledObjects)
        {
            ProximityManaged[] items = FindObjectsOfType<ProximityManaged>();
            foreach (ProximityManaged managed in items)
            {
                ControlledObjects.Add(managed);
            }
        }
    }

    public virtual void AddControlledObject(ProximityManaged newObject)
    {
        ControlledObjects.Add(newObject);
    }


    /// <summary>
    /// Grabs the player from the level manager
    /// </summary>
    protected virtual void SetPlayerAsTarget()
    {
        if (AutomaticallySetPlayerAsTarget)
        {
            ProximityTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    /// <summary>
    /// On Update we check our distances
    /// </summary>
    protected virtual void Update()
    {
        EvaluateDistance();
    }

    /// <summary>
    /// Checks distances if needed
    /// </summary>
    protected virtual void EvaluateDistance()
    {

        if (Time.time - _lastEvaluationAt > EvaluationFrequency)
        {
            _lastEvaluationAt = Time.time;
        }
        else
        {
            return;
        }
        foreach (ProximityManaged proxy in ControlledObjects)
        {
            float distance = Vector3.Distance(proxy.transform.position, ProximityTarget.position);
            if (proxy.gameObject.activeInHierarchy && (distance > proxy.DisableDistance))
            {
                proxy.gameObject.SetActive(false);
                proxy.DisabledByManager = true;
            }
            if (!proxy.gameObject.activeInHierarchy && proxy.DisabledByManager && (distance < proxy.EnableDistance))
            {
                proxy.gameObject.SetActive(true);
                proxy.DisabledByManager = false;
            }
        }
    }

   
}
