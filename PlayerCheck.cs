using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab, doorPrefab, doorPrefabExit;
    BossEnemy boss;
    private void Start()
    {
        boss = BossEnemy.instance;
        bossPrefab.gameObject.SetActive(false);
        doorPrefab.gameObject.SetActive(false);
        doorPrefabExit.gameObject.SetActive(true);
    }

    void CheckExit()
    {
        if (boss.health == 0)
        {
            doorPrefabExit.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorPrefab.gameObject.SetActive(true);
            bossPrefab.gameObject.SetActive(true);
        }
    }

}
