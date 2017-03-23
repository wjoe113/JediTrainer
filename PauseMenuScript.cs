/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {

    [Tooltip("The pause menu")]
    public Canvas pauseMenu;
    public bool isPaused;
    public bool gameOver;
    Scene currentScene;
    public Text failedText;
    public Text pausedText;
    public Button resumeText;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        isPaused = false;
        gameOver = false;
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        pauseMenu.gameObject.SetActive(false);
        pauseMenu.enabled = false;
        pausedText = pausedText.GetComponent<Text>();
        failedText = failedText.GetComponent<Text>();
        resumeText = resumeText.GetComponent<Button>();
    }

    void Update()
    {
        //Escape to pause
        if (Input.GetKey(KeyCode.Escape))
        {
            isPaused = true;
            pauseGame(true);
        }

        //Game over menu if you lose all your health
        if (gameObject.GetComponent<ForceManager>().curHp <= 0.0f)
        {
            gameOver = true;
            simFailed(true);
        }
    }

    //Game Over
    public void simFailed(bool state)
    {
        if (state)
        {
            Time.timeScale = 0.0f;
            pauseMenu.enabled = true;
            pauseMenu.gameObject.SetActive(true);
            failedText.enabled = true;
            pausedText.enabled = false;
            resumeText.enabled = false;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    //Pause the game
    public void pauseGame(bool state)
    {
        if (state)
        {
            Time.timeScale = 0.0f;
            pauseMenu.enabled = true;
            pauseMenu.gameObject.SetActive(true);
            failedText.enabled = false;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    //Resume the game
    public void resumeGame()
    {
        isPaused = false;
        pauseMenu.enabled = false;
        pauseMenu.gameObject.SetActive(false);
        pauseGame(false);
    }

    //Restart the game
    public void restartGame()
    {
        SceneManager.LoadScene(currentScene.name);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    //Quit game
    public void exit()
    {
        Application.Quit();
    }

    //Return to main menu
    public void mainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
