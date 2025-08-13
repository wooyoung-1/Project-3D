using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;

    [Header("UIx")]
    public Image itemIcon;
    public Text itemName;
    public Text itemText;
    public Animator prevButton;
    public Animator nextButton;

    private int itemIndex = 0;

    private bool buttonCooldown;

    void Start()
    {
        UpdateUI();
    }

    public void MoveNextItem()
    {
        if (playerInventory.items.Count == 0) return;

        if (buttonCooldown) return;

        itemIndex++;
        if (itemIndex >= playerInventory.items.Count)
        {
            itemIndex = 0;
        }
        nextButton.SetBool("IsButton", true);
        StartCoroutine(Buttondelay());
        UpdateUI();
    }

    public void MovePreviousItem()
    {
        if (playerInventory.items.Count == 0) return;

        if (buttonCooldown) return;

        itemIndex--;
        if (itemIndex < 0)
        {
            itemIndex = playerInventory.items.Count - 1;
        }
        prevButton.SetBool("IsButton", true);
        StartCoroutine(Buttondelay());
        UpdateUI();
    }

    private IEnumerator Buttondelay()
    {
        buttonCooldown = true;
        yield return new WaitForSeconds(0.4f);
        prevButton.SetBool("IsButton", false);
        nextButton.SetBool("IsButton", false);
        buttonCooldown = false;
    }

    public void UseSelectedItem()
    {
        if (playerInventory.items.Count == 0)
        {
            Debug.Log("템없음.");
            return;
        }

        ItemData itemToUse = playerInventory.items[itemIndex];

        playerInventory.UseItem(itemToUse);
        if (itemIndex >= playerInventory.items.Count)
        {
            itemIndex = playerInventory.items.Count - 1;
        }

        if (itemIndex < 0)
        {
            itemIndex = 0;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (playerInventory.items.Count == 0)
        {
            itemIcon.gameObject.SetActive(false);
            itemName.text = " ";
            itemText.text = "아이템이 없습니다.";
            return;
        }

        itemIcon.gameObject.SetActive(true);
        ItemData currentItem = playerInventory.items[itemIndex];
        itemIcon.sprite = currentItem.icon;
        itemName.text = currentItem.displayName;
        itemText.text = currentItem.description;
    }
}
