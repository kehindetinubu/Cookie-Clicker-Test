using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCookieClicker : MonoBehaviour
{
    public Text startAutoClickerText;
    public Text currentCookiesText;
    public Text cookieClickValueText;
    public Text achievementText;
    public Text eventNameText;
    public Text eventDescriptionText;
    public Text eventCountdownText;

    public int currentCookies;
    public int cookiesInterval = 1;
    public int cookieClickValue = 1;
    public int autoclickCount = 0;
    public int totalCookiesEver;
    public int eventCountdown;

    public bool autoClickerEnabled = false;

    private bool[] achievementsUnlocked = new bool[6];

    private AudioController audioController;

    // List of special events
    public List<SpecialEvent> specialEvents;

    // Minimum and maximum delay times between events
    public float minDelayTime = 15f;
    public float maxDelayTime = 20f;

    public void OnClick()
    {
        StartAutoClicker();
        StartRandomEventWithRandomDelay();
    }

    // Start is called before the first frame update
    void Start()
    {
        startAutoClickerText.text = " START BAKING ";
        cookieClickValueText.text = "";
        //achievementText.text = "";
        ClearAchievementText();
        ClearEventText();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        // Add the events to the specialEvents list
        specialEvents = new List<SpecialEvent>()
    {
        new SpecialEvent()
        {
            eventName = " Grandma's Home !!! ",
            description = " She's here with energy and you know she got that blood in her. Double cookies in the same time. She's old... do better ",
            duration = 15f,
            rewardCookies = 0,
            rewardMultiplier = 2
        },
        new SpecialEvent()
        {
            eventName = " Mom's Home !!! ",
            description = " Mom came back from grocery shopping ... looks like she bought some cookies ",
            duration = 5f,
            rewardCookies = 100,
            rewardMultiplier = 1
        }
    };
    }

    // Update is called once per frame
    void Update()
    {
        if ( autoClickerEnabled == false )
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }

        currentCookiesText.text = "Cookies: " + currentCookies;
        //cookieClickValueText.text = cookieClickValue + " cookies(s) per sec";
        //CheckAchievements();
    }

    private IEnumerator AutomaticCookieClicker()
    {
        while (autoClickerEnabled)
        {
            currentCookies += cookieClickValue;
            totalCookiesEver += cookieClickValue;
            cookieClickValueText.text = cookieClickValue + " cookie(s) per sec";
            CheckAchievements();
            yield return new WaitForSeconds(cookiesInterval);
        }
    }
    public void StartAutoClicker()
    {
        autoClickerEnabled = true;

        if (autoClickerEnabled)
        {
            startAutoClickerText.text = " BAKING . . . ";
            StartCoroutine(AutomaticCookieClicker());
        }
    }

    private void CheckAchievements()
    {
        if (totalCookiesEver >= 50 && !achievementsUnlocked[0])
        {
            UnlockAchievement(0, "Achievement 1 Unlocked! : Novice Entrepreneur - Collect 50 cookies in total");
        }

        if (totalCookiesEver >= 1000 && !achievementsUnlocked[1])
        {
            UnlockAchievement(1, "Achievement 2 Unlocked! : Skilled Entrepreneur - Collect 1000 cookies in total");
        }

        if (totalCookiesEver >= 50000 && !achievementsUnlocked[2])
        {
            UnlockAchievement(2, "Achievement 3 Unlocked! : Professional Entrepreneur - Collect 50000 cookies in total");
        }

        if (totalCookiesEver >= 500000 && !achievementsUnlocked[2])
        {
            UnlockAchievement(3, "Achievement 4 Unlocked! : Master Entrepreneur - Collect 500000 cookies in total");
        }

        if (totalCookiesEver >= 1000000 && !achievementsUnlocked[2])
        {
            UnlockAchievement(4, "Achievement 5 Unlocked! : Greatest Entrepreneur - Collect 1000000 cookies in total");
        }

        if (totalCookiesEver >= 1500000 && !achievementsUnlocked[2])
        {
            UnlockAchievement(5, "Achievement 6 Unlocked! : Unstoppable Entrepreneur - Collect 1500000 cookies in total");
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
    private void ClearEventText()
    {
        eventNameText.text = "";
        eventDescriptionText.text = "";
        eventCountdownText.text = "";
    }

    public void PlayAchievementSound()
    {
        audioController.PlayAchievementSound();
    }

    public void PlayEventSound()
    {
        audioController.PlayEventSound();
    }

    private void StartRandomEventWithRandomDelay()
    {
        if (specialEvents.Count == 0)
            return;

        // Generate a random delay between the specified minimum and maximum times
        float delay = Random.Range(minDelayTime, maxDelayTime);

        // Start a coroutine to wait for the delay and then trigger the event
        StartCoroutine(StartEventWithDelay(delay));
    }

    private void StartRandomEvent()
    {
        if (specialEvents.Count == 0)
            return;

        // Choose a random event from the list
        int randomIndex = Random.Range(0, specialEvents.Count);
        SpecialEvent randomEvent = specialEvents[randomIndex];

        // Apply the event's effects
        ApplyEventEffects(randomEvent);

        PlayEventSound();

        eventNameText.text = randomEvent.eventName;
        eventDescriptionText.text = randomEvent.description;

        // Start a coroutine to handle the event duration
        StartCoroutine(HandleEventDuration(randomEvent));
    }

    // Apply the effects of the special event
    private void ApplyEventEffects(SpecialEvent specialEvent)
    {
        cookieClickValue *= specialEvent.rewardMultiplier;
        currentCookies += specialEvent.rewardCookies;
    }

    // Coroutine to start an event after a delay
    private IEnumerator StartEventWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Start a random event
        StartRandomEvent();

        // Start the next event with a random delay - - -- loops it..
        //StartRandomEventWithRandomDelay();
    }

    // Coroutine to handle the event duration and revert its effects
    private IEnumerator HandleEventDuration(SpecialEvent specialEvent)
    {
        float startTime = Time.time;
        float endTime = startTime + specialEvent.duration;

        while (Time.time < endTime)
        {
            float remainingTime = endTime - Time.time;
            int seconds = Mathf.CeilToInt(remainingTime);

            eventCountdownText.text = "Event Ends In: " + seconds + "s";

            yield return null; // Wait for the next frame
        }
        ClearEventText();

        cookieClickValue /= specialEvent.rewardMultiplier;
        StartRandomEventWithRandomDelay();
    }


    //private IEnumerator HandleEventDuration(SpecialEvent specialEvent)
    //{
    //    yield return new WaitForSeconds(specialEvent.duration);

    //    cookieClickValue /= specialEvent.rewardMultiplier;
    //    ClearEventText();
    //}
}

    


// Class to hold information about special events
[System.Serializable]
public class SpecialEvent
{
    public string eventName;
    public string description;
    public float duration;
    public int rewardCookies;
    public int rewardMultiplier;
    // Add any additional properties specific to your special events
}
