using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneShakeController : MonoBehaviour
{
    public static SceneShakeController Instance {  get; private set; }
    private CinemachineVirtualCamera _camera;
    private float shakeTimer;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (Instance != null) { Destroy(this.gameObject); }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            _camera = GetComponent<CinemachineVirtualCamera>();

        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
}
