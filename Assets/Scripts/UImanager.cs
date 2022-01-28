using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lapTimeText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject scoreboard;
    [SerializeField] private TextMeshProUGUI[] scoreboardNumbers;
    [SerializeField] private bool timeRunning;

    private float lapTime;
    private float highscore;

    private void Start()
    {
        highscore = PlayerPrefs.GetFloat("highscore", float.MaxValue);

        for (int i = 0; i < scoreboardNumbers.Length; i++)
        {
            if (PlayerPrefs.GetFloat("scoreboardNumbers" + i) == 0 || PlayerPrefs.HasKey("scoreboardNumbers" + i) != true)
            {
                PlayerPrefs.SetFloat("scoreboardNumber" + i, float.MaxValue);
            }
            else return;
        }
    }

    private void Update()
    {
        raceTimer(timeRunning);

        if (Input.GetKeyDown(KeyCode.Escape))
            Scoreboard();

        if (Input.GetKeyDown(KeyCode.Backspace))
            ResetAllPlayerPrefs();

        if (Input.GetKeyDown(KeyCode.Space))
            HighscoreCheck();

    }

    public void raceTimer(bool isRunning)
    {
        if (isRunning == true)
            lapTime = lapTime + Time.deltaTime;
        

        lapTimeText.text = "Laptime: " + lapTime.ToString("0.0");
        highscoreText.text = "Highscore: " + highscore.ToString("0.0");
    }

    public void HighscoreCheck()
    {
        if (lapTime < highscore)
        {
            highscore = lapTime;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        for (int i = 0; i < scoreboardNumbers.Length; i++)
        {
            if (lapTime < PlayerPrefs.GetFloat("scoreboardNumber" + i))
            {
                PlayerPrefs.SetFloat("scoreboardNumber" + i, lapTime);
                return;
            }
        }
    }

    public void Scoreboard()
    {
        if (scoreboard.active == false)
            scoreboard.SetActive(true);
        else if (scoreboard.active == true)
            scoreboard.SetActive(false);

        for (int i = 0; i < scoreboardNumbers.Length; i++)
        {
            scoreboardNumbers[i].text =(1 + i) + ": " + PlayerPrefs.GetFloat("scoreboardNumber" + i).ToString("0.0");
        }
    }

    public void OnApplicationQuit()
    {
        for (int i = 0; i < scoreboardNumbers.Length; i++)
        {
            PlayerPrefs.DeleteKey("scoreboardNumber" + i);
        }
    }

    public void ResetAllPlayerPrefs()
    {
       PlayerPrefs.DeleteAll();
    }
}
