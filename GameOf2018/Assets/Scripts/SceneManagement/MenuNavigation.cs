using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MenuNavigation : MonoBehaviour {

    public string playGame;
    public string mainMenu;
    public string credits;
    public string intro;
    public string titleScreen;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(playGame);
    }
    public void PlayIntro()
    {
        SceneManager.LoadScene(intro);
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene(credits);
    }
    public void ReturnToTitle()
    {

        SceneManager.LoadScene("STARTUP");
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}