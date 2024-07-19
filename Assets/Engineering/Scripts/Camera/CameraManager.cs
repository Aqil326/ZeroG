using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.2f;
    [Header("Post Exposure Data")]
    public Volume globalVolume;
    public float endExposure;
    public float durationToFade;
    
    
    private Vector3 originalLocalPosition;
    private ColorAdjustments colorAdjustments;
    private float initialExposure;
    private Vector3 originalRotation;

    private void Awake()
    {
        globalVolume.profile.TryGet(out colorAdjustments);
        initialExposure = colorAdjustments.postExposure.value;
    }

    private void Start()
    {
        originalRotation = transform.rotation.eulerAngles;
    }


    // Call this method to start the camera shake
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void UpsideDownCamera()
    {
        StartCoroutine(ExposureToggler());
    }

    
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;
        originalLocalPosition = transform.localPosition;
        while (elapsedTime < shakeDuration)
        {
            // Shake the camera locally
            transform.localPosition = originalLocalPosition + Random.insideUnitSphere * shakeIntensity;

            // Increment the timer
            elapsedTime += Time.deltaTime;

            yield return null; // Yielding null allows the coroutine to resume on the next frame
        }

        // Reset the local position when the shake duration is over
        transform.localPosition = originalLocalPosition;
    }
    
    IEnumerator ExposureToggler()
    {
            float elapsedTime = 0f;
            float _initialExposure = colorAdjustments.postExposure.value;
            while (elapsedTime < durationToFade)
            {
                float t = elapsedTime / durationToFade;
                colorAdjustments.postExposure.Interp(_initialExposure, endExposure, t);
    
                elapsedTime += Time.deltaTime;
                yield return null;
            }
    
            colorAdjustments.postExposure.value = endExposure; // Ensure final saturation value
            // Rotating camera
            RotateCameraZ(180);
            Shake();
            elapsedTime = 0f;
            _initialExposure = colorAdjustments.postExposure.value;
            while (elapsedTime < durationToFade)
            {
                float t = elapsedTime / durationToFade;
                colorAdjustments.postExposure.Interp(_initialExposure, initialExposure, t);
    
                elapsedTime += Time.deltaTime;
                yield return null;
            }
    
            colorAdjustments.postExposure.value = initialExposure; // Ensure final saturation value
    }
    
  

    

    
    public void RotateCameraZ(float angle = 0)
    {
        transform.Rotate(Vector3.forward, angle);
    }

}