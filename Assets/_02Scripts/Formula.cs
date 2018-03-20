using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formula
{
    private int item1ID;
    private int item1Amount;
    private int item2ID;
    private int item2Amount;
    private int resID;

    public int Item1ID
    {
        get { return item1ID; }
        private set { item1ID = value; }
    }
    public int Item1Amount
    {
        get { return item1Amount; }
        private set { item1Amount = value; }
    }
    public int Item2ID
    {
        get { return item2ID; }
        private set { item2ID = value; }
    }
    public int Item2Amount
    {
        get { return item2Amount; }
        private set { item2Amount = value; }
    }
    public int ResID
    {
        get { return resID; }
        private set { resID = value; }
    }

    private List<int> needIdList = new List<int>();
    public List<int> NeedIdList
    {
        get { return needIdList; }
    }

    public Formula(int item1ID,int item1Amount,int item2ID,int item2Amount,int resID)
    {
        this.Item1ID = item1ID;
        this.Item1Amount = item1Amount;
        this.Item2ID = item2ID;
        this.Item2Amount = item2Amount;
        this.ResID = resID;

        for (int i = 0; i < Item1Amount; i++)
        {
            NeedIdList.Add(Item1ID);
        }
        for (int i = 0; i < Item2Amount; i++)
        {
            NeedIdList.Add(Item2ID);
        }
    }

    public bool Match(List<int> idList)
    {
        List<int> tempIDList = new List<int>(idList);
        foreach (int id in NeedIdList)
        {
            bool isSuccess = tempIDList.Remove(id);
            if (isSuccess == false)
            {
                return false;
            }
        }
        return true;
    }

    public override string ToString()
    {
        return string.Format("Item1ID:{0}  Item1Amount:{1}  Item2ID:{2}  Item2Amount:{3} ResID:{4}", Item1ID, Item1Amount, Item2ID, Item2Amount, ResID);
    }
}
