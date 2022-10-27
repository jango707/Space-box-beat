using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum state { IDLE, PLAYING, PAUSE, END }
    public state gameState;

    public GameObject endScreen;
    public GameObject spaceStartScreen;
    public GameObject CongratsScreen;
    public GameObject[] objects;
    private AudioProcessor processor;
    public Text time;
    public Text finalScore;
    public Text failedScore;
    private float timeleft;
    public float shotspeed;
    public int shotCount;
    public int missedShots;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        processor = FindObjectOfType<AudioProcessor>();

    }

    public bool isPlaying()
    {
        if (gameState == state.PLAYING) return true;
        return false;
    }
    private void Start()
    {
        gameState = state.IDLE;
        timeleft = processor.audioSource.clip.length;
    }

    private void Update()
    {
        float minutes = TimeSpan.FromSeconds(timeleft).Minutes;
        float seconds = TimeSpan.FromSeconds(timeleft % 60).Seconds;
        string M = minutes < 10f ? "0" + minutes.ToString() : minutes.ToString();
        string S = seconds < 10f ? "0" + seconds.ToString() : seconds.ToString();
        time.text = M + ":" + S;
        if (Input.GetKeyDown(KeyCode.Space) && gameState == state.END) restart();
        if (Input.GetKeyDown(KeyCode.Space) && gameState == state.IDLE) startGame();
        if (gameState == state.PLAYING) timeleft -= Time.deltaTime;
        if (timeleft <= 0) endLevel();
    }
    public void endLevel()
    {
        pauseGame();
        finalScore.text = "Score: " + (shotCount - missedShots) * 100 / shotCount + " %";
        CongratsScreen.gameObject.SetActive(true);
        time.gameObject.SetActive(false);
        foreach(GameObject O in objects)
        {
            O.gameObject.SetActive(false);
        }
    }

    public void pauseGame()
    {
        gameState = state.PAUSE;
    }

    public void startGame()
    {
        spaceStartScreen.gameObject.SetActive(false);

        gameState = state.PLAYING;
        processor.StartMusic();
        endScreen.SetActive(false);
    }
    public void GotoMain()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void freeze()
    {
        Time.timeScale = 0;
    }

    private void unfreeze()
    {
        Time.timeScale = 1;
    }

    public void endGame()
    {
        gameState = state.END;
        processor.FadeOut();

        time.gameObject.SetActive(false);
        foreach (GameObject O in objects)
        {
            O.gameObject.SetActive(false);
        }
        float t = processor.audioSource.clip.length - timeleft;
        float minutes = TimeSpan.FromSeconds(t).Minutes;
        float seconds = TimeSpan.FromSeconds(t % 60).Seconds;
        string M = minutes < 10f ? "0" + minutes.ToString() : minutes.ToString();
        string S = seconds < 10f ? "0" + seconds.ToString() : seconds.ToString();
        
        failedScore.text = "You reached " + M + ":" + S;
        endScreen.SetActive(true);
    }
}
