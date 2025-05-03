using UnityEngine;
using TMPro; // 用 TextMeshPro
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText; // 改为 TMP_Text
    public float timeRemaining = 40f;
    private bool timerIsRunning = true;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("badend");
            }
        }
    }

    void UpdateDisplay(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        countdownText.text = "Grab a gun! Survive " + seconds + "s!";
    }
}
