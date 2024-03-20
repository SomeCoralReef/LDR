using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Chapter1Scene");
    }

    public void ShowChaptersSelection()
    {
        SceneManager.LoadScene("ChaptersScene");
    }

    public void ShowOptions()
    {
        // Code to show options menu
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
