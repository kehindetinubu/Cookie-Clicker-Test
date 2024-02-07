using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    public Text upgradeText;
    public CookieClicker3 cookieClicker;

    private int upgradeCost = 10;
    private int upgradeValue = 2;

    public void OnClick()
    {
        cookieClicker.cookies -= upgradeCost;
        upgradeValue *= 2;
        upgradeCost *= 2;
        cookieClicker.scoreText.text = "Score: " + cookieClicker.cookies;

        cookieClicker.cookieClickValue = upgradeValue;
        Debug.Log("StartAutoClicker has been toggled");

        upgradeText.text = "Upgrade (" + upgradeCost + ")";

    }


     // Start is called before the first frame update
    void Start()
    {
        upgradeText.text = "Upgrade (" + upgradeCost + ")";
    }

    // Update is called once per frame
    void Update()
    {
        if (cookieClicker.cookies >= upgradeCost)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
