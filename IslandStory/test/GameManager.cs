using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameEnd = false;

    public void EndGame(){
        if (gameEnd == false)
        {
            gameEnd = true;
            Time.timeScale = 0f;
            GameObject.Find("Canvas/Menu/Death panel").SetActive(true);
        }
    }
    
}
