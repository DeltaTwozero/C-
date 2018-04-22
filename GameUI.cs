using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject hp1, hp2, hp3;
    [SerializeField] Text bullets, bossHP;
    [SerializeField] GameObject bossTXT;
    Character player;
    BossEnemy boss;
    void Start()
    {
        player = Character.instance;
        boss = BossEnemy.instance;
        bossTXT.gameObject.SetActive(false);
    }

    void Update ()
    {
        HealthCheck();
        bullets.text = player.ammo.ToString();
        if (boss.isActive)
        {
            bossTXT.gameObject.SetActive(true);
            bossHP.text = boss.health.ToString();
        }
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
