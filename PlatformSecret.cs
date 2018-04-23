using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSecret : Platform {

    [SerializeField] bool easterEgg, isPlay;
    [SerializeField] AudioClip congrats;
    SpriteRenderer rend;
    AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        easterEgg = false;
        rend.enabled = false;
        isPlay = false;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (easterEgg)
        {
            Debug.Log("worksfunc");
            float weight = Mathf.Cos(Time.time * speed) * 0.5f + 0.5f;
            transform.position = Vector3.Lerp(startPos.position, endPos.position, 1 - weight);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isPlay)
            {
                src.PlayOneShot(congrats);
                isPlay = true;
            }
            easterEgg = true;
            Debug.Log("workscol");
            rend.enabled = true;
            col.transform.parent = this.transform;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = null;
        }
    }
}
