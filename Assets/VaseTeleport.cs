using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseTeleport : MonoBehaviour
{
    public Vector3 teleportDestination = new Vector3(647.69f, 0.522f, 492.6f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArrowIgnore"))
        {
            // Teleport the player to the specified destination
            other.transform.position = teleportDestination;
        }
    }
}
