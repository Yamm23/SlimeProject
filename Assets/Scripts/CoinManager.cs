using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public int totalCoins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI collectedCoins;

    void Start()
    {
        // Count all the coins in the scene using the tag "CoinItem"
        totalCoins = GameObject.FindGameObjectsWithTag("CoinItem").Length;
        UpdateCoinText();
    }
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
    }

    void UpdateCollectedCoins()
    {
        collectedCoins.text = $"Coins: {coinCount.ToString()} / {totalCoins.ToString()}";
    }
    void UpdateCoinText()
    {
        coinText.text = $"CoinCount : {coinCount.ToString()}";
    }
}
