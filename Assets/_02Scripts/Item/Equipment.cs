using UnityEngine;
using System.Collections;
using System.Text;

public class Equipment : Item
{
    public enum EquipmentType
    {
        None,
        Head,
        Neck,
        Chest,
        Ring,
        Leg,
        Bracer,
        Boots,
        Shoulder,
        Belt,
        OffHand
    }

    private int m_strength;
    private int m_intellect;
    private int m_agility;
    private int m_stamina;
    private EquipmentType m_equipmentType;

    public int M_Strength
    {
        get { return m_strength; }
        set { m_strength = value; }
    }
    public int M_Intellect
    {
        get { return m_intellect; }
        set { m_intellect = value; }
    }
    public int M_Agility
    {
        get { return m_agility; }
        set { m_agility = value; }
    }
    public int M_Stamina
    {
        get { return m_stamina; }
        set { m_stamina = value; }
    }
    public EquipmentType M_EquipmentType
    {
        get { return m_equipmentType; }
        set { m_equipmentType = value; }
    }

    public Equipment(int id,string name,ItemType type,ItemQuality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite,int strength,int intellect,int agility,int stamina,EquipmentType equipType)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        this.M_Strength = strength;
        this.M_Intellect = intellect;
        this.M_Agility = agility;
        this.M_Stamina = stamina;
        this.M_EquipmentType = equipType;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(M_ID);
        sb.Append("  ");
        sb.Append(M_ItemType);
        sb.Append("  ");
        sb.Append(M_ItemQuality);
        sb.Append("  ");
        sb.Append(M_Description);
        sb.Append("  ");
        sb.Append(M_Capacity);
        sb.Append("  ");
        sb.Append(M_BuyPrice);
        sb.Append("  ");
        sb.Append(M_SellPrice);
        sb.Append("  ");
        sb.Append(M_Sprite);
        sb.Append("  ");
        sb.Append(M_Strength);
        sb.Append("  ");
        sb.Append(M_Intellect);
        sb.Append("  ");
        sb.Append(M_Agility);
        sb.Append("  ");
        sb.Append(M_Stamina);
        sb.Append("  ");
        sb.Append(M_EquipmentType);

        return sb.ToString();
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string equipTypeText = "";
        switch (M_EquipmentType)
        {
            case EquipmentType.None:
                equipTypeText = "";
                break;
            case EquipmentType.Head:
                equipTypeText = "头部";
                break;
            case EquipmentType.Neck:
                equipTypeText = "脖子";
                break;
            case EquipmentType.Chest:
                equipTypeText = "胸部";
                break;
            case EquipmentType.Ring:
                equipTypeText = "戒指";
                break;
            case EquipmentType.Leg:
                equipTypeText = "腿部";
                break;
            case EquipmentType.Bracer:
                equipTypeText = "护腕";
                break;
            case EquipmentType.Boots:
                equipTypeText = "靴子";
                break;
            case EquipmentType.Shoulder:
                equipTypeText = "护肩";
                break;
            case EquipmentType.Belt:
                equipTypeText = "腰带";
                break;
            case EquipmentType.OffHand:
                equipTypeText = "副手";
                break;
        }
        string newText = string.Format("{0}\n\n<color=blue>装备类型:{1}\n力量:{2}\n智力:{3}\n敏捷:{4}\n体力:{5}</color>", text, equipTypeText, M_Strength, M_Intellect, M_Agility, M_Stamina);
        return newText;
    }
}
