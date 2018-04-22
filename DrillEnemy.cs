using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemy : MonoBehaviour
{
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip[] clips;

    Transform playerPos;

	void Start ()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
    {
        float distance = Vector3.Distance(playerPos.position, this.transform.position);
        src.volume = 1 - Mathf.Clamp(distance, 0, 10) / 10;
	}

    void SetDown(int i)
    {
        src.clip = clips[i];
        src.Play();
    }
}
