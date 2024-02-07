using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource achievementSound; // AudioSource for the achievement sound
    public AudioSource eventSound; // AudioSource for the event sound

    public AudioClip achievementClip; // AudioClip for the achievement sound
    public AudioClip eventClip; // AudioClip for the event sound

    public void PlayAchievementSound()
    {
        // Play the achievement sound
        achievementSound.PlayOneShot(achievementClip);
    }

    public void PlayEventSound()
    {   
        // Play the event sound
        eventSound.PlayOneShot(eventClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the audio clips for the audio sources
        achievementSound.clip = achievementClip;
        eventSound.clip= eventClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
