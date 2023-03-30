using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject menuPlayer;

    private void Start()
    {
        animator = menuPlayer.GetComponent<Animator>();
    }

    public void StartGame()
    {
        animator.SetBool("JumpIfPlay", true);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
