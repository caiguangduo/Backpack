using UnityEngine;
using System.Collections;

public class Knapsack : Inventory
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static Knapsack m_instance;
    public static Knapsack M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
            }
            return m_instance;
        }
    }
}
