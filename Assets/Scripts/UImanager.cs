using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] private Text lapTimeText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private GameObject scoreboard;
    [SerializeField] private Text[] scoreboardNumbers;

    private float lapTime;
    private float highscore;

    private void Awake()
    {
        highscore = PlayerPrefs.GetFloat("highscore", 0);

        if (PlayerPrefs.GetFloat("scoreboardNumbers1") == null)
        {
            for (int i = 0; i < scoreboardNumbers.Length; i++)
            {
                PlayerPrefs.SetFloat("scoreboardNumber" + i, 0);
            }
        }
    }

    private void Update()
    {
        raceTimer(true);
    }

    public void raceTimer(bool isRunning)
    {
        if (isRunning == true)
        {
            lapTime = lapTime + Time.deltaTime;
        }

        lapTimeText.text = "Laptime: " + lapTime.ToString("0.0");
        highscoreText.text = "Highscore: " + highscore.ToString("0.0");
    }

    public void HighscoreCheck()
    {
        if (lapTime < highscore)
        {
            PlayerPrefs.SetFloat("highscore", lapTime);
        }
    }

    public void Scoreboard()
    {
        scoreboard.SetActive(true);

        for (int i = 0; i < scoreboardNumbers.Length -1; i++)
        {
            scoreboardNumbers[i].text = "" + PlayerPrefs.GetFloat("scoreboardNumber" + i);
        }
    }
}
