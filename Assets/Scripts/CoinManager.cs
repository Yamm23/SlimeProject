using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;
    void Update()
    {
        coinText.text = $"CoinCount : {coinCount.ToString()}";
    }
}
