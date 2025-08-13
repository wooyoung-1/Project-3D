using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{

    private bool off = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !off)
        {
            off = true;

            GameObject spawnerObj = GameObject.Find("Ground_GameStart");
            PlatformSpawner spawner = spawnerObj.GetComponent<PlatformSpawner>();

            spawner.SpawnNext();
        }
    }
}

