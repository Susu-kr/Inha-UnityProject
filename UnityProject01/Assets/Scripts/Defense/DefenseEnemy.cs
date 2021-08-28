using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnemy : MonoBehaviour
{
    Animation anim;
    public GameObject target = null;
    private bool isDeath = false;
    private bool isAttack = false;
    public GameObject objSword = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        objSword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDeath && !isAttack)
            Running();

        if(!isDeath && isAttack)
        {
            Attack();
        }
    }

    private void Running()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        this.transform.forward = dirToTarget.normalized; // 단위벡터가 된다. (크기가 1)
        if (transform.position.y <= 0.5f)
        {
            transform.position +=  transform.forward * Time.deltaTime * 3.0f;
            anim.CrossFade("charge", 0.3f);
        }
    }

    private void Attack()
    {
        StartCoroutine("AttackToIdle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            isAttack = true;
        }
    }

    IEnumerator AttackToIdle()
    {
        if (anim.IsPlaying("attack") == true)
            yield break;
        // 1. attack 실행

        objSword.SetActive(true);
        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("attack", 0.3f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            Debug.Log(DefenseGameManager.Instance.score);
            StartCoroutine("Death");
            isDeath = true;
        }
    }

    IEnumerator Death()
    {
        DefenseGameManager.Instance.score++;
        int i = Random.Range(0, 2);
        if(i == 1)
        {
            anim.Play("die");

            float delayTime = anim.GetClip("die").length - 0.8f;
            yield return new WaitForSeconds(delayTime);
        }
        else
        {
            anim.Play("diehard");

            float delayTime = anim.GetClip("diehard").length - 0.8f;
            yield return new WaitForSeconds(delayTime);
        }
        Destroy(this.gameObject);
    }
}
