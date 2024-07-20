using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] Text timerText; // Reference to the UI Text element
    [SerializeField] float startTime = 120f; // 2 minutes

    private float timeRemaining;
    private bool timerIsRunning = false;

    void Start()
    {
        timeRemaining = startTime;
        timerIsRunning = true;
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText(timeRemaining);
            yield return null; // Wait for the next frame
        }

        timeRemaining = 0;
        timerIsRunning = false;
        UpdateTimerText(timeRemaining);
        GameOver();
    }

    void UpdateTimerText(float timeToDisplay)
    {
        timeToDisplay += 1; // Add 1 to display 00:00 instead of -00:01

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        UIController.inst.ShowGameOver();
        Debug.Log("Time is up! Game Over!");
    }
}
