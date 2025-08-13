using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{

    [SerializeField] private string wallTag = "Wall"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(wallTag))
        {
            Debug.Log($"º®: {other.name}");
        }
    }

}
