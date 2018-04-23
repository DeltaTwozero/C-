using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab, doorPrefab;
    [SerializeField] Transform spawnBoss;
    BossEnemy boss;
    private void Start()
    {
        //boss = BossEnemy.instance;
        doorPrefab.gameObject.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(bossPrefab, spawnBoss.transform.position, Quaternion.identity);
            doorPrefab.gameObject.SetActive(true);
            bossPrefab.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }

}
