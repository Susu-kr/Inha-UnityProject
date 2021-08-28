using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animation anim;
    CharacterController pControl;

    public float runSpeed = 6.0f;
    public float rotSpeed = 360.0f;

    Vector3 velocity;

    public GameObject objSword = null;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        anim.wrapMode = WrapMode.Loop;

        pControl = gameObject.GetComponent<CharacterController>();
        objSword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //AnimationPlay_1();
        //AnimationPlay_2();
        //AnimationPlay_3();

        CharacterControl();

    }

    private void CharacterControl()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal")
            , 0, Input.GetAxis("Vertical"));
        velocity *= runSpeed;
        if(velocity.magnitude > 0.5)
        {
            anim.CrossFade("run", 0.3f);
            // 캐릭터 회전 자연스럽게
            Vector3 forward = Vector3.Slerp(transform.forward,
                velocity, rotSpeed * Time.deltaTime / Vector3.Angle(transform.forward, velocity));

            transform.LookAt(transform.position + forward);
        }
        else
        {
            anim.CrossFade("idle", 0.3f);
        }

        //InvokeRepeating("Invoke_Attack", 2.0f, 1.0f);
        //IsInvoking("Invoke_Attack");
        //CancelInvoke("Invoke_Attack");
        //pControl.Move(velocity * Time.deltaTime + Physics.gravity*Time.deltaTime);

        pControl.SimpleMove(velocity);
    }

    void AnimationPlay_1()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.Play("idle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.Play("walk");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.Play("run");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.Play("charge");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            anim.Play("idlebattle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            anim.Play("resist");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            anim.Play("victory");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            anim.Play("salute");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            anim.Play("die");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.Play("diehard");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Play("attack");
        }
    }

    void AnimationPlay_2()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.wrapMode = WrapMode.Once;
            anim.CrossFade("attack", 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.wrapMode = WrapMode.Loop;
            anim.Play("idle");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.wrapMode = WrapMode.Once;
            anim.Play("attack");
        }
    }

    void AnimationPlay_3()
    {
        if(Input.GetMouseButton(0))
        {
            StartCoroutine("AttackToIdle");
        }
        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    anim.wrapMode = WrapMode.Loop;
        //    anim.CrossFade("walk", 0.3f);
        //}
        //if(Input.GetKeyUp(KeyCode.W))
        //{
        //    anim.wrapMode = WrapMode.Loop;
        //    anim.CrossFade("idle", 0.2f);
        //}
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    anim.wrapMode = WrapMode.Loop;
        //    anim.CrossFade("run", 0.3f);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    anim.wrapMode = WrapMode.Loop;
        //    anim.CrossFade("walk", 0.3f);
        //}
    }

    void Invoke_Attack()
    {
        StartCoroutine("AttackToIdle");
    }

    IEnumerator AttackToIdle()
    {
        if (anim.IsPlaying("attack") == true)
            yield break;
        // 1. attack 실행

        objSword.SetActive(true);
        anim.wrapMode = WrapMode.Once;
        anim.CrossFade("attack", 0.3f);

        // 2. delayTime 만큼 대기
        float delayTime = anim.GetClip("attack").length - 0.3f;
        yield return new WaitForSeconds(delayTime);

        // 3. 대기시간 이후 idle 실행

        objSword.SetActive(false);
        anim.wrapMode = WrapMode.Loop;
        anim.CrossFade("idle", 0.3f);
    }
    
}
