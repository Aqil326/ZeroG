using System;
using UnityEngine;


/* This script will help camera to follow player */
public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _transform.position = target.position + offset;
    }
}
