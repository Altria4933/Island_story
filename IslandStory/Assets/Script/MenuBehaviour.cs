using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject deathMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(true);
    }

    public void PanelEnable()
    {
        GameObject.Find("Canvas/Menu/Option panel").SetActive(true);
    }

    public void UIDisable()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(false);
    }



    public void PanelDisable()
    {
        GameObject.Find("Canvas/Menu/Option panel").SetActive(false);
    }

    public void LeverSelect(int lv)
    {
        SceneManager.LoadScene(lv);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void endGame()
    {  
        deathMenu.SetActive(true);
    }

    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathMenu.SetActive(false);

    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }






}


