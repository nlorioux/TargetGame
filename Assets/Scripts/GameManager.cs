using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; //{ get; set; }

    public int score;

    public float timeRemaining;
    private bool timerIsRunning;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public int targetCount;

    public GameObject redTargetObject;
    public GameObject purpleTargetObject;
    public GameObject goldTargetObject;
    private int lifeExpectancy;
    private float timeLastInstantiate;

    private void Start()
    {
        Instance = this;
        score = 0;
        timeRemaining = 60;
        timerIsRunning = true;
        timeLastInstantiate = Time.timeSinceLevelLoad;
        lifeExpectancy = 10;
        Instantiate(redTargetObject, new Vector3(-1.3f, 3f, 0f), redTargetObject.transform.rotation);
        targetCount = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
            updateUI();
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                PlayerPrefs.SetInt("score", score);
                SceneManager.LoadScene("Scenes/Leaderboard");
            }
        }
    }

    public void loadLevelTarget()
    {
        int dice = Random.Range(1, 10) * score;
        int numberNewTargets;
        if (targetCount == 0)
        {
            numberNewTargets = Random.Range(1, 4);
        }
        else if (targetCount < score)
        {
            numberNewTargets = Random.Range(0, Mathf.Min(4 - targetCount, score));
        }
        else
        {
            numberNewTargets = 0;
        }

        for (int i = 0; i < numberNewTargets; i++)
        {
            if (dice < 10)
            {
            targetCount++;
            instantiateTargetOnWall("red", lifeExpectancy);
            }
            else if (dice > 40)
            {
                targetCount++;
                instantiateTargetOnWall("gold", lifeExpectancy);
            }
            else
            {
                targetCount++;
                instantiateTargetOnWall("purple", lifeExpectancy);
            }
        }
    }

    public void addScore(int value)
    {
        score = Mathf.Max(GameManager.Instance.score + value, 0);
        if (score < 3)
        {
            lifeExpectancy = 10;
        }
        else if (score > 12)
        {
            lifeExpectancy = 4;
        }
        else
        {
            lifeExpectancy = 10 - score / 2;
        }
    }

    void updateUI()
    {
        //Score
        scoreText.text = "Score : " + score.ToString();

        //Timer
        float minutes = Mathf.FloorToInt((timeRemaining + 1) / 60);
        float seconds = Mathf.FloorToInt((timeRemaining + 1) % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void instantiateTargetOnWall(string type, int lifeExpectancy)
    {
        bool alreadyTaken;
        float targetY;
        float targetZ;
        if (type == "red")
        {
            GameObject[] Targets = GameObject.FindGameObjectsWithTag("Target");
            do
            {
                targetY = Random.Range(1.4f, 4f);
                targetZ = Random.Range(-5.6f, 5.6f);
                alreadyTaken = false;
                for (int i = 0; i < Targets.Length; i++)
                {
                    if ((Mathf.Pow(Targets[i].transform.position.y - targetY, 2) + Mathf.Pow(Targets[i].transform.position.z - targetZ, 2)) < (Mathf.Pow(Targets[i].GetComponent<Collider>().bounds.size.y, 2) / 2 + Mathf.Pow(redTargetObject.GetComponent<Collider>().bounds.size.y, 2) / 2))
                    {
                        alreadyTaken = true;
                    }
                }
            } while (alreadyTaken);
            Instantiate(redTargetObject, new Vector3(-1.3f, targetY, targetZ), redTargetObject.transform.rotation);
        }
        else if (type == "purple")
        {
            GameObject[] Targets = GameObject.FindGameObjectsWithTag("Target");
            do
            {
                targetY = Random.Range(1.4f, 4f);
                targetZ = Random.Range(-5.6f, 5.6f);
                alreadyTaken = false;
                for (int i = 0; i < Targets.Length; i++)
                {
                    if ((Mathf.Pow(Targets[i].transform.position.y - targetY, 2) + Mathf.Pow(Targets[i].transform.position.z - targetZ, 2)) < (Mathf.Pow(Targets[i].GetComponent<Collider>().bounds.size.y, 2) / 2 + Mathf.Pow(purpleTargetObject.GetComponent<Collider>().bounds.size.y, 2) / 2))
                    {
                        alreadyTaken = true;
                    }
                }
            } while (alreadyTaken);
            Instantiate(purpleTargetObject, new Vector3(-1.3f, targetY, targetZ), purpleTargetObject.transform.rotation);
        }
        else if (type == "gold")
        {
            GameObject[] Targets = GameObject.FindGameObjectsWithTag("goldTarget");
            do
            {
                targetY = Random.Range(1.4f, 4f);
                targetZ = Random.Range(-5.6f, 5.6f);
                alreadyTaken = false;
                for (int i = 0; i < Targets.Length; i++)
                {
                    if ((Mathf.Pow(Targets[i].transform.position.y - targetY, 2) + Mathf.Pow(Targets[i].transform.position.z - targetZ, 2)) < (Mathf.Pow(Targets[i].GetComponent<Collider>().bounds.size.y, 2) / 2 + Mathf.Pow(goldTargetObject.GetComponent<Collider>().bounds.size.y, 2) / 2))
                    {
                        alreadyTaken = true;
                    }
                }
            } while (alreadyTaken);
            Instantiate(goldTargetObject, new Vector3(-0.7f, targetY, targetZ), goldTargetObject.transform.rotation);
        }

    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

}
