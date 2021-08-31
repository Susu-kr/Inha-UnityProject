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
    
    public GameObject ItemCoin;
    public GameObject ItemPower;
    public GameObject ItemBoom;
    public GameObject player;

    public GameManager2D gameManager;
    public ObjectManager objectManager;

    public AudioClip DamageClip;

    private AudioSource audioSource;

    // Boss Pattern
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        rig2D.velocity = Vector2.left * speed;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        switch(enemyName)
        {
            case "S":
                health = 5;
                break;
            case "L":
                health = 15;
                break;
            case "B":
                health = 100;
                Invoke("Stop", 2);
                break;
        }
    }
    void PlayDamageSound()
    {
        audioSource.PlayOneShot(DamageClip);
    }
    void Stop()
    {
        if (!gameObject.activeSelf) return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;
        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireFoward()
    {
        if (health <= 0) return;
        // #.Fire 4 Bullet Foward
        GameObject bullet = objectManager.MakeObj("BulletBossA");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * 400, ForceMode2D.Impulse);

        // #.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 0.2f);
        else
            Invoke("Think", 3.0f);
    }
    void FireShot()
    {
        if (health <= 0) return;
        // #.Fire 5 Random Shotgun Bullet to Player
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position; // 바라보는 각도
            Vector2 ranVec = new Vector2(Random.Range(0f, 10f), Random.Range(-300f, 300f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 100, ForceMode2D.Impulse);
        }
        // #.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 0.5f);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {
        if (health <= 0) return;
        // #.Fire Arc Continue Fire
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(-1, Mathf.Cos(Mathf.PI * 10 * curPatternCount/ maxPatternCount[patternIndex]));
        rigid.AddForce(dirVec.normalized * 200, ForceMode2D.Impulse);

        // #.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.03f);
        else
            Invoke("Think", 3);
    }
    void FireAround()
    {
        if (health <= 0) return;
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        // #.Fire Around
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 2 * index / roundNum)
                                        , Mathf.Cos(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 150, ForceMode2D.Impulse);
            
        }
        // #.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyName == "B")
            return;
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
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position; // 바라보는 각도
            rigid.AddForce(dirVec.normalized * 250, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position; // 바라보는 각도
            rigid.AddForce(dirVec.normalized * 250, ForceMode2D.Impulse);
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
        health -= dmg;
        if(health > 0) anim.SetTrigger("OnHit");
        if (health <= 0)
        {
            Player2D playerLogic = player.GetComponent<Player2D>();
            playerLogic.score += enemyScore;
            // #. Random Raito Item Drop
            int rand = enemyName == "B" ? 0 : Random.Range(0, 10);
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
            CancelInvoke();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;

            //#.Boss Kill
            if (enemyName == "B")
            {
                objectManager.DeleteAllObj("B");
                gameManager.StageEnd();
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border Bullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "Player Bullet")
        {
            PlayDamageSound();

            Debug.Log("맞음");
            Bullet1 bullet = collision.gameObject.GetComponent<Bullet1>();
            string effectname = "EffectA";
            Player2D playerLogic = player.GetComponent<Player2D>();
            switch(playerLogic.power)
            {
                case 1:
                    effectname = "EffectA";
                    break;
                case 2:
                    effectname = "EffectB";
                    break;
                case 3:
                    effectname = "EffectC";
                    break;
            }
            gameManager.CallExplosion(bullet.transform.position, effectname);
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }
    
}


