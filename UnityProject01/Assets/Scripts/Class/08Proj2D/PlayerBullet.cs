using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    public float maxShotDelay;
    public float curShotDelay;

    public float power = 1.0f;

    public AudioClip shotClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1")) return;
        if (!audioSource.isPlaying) PlaySound();
        if (curShotDelay < maxShotDelay) return;

        switch(power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.right * 600, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bullet2 = Instantiate(bulletPrefab2, transform.position, transform.rotation);
                Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
                rigid2.AddForce(Vector2.right * 600, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bullet3 = Instantiate(bulletPrefab3, transform.position, transform.rotation);
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

    void PlaySound()
    {
        audioSource.PlayOneShot(shotClip);
    }
}
