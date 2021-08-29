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
    public int enemyScore;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public GameObject ItemCoin;
    public GameObject ItemPower;
    public GameObject ItemBoom;

    public GameObject player;
    public ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        rig2D.velocity = Vector2.left * speed;
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch(enemyName)
        {
            case "L":
                health = 15;
                break;
            case "S":
                health = 7;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }
    void Fire()
    {
        if (curShotDelay <= maxShotDelay) return;

        if(enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = bulletObjA.transform.rotation;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position; // 바라보는 각도
            rigid.AddForce(dirVec.normalized * 300, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
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

    public void OnHit(int dmg)
    {
        if (health <= 0) return;

        anim.SetBool("Hit", true);
        health -= dmg;
        Invoke("Return", 0.2f);
        if(health <= 0)
        {
            Player2D playerLogic = player.GetComponent<Player2D>();
            playerLogic.score += enemyScore;

            // #. Random Raito Item Drop
            int rand = Random.Range(0, 10);
            if(rand < 3)
            {
                Debug.Log("Not Item");
            }
            else if(rand < 6)
            { // Coin 30%
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if(rand < 8)
            { // Power 20%
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if(rand < 10)
            { // Boom 20%
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    void Return()
    {
        anim.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border Bullet")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "Player Bullet")
        {
            Debug.Log("맞음");
            Bullet1 bullet = collision.gameObject.GetComponent<Bullet1>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }

    }
}
