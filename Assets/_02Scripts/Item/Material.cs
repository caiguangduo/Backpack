using UnityEngine;
using System.Collections;
using System.Text;

public class Material : Item
{
    public Material(int id,string name,ItemType type,ItemQuality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {

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

        return sb.ToString();
    }
}
