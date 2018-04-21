using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject hp1, hp2, hp3;
    [SerializeField] Text bullets;
    Character player;

    void Start()
    {
        player = Character.instance;
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
            hp1.SetActive(false);
            hp2.SetActive(false);
            hp3.SetActive(false);
        }
        else if (player.health == 1)
        {
            hp1.SetActive(true);
            hp2.SetActive(false);
            hp3.SetActive(false);
        }
        else if (player.health == 2)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(false);
        }
        else if (player.health >= 3)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
        }
    }
}
