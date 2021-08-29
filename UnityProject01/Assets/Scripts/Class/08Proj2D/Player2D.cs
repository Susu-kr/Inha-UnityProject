using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private Rigidbody2D rig2D;
    float maxSpeed = 200.0f;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject BoomEffect;

    public float maxShotDelay;
    public float curShotDelay;

    public AudioClip shotClip;
    private AudioSource audioSource;

    public int life;
    public int score;
    public int power;
    public int maxpower;
    public int boom;
    public int maxboom;

    public bool isHit;
    public bool isBoom;

    public GameManager2D gameManager;
    public ObjectManager objectManager;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //anim.speed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            Move_2D();
            Fire();
            Boom();
            Reload();
        }

    }

    void Move_2D()
    {
        Move_Player();
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Fire", true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Fire", false);
        }
    }

    void Move_1(float x, float y)
    {
        Vector3 position = rig2D.transform.position;
        position = new Vector3(
            position.x + (x * maxSpeed * Time.deltaTime),
            position.y + (y * maxSpeed * Time.deltaTime),
            position.z);
        rig2D.MovePosition(position);
    }

    void Move_2(float x, float y) // 包己捞 利侩
    {
        rig2D.AddForce(new Vector2(
            x * maxSpeed * Time.deltaTime,
            y * maxSpeed * Time.deltaTime));
    }

    void Move_Player()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)) h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)) v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * maxSpeed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1")) return;
        if (!audioSource.isPlaying) PlaySound();
        if (curShotDelay < maxShotDelay) return;
        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.right * 600, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bullet2 = objectManager.MakeObj("BulletPlayerB");
                bullet2.transform.position = transform.position;
                Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
                rigid2.AddForce(Vector2.right * 600, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bullet3 = objectManager.MakeObj("BulletPlayerC");
                bullet3.transform.position = transform.position;
                Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
                rigid3.AddForce(Vector2.right * 600, ForceMode2D.Impulse);
                break;
        }
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Boom()
    {
        if (!Input.GetButton("Fire2")) return;
        if (isBoom) return;
        if (boom == 0) return;

        boom--;
        isBoom = true;
        gameManager.UpdateBoomIcon(boom);

        // #1. Effect Visible
        BoomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);
        // #2. Remove Enemy
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");

        for (int index = 0; index < enemiesL.Length; index++)
        {
            Enemy2D enemyLogic = enemiesL[index].GetComponent<Enemy2D>();
            enemyLogic.OnHit(1000);
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            Enemy2D enemyLogic = enemiesS[index].GetComponent<Enemy2D>();
            enemyLogic.OnHit(1000);
        }
        // #3. Remove Enemy Bullet
        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            bulletsA[index].SetActive(false);
        }
        for (int index = 0; index < bulletsB.Length; index++)
        {
            bulletsB[index].SetActive(false);
        }
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(shotClip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            // to do
            // Life 皑家
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            if(life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }

        // Item
        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch(item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power == maxpower)
                        score += 500;
                    else
                        power++;
                    break;
                case "Boom":
                    if (boom == maxboom)
                        score += 500;
                    else
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }

    void OffBoomEffect()
    {
        BoomEffect.SetActive(false);
        isBoom = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
