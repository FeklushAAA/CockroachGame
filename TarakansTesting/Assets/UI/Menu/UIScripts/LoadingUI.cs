using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private GameObject loading;

    [SerializeField] private GameObject pressAnyKey;

    private void Start()
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine(LoadingLogic(2.5f));
        }
    }

    private void Update()
    {
        NextLevelButton();
    }

    IEnumerator LoadingLogic(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);

        loading.SetActive(false);

        pressAnyKey.SetActive(true);
    }

    private void NextLevelButton()
    {
        if(pressAnyKey.activeSelf)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
