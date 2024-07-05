using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInGame : MonoBehaviour
{
    public GameObject _pausePanel, _inventory, _tapEffect;
    public int _level;

   public void Pause()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Play()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pausePanel.activeSelf)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
       
    }
}
