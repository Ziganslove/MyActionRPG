using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private Vector3 distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        distanceToPlayer = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + distanceToPlayer;
    }
}
