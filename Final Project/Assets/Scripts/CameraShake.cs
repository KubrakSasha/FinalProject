using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera _camera;
    private float _shakeAmount;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Shake(float amount, float length)
    {
        _shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.1f);
        Invoke("StopShake", length);
    }
    private void BeginShake()
    {
        if (_shakeAmount > 0)
        {
            Vector3 camPos = _camera.transform.position;

            float offsetX = Random.value * _shakeAmount * 2 - _shakeAmount;
            float offsetY = Random.value * _shakeAmount * 2 - _shakeAmount;
            camPos.x = offsetX;
            camPos.y = offsetY;
            _camera.transform.position = camPos;
        }
    }
    private void StopShake()
    {
        CancelInvoke("BeginShake");
        _camera.transform.position = new Vector3 (0,0,-10);
    }
}
