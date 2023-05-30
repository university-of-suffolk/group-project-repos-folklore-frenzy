using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenCoinUI : MonoBehaviour
{

    public static int coinsFound;
    public int totalCoins;

    public Image[] coins;

    private void Start()
    {
        coinsFound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < totalCoins; i++)
        {
            if(i < coinsFound)
            {
                coins[i].color = new Color32(255, 255, 255, 255);
            }
            else
            {
                coins[i].color = new Color32(0, 0, 0, 150);
            }
        }
    }
}
