using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    public static BossEnemy instance;

    #region Variables
    [SerializeField] Transform ballSpawn;
    [SerializeField] GameObject[] ballPrefab;
    [SerializeField] AudioClip[] clips;

    AudioSource src;
    Animator anim;
    SpriteRenderer rend;
    Collider2D bossCol;

    public int health;
    bool isAttack, isPlayDeath;
    #endregion
    #region HP Bar
    [SerializeField] Sprite[] hpIcon;
    [SerializeField] float speed;
    [SerializeField] Transform startPos, endPos;
    [SerializeField] GameObject HP; 
    #endregion

    void Awake()
    {
        health = 18;
        instance = this;
    }

    void Start ()
    {
        InvokeRepeating("FireBall", 1f, 3f);
        isAttack = false;
        src = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        bossCol = GetComponent<Collider2D>();
        isPlayDeath = true;
    }

	void Update ()
    {
        HealthControll();
        CheckDeath();
    }

    void HealthControll()
    {
        //Moving HP icon above Boss
        if (health <= 18 && health >= 11)
        {
            speed = 3;
            HP.GetComponent<SpriteRenderer>().sprite = hpIcon[0];
        }
        else if (health <= 10 && health >= 6)
        {
            speed = 5;
            HP.GetComponent<SpriteRenderer>().sprite = hpIcon[1];
        }
        else if (health <= 5 && health >= 1)
        {
            speed = 7;
            HP.GetComponent<SpriteRenderer>().sprite = hpIcon[2];
        }

        if(health >= 1)
        {
            float weight = Mathf.Cos(Time.time * speed) * 0.5f + 0.5f;
            HP.transform.position = Vector3.Lerp(startPos.position, endPos.position, 1 - weight);
        }

    }

    void CheckDeath()
    {
        if (health == 0)
        {
            if (isPlayDeath)
            {
                src.PlayOneShot(clips[1]);
                isPlayDeath = false;
            }
            bossCol.enabled = false;
            rend.enabled = false;
            HP.GetComponent<Collider2D>().enabled = true;
            HP.GetComponent<Rigidbody2D>().simulated = true;
            CancelInvoke();
            StartCoroutine("Kill");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health--;
        }
    }

    void FireBall()
    {
        src.PlayOneShot(clips[0]);
        int random = Random.Range(0, 2);
        GameObject bulletInst = Instantiate(ballPrefab[random], ballSpawn.position, ballSpawn.rotation) as GameObject;
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        src.Stop();
        Destroy(gameObject);
    }
}
