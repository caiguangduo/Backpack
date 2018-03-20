using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Forge : Inventory
{
    private static Forge m_instance;
    public static Forge M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("ForgePanel").GetComponent<Forge>();
            }
            return m_instance;
        }
    }

    private List<Formula> formulaList;

    private void Start()
    {
        ParseFormulaJson();
    }

    void ParseFormulaJson()
    {
        formulaList = new List<Formula>();
        TextAsset formulasText = Resources.Load<TextAsset>("Formulas");
        string formulasJson = formulasText.text;
        JSONObject jo = new JSONObject(formulasJson);
        foreach (JSONObject temp in jo.list)
        {
            int item1ID = (int)(temp["Item1ID"].n);
            int item1Amount = (int)(temp["Item1Amount"].n);
            int item2ID = (int)(temp["Item2ID"].n);
            int item2Amount = (int)(temp["Item2Amount"].n);
            int resID = (int)(temp["ResID"].n);
            Formula formula = new Formula(item1ID, item1Amount, item2ID, item2Amount, resID);
            formulaList.Add(formula);
        }
        foreach (Formula item in formulaList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void ForgeItem()
    {
        List<int> haveMaterialIDList = new List<int>();
        foreach (Slot slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI currentItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                for (int i = 0; i < currentItemUI.M_Amount; i++)
                {
                    haveMaterialIDList.Add(currentItemUI.M_Item.M_ID);
                }
            }
        }

        Formula matchedFormula = null;
        foreach (Formula formula in formulaList)
        {
            bool isMatch = formula.Match(haveMaterialIDList);
            if (isMatch)
            {
                matchedFormula = formula;
                break;
            }
        }

        if (matchedFormula != null)
        {
            Knapsack.M_Instance.StoreItem(matchedFormula.ResID);
            foreach (int id in matchedFormula.NeedIdList)
            {
                foreach (Slot slot in SlotList)
                {
                    if (slot.transform.childCount > 0)
                    {
                        ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                        if (itemUI.M_Item.M_ID == id && itemUI.M_Amount > 0)
                        {
                            itemUI.ReduceAmount();
                            if (itemUI.M_Amount <= 0)
                            {
                                Destroy(itemUI.gameObject);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
