using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public bool isOver;
    public int maxScore;
    public int starCount;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("Game Over UI")]
    public GameObject youWin;
    public GameObject youLose;

    [Header("InGame UI")]
    public Text playerScoreTxt;
    // Start is called before the first frame update

    private void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
        }
    }

    void Start()
    {
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        isOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreTxt.text = starCount.ToString();
        if(starCount == maxScore)
        {
            GameOver();
        }
    }

    public void starUp()
    {
        starCount++;
    }

    public void GameOver()
    {
        if(starCount == maxScore)
        {
            youWin.SetActive(true);
        }
        else{
            youLose.SetActive(true);
        }
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void BackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }

}
