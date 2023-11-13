using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseTeleport : MonoBehaviour
{
    public Vector3 teleportDestination = new Vector3(647.69f, 0.522f, 492.6f);

    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = teleportDestination;
    }
}
