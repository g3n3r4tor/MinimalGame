using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostHolder : MonoBehaviour {

    public static CostHolder instance;
    public Text[] costs = new Text[4];
    public bool first = true;
    public bool maxed = false;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }


    public void updateCost(int[] kosts)
    {
        for (int i = 0; i < costs.Length; i++)
        {
            costs[i].GetComponent<Text>().text = kosts[i].ToString();
        }
        if (maxed)
        {
            costs[0].GetComponent<Text>().text = "Maxed";
        }
    }
}
