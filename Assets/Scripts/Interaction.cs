using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();

    }

    public void HandleInteraction()
    {

        if (GameManager.hitObject == null)
        {
            Debug.Log("����");
            return;
        }

        if (GameManager.hitObject.CompareTag("Item"))
        {
            ItemObject itemObject = GameManager.hitObject.GetComponent<ItemObject>();

            playerInventory.AddItem(itemObject.data);
            Debug.Log($"������: {itemObject.data.displayName}");
            //������ ����
            Destroy(GameManager.hitObject);
            GameManager.hitObject = null;
            return;
        }

        else if (GameManager.hitObject.CompareTag("Object"))
        {
            Animator _hitObject;
            _hitObject = GameManager.hitObject.GetComponent<Animator>();
            _hitObject.SetBool("IsOpen", true);
            return;
        }
    }
}
