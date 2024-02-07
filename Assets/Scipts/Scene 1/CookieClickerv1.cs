using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookieClickerv1 : MonoBehaviour
{

    public int score;
    //public TextMeshPro scoreText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(IncrementScoreWithDelay());
        }
    }

    private IEnumerator IncrementScoreWithDelay()
    {
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Increment the score
        score++;
        //scoreText.text = "Score: " + score;
        scoreText.text = "Score: " + score;

        
    }
}
