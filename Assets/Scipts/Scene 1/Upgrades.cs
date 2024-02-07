using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    
    public string upgradeName;
    public string upgradeDescription;
    public int upgradeCost;
    public int upgradeEffect;

    public CookieClickerv1 cookieClicker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U) && cookieClicker.score >= upgradeCost)
        {
            cookieClicker.score -= upgradeCost;
            // Apply the upgrade effect
            cookieClicker.score += upgradeEffect;
        }
    }

}
