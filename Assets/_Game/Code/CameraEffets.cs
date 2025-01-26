using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffets : MonoBehaviour
{
    public CinemachineFreeLook vcam;
    public float shakeMagnitude;
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTime;

    public void Start()
    {
        noise = vcam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera()
    {
        noise.m_AmplitudeGain = shakeMagnitude;
        shakeTime = Time.time;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera();
        }
        if (Time.time - shakeTime > 0.2f)
        {
            noise.m_AmplitudeGain = 0;
        }
    }


}
