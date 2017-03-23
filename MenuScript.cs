/* ---------------------------------------------------
 * Jedi Trainer - By Brandon McMillan and Joe Wileman
 * CAP6121 Spring 2017 Homework 1
 * -------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Canvas exitMenu;
    public Canvas optionsMenu;
    public Dropdown screenRez;
    public Slider volume;
    public Button startText;
    public Button optionText;
    public Button exitText;

    void Start ()
    {
        exitMenu = exitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        optionText = optionText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        exitMenu.enabled = false;
        optionsMenu.enabled = false;
    }
	
	public void ExitPress()
    {
        exitMenu.enabled = true;
        optionsMenu.enabled = false;
        startText.enabled = false;
        optionText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        exitMenu.enabled = false;
        optionsMenu.enabled = false;
        startText.enabled = true;
        optionText.enabled = true;
        exitText.enabled = true;
    }

    public void startLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }
    public void OptionsPress()
    {
        exitMenu.enabled = false;
        optionsMenu.enabled = true;
        startText.enabled = false;
        optionText.enabled = false;
        exitText.enabled = false;
    }
    public void Apply()
    {
        if(screenRez.value.Equals(0))
        {
            Screen.SetResolution(1280, 720, true, 60);
        }
        else if(screenRez.value.Equals(1))
        {
            Screen.SetResolution(1600, 900, true, 60);
        }
        else if (screenRez.value.Equals(2))
        {
            Screen.SetResolution(1920, 1080, true, 60);
        }
        AudioListener.volume = volume.value;
    }
}