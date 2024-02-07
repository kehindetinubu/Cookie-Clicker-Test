using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACCUpgrades : MonoBehaviour
{
    public Text accUpgradesText;

    public AutoCookieClicker autoCookieClicker;

    public float costScalingFactor = 1.2f;
    public float valueScalingFactor = 1.5f;

    private int acInitialCost = 10;
    private int acInitialValue = 1;

    private int acUpgradeCost;
    private int acUpgradeValue;
    private int upgradeLevel = 0;
    public void OnClick()
    {
        autoCookieClicker.currentCookies -= acUpgradeCost;

        acUpgradeValue = CalculateUpgradeValue();
        acUpgradeCost = CalculateUpgradeCost();

        autoCookieClicker.cookieClickValue = acUpgradeValue;

        upgradeLevel++;

        accUpgradesText.text = "Upgrade (" + acUpgradeCost + ")";
    }

    // Start is called before the first frame update
    void Start()
    {

        accUpgradesText.text = " UNAVAILABLE ";

    }

    // Update is called once per frame
    void Update()
    {
        if (autoCookieClicker.currentCookies >= acUpgradeCost && autoCookieClicker.autoClickerEnabled && upgradeLevel < 40)
        {
            GetComponent<Button>().interactable = true;
            accUpgradesText.text = "Upgrade (" + acUpgradeCost + ")";

        }
        else
        {
            GetComponent<Button>().interactable = false;
            accUpgradesText.text = "Upgrade (" + acUpgradeCost + ")";

        }
    }
    public int CalculateUpgradeCost()
    {
        return Mathf.RoundToInt(acInitialCost * Mathf.Pow(costScalingFactor, upgradeLevel));
    }

    public int CalculateUpgradeValue()
    {
        return Mathf.RoundToInt(acInitialValue * Mathf.Pow(valueScalingFactor, upgradeLevel));
    }
}
