using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //creating a static Instance
    [HideInInspector] public static GameManager Instance;

    //recording the name of the player with the best score
    [HideInInspector] public string bestPlayerName;

    //recording the best score
    [HideInInspector] public int bestScore;

    //recording the current player's name
    [HideInInspector] public string currentPlayerName;

    // Start is called before the first frame update
    void Awake()
    {
        //checks if the instance exists or not, if it does, kill the dup one with fire
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        //sets the instance as this object in case there isnt any already, and puts a DontDestroyOnLoad on the object
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Loads the data from the save file if it exists
        LoadData();
    }

    //the function that changes both the current player record as well as the one in the UI
    public void OnValueChange(string placeName)
    {
        currentPlayerName = placeName;
        GameObject.Find("Canvas").GetComponent<UIManager>().ChangeName(currentPlayerName);
    }

    //Class for the recorded variables
    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;

        public int bestScore;

        public string currentPlayerName;
    }

    //function that saves the current players name, so that he can load it later
    public void SaveName()
    {
        SaveData data = new SaveData();

        data.currentPlayerName = currentPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //loads the current players name
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            currentPlayerName = data.currentPlayerName;

            Debug.Log(currentPlayerName);

            OnValueChange(currentPlayerName);
        }
    }

    //Loads the best score and the best player's name from a existing json, in case there's no record, deactivates the best score text
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            GameObject bestScore_text = GameObject.Find("Best Score");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestPlayerName = data.bestPlayerName;
            if (bestScore != 0 && bestPlayerName != "")
            {
                bestScore_text.SetActive(true);
                bestScore_text.GetComponent<Text>().text = "Best Score :" + bestScore + "  Name:" + bestPlayerName;
            }
            else
            {
                bestScore_text.SetActive(false);
            }
        }
    }

    //saves the new score data if it is higher than the previous one
    public void NewHighScore(int score)
    {
        SaveData data = new SaveData();

        if (score > data.bestScore)
        {
            data.bestPlayerName = currentPlayerName;

            data.bestScore = score;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }
}
