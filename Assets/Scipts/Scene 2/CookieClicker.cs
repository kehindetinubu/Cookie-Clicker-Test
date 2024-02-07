using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CookieClicker : MonoBehaviour
{
    
    public int cookies = 0;
    public int cookiesPerClick = 0; // N/A
    public int cookiesPerSecond = 0; // N/A
    public int clickcount = 0;
    public int cookieMultiplier = 1;
    public int clicksPerMultiplier = 10;
    public int clicksUntilEvent = 50;
    public int eventCookies = 10;
    public int achievementCount = 0; // N/A
    public int achievementThreshold = 100; // N/A

    public float cookieClickValue = 1; // N/A
    public float cookieSpawnOffset = 0.1f; // N/A
    public float eventPopupDuration = 3; // N/A
    public float achievementPopupDuration = 3; // N/A

    public int[] achievementRequirements;
    public bool[] achievementsEarned;
    public int[] eventRequirements; // N/A
    public bool[] eventsEarned; // N/A
    public string[] achievementNames;
    public string[] achievementDescriptions;
    public string[] eventNames; // N/A
    public string[] eventDescriptions; // N/A

    private int clickCount = 0;
    private int eventCountdown;
    private int eventDuration = 15;
    private int eventClicks = 0;
    private bool eventActive = false;
    private int cookieCount = 0; // new additions??? step 35

    private static int numAchievements;
    public int[] achievements = new int[numAchievements]; // deal with this later
    private int totalCookies = 0;
    private string saveFileName = "cookieClickerSave.txt";


    public GameObject cookieParticles; // N/A
    public GameObject eventPopup;
    public GameObject achievementPopup;
    
    public TextMeshProUGUI cookiesText; // N/A
    public TextMeshProUGUI achievementText; // N/A
    public TextMeshProUGUI clickCountText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI eventCountdownText;
    public TextMeshProUGUI eventCookieText;
    public TextMeshProUGUI achievementDescriptionsText;
    public TextMeshProUGUI achievementNamesText;
    public TextMeshProUGUI eventNamesText; // N/A
    public TextMeshProUGUI eventDescriptionsText; // N/A
    public TextMeshProUGUI totalCookiesText;
    
   // public AudioSource cookieSound;
    public AudioClip clickSound;
    public AudioClip achievementSound;
    
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(saveFileName))
        {
            Load();
        }
        else
        {
            cookieCount = 0;
            cookieMultiplier = 1;
            clickCount = 0;
            totalCookies = 0;
            for (int i = 0; i < numAchievements; i++)
            {
                achievements[i] = 0;
            }
            eventCountdown = eventDuration;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    
        achievementRequirements = new int[] { 10, 100, 1000, 10000 };
        achievementNames = new string[] { "Novice Clicker", "Skilled Clicker", "Professional Clicker", "Master Clicker" };
        achievementsEarned = new bool[] { false, false, false, false };
        achievementDescriptions = new string[] { "Click 10 times", "Click 100 times", "Click 1000 times", "Click 10000 times" };
    }

    // Handles Saving
    void Save()
    {
        StreamWriter writer = new StreamWriter(saveFileName);
        writer.WriteLine(cookieCount);
        writer.WriteLine(cookieMultiplier);
        writer.WriteLine(clickCount);
        writer.WriteLine(totalCookies);
        for (int i = 0; i < numAchievements; i++)
        {
            writer.WriteLine(achievements[i]);
        }
        writer.WriteLine(eventCountdown);
        writer.Close();
    }

    // Handles Loading
    void Load()
    {
        StreamReader reader = new StreamReader(saveFileName);
        cookieCount = int.Parse(reader.ReadLine());
        cookieMultiplier = int.Parse(reader.ReadLine());
        clickCount = int.Parse(reader.ReadLine());
        totalCookies = int.Parse(reader.ReadLine());
        for (int i = 0; i < numAchievements; i++)
        {
            achievements[i] = int.Parse(reader.ReadLine());
        }
        eventCountdown = int.Parse(reader.ReadLine());
        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if player has clicked on cookie
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("player has clicked cookie");
            clickCount++;
            audioSource.PlayOneShot(clickSound);
            clickCountText.text = clickCount.ToString();
            cookieCount += (int)(clickCount * cookieMultiplier); // added
            cookiesText.text = "Cookies: " + cookieCount.ToString(); // added
        }

        // checks if player has earned a cookie multiplier
        if (clickCount >= clicksPerMultiplier)
        {
            cookieMultiplier++;
            clickCount = 0;
            multiplierText.text = "Multiplier: x" + cookieMultiplier.ToString();
        }

        // updates countdown event text on screen
        if (eventCountdown > 0)
        {
            eventCountdown -= Mathf.RoundToInt(Time.deltaTime);
            //eventCountdownText.text = eventCountdown.ToString();
            eventCountdownText.text = "Event Countdown: " + Mathf.Ceil(eventCountdown).ToString();
        }

        // checks if player has triggered a special event
        if (eventCountdown <= 0)
        {
            eventActive = true;
            eventCountdown = clicksUntilEvent;
            eventPopup.SetActive(true);
            eventCookieText.text = eventCookies.ToString();
        }

        // how to handle special effects
        if (eventActive)
        {
            cookieCount += eventCookies;
            eventCookieText.text = "+" + eventCookies.ToString();
            eventClicks++;
            cookiesText.text = "Cookies: " + cookieCount.ToString(); // added


            if (eventClicks >= clicksUntilEvent)
            {
                eventActive = false;
                eventClicks = 0;
                eventPopup.SetActive(false);
            }
        }

        // checks if player has earned an achievement
        for (int i = 0; i < numAchievements; i++)
        {
            if (cookieCount >= achievementRequirements[i] && achievements[i] == 0)
            {
                achievements[i] = 1;
                audioSource.PlayOneShot(achievementSound);
                achievementPopup.SetActive(true);
                achievementNamesText.text = achievementNames[i];
                achievementDescriptionsText.text = achievementDescriptions[i];
            }
        }

        if (achievementPopup.activeSelf)
        {
            audioSource.enabled= false;
        }
        else
        {
            audioSource.enabled = true;
        }

        //cookieCount += (int)(clickCount * cookieMultiplier);


        // updates total cookie count ( as well as on screen )
        totalCookies += (int)(cookieCount * cookieMultiplier);
        totalCookiesText.text = "Total Cookies: " + totalCookies.ToString();
    }
}
