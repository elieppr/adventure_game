using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//public enum ItemCategory { Items }
public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemSlot> slots;
    public List<ItemSlot> Slots => slots;
    public event Action OnUpdated;
      
    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    public void AddItem(ItemBase item, int count = 1)
    {
        var currentSlots = slots;
        var itemSlot = currentSlots.FirstOrDefault(slots => slots.Item == item);
        if (itemSlot != null)
        {
            itemSlot.Count += count;
            Debug.Log("count " + itemSlot.Count);
        }
        else
        {
            currentSlots.Add(new ItemSlot()
            {
                Item = item,
                Count = count
            });
        }
        //InventoryUI.UpdateItemList();
        OnUpdated?.Invoke();
    }
    public void RemoveItem(ItemBase item)
    {
        var currentSlots = slots;
        var itemSlot = currentSlots.First(slot => slot.Item == item);
        itemSlot.Count--;
        if(itemSlot.Count == 0)
        {
            currentSlots.Remove(itemSlot);
        }
        OnUpdated?.Invoke();
    }
    public bool HasItem(ItemBase item)
    {
        var currentSlots = slots;
        return currentSlots.Exists(slots => slots.Item == item);
    }
}
[Serializable] 
public class ItemSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemBase Item
    {
        get => item;
        set => item = value;
       
    }
    public int Count
    {
        get => count;
        set => count = value;
    }

}

