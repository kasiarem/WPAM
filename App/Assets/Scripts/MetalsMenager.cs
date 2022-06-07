using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MetalsMenager : MonoBehaviour
{
    public TextMeshProUGUI metalsText;
    public TextMeshProUGUI metalsText1;
    // Start is called before the first frame update

    public void AddMetals(int amount)
    {
        GameData.Metals += amount;
        metalsText.text = GameData.Metals.ToString();
        metalsText1.text = GameData.Metals.ToString();
        SaveAmountMetals(GameData.Metals);
    }
    public void DecrementMetals(int amount)
    {
        GameData.Metals -= amount;
        metalsText.text = GameData.Metals.ToString();
        metalsText1.text = GameData.Metals.ToString();
        SaveAmountMetals(GameData.Metals);
    }
    public bool EnoughMetals(int amount)
    {
        if (GameData.Metals - amount >= 0)
            return true;
        else
            return false;
    }
    public void SaveAmountMetals(int newAmount)
    {
        PlayerPrefs.SetInt("Metals", newAmount);
    }
}
