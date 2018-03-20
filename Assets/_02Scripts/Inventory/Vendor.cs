using UnityEngine;
using System.Collections;

public class Vendor : Inventory
{
    private static Vendor m_instance;
    public static Vendor M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("VendorPanel").GetComponent<Vendor>();
            }
            return m_instance;
        }
    }

    public int[] itemIdArray;
    private Player m_player;
    private Player M_Player
    {
        get
        {
            if (m_player == null)
            {
                m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
            }
            return m_player;
        }
    }

    private void Start()
    {
        InitShop();
    }
    private void InitShop()
    {
        foreach (int itemId in itemIdArray)
        {
            StoreItem(itemId);
        }
    }

    public void BuyItem(Item item)
    {
        bool isSuccess = M_Player.ConsumeCoin(item.M_BuyPrice);
        if (isSuccess)
        {
            Knapsack.M_Instance.StoreItem(item);
        }
    }

    public void SellItem()
    {
        int sellAmount = 1;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sellAmount = 1;
        }
        else
        {
            sellAmount = InventoryManager.Instance.M_PickedItem.M_Amount;
        }
        int coinAmount = InventoryManager.Instance.M_PickedItem.M_Item.M_SellPrice * sellAmount;
        M_Player.EarnCoin(coinAmount);
        InventoryManager.Instance.RemoveItem(sellAmount);
    }
}
