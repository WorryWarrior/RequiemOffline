using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private float zooming = 0f;
    Vector3 position;

    private void Awake()
    {
        GetComponent<Camera>().orthographicSize = ((Screen.height / 2) / zooming);
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            position.Set(player.position.x, player.position.y, player.position.z - 1);
            transform.position = position;
        }
    }

}
