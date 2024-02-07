using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieClicker3 : MonoBehaviour
{

    public Text scoreText;
    public Text totalCookiesEverText;
    public Text totalAutoCookiesEverText;
    public Text cookieClickValueText;
    public Text cookieAutoClickValueText;
    public int cookies;
    public int cookieClickValue = 1;
    public int clickCount = 0;

    public int totalCookiesEver;
    public int totalAutoCookiesEver;

    private AudioManager audioManager;

    public Text achievementText;

    private bool[] achievementsUnlocked = new bool[9];


    public void OnClick()
    {
        Debug.Log("cookie has been clicked");
        cookies += cookieClickValue;
        totalCookiesEver += cookieClickValue;
        clickCount++;
        PlayClickSound();
        CheckAchievements();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Cookies: " + cookies;
        cookieClickValueText.text = " Current Click Value: " + cookieClickValue;
        totalCookiesEverText.text = " Total Cookies Ever: " + totalCookiesEver;
        totalAutoCookiesEverText.text = " Total Auto Cookies Ever: " + totalAutoCookiesEver;
    }

    private void CheckAchievements()
    {
        if (clickCount >= 10 && !achievementsUnlocked[0])
        {
            UnlockAchievement(0, "Achievement 1 Unlocked! : Novice Clicker - Click 10 times"); 
        }

        if (clickCount >= 50 && !achievementsUnlocked[1]) 
        {
            UnlockAchievement(1, "Achievement 2 Unlocked! : Skilled Clicker - Click 50 times");
        }

        if (clickCount >= 100 && !achievementsUnlocked[2]) 
        {
            UnlockAchievement(2, "Achievement 3 Unlocked! : Professional Clicker - Click 100 times");
        }
        
        if (totalCookiesEver >= 50 && !achievementsUnlocked[3])
        {
            UnlockAchievement(3, "Achievement 3 Unlocked! : Novice Baker - Collect 50 cookies in total");
        }
        
        if (totalCookiesEver >= 1000 && !achievementsUnlocked[4])
        {
            UnlockAchievement(4, "Achievement 4 Unlocked! : Skilled Baker - Collect 1000 cookies in total");
        }
        
        if (totalCookiesEver >= 100000 && !achievementsUnlocked[5])
        {
            UnlockAchievement(5, "Achievement 5 Unlocked! : Professional Baker - Collect 100000 cookies in total");
        }

        if (totalAutoCookiesEver >= 50 && !achievementsUnlocked[6])
        {
            UnlockAchievement(6, "Achievement 6 Unlocked! : Novice Entrepreneur - Collect 50 cookies from auto-clicker total");
        }

        if (totalAutoCookiesEver >= 1000 && !achievementsUnlocked[7])
        {
            UnlockAchievement(7, "Achievement 7 Unlocked! : Skilled Entrepreneur - Collect 1000 cookies from auto-clicker total");
        }

        if (totalAutoCookiesEver >= 50000 && !achievementsUnlocked[8])
        {
            UnlockAchievement(8, "Achievement 8 Unlocked! : Professional Entrepreneur - Collect 50000 cookies from auto-clicker total"); 
        }
    }

    private void UnlockAchievement(int achievementIndex, string achievementMessage)
    {
        achievementsUnlocked[achievementIndex] = true;
        achievementText.text = achievementMessage;
        Invoke("ClearAchievementText", 3f);
        PlayAchievementSound();
    }

    private void ClearAchievementText()
    {
        achievementText.text = "";
    }


    public void PlayClickSound()
    {
        audioManager.PlayClickSound();
    }

    public void PlayAchievementSound()
    {
        audioManager.PlayAchievementSound();
    }
}
