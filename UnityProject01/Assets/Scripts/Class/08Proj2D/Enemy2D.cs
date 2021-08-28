using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private Animator anim;

    public float speed;
    public int health;

    public string enemyName;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        rig2D.velocity = Vector2.left * speed;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }
    void Fire()
    {
        if (curShotDelay < maxShotDelay) return;

        if(enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, bulletObjA.transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position; // 바라보는 각도
            rigid.AddForce(dirVec.normalized * 300, ForceMode2D.Impulse);
        }
        if (enemyName == "L")
        {
            GameObject bullet = Instantiate(bulletObjB, transform.position, bulletObjB.transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position; // 바라보는 각도
            rigid.AddForce(dirVec.normalized * 300, ForceMode2D.Impulse);
        }
        curShotDelay = 0;
    }


    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void OnHit(int dmg)
    {
        anim.SetBool("Hit", true);
        health -= dmg;
        Invoke("Return", 0.2f);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Return()
    {
        anim.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if(collision.gameObject.tag == "Player Bullet")
        {
            Debug.Log("맞음");
            Bullet1 bullet = collision.gameObject.GetComponent<Bullet1>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }

    }
}
