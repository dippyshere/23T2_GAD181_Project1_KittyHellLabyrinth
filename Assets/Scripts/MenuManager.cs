using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject titlePanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void QuitGame()
    {
        // Only works in standalone builds, not in the Unity Editor
        Application.Quit();
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        titlePanel.SetActive(false);
    }

    public void HideControls()
    {
        controlsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }
}
