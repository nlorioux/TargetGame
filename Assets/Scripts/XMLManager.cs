using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour
{
    public static XMLManager Instance;
    public scoresList scoresList;

    void Awake()
    {
        Instance = this;
    }

    public void SaveScores(List<registeredScore> scoresToSave)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Leaderboard/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Leaderboard/");
        }
        else
        {
            scoresList.list = scoresToSave;
            XmlSerializer serializer = new XmlSerializer(typeof(scoresList));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Leaderboard/scoresList.xml", FileMode.Create);
            serializer.Serialize(stream, scoresList);
            stream.Close();
        }
    }

    public List<registeredScore> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/Leaderboard/scoresList.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(scoresList));
            FileStream stream = new FileStream(Application.persistentDataPath + "/Leaderboard/scoresList.xml", FileMode.Open);
            scoresList = serializer.Deserialize(stream) as scoresList;
            stream.Close();
        }
        return scoresList.list;
    }
}

[System.Serializable]
public class scoresList
{
    public List<registeredScore> list = new List<registeredScore>();
}