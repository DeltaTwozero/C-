using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelToLoad;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
