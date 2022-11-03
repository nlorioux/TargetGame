using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour
{

    public Text nameText;
    public Text scoreText;
    public Text rankText;

    public void DisplayScore(string name, int score, int rank)
    {
        nameText.text = name;
        scoreText.text = score.ToString();
        rankText.text = rank.ToString();
    }

    public void HideScore()
    {
        nameText.text = "";
        scoreText.text = "";
        rankText.text = "";
    }
}
