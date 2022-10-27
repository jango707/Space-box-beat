using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Architect : MonoBehaviour
{

    public GameObject mainScreen;
    public GameObject instructionScreen;
    public GameObject levelScreen;

    private void Start()
    {
        ShowMain();
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void ShowLevels()
    {
        levelScreen.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
    }
    public void ShowMain()
    {
        instructionScreen.gameObject.SetActive(false);
        levelScreen.gameObject.SetActive(false);
        mainScreen.gameObject.SetActive(true);
    }
    public void ShowInstructions()
    {
        instructionScreen.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
