using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();

    public InventoryUI inventoryUI;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

    }

    public void AddItem(ItemData item)
    {
        SEManager.Instance.SoundPlay(4);
        items.Add(item);
        inventoryUI.UpdateUI();
    }

    public void UseItem(ItemData item)
    {
        if (item.type == ItemType.Consumable)
        {
            if (item.consumables[0].type == ConsumableType.SpeedUp)
            {
                SEManager.Instance.SoundPlay(2);
                StartCoroutine(ItemSpeedUp(2f, 5f));
            }
            else if (item.consumables[0].type == ConsumableType.SuperJump)
            {
                SEManager.Instance.SoundPlay(3);
                playerController.ItemSuperJump();
            }
            items.Remove(item);
        }
    }

    IEnumerator ItemSpeedUp(float a, float time)
    {
        playerController.ItemSpeedUP();
        yield return new WaitForSeconds(time);
        playerController.EndItemSpeedUP();
    }

}