using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera playerCamera;
    private Transform _player;
   

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        _player = PlayerMain.Instance.GetComponent<Transform>();
        
    }

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}
