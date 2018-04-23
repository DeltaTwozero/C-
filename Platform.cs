using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform startPos, endPos;
    public float speed;

	
	void Update ()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        float weight = Mathf.Cos(Time.time * speed) * 0.5f + 0.5f;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, 1 - weight);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
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
