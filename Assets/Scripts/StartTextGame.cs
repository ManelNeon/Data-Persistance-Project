using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTextGame : MonoBehaviour
{
    //when the game starts, the text on the best score part will adapt to what the savefile has
    void Start()
    {
        this.GetComponent<Text>().text = "Best Score: "+ GameManager.Instance.bestScore +" Name: " + GameManager.Instance.bestPlayerName;
    }
}
