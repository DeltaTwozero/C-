using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    [SerializeField] Sprite[] spikes;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip[] clips;
    [SerializeField] int life;
    [SerializeField] Transform spikePos;
    bool killMe, playClip;
    SpriteRenderer rend;
    Collider2D spikeCol;

	void Start ()
    {
        spikeCol = GetComponent<Collider2D>();
        playClip = true;
        killMe = true;
        rend = GetComponent<SpriteRenderer>();
        life = 3;
        rend.sprite = spikes[0];
	}
	

	void Update ()
    {
        if (life == 0)
        {
            if (playClip)
            {
                src.PlayOneShot(clips[1]);
                playClip = false;
            }
            spikeCol.enabled = false;
            rend.enabled = false;
            StartCoroutine("Kill");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet" && killMe)
        {
            life--;
            killMe = false;
            spikePos.position = new Vector3(spikePos.position.x, spikePos.position.y + 0.25f, spikePos.position.z);
            src.PlayOneShot(clips[0]);
            rend.sprite = spikes[1];
            StartCoroutine("ResetKill");
        }
    }

    IEnumerator ResetKill()
    {
        yield return new WaitForSeconds(4.0f);
        killMe = true;
        src.PlayOneShot(clips[0]);
        rend.sprite = spikes[0];
        spikePos.position = new Vector3(spikePos.position.x, spikePos.position.y - 0.3f, spikePos.position.z);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        src.Stop();
        Destroy(gameObject);
    }
}
