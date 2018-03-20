using UnityEngine;
using System.Collections;

public class Item
{
    public enum ItemType
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }
    public enum ItemQuality
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact
    }

    private int m_id;
    private string m_name;
    private ItemType m_itemType;
    private ItemQuality m_itemQuality;
    private string m_description;
    private int m_capacity;
    private int m_buyPrice;
    private int m_sellPrice;
    private string m_sprite;

    public int M_ID
    {
        get { return m_id; }
        set { m_id = value; }
    }
    public string M_Name
    {
        get { return m_name; }
        set { m_name = value; }
    }
    public ItemType M_ItemType
    {
        get { return m_itemType; }
        set { m_itemType = value; }
    }
    public ItemQuality M_ItemQuality
    {
        get { return m_itemQuality; }
        set { m_itemQuality = value; }
    }
    public string M_Description
    {
        get { return m_description; }
        set { m_description = value; }
    }
    public int M_Capacity
    {
        get { return m_capacity; }
        set { m_capacity = value; }
    }
    public int M_BuyPrice
    {
        get { return m_buyPrice; }
        set { m_buyPrice = value; }
    }
    public int M_SellPrice
    {
        get { return m_sellPrice; }
        set { m_sellPrice = value; }
    }
    public string M_Sprite
    {
        get { return m_sprite; }
        set { m_sprite = value; }
    }

    public Item()
    {
        this.M_ID = -1;
    }
    public Item(int id,string name,ItemType type,ItemQuality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite)
    {
        this.M_ID = id;
        this.M_Name = name;
        this.M_ItemType = type;
        this.M_ItemQuality = quality;
        this.M_Description = des;
        this.M_Capacity = capacity;
        this.M_BuyPrice = buyPrice;
        this.M_SellPrice = sellPrice;
        this.M_Sprite = sprite;
    }

    public virtual string GetToolTipText()
    {
        string color = "";
        switch (M_ItemQuality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.Uncommon:
                color = "lime";
                break;
            case ItemQuality.Rare:
                color = "navy";
                break;
            case ItemQuality.Epic:
                color = "magenta";
                break;
            case ItemQuality.Legendary:
                color = "orange";
                break;
            case ItemQuality.Artifact:
                color = "red";
                break;
        }
        string text = string.Format("<color={4}>{0}</color>\n<size=18><color=green>购买价格：{1} 出售价格:{2}</color></size>\n<color=yellow><size=16>{3}</size></color>", M_Name, M_BuyPrice, M_SellPrice, M_Description, color);
        return text;
    }
}
