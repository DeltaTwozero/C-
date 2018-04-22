using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    public static BossEnemy instance;

    [SerializeField] Transform ballSpawn;
    [SerializeField] GameObject[] ball;
    //[SerializeField] GameObject bossPrefab;
    Animator anim;
    public int health;
    bool isAttack, isPlayDeath, isPlayAttack;
    public bool isActive;

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        isAttack = false;
        isPlayDeath = true;
        isActive = true;
        health = 18;
	}

	void Update ()
    {
        CheckDeath();
        CheckAttack();
    }

    void CheckDeath()
    {
        if (health == 0)
        {

        }
    }

    void CheckAttack()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health--;
        }
    }
}
