using UnityEngine;
using System.Collections;
using System.Text;

public class Inventory : MonoBehaviour
{
    private Slot[] slotList;
    private bool isInit = false;
    protected Slot[] SlotList
    {
        get
        {
            if (!isInit)
            {
                slotList = GetComponentsInChildren<Slot>();
                isInit = true;
            }
            return slotList;
        }
    }

    private float targetAlpha = 1;
    private float smoothing = 4;
    private CanvasGroup m_canvasGroup;
    private CanvasGroup M_CanvasGroup
    {
        get
        {
            if (m_canvasGroup == null)
            {
                m_canvasGroup = GetComponent<CanvasGroup>();
            }
            return m_canvasGroup;
        }
    }

    //protected virtual void Start()
    //{
    //    slotList = GetComponentsInChildren<Slot>();
    //}

    private void Update()
    {
        if (M_CanvasGroup.alpha != targetAlpha)
        {
            M_CanvasGroup.alpha = Mathf.Lerp(M_CanvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(M_CanvasGroup.alpha - targetAlpha) < 0.02f)
            {
                M_CanvasGroup.alpha = targetAlpha;
            }
        }
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物品不存在");
            return false;
        }
        if (item.M_Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }

    private Slot FindEmptySlot()
    {
        foreach (Slot slot in SlotList)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
    }
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in SlotList)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.M_ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    public void Show()
    {
        M_CanvasGroup.blocksRaycasts = true;
        targetAlpha = 1;
    }
    public void Hide()
    {
        M_CanvasGroup.blocksRaycasts = false;
        targetAlpha = 0;
    }
    public void DisplaySwitch()
    {
        if (targetAlpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.M_Item.M_ID + "," + itemUI.M_Amount + "-");
            }
            else
            {
                sb.Append("0-");
            }
        }
        PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
    }
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name) == false) return;

        string str = PlayerPrefs.GetString(this.gameObject.name);
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length-1; i++)
        {
            string itemStr = itemArray[i];
            if (itemStr != "0")
            {
                string[] temp = itemStr.Split(',');
                int id = int.Parse(temp[0]);
                Item item = InventoryManager.Instance.GetItemById(id);
                int amount = int.Parse(temp[1]);
                for (int j = 0; j < amount; j++)
                {
                    SlotList[i].StoreItem(item);
                }
            }
        }
    }
}
