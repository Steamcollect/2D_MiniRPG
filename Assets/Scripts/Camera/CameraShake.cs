using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float intensityTest;
    public float timeTest;

    CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin camPerlin;

    float startingIntensity;

    float shakeTimerTotal;
    float shakeTimer;

    public static CameraShake instance;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        camPerlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ShakeCam(intensityTest, timeTest);

        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            camPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, 1 - (shakeTimer / shakeTimerTotal));
        }
    }

    public void ShakeCam(float intensity, float time)
    {
        startingIntensity = intensity;
        camPerlin.m_AmplitudeGain = intensity;

        shakeTimerTotal = time;
        shakeTimer = time;
    }
}
