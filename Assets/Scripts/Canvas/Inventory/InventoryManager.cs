using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public List<ItemsInInventory> inventoryContent = new List<ItemsInInventory>();

    public GameObject slotPrefabs;
    public Transform inventoryContentTransform;

    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < inventoryContent.Count; i++)
        {
            Slot tempSlot = Instantiate(slotPrefabs, inventoryContentTransform).GetComponent<Slot>();
            tempSlot.currentItem = inventoryContent[i].currentItem;
            tempSlot.itemCount = inventoryContent[i].itemCount;
            tempSlot.SetVisual();

            inventoryContent[i].currentSlot = tempSlot;
        }
    }

    public void AddItem(ItemData currentItemToAdd, int count)
    {
        ItemsInInventory itemsInInventory = inventoryContent.Where(elem => elem.currentItem == currentItemToAdd).FirstOrDefault();

        if (itemsInInventory != null && currentItemToAdd.isStackable) itemsInInventory.itemCount += count;
        else
        {
            inventoryContent.Add(new ItemsInInventory {currentItem = currentItemToAdd, itemCount = count });
            Slot tempSlot = Instantiate(slotPrefabs, inventoryContentTransform).GetComponent<Slot>();
            tempSlot.currentItem = currentItemToAdd;
            tempSlot.itemCount = count;
            tempSlot.SetVisual();

            ItemsInInventory currentsItemsInInventory = inventoryContent.Where(elem => elem.currentItem == tempSlot.currentItem).FirstOrDefault();
            currentsItemsInInventory.currentSlot = tempSlot;
        }
    }

    public void RemoveItem(ItemData currentItemToRemove, int count)
    {
        ItemsInInventory itemsInInventory = inventoryContent.Where(elem => elem.currentItem == currentItemToRemove).FirstOrDefault();

        if (itemsInInventory.itemCount >= count + 1)
        {
            itemsInInventory.itemCount -= count;
            itemsInInventory.currentSlot.itemCount = itemsInInventory.itemCount;
            itemsInInventory.currentSlot.SetVisual();
        }
        else
        {
            Destroy(itemsInInventory.currentSlot.gameObject);
            inventoryContent.Remove(itemsInInventory);
        }
    }
}

[System.Serializable]
public class ItemsInInventory
{
    public ItemData currentItem;
    public int itemCount;

    public Slot currentSlot;
}