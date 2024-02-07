using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    public Text achievementText; // Reference to the UI text displaying the achievements

    CookieClicker3 cookieClicker;
    AutoClickButton autoClickButton;

    private int totalClicks; // Total number of clicks made by the player
    private int totalCookies; // Total number of cookies earned by the player

    private bool[] achievementsUnlocked; // Array to track the unlocked status of each achievement
    private AudioManager audioManager;


    // Start is called before the first frame update
    private void Start()
    {
        InitializeAchievements();
        UpdateAchievementText();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void InitializeAchievements()
    {
        int totalAchievements = 10; // The total number of achievements

        achievementsUnlocked = new bool[totalAchievements];
        // Initialize all achievements as locked
        for (int i = 0; i < totalAchievements; i++)
        {
            achievementsUnlocked[i] = false;
        }
    }

    public void IncrementClicks()
    {
        totalClicks++;

        // Check if any achievements are unlocked based on the total clicks
        CheckAchievementConditions();
    }

    public void AddCookies(int amount)
    {
        totalCookies += amount;

        // Check if any achievements are unlocked based on the total cookies
        CheckAchievementConditions();
    }

    public void CheckAchievementConditions()
    {
        // Check conditions for each achievement and unlock if fulfilled
        if (cookieClicker.clickCount >= 10 && !achievementsUnlocked[0])
        {
            UnlockAchievement(0); // Novice Clicker - Click 10 times
            Debug.Log(" Novice Clicker - Click 10 times ");
        }
        if (cookieClicker.clickCount >= 50 && !achievementsUnlocked[1])
        {
            UnlockAchievement(1); // Skilled Clicker - Click 50 times
            Debug.Log(" Skilled Clicker - Click 50 times ");

        }
        if (cookieClicker.clickCount >= 100 && !achievementsUnlocked[2])
        {
            UnlockAchievement(2); // Professional Clicker - Click 100 times
            Debug.Log(" Professional Clicker - Click 100 times ");

        }
        if (cookieClicker.totalCookiesEver >= 50 && !achievementsUnlocked[3])
        {
            UnlockAchievement(3); // Novice Baker - Collect 50 cookies in total
            Debug.Log(" Novice Baker - Collect 50 cookies in total ");

        }
        if (cookieClicker.totalCookiesEver >= 1000 && !achievementsUnlocked[4])
        {
            UnlockAchievement(4); // Skilled Baker - Collect 1000 cookies in total
            Debug.Log(" Skilled Baker - Collect 1000 cookies in total ");

        }
        if (cookieClicker.totalCookiesEver >= 100000 && !achievementsUnlocked[5])
        {
            UnlockAchievement(5); // Professional Baker - Collect 100000 cookies in total
            Debug.Log(" Professional Baker - Collect 100000 cookies in total ");

        }
        if (cookieClicker.totalAutoCookiesEver >= 50&& !achievementsUnlocked[6])
        {
            UnlockAchievement(6); // Novice Entrepreneur - Collect 50 cookies from auto-clicker total
            Debug.Log(" Novice Entrepreneur - Collect 50 cookies from auto-clicker total ");

        }
        if (cookieClicker.totalAutoCookiesEver >= 1000 && !achievementsUnlocked[7])
        {
            UnlockAchievement(7); // Skilled Entrepreneur - Collect 1000 cookies from auto-clicker total
            Debug.Log(" Skilled Entrepreneur - Collect 1000 cookies from auto-clicker total ");

        }
        if (cookieClicker.totalAutoCookiesEver >= 50000 && !achievementsUnlocked[8])
        {
            UnlockAchievement(8); // Professional Entrepreneur - Collect 50000 cookies from auto-clicker total
            Debug.Log(" Professional Entrepreneur - Collect 50000 cookies from auto-clicker total ");

        }
        if (cookieClicker.totalAutoCookiesEver >= 1000000 && !achievementsUnlocked[8])
        {
            UnlockAchievement(8); // Professional Entrepreneur - Collect 50000 cookies from auto-clicker total
            Debug.Log(" Master Entrepreneur - Collect 1000000 cookies from auto-clicker total ");

        }
        //if (autoClickButton.autoClickerEnabled == true && !achievementsUnlocked[9])
        //{
        //    UnlockAchievement(9); // Business Degree - Unlock the auto clicker
        //    Debug.Log(" Business Degree - Unlock the auto clicker ");

        //}
    }

    public void UnlockAchievement(int index)
    {
        achievementsUnlocked[index] = true;
        UpdateAchievementText();
        // Trigger any additional actions or effects for unlocking an achievement
        PlayAchievementSound();

    }

    public void UpdateAchievementText()
    {
        // Update the UI text to display the unlocked achievements
        string text = "Achievements:\n";
        for (int i = 0; i < achievementsUnlocked.Length; i++)
        {
            text += achievementsUnlocked[i] ? "Unlocked" : "Locked";
            text += "\n";
        }
        achievementText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckAchievementConditions();
    }

    public void PlayAchievementSound()
    {
        audioManager.PlayAchievementSound();
    }
}
