using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject itemPrefab;

    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localScale = Vector3.one;
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }

    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().M_Item.M_ID;
    }

    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.M_Amount >= itemUI.M_Item.M_Capacity;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().M_Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            InventoryManager.Instance.HideToolTip();
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        #region
        //自身为空  
        //1、IsPickedItem==true
        //摁下ctrl  放置当前鼠标上的物品的一个
        //没有摁下ctrl  放置当前鼠标上的物品的所有
        //2、IsPickedItem==false  不做任何处理
        //自身不为空
        //1、IsPickedItem==false
        //摁下ctrl  取得当前物品槽中物品的一半
        //没有摁下ctrl  取得当前物品槽中所有的物品
        //2、IsPickedItem==true
        //自身ID!=pickedItem.id  pickedItem跟当前物品交换
        //自身id==pickedItem.id
        //摁下ctrl  放置当前鼠标上的物品的一个
        //没有摁下ctrl  
        //可以完全放下鼠标上的物品
        //只能放下鼠标上物品的一部分
        #endregion

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (InventoryManager.Instance.M_IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (currentItemUI.M_Item is Equipment || currentItemUI.M_Item is Weapon)
                {
                    currentItemUI.ReduceAmount(1);
                    Item currentItem = currentItemUI.M_Item;
                    if (currentItemUI.M_Amount <= 0)
                    {
                        Destroy(currentItemUI.gameObject);
                        InventoryManager.Instance.HideToolTip();
                    }
                    CharacterPanel.M_Instance.PutOn(currentItem);
                }
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;


        if (transform.childCount > 0)//点击的物品槽不为空
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();

            if (InventoryManager.Instance.M_IsPickedItem == false)//点击的物品槽不为空 并且当前鼠标上没有任何物品
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    int amountPicked = (currentItem.M_Amount + 1) / 2;
                    InventoryManager.Instance.PickUpItem(currentItem.M_Item, amountPicked);
                    int amountRemained = currentItem.M_Amount - amountPicked;
                    if (amountRemained <= 0)
                    {
                        Destroy(currentItem.gameObject);
                    }
                    else
                    {
                        currentItem.SetAmount(amountRemained);
                    }
                }
                else
                {
                    InventoryManager.Instance.PickUpItem(currentItem.M_Item, currentItem.M_Amount);
                    Destroy(currentItem.gameObject);
                }
            }
            else//点击的物品槽不为空 并且当前鼠标上有物品
            {
                if (currentItem.M_Item.M_ID == InventoryManager.Instance.M_PickedItem.M_Item.M_ID)//点击的物品槽不为空  当前鼠标上有物品  并且当前鼠标上的物品和点击的物品ID相同
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if (currentItem.M_Item.M_Capacity > currentItem.M_Amount)//如果当前物品槽还有容量
                        {
                            currentItem.AddAmount();
                            InventoryManager.Instance.RemoveItem();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (currentItem.M_Item.M_Capacity > currentItem.M_Amount)
                        {
                            int amountRemain = currentItem.M_Item.M_Capacity - currentItem.M_Amount;
                            if (amountRemain >= InventoryManager.Instance.M_PickedItem.M_Amount)
                            {
                                currentItem.SetAmount(currentItem.M_Amount + InventoryManager.Instance.M_PickedItem.M_Amount);
                                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.M_PickedItem.M_Amount);
                            }
                            else
                            {
                                currentItem.SetAmount(currentItem.M_Amount + amountRemain);
                                InventoryManager.Instance.RemoveItem(amountRemain);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else//点击的物品槽不为空  当前鼠标上有物品  并且当前鼠标上的物品和点击的物品ID不同
                {
                    Item item = currentItem.M_Item;
                    int amount = currentItem.M_Amount;
                    currentItem.SetItem(InventoryManager.Instance.M_PickedItem.M_Item, InventoryManager.Instance.M_PickedItem.M_Amount);
                    InventoryManager.Instance.M_PickedItem.SetItem(item, amount);
                }
            }
        }
        else//当前点击的物品槽为空
        {
            if (InventoryManager.Instance.M_IsPickedItem == true)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    this.StoreItem(InventoryManager.Instance.M_PickedItem.M_Item);
                    InventoryManager.Instance.RemoveItem();
                }
                else
                {
                    for (int i = 0; i < InventoryManager.Instance.M_PickedItem.M_Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.M_PickedItem.M_Item);
                    }
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.M_PickedItem.M_Amount);
                }
            }
            else
            {
                return;
            }
        }
    }
}
