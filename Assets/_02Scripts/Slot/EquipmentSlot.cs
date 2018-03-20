using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    [SerializeField]
    private Equipment.EquipmentType equipType;
    [SerializeField]
    private Weapon.WeaponType wpType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (InventoryManager.Instance.M_IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                Item itemTemp = currentItemUI.M_Item;
                Destroy(currentItemUI.gameObject);
                transform.parent.SendMessage("PutOff", itemTemp);
                InventoryManager.Instance.HideToolTip();
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;

        //鼠标上有物品
        //当前装备槽有装备
        //当前装备槽无装备
        //鼠标上无物品
        //当前装备槽有装备
        //取得当前装备槽内的装备，放置到鼠标上
        //当前装备槽无装备
        //不做任何处理

        bool isUpdateProperty = false;

        if (InventoryManager.Instance.M_IsPickedItem == true)//鼠标上有物品
        {
            ItemUI pickedItem = InventoryManager.Instance.M_PickedItem;
            if (transform.childCount > 0)//当前装备槽内有装备
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();

                if (IsRightItem(pickedItem.M_Item))
                {
                    InventoryManager.Instance.M_PickedItem.Exchange(currentItemUI);
                    isUpdateProperty = true;
                }
            }
            else//当前装备槽内无装备
            {
                if (IsRightItem(pickedItem.M_Item))
                {
                    this.StoreItem(InventoryManager.Instance.M_PickedItem.M_Item);
                    InventoryManager.Instance.RemoveItem(1);
                    isUpdateProperty = true;
                }
            }
        }
        else//鼠标上无物品
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                InventoryManager.Instance.PickUpItem(currentItemUI.M_Item, currentItemUI.M_Amount);
                Destroy(currentItemUI.gameObject);
                isUpdateProperty = true;
            }
        }

        if (isUpdateProperty)
        {
            transform.parent.SendMessage("UpdatePropertyText");
        }
    }

    public bool IsRightItem(Item item)
    {
        if((item is Equipment&&((Equipment)(item)).M_EquipmentType==this.equipType)||(item is Weapon && ((Weapon)(item)).M_WeaponType == this.wpType))
        {
            return true;
        }
        return false;
    }
}
