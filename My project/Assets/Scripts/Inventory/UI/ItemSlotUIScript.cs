using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI countText;

    public TextMeshProUGUI NameText => nameText;
    public TextMeshProUGUI CountText => countText;
    public void SetData(ItemSlot itemSlot)
    {
        nameText.text = itemSlot.Item.Name;
        countText.text = $"X {itemSlot.Count}";
    }
}
