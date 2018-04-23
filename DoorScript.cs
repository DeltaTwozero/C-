using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] GameObject door, doorOpen;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            door.transform.position = Vector3.Lerp(door.transform.position, doorOpen.transform.position, doorOpen.transform.position.y);
        }
    }
}
