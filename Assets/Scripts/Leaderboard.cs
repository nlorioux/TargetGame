using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public scoreDisplay[] scoreDisplayArray;
    List<registeredScore> scores = new List<registeredScore>();

    // Start is called before the first frame update
    void Start()
    {
        string playerName = PlayerPrefs.GetString("playerName");
        int score = PlayerPrefs.GetInt("score");

        LoadScores();
        AddNewScore(playerName, score);
        SaveScores();
        updateLeaderboard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void updateLeaderboard()
    {
        scores.Sort((registeredScore x, registeredScore y) => y.score.CompareTo(x.score));
        for (int i = 0; i < scoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                scoreDisplayArray[i].DisplayScore(scores[i].name, scores[i].score, i + 1);
            }
            else
            {
                scoreDisplayArray[i].HideScore();
            }
        }
    }

    void AddNewScore(string newName, int newScore)
    {
        scores.Add(new registeredScore { name = newName, score = newScore });
    }

    void SaveScores()
    {
        XMLManager.Instance.SaveScores(scores);
    }

    void LoadScores()
    {
        scores = XMLManager.Instance.LoadScores();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }

}
