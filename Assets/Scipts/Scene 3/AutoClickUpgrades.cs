using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutoClickUpgrades : MonoBehaviour
{
    public Text cookieAutoClickValueUpgradeButtonText;
    public CookieClicker3 cookieClicker;
    public AutoClickButton autoClickButton;

    public float costScalingFactor = 1.2f; 
    public float valueScalingFactor = 1.5f;

    private int acUpgradeCost;
    private int acUpgradeValue;
    private int upgradeLevel = 0;

    public void OnClick()
    {
        cookieClicker.cookies -= acUpgradeCost;

        acUpgradeValue = CalculateUpgradeValue();
        acUpgradeCost = CalculateUpgradeCost();

        autoClickButton.cookieAutoClickValue = acUpgradeValue;

        //Debug.Log("StartAutoClicker has been toggled");
        upgradeLevel++;

        cookieAutoClickValueUpgradeButtonText.text = "Upgrade (" + acUpgradeCost + ")";
    }

    // Start is called before the first frame update
    void Start()
    {

        cookieAutoClickValueUpgradeButtonText.text = " UNAVAILABLE ";

    }

    // Update is called once per frame
    void Update()
    {
        if (cookieClicker.cookies >= acUpgradeCost && autoClickButton.autoClickerEnabled && upgradeLevel < 20)
        {
            GetComponent<Button>().interactable = true;
            cookieAutoClickValueUpgradeButtonText.text = "Upgrade (" + acUpgradeCost + ")";

        }
        else
        {
            GetComponent<Button>().interactable = false;
            cookieAutoClickValueUpgradeButtonText.text = "Upgrade (" + acUpgradeCost + ")";

        }
    }
    public int CalculateUpgradeCost()
    {
        return Mathf.RoundToInt(autoClickButton.acInitialCost * Mathf.Pow(costScalingFactor, upgradeLevel));
    }

    public int CalculateUpgradeValue()
    {
        return Mathf.RoundToInt(autoClickButton.acInitialValue * Mathf.Pow(valueScalingFactor, upgradeLevel));
    }

    //private int aCalculateUpgradeCost()
    //{
    //    // Calculate the number of upgrades purchased based on the cost ratio
    //    float costRatio = autoClickButton.acInitialCost / costScalingFactor;
    //    int upgradeCount = Mathf.FloorToInt(Mathf.Log(costRatio) / Mathf.Log(costScalingFactor));

    //    // Calculate the cost of the next upgrade using the number of upgrades purchased
    //    return Mathf.RoundToInt(autoClickButton.acInitialCost * Mathf.Pow(costScalingFactor, upgradeCount));
    //}

    //private int aCalculateUpgradeValue()
    //{
    //    // Calculate the number of upgrades purchased based on the value ratio
    //    float valueRatio = autoClickButton.acInitialValue / valueScalingFactor;
    //    int upgradeCount = Mathf.FloorToInt(Mathf.Log(valueRatio) / Mathf.Log(valueScalingFactor));

    //    // Calculate the value of the next upgrade using the number of upgrades purchased
    //    return Mathf.RoundToInt(autoClickButton.acInitialValue * Mathf.Pow(valueScalingFactor, upgradeCount));
    //}

}
