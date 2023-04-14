using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    

    [SerializeField] private GameObject pauseGameMenu;

    private bool PauseGame;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1.0f;
        PauseGame = false;
        // gameObject.GetComponent(MouseLook).enabled = true;
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
        
    }
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
