using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && InventoryManager.Instance.M_IsPickedItem == false)
        {
            if (transform.childCount > 0)
            {
                Item currentItem = transform.GetChild(0).GetComponent<ItemUI>().M_Item;
                transform.parent.parent.SendMessage("BuyItem", currentItem);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left && InventoryManager.Instance.M_IsPickedItem == true)
        {
            transform.parent.parent.SendMessage("SellItem");
        }
    }
}
