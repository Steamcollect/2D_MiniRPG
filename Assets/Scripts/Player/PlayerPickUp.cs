using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{
    public Transform playerCenter;
    public TMP_Text pickUpText;

    public LayerMask itemLayer;
    public float pickUpRange;

    public Vector2 posOffset;

    GameObject currentItemLoaded;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Collider2D[] itemInRange = Physics2D.OverlapCircleAll(playerCenter.position, pickUpRange, itemLayer);

        if (itemInRange.Length > 0)
        {
            if (currentItemLoaded != itemInRange[0].gameObject) setVisual(itemInRange[0].GetComponent<ItemInInventory>());

            currentItemLoaded = itemInRange[0].gameObject;

            pickUpText.transform.position = cam.WorldToScreenPoint(new Vector2(currentItemLoaded.transform.position.x + posOffset.x, currentItemLoaded.transform.position.y + posOffset.y));

            if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager.instance.AddItem(itemInRange[0].GetComponent<ItemInInventory>().item, itemInRange[0].GetComponent<ItemInInventory>().itemCount);
                Destroy(itemInRange[0].gameObject);
            }
        }
        else
        {
            currentItemLoaded = null;
            pickUpText.text = "";
        }
    }

    void setVisual(ItemInInventory currentItem)
    {
        UiTextManager.instance.ResetText();

        if (currentItem.itemCount == 1) UiTextManager.instance.SetText(pickUpText, "Ramasser (" + currentItem.item.itemName + ") [E]");
        else UiTextManager.instance.SetText(pickUpText, "Ramasser (" + currentItem.item.itemName + " x " + currentItem.itemCount + ") [E]");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerCenter.position, pickUpRange);
    }
}
