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
    public StarRatingUI starRatingUI;

    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("CoinItem").Length;
        UpdateCoinText();
    }
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
        UpdateCollectedCoins();
    }

    void UpdateCollectedCoins()
    {
        collectedCoins.text = $"Coins: {coinCount.ToString()} / {totalCoins.ToString()}";
    }
    void UpdateCoinText()
    {
        coinText.text = $"CoinCount : {coinCount.ToString()}";
    }
    public int GetStarRating()
    {
        if (totalCoins == 0) return 3; // Automatically give 3 stars if no coins present

        float percentage = (float)coinCount / totalCoins;
        if (percentage >= 1f) return 3;  // 100% coins collected = 3 stars
        if (percentage >= 0.66f) return 2;  // 66% or more coins collected = 2 stars
        if (percentage >= 0.33f) return 1;  // 33% or more coins collected = 1 star
        return 0;  // Less than 33% coins collected = 0 stars
    }
    public void OnGameComplete()
    {
        int starRating = GetStarRating();  // Calculate the star rating
        starRatingUI.UpdateStarUI(starRating);
    }
}
