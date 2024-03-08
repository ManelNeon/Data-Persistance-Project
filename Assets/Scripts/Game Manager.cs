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
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            data.currentPlayerName = currentPlayerName;

            json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        else
        {
            SaveData data = new SaveData();

            data.currentPlayerName = currentPlayerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
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

            GameObject.Find("Canvas").GetComponent<UIManager>().OnValueChange(currentPlayerName);
        }
    }

    //Loads the best score and the best player's name from a existing json
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
          
            bestScore_text.SetActive(true);
            bestScore_text.GetComponent<Text>().text = "Best Score: " + bestScore + " Name: " + bestPlayerName;
        }
    }

    //saves the new score data if it is higher than the previous one
    public void NewHighScore(int score)
    {
        //gets the path for the file and the file
        string path = Application.persistentDataPath + "/savefile.json";

        //checks if it exists
        if (File.Exists(path))
        {
            //reads the text in json
            string json = File.ReadAllText(path);

            //transfers the json data to a the class save data
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //checks if the score is bigget than the previous biggest score
            if (score > data.bestScore)
            {
                //if it is changes the variables
                data.bestPlayerName = currentPlayerName;

                data.bestScore = score;

                //picks up the same json and gets the new data
                json = JsonUtility.ToJson(data);

                //writes the new data
                File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            }
        }
        
    }
}
