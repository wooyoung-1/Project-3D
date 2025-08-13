using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    public float jumpForce = 200f;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                SEManager.Instance.SoundPlay(1);

                player.JumpPad(jumpForce);

                animator.SetBool("IsJump", true);
                StartCoroutine(ResetJump());

            }
        }
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsJump", false);
    }
}
