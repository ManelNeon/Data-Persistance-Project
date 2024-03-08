using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //display on main menu where the player's name located
    [SerializeField] TextMeshProUGUI nameDisplay_text;

    //function that changes the name
    public void OnValueChange(string name)
    {
        nameDisplay_text.text = "Player : " + name;
        GameManager.Instance.OnValueChange(name);
    }

    //Starts the game by loading the scene index 1, the main scene, and saves the name that is currently on the prompt
    public void StartGame()
    {
        GameManager.Instance.SaveName();
        SceneManager.LoadScene(1);
    }

    public void LoadText()
    {
        GameManager.Instance.LoadName();
    }
}
