using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character instance;

    #region Variables
    [SerializeField] Transform playerPos, groundCheck, bulletSpawn;
    [SerializeField] float speed, jumpSpeed, superJump, maxSpeed, jumpDapm;
    [SerializeField] Animator anim;
    [SerializeField] AudioClip[] clips;
    [SerializeField] GameObject bulletPrefab;
    public bool isGround, isLadder, isJump, isJumpSuper, isJumpCharging, isSuperCharging, isRight, isFire, takeDMG, isPlay;
    float jumpTimer, axisX, axisY;
    public int health, ammo;
    Rigidbody2D rb;
    AudioSource src;
    #endregion

    private void Awake()
    {
        instance = this;
        anim.SetBool("dead", false);
        ammo = 25;
        health = 4;
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        src = this.GetComponent<AudioSource>();

        isLadder = false;
        isJump = false;
        isSuperCharging = false;
        isJumpCharging = false;
        takeDMG = true;
        isPlay = true;


    }

    void Update()
    {
        if (health >= 1)
        {
            MoveCheck();
            CheckGround();
            SetAnimSpeed();
            FireWeapon();
        }
        CheckDeath();
    }

    void FixedUpdate()
    {
        if (health >= 1)
        {
            if (isJump)
            {
                if (!isJumpSuper)
                {
                    src.PlayOneShot(clips[4]);
                    rb.AddForce(new Vector2(0, jumpSpeed * Time.fixedDeltaTime));
                }
                else
                {
                    src.PlayOneShot(clips[6]);
                    rb.AddForce(new Vector2(0, superJump * Time.fixedDeltaTime));
                    isJumpSuper = false;
                }
                isJump = false;
            }

            else if (isLadder)
            {
                rb.AddForce(new Vector2(axisX * speed * Time.fixedDeltaTime, axisY * speed * Time.fixedDeltaTime));
            }

            else rb.AddForce(new Vector2(axisX * (isGround ? speed : speed / jumpDapm) * Time.fixedDeltaTime, 0));
        }
    }

    void MoveCheck()
    {
        if (isLadder)
        {
            rb.gravityScale = 0;
            rb.drag = 15;
            axisX = Input.GetAxis("Horizontal");
            axisY = Input.GetAxis("Vertical");
            anim.SetFloat("speed", Mathf.Abs(axisX + axisY));
        }
        else
        {
            rb.gravityScale = 3;
            rb.drag = 5;
            axisX = Input.GetAxis("Horizontal");
            if (axisX < 0)
            {
                isRight = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (axisX > 0)
            {
                isRight = true;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGround && !isSuperCharging)
            {
                isJumpCharging = true;
                StartCoroutine("SuperJump");
            }

            if (Input.GetKeyUp(KeyCode.Space) && isGround)
            {
                isJump = true;
                isJumpCharging = false;
                StopCoroutine("SuperJump");
                if (jumpTimer >= 3f)
                {
                    isJumpSuper = true;
                    jumpTimer = 0;
                }
            }
            anim.SetFloat("speed", Mathf.Abs(axisX));
        }
    }

    void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, 0.08f);
        isGround = (collider != null);
        if (isLadder) isGround = true;
        anim.SetBool("jump", !isGround);
    }

    void SetAnimSpeed()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("climb"))
            anim.speed = Mathf.Abs(axisX + axisY);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
            anim.speed = Mathf.Abs(axisX);
        else
            anim.speed = 1.0f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isJump)
        {
            isJump = false;
            anim.SetBool("jump", false);
        }
        if (col.tag == "Ladder")
        {
            SetLadder(true);
        }

        if (col.gameObject.tag == "Ammo")
        {
            ammo = 25;
            src.PlayOneShot(clips[0]);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Medkit")
        {
            health = 4;
            src.PlayOneShot(clips[5]);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Enemy" && takeDMG)
        {
            takeDMG = false;
            health--;
            StartCoroutine("InvulnerabilityCheck");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            SetLadder(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && takeDMG)
        {
            if(health >= 1)
                src.PlayOneShot(clips[7]);

            takeDMG = false;
            health--;
            StartCoroutine("InvulnerabilityCheck");
        }

        if (col.gameObject.tag == "EnemyBLT" && takeDMG)
        {
            if (health >= 1)
                src.PlayOneShot(clips[7]);

            takeDMG = false;
            health = health - 2;
            StartCoroutine("InvulnerabilityCheck");
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && takeDMG)
        {
            if(health >= 1)
            src.PlayOneShot(clips[7]);

            takeDMG = false;
            health--;
            StartCoroutine("InvulnerabilityCheck");
        }

        if (col.gameObject.tag == "EnemyBLT" && takeDMG)
        {
            if (health >= 1)
                src.PlayOneShot(clips[7]);

            takeDMG = false;
            health = health - 2;
            StartCoroutine("InvulnerabilityCheck");
        }
    }

    void SetLadder(bool b)
    {
        isLadder = b;
        anim.SetBool("ladder", isLadder);
    }

    void FireWeapon()
    {
        if (Input.GetKeyDown(KeyCode.F) && ammo > 0)
        {
            ammo--;
            src.PlayOneShot(clips[3]);
            GameObject bulletInst = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        }

        if (Input.GetKey(KeyCode.F))
            isFire = true;
        else
            isFire = false;

        anim.SetBool("fire", isFire);
    }

    void CheckDeath()
    {
        if (health == 0)
        {
            if (isPlay)
            {
                //print("test");
                src.PlayOneShot(clips[2]);
                isPlay = false;
            }
            anim.SetBool("dead", true);
        }

        if (this.transform.position.y <= -30)
        {
            health = 0;
        }
    }

    IEnumerator SuperJump()
    {
        while (true)
        {
            jumpTimer++;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator InvulnerabilityCheck()
    {
        yield return new WaitForSeconds(1.0f);
        takeDMG = true;
    }
}
