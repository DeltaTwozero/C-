using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Character player;
    Animator anim;
    bool goRight;

    Transform bullet;
    float speed = 10;

    void Start()
    {
        player = Character.instance;

        if (player.isRight)
            goRight = true;
        else goRight = false;

    }

    void Update()
    {
        if (goRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetBool("explode", true);
        Destroy(gameObject, 1);
    }
}
