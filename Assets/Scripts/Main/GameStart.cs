using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject room;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance._start)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                PlatformSpawner spawner = GetComponentInParent<PlatformSpawner>();
                spawner.enabled = true;

                Animator _room = room.GetComponent<Animator>();
                _room.SetBool("IsSet", true);
                GameManager.Instance.GameStart();
            }
        }
    }

}
