using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityManaged : MonoBehaviour
{

    /// the distance from the proximity center (the player) under which the object should be enabled
    [Tooltip("the distance from the proximity center (the player) under which the object should be enabled")]
    public float EnableDistance = 35f;
    /// the distance from the proximity center (the player) after which the object should be disabled
    [Tooltip("the distance from the proximity center (the player) after which the object should be disabled")]
    public float DisableDistance = 45f;

    /// whether or not this object was disabled by the ProximityManager
 
    [Tooltip("whether or not this object was disabled by the ProximityManager")]
    public bool DisabledByManager;

    [Header("Debug")]
    /// a debug manager to add this object to, only used for debug
    [Tooltip("a debug manager to add this object to, only used for debug")]
    public ProximityManager DebugProximityManager;
    /// a debug button to add this object to the debug manager

    public bool AddButton;

    /// <summary>
    /// A debug method used to add this object to a proximity manager
    /// </summary>
    public void Start()
    {
        var manager = FindObjectOfType<ProximityManager>();
        manager.AddControlledObject(this);
    }

    public virtual void DebugAddObject()
    {
        DebugProximityManager.AddControlledObject(this);
    }
}
