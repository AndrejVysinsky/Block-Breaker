using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 1f;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //transition from Game Over scene to Start Menu
            nextSceneIndex = 0;
            GameController.Instance.ResetGame();
        }

        StartCoroutine(LoadScene(nextSceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");

        //pauses coroutine for specified amount of seconds
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadGameOverScene()
    {
        int gameOverSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

        StartCoroutine(LoadScene(gameOverSceneIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
