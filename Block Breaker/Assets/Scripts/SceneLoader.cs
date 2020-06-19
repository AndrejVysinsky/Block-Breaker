using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            GameController.Instance.ResetGame();
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
            
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
