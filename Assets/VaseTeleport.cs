using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VaseTeleport : MonoBehaviour
{
    public Vector3 teleportDestination = new Vector3(647.69f, 0.522f, 492.6f);

    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("VR-game-end");
        player.transform.position = teleportDestination;
    }
}
