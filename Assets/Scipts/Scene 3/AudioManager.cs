using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickSound; // AudioSource for the clicking sound
    public AudioSource achievementSound; // AudioSource for the acheivement sound

    public AudioClip clickClip; // AudioClip for the click sound
    public AudioClip achievementClip; // AudioClip for the achievement sound

    public void PlayClickSound()
    {
        // Play the click sound
        clickSound.PlayOneShot(clickClip);
    } 
    public void PlayAchievementSound()
    {
        // Play the achievement sound
        achievementSound.PlayOneShot(achievementClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the audio clips for the audio sources
        clickSound.clip = clickClip;
        achievementSound.clip= achievementClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
