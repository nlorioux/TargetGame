using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartMenu : MonoBehaviour
{
    public InputField InputName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayGame()
    {
        string inputName = InputName.GetComponent<InputField>().text;
        if (inputName == "")
        {
            // color : #CF8C8C
            InputName.GetComponent<InputField>().image.color = new Color(0.8117647f, 0.5490196f, 0.5490196f, 1.0f);
        }
        else
        {
            PlayerPrefs.SetString("playerName", inputName);
            SceneManager.LoadScene("Scenes/GameScene");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
