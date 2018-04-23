using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject hp1, hp2, hp3, hp4, gameOver, btn, btn1;
    [SerializeField] Text bullets;
    Character player;

    void Start()
    {
        player = Character.instance;
        gameOver.SetActive(false);
        btn.SetActive(false);
        btn1.SetActive(false);
    }

    void Update ()
    {
        HealthCheck();
        bullets.text = player.ammo.ToString();
    }

    void HealthCheck()
    {
        if (player.health <= 0)
        {
            gameOver.SetActive(true);
            btn.SetActive(true);
            btn1.SetActive(true);
            hp1.SetActive(false);
            hp2.SetActive(false);
            hp3.SetActive(false);
            hp4.SetActive(false);
        }
        else if (player.health == 1)
        {
            hp1.SetActive(true);
            hp2.SetActive(false);
            hp3.SetActive(false);
            hp4.SetActive(false);
        }
        else if (player.health == 2)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(false);
            hp4.SetActive(false);
        }
        else if (player.health == 3)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
            hp4.SetActive(false);
        }
        else if (player.health >= 4)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
            hp4.SetActive(true);
        }
    }

    public void RestartLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
