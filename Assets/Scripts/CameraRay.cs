using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRay : MonoBehaviour
{
    public float distance = 3f;
    public GameObject objectToEnable;
    public Text TextUI;
    public Text TextUI_name;

    private Outline getOutline;

    void Update()
    {
        Vector3 startPoint = transform.position;
        Vector3 direction = transform.forward;

        //Debug.DrawRay(startPoint, direction * distance, Color.red);

        if (Physics.Raycast(startPoint, direction, out RaycastHit hit, distance))
        {
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Object"))
            {
                GameManager.hitObject = hit.collider.gameObject;

                
                if (hit.collider.CompareTag("Item"))
                {
                    ItemObject itemObject = hit.collider.GetComponent<ItemObject>();

                    string itemName = itemObject.data.displayName;
                    string itemDesc = itemObject.data.description;

                    TextUI.text = "E¸¦ ´­·¯ È¹µæ";
                    TextUI_name.text = $"{itemName} - {itemDesc}";
                }
                else if (hit.collider.CompareTag("Object"))
                {
                    TextUI.text = "E¸¦ ´­·¯ ¿­±â";
                    TextUI_name.text = " ";
                }

                if (objectToEnable != null && !objectToEnable.activeSelf)
                    objectToEnable.SetActive(true);

                if (getOutline != null && getOutline.gameObject != hit.collider.gameObject)
                {
                    getOutline.enabled = false;
                    getOutline = null;
                }

                Outline outline = hit.collider.GetComponentInChildren<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                    getOutline = outline;
                }

                return;
            }
        }

        GameManager.hitObject = null;

        if (objectToEnable != null && objectToEnable.activeSelf)
            objectToEnable.SetActive(false);

        if (getOutline != null)
        {
            getOutline.enabled = false;
            getOutline = null;
        }
    }
}