using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutoClickButton : MonoBehaviour
{
  
    public const int acIC = 100;
    public const int acIV = 1;

    public Text cookieAutoClickValueButtonText;
    public CookieClicker3 cookieClicker;
    public int cookiesInterval = 100;
    public int cookieAutoClickValue = 0;

    public bool autoClickerEnabled = false;

    public int acInitialCost = acIC;
    public int acInitialValue = acIV;



    public void OnClick()
    {

        cookieClicker.cookies -= acInitialCost;
        cookieAutoClickValue = acInitialValue;

        Debug.Log("StartAutoClicker has been toggled");
        StartAutoClicker(); // problem ---> multiple threads being made // problem solved
    }

    // Start is called before the first frame update
    void Start()
    {
        cookieAutoClickValueButtonText.text = "Upgrade (" + acInitialCost + ")";
        cookieClicker.cookieAutoClickValueText.text = cookieAutoClickValue + " cookies per sec";

    }

    // Update is called once per frame
    void Update()
    {
        if (cookieClicker.cookies >= acInitialCost && (autoClickerEnabled == false))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    private IEnumerator AutoCookieClicker()
    {
        while (autoClickerEnabled)
        {
            cookieClicker.cookies += cookieAutoClickValue;
            cookieClicker.totalCookiesEver += cookieAutoClickValue;
            cookieClicker.totalAutoCookiesEver+= cookieAutoClickValue;
            cookieClicker.cookieAutoClickValueText.text = cookieAutoClickValue + " cookies per sec";
            yield return new WaitForSeconds(cookiesInterval);
        }
    }
    public void StartAutoClicker()
    {
        autoClickerEnabled = true;

        if (autoClickerEnabled)
        {
            cookieAutoClickValueButtonText.text = " AutoClicker ENABLED ";
            StartCoroutine(AutoCookieClicker());
        }
    }

    public void ToggleAutoClicker()
    {
        autoClickerEnabled = !autoClickerEnabled;

        if (autoClickerEnabled)
        {
            cookieAutoClickValueButtonText.text = " AutoClicker : ON ";
            StartCoroutine(AutoCookieClicker());
        }
        else
        {
            cookieAutoClickValueButtonText.text = " AutoClicker : OFF ";
        }
    }
}
