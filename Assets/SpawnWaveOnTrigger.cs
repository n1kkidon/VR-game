using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaveOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>(); // Find the WaveSpawner script
        if (waveSpawner != null)
        {
            waveSpawner.TriggerWaveSpawn();
        }

        // Optionally, you can also destroy the arrow or perform other actions here
        Destroy(gameObject);
    }
}
